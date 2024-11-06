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
    public class ServiseOfferRepository : IServiseOfferRepository
    {
        private readonly string _connectionString;

        public ServiseOfferRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<ServiseOffer> GetAll()
        {
            var offers = new List<ServiseOffer>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT offer_id, price_per_hour, date_offer, employer_employer_id, car_car_id, service_type_id, working_hours FROM servise_offer";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var offer = new ServiseOffer
                            {
                                OfferId = Convert.ToInt32(reader["offer_id"]),
                                PricePerHour = Convert.ToDecimal(reader["price_per_hour"]),
                                DateOffer = Convert.ToDateTime(reader["date_offer"]),
                                EmployerEmployerId = Convert.ToInt32(reader["employer_employer_id"]),
                                CarCarId = Convert.ToInt32(reader["car_car_id"]),
                                ServiceTypeId = Convert.ToInt32(reader["service_type_id"]),
                                WorkingHours = Convert.ToDecimal(reader["working_hours"])
                            };
                            offers.Add(offer);
                        }
                    }
                }
            }

            return offers;
        }

        public ServiseOffer GetByOfferId(int offerId)
        {
            ServiseOffer offer = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT offer_id, price_per_hour, date_offer, employer_employer_id, car_car_id, service_type_id, working_hours FROM servise_offer WHERE offer_id = :OfferId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("OfferId", OracleDbType.Int32).Value = offerId;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            offer = new ServiseOffer
                            {
                                OfferId = Convert.ToInt32(reader["offer_id"]),
                                PricePerHour = Convert.ToDecimal(reader["price_per_hour"]),
                                DateOffer = Convert.ToDateTime(reader["date_offer"]),
                                EmployerEmployerId = Convert.ToInt32(reader["employer_employer_id"]),
                                CarCarId = Convert.ToInt32(reader["car_car_id"]),
                                ServiceTypeId = Convert.ToInt32(reader["service_type_id"]),
                                WorkingHours = Convert.ToDecimal(reader["working_hours"])
                            };
                        }
                    }
                }
            }

            return offer;
        }

        public void Add(ServiseOffer serviseOffer, User currentUser)
        {
            // Проверка прав доступа
            if (!currentUser.HasPermission("AddServiseOffer"))
                throw new UnauthorizedAccessException("У вас нет прав для добавления предложения услуги.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Вызов хранимой процедуры для вставки данных
                    using (var command = new OracleCommand("insert_servise_offer", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_price_per_hour", OracleDbType.Decimal).Value = serviseOffer.PricePerHour;
                        command.Parameters.Add("p_date_offer", OracleDbType.Date).Value = serviseOffer.DateOffer;
                        command.Parameters.Add("p_employer_employer_id", OracleDbType.Int32).Value = serviseOffer.EmployerEmployerId;
                        command.Parameters.Add("p_car_car_id", OracleDbType.Int32).Value = serviseOffer.CarCarId;
                        command.Parameters.Add("p_service_type_id", OracleDbType.Int32).Value = serviseOffer.ServiceTypeId;
                        command.Parameters.Add("p_working_hours", OracleDbType.Decimal).Value = serviseOffer.WorkingHours;

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

        public void Update(ServiseOffer serviseOffer, User currentUser)
        {
            // Проверка прав доступа
            if (!currentUser.HasPermission("UpdateServiseOffer"))
                throw new UnauthorizedAccessException("У вас нет прав для обновления предложения услуги.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Вызов хранимой процедуры для обновления данных
                    using (var command = new OracleCommand("update_servise_offer", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_offer_id", OracleDbType.Int32).Value = serviseOffer.OfferId;
                        command.Parameters.Add("p_price_per_hour", OracleDbType.Decimal).Value = serviseOffer.PricePerHour;
                        command.Parameters.Add("p_date_offer", OracleDbType.Date).Value = serviseOffer.DateOffer;
                        command.Parameters.Add("p_employer_employer_id", OracleDbType.Int32).Value = serviseOffer.EmployerEmployerId;
                        command.Parameters.Add("p_car_car_id", OracleDbType.Int32).Value = serviseOffer.CarCarId;
                        command.Parameters.Add("p_service_type_id", OracleDbType.Int32).Value = serviseOffer.ServiceTypeId;
                        command.Parameters.Add("p_working_hours", OracleDbType.Decimal).Value = serviseOffer.WorkingHours;

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

        public void Delete(int offerId, User currentUser)
        {
            // Проверка прав доступа
            if (!currentUser.HasPermission("DeleteServiseOffer"))
                throw new UnauthorizedAccessException("У вас нет прав для удаления предложения услуги.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Вызов хранимой процедуры для удаления данных
                    using (var command = new OracleCommand("delete_servise_offer", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_offer_id", OracleDbType.Int32).Value = offerId;

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
    }
}
