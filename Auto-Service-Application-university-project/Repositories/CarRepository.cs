using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Repositories.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly string _connectionString;

        public CarRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Car> GetAll()
        {
            var cars = new List<Car>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT spz, car_brand, symptoms, reservation_reservation_id FROM car";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var car = new Car
                            {
                                Spz = reader["spz"].ToString(),
                                CarBrand = reader["car_brand"].ToString(),
                                Symptoms = reader["symptoms"].ToString(),
                                ReservationReservationId = Convert.ToInt32(reader["reservation_reservation_id"])
                            };
                            cars.Add(car);
                        }
                    }
                }
            }

            return cars;
        }

        public Car GetBySpz(string spz)
        {
            Car car = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT spz, car_brand, symptoms, reservation_reservation_id FROM car WHERE spz = :Spz";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("Spz", OracleDbType.Varchar2).Value = spz;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            car = new Car
                            {
                                Spz = reader["spz"].ToString(),
                                CarBrand = reader["car_brand"].ToString(),
                                Symptoms = reader["symptoms"].ToString(),
                                ReservationReservationId = Convert.ToInt32(reader["reservation_reservation_id"])
                            };
                        }
                    }
                }
            }

            return car;
        }

        public void Add(Car car, User currentUser)
        {
            // Проверка прав доступа
            if (!currentUser.HasPermission("AddCar"))
                throw new UnauthorizedAccessException("У вас нет прав для добавления автомобиля.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Вызов хранимой процедуры для вставки данных
                    using (var command = new OracleCommand("insert_car", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_spz", OracleDbType.Varchar2).Value = car.Spz;
                        command.Parameters.Add("p_car_brand", OracleDbType.Varchar2).Value = car.CarBrand;
                        command.Parameters.Add("p_symptoms", OracleDbType.Varchar2).Value = car.Symptoms;
                        command.Parameters.Add("p_reservation_reservation_id", OracleDbType.Int32).Value = car.ReservationReservationId;

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Update(Car car, User currentUser)
        {
            // Проверка прав доступа
            if (!currentUser.HasPermission("UpdateCar"))
                throw new UnauthorizedAccessException("У вас нет прав для обновления автомобиля.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Получаем car_id по spz
                    int carId = GetCarIdBySpz(car.Spz, connection);

                    if (carId == 0)
                        throw new Exception("Автомобиль не найден.");

                    // Вызов хранимой процедуры для обновления данных
                    using (var command = new OracleCommand("update_car", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_car_id", OracleDbType.Int32).Value = carId;
                        command.Parameters.Add("p_spz", OracleDbType.Varchar2).Value = car.Spz;
                        command.Parameters.Add("p_car_brand", OracleDbType.Varchar2).Value = car.CarBrand;
                        command.Parameters.Add("p_symptoms", OracleDbType.Varchar2).Value = car.Symptoms;
                        command.Parameters.Add("p_reservation_reservation_id", OracleDbType.Int32).Value = car.ReservationReservationId;

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Delete(string spz, User currentUser)
        {
            // Проверка прав доступа
            if (!currentUser.HasPermission("DeleteCar"))
                throw new UnauthorizedAccessException("У вас нет прав для удаления автомобиля.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Получаем car_id по spz
                    int carId = GetCarIdBySpz(spz, connection);

                    if (carId == 0)
                        throw new Exception("Автомобиль не найден.");

                    // Вызов хранимой процедуры для удаления данных
                    using (var command = new OracleCommand("delete_car", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_car_id", OracleDbType.Int32).Value = carId;

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private int GetCarIdBySpz(string spz, OracleConnection connection)
        {
            string query = "SELECT car_id FROM car WHERE spz = :Spz";
            using (var command = new OracleCommand(query, connection))
            {
                command.Parameters.Add("Spz", OracleDbType.Varchar2).Value = spz;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader["car_id"]);
                    }
                }
            }
            return 0;
        }
    }
}
