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
    public class ServiceSpareRepository
    {

        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";

        public async Task AddServiceSpareAsync(ServiceSpare serviceSpare, ServiceOffer serviceOffer)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("service_spare_pkg.add_service_spare", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_servise_offer_id", OracleDbType.Int32).Value = serviceOffer.OfferId;
                    command.Parameters.Add("p_spare_part_id", OracleDbType.Int32).Value = serviceSpare.SparePart.SparePartId;
                    command.Parameters.Add("p_price_per_hour", OracleDbType.Int32).Value = (object)serviceOffer.PricePerHour ?? DBNull.Value;
                    command.Parameters.Add("p_date_offer", OracleDbType.Date).Value = (object)serviceOffer.DateOffer ?? DBNull.Value;
                    command.Parameters.Add("p_employer_id", OracleDbType.Int32).Value = (object)serviceOffer.Employer.EmployerId ?? DBNull.Value;
                    command.Parameters.Add("p_working_hours", OracleDbType.Int32).Value = (object)serviceOffer.WorkingHours ?? DBNull.Value;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при добавлении ServiceSpare: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task RemoveServiceSpareAsync(int serviceOfferId, int sparePartId)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("service_spare_pkg.remove_service_spare", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_servise_offer_id", OracleDbType.Int32).Value = serviceOfferId;
                    command.Parameters.Add("p_spare_part_id", OracleDbType.Int32).Value = sparePartId;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при удалении ServiceSpare: {ex.Message}", ex);
                    }
                }
            }
        }

        //public async Task<ObservableCollection<ServiceSpare>> GetServiceSparesByOfferAsync(int serviceOfferId)
        //{
        //    var serviceSpares = new ObservableCollection<ServiceSpare>();

        //    using (var connection = new OracleConnection(connectionString))
        //    {
        //        await connection.OpenAsync();

        //        using (var command = new OracleCommand("service_spare_pkg.get_service_spare_by_offer", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;

        //            // Входные параметры
        //            command.Parameters.Add("p_servise_offer_id", OracleDbType.Int32).Value = serviceOfferId;

        //            // Выходные параметры
        //            var cursorParam = new OracleParameter("p_cursor", OracleDbType.RefCursor)
        //            {
        //                Direction = ParameterDirection.Output
        //            };
        //            command.Parameters.Add(cursorParam);

        //            try
        //            {
        //                using (var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
        //                {
        //                    while (await reader.ReadAsync())
        //                    {
        //                        var serviceSpare = new ServiceSpare
        //                        {
        //                            ServiceOfferId = serviceOfferId,
        //                            SparePartId = reader.GetInt32(reader.GetOrdinal("spare_part_spare_part_id"))
        //                        };

        //                        serviceSpares.Add(serviceSpare);
        //                    }
        //                }
        //            }
        //            catch (OracleException ex)
        //            {
        //                throw new ApplicationException($"Ошибка при получении ServiceSpares: {ex.Message}", ex);
        //            }
        //        }
        //    }

        //    return serviceSpares;
        //}

        //public async Task<ObservableCollection<ServiceSpare>> GetAllServiceSparesAsync()
        //{
        //    var serviceSpares = new ObservableCollection<ServiceSpare>();

        //    using (var connection = new OracleConnection(connectionString))
        //    {
        //        await connection.OpenAsync();

        //        using (var command = new OracleCommand("service_spare_pkg.get_all_service_spare", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;

        //            // Выходные параметры
        //            var cursorParam = new OracleParameter("p_cursor", OracleDbType.RefCursor)
        //            {
        //                Direction = ParameterDirection.Output
        //            };
        //            command.Parameters.Add(cursorParam);

        //            try
        //            {
        //                using (var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
        //                {
        //                    while (await reader.ReadAsync())
        //                    {
        //                        var serviceSpare = new ServiceSpare
        //                        {
        //                            ServiceOfferId = reader.GetInt32(reader.GetOrdinal("servise_offer_offer_id")),
        //                            SparePartId = reader.GetInt32(reader.GetOrdinal("spare_part_spare_part_id"))
        //                        };

        //                        serviceSpares.Add(serviceSpare);
        //                    }
        //                }
        //            }
        //            catch (OracleException ex)
        //            {
        //                throw new ApplicationException($"Ошибка при получении всех ServiceSpares: {ex.Message}", ex);
        //            }
        //        }
        //    }

        //    return serviceSpares;
        //}
    }
}
