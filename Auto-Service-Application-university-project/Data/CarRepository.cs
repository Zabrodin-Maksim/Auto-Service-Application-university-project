using Auto_Service_Application_university_project.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Data
{
    public class CarRepository
    {
        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";

        public async Task InsertCarAsync(Car car)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("car_pkg.insert_car", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры для машины
                    command.Parameters.Add("p_spz", OracleDbType.Varchar2).Value = car.SPZ;
                    command.Parameters.Add("p_car_brand", OracleDbType.Varchar2).Value = car.CarBrand;
                    command.Parameters.Add("p_symptoms", OracleDbType.Varchar2).Value = car.Symptoms;

                    // Входные параметры для резервации
                    command.Parameters.Add("p_date_reservace", OracleDbType.Date).Value = car.Reservation.DateReservace;
                    command.Parameters.Add("p_office_office_id", OracleDbType.Int32).Value = car.Reservation.Office.OfficeId;
                    command.Parameters.Add("p_client_client_id", OracleDbType.Int32).Value = car.Reservation.Client.ClientId;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при вставке машины: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task UpdateCarAsync(Car car)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("car_pkg.update_car", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры для машины
                    command.Parameters.Add("p_car_id", OracleDbType.Int32).Value = car.CarId;
                    command.Parameters.Add("p_spz", OracleDbType.Varchar2).Value = car.SPZ;
                    command.Parameters.Add("p_car_brand", OracleDbType.Varchar2).Value = car.CarBrand;
                    command.Parameters.Add("p_symptoms", OracleDbType.Varchar2).Value = car.Symptoms;

                    // Входные параметры для резервации
                    command.Parameters.Add("p_date_reservace", OracleDbType.Date).Value = car.Reservation.DateReservace;
                    command.Parameters.Add("p_office_office_id", OracleDbType.Int32).Value = car.Reservation.Office.OfficeId;
                    command.Parameters.Add("p_client_client_id", OracleDbType.Int32).Value = car.Reservation.Client.ClientId;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при обновлении машины: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task DeleteCarAsync(int carId)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("car_pkg.delete_car", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_car_id", OracleDbType.Int32).Value = carId;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при удалении машины: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task<Car> GetCarAsync(int carId)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("car_pkg.get_car", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_car_id", OracleDbType.Int32).Value = carId;

                    // Выходные параметры
                    var cursorParam = new OracleParameter("p_cursor", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(cursorParam);

                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Car
                                {
                                    CarId = reader.GetInt32(reader.GetOrdinal("car_id")),
                                    SPZ = reader.GetString(reader.GetOrdinal("spz")),
                                    CarBrand = reader.GetString(reader.GetOrdinal("car_brand")),
                                    Symptoms = reader.IsDBNull(reader.GetOrdinal("symptoms")) ? null : reader.GetString(reader.GetOrdinal("symptoms")),
                                    Reservation = new Reservation
                                    {
                                        DateReservace = reader.GetDateTime(reader.GetOrdinal("date_reservace")),
                                        Office = new Office
                                        {
                                            OfficeId = reader.GetInt32(reader.GetOrdinal("office_office_id"))
                                            // Дополнительно можно загрузить данные офиса при необходимости
                                        },
                                        Client = new Client
                                        {
                                            ClientId = reader.GetInt32(reader.GetOrdinal("client_client_id"))
                                            // Дополнительно можно загрузить данные клиента при необходимости
                                        }
                                    }
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении данных машины: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task<ObservableCollection<Car>> GetAllCarsAsync()
        {
            var cars = new ObservableCollection<Car>();

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("car_pkg.get_all_cars", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Выходные параметры
                    var cursorParam = new OracleParameter("p_cursor", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(cursorParam);

                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                        {
                            while (await reader.ReadAsync())
                            {
                                var car = new Car
                                {
                                    CarId = reader.GetInt32(reader.GetOrdinal("car_id")),
                                    SPZ = reader.GetString(reader.GetOrdinal("spz")),
                                    CarBrand = reader.GetString(reader.GetOrdinal("car_brand")),
                                    Symptoms = reader.IsDBNull(reader.GetOrdinal("symptoms")) ? null : reader.GetString(reader.GetOrdinal("symptoms")),
                                    Reservation = new Reservation
                                    {
                                        DateReservace = reader.GetDateTime(reader.GetOrdinal("date_reservace")),
                                        Office = new Office
                                        {
                                            OfficeId = reader.GetInt32(reader.GetOrdinal("office_office_id"))
                                            // Дополнительно можно загрузить данные офиса при необходимости
                                        },
                                        Client = new Client
                                        {
                                            ClientId = reader.GetInt32(reader.GetOrdinal("client_client_id"))
                                            // Дополнительно можно загрузить данные клиента при необходимости
                                        }
                                    }
                                };
                                cars.Add(car);
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении списка машин: {ex.Message}", ex);
                    }
                }
            }

            return cars;
        }
    }
}
