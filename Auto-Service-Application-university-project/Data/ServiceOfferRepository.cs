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
    public class ServisOfferRepository
    {

        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";

        public async Task InsertServiceOfferAsync(ServiceOffer offer)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("service_offer_pkg.insert_service_offer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_price_per_hour", OracleDbType.Int32).Value = (object)offer.PricePerHour ?? DBNull.Value;
                    command.Parameters.Add("p_date_offer", OracleDbType.Date).Value = (object)offer.DateOffer ?? DBNull.Value;
                    command.Parameters.Add("p_employer_employer_id", OracleDbType.Int32).Value = (object)offer.Employer?.EmployerId ?? DBNull.Value;
                    command.Parameters.Add("p_car_spz", OracleDbType.Varchar2).Value = (object)offer.Car.SPZ ?? DBNull.Value;
                    command.Parameters.Add("p_car_car_brand", OracleDbType.Varchar2).Value = (object)offer.Car.CarBrand ?? DBNull.Value;
                    command.Parameters.Add("p_car_symptoms", OracleDbType.Varchar2).Value = (object)offer.Car.Symptoms ?? DBNull.Value;
                    command.Parameters.Add("p_date_reservace", OracleDbType.Date).Value = (object)offer.Car.Reservation?.DateReservace ?? DBNull.Value;
                    command.Parameters.Add("p_office_office_id", OracleDbType.Int32).Value = (object)offer.Car?.Reservation?.Office.OfficeId ?? DBNull.Value;
                    command.Parameters.Add("p_client_client_id", OracleDbType.Int32).Value = (object)offer.Car?.Reservation?.Client.ClientId ?? DBNull.Value;
                    command.Parameters.Add("p_service_type_id", OracleDbType.Int32).Value = (object)offer.ServiceType?.ServiceTypeId ?? DBNull.Value;
                    command.Parameters.Add("p_working_hours", OracleDbType.Int32).Value = (object)offer.WorkingHours ?? DBNull.Value;

                    // Специфичные параметры
                    command.Parameters.Add("p_speciality", OracleDbType.Varchar2).Value = (object)offer.Speciality ?? DBNull.Value;
                    command.Parameters.Add("p_radius_wheel", OracleDbType.Int32).Value = (object)offer.RadiusWheel ?? DBNull.Value;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при вставке ServiceOffer: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task<ObservableCollection<ServiceOffer>> GetAllServiceOffersAsync()
        {
            var offers = new ObservableCollection<ServiceOffer>();

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("service_offer_pkg.get_all_service_offers", connection))
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
                                var offer = new ServiceOffer
                                {
                                    OfferId = reader.GetInt32(reader.GetOrdinal("offer_id")),
                                    PricePerHour = reader.IsDBNull(reader.GetOrdinal("price_per_hour")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("price_per_hour")),
                                    DateOffer = reader.IsDBNull(reader.GetOrdinal("date_offer")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("date_offer")),
                                    Employer = reader.IsDBNull(reader.GetOrdinal("employer_employer_id")) ? null : new Employer { EmployerId = reader.GetInt32(reader.GetOrdinal("employer_employer_id")) },
                                    Car = new Car { CarId = reader.GetInt32(reader.GetOrdinal("car_car_id")) }, // Дополнительно загрузить данные автомобиля
                                    ServiceType = new ServiceType { ServiceTypeId = reader.GetInt32(reader.GetOrdinal("service_type_id")), TypeName = reader.GetString(reader.GetOrdinal("type_name")) },
                                    WorkingHours = reader.IsDBNull(reader.GetOrdinal("working_hours")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("working_hours"))
                                };

                                // Получение специфичных данных
                                // Здесь можно добавить дополнительную логику для получения специфичных данных из связанных таблиц

                                offers.Add(offer);
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении списка ServiceOffers: {ex.Message}", ex);
                    }
                }
            }

            return offers;
        }

    }
}
