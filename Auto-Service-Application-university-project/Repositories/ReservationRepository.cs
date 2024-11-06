using BDAS2_University_Project.Models;
using BDAS2_University_Project.Repositories.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly string _connectionString;

        public ReservationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Reservation> GetAll()
        {
            var reservations = new List<Reservation>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT reservation_id, date_reservace, office_office_id, client_client_id FROM reservation";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var reservation = new Reservation
                            {
                                ReservationId = Convert.ToInt32(reader["reservation_id"]),
                                DateReservace = Convert.ToDateTime(reader["date_reservace"]),
                                OfficeOfficeId = Convert.ToInt32(reader["office_office_id"]),
                                ClientClientId = Convert.ToInt32(reader["client_client_id"])
                            };
                            reservations.Add(reservation);
                        }
                    }
                }
            }

            return reservations;
        }

        public Reservation GetByDate(DateTime date)
        {
            Reservation reservation = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT reservation_id, date_reservace, office_office_id, client_client_id FROM reservation WHERE date_reservace = :DateReservace";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("DateReservace", OracleDbType.Date).Value = date;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            reservation = new Reservation
                            {
                                ReservationId = Convert.ToInt32(reader["reservation_id"]),
                                DateReservace = Convert.ToDateTime(reader["date_reservace"]),
                                OfficeOfficeId = Convert.ToInt32(reader["office_office_id"]),
                                ClientClientId = Convert.ToInt32(reader["client_client_id"])
                            };
                        }
                    }
                }
            }

            return reservation;
        }

        public void Add(Reservation reservation, User currentUser)
        {
            // Проверка прав доступа
            if (!currentUser.HasPermission("AddReservation"))
                throw new UnauthorizedAccessException("У вас нет прав для добавления бронирования.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Вызов хранимой процедуры для вставки данных
                    using (var command = new OracleCommand("insert_reservation", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_date_reservace", OracleDbType.Date).Value = reservation.DateReservace;
                        command.Parameters.Add("p_office_office_id", OracleDbType.Int32).Value = reservation.OfficeOfficeId;
                        command.Parameters.Add("p_client_client_id", OracleDbType.Int32).Value = reservation.ClientClientId;

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

        public void Update(Reservation reservation, User currentUser)
        {
            // Проверка прав доступа
            if (!currentUser.HasPermission("UpdateReservation"))
                throw new UnauthorizedAccessException("У вас нет прав для обновления бронирования.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Вызов хранимой процедуры для обновления данных
                    using (var command = new OracleCommand("update_reservation", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_reservation_id", OracleDbType.Int32).Value = reservation.ReservationId;
                        command.Parameters.Add("p_date_reservace", OracleDbType.Date).Value = reservation.DateReservace;
                        command.Parameters.Add("p_office_office_id", OracleDbType.Int32).Value = reservation.OfficeOfficeId;
                        command.Parameters.Add("p_client_client_id", OracleDbType.Int32).Value = reservation.ClientClientId;

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

        public void Delete(DateTime date, User currentUser)
        {
            // Проверка прав доступа
            if (!currentUser.HasPermission("DeleteReservation"))
                throw new UnauthorizedAccessException("У вас нет прав для удаления бронирования.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Получаем reservation_id по дате
                    int reservationId = GetReservationIdByDate(date, connection);

                    if (reservationId == 0)
                        throw new Exception("Бронирование не найдено.");

                    // Вызов хранимой процедуры для удаления данных
                    using (var command = new OracleCommand("delete_reservation", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_reservation_id", OracleDbType.Int32).Value = reservationId;

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

        private int GetReservationIdByDate(DateTime date, OracleConnection connection)
        {
            string query = "SELECT reservation_id FROM reservation WHERE date_reservace = :DateReservace";
            using (var command = new OracleCommand(query, connection))
            {
                command.Parameters.Add("DateReservace", OracleDbType.Date).Value = date;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader["reservation_id"]);
                    }
                }
            }
            return 0;
        }
    }
}
