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
    public class OfficeRepository
    {
        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";


        public async Task<ObservableCollection<Office>> GetAllOfficesAsync()
        {
            var offices = new ObservableCollection<Office>();

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("office_pkg.get_all_offices", connection))
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
                                var office = new Office
                                {
                                    OfficeId = reader.GetInt32(reader.GetOrdinal("office_id")),
                                    OfficeSize = reader.GetInt32(reader.GetOrdinal("office_size")),
                                    Address = new Address
                                    {
                                        Country = reader.GetString(reader.GetOrdinal("country")),
                                        City = reader.GetString(reader.GetOrdinal("city")),
                                        Street = reader.GetString(reader.GetOrdinal("street")),
                                        HouseNumber = reader.GetInt32(reader.GetOrdinal("house_number")),
                                        IndexAdd = reader.GetInt32(reader.GetOrdinal("index_add"))
                                    }
                                };
                                offices.Add(office);
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении списка офисов: {ex.Message}", ex);
                    }
                }
            }

            return offices;
        }

        public async Task UpdateOfficeAsync(Office office)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("office_pkg.update_office", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_office_id", OracleDbType.Int32).Value = office.OfficeId;
                    command.Parameters.Add("p_country", OracleDbType.Varchar2).Value = office.Address.Country;
                    command.Parameters.Add("p_city", OracleDbType.Varchar2).Value = office.Address.City;
                    command.Parameters.Add("p_index_add", OracleDbType.Int32).Value = office.Address.IndexAdd;
                    command.Parameters.Add("p_street", OracleDbType.Varchar2).Value = office.Address.Street;
                    command.Parameters.Add("p_house_number", OracleDbType.Int32).Value = office.Address.HouseNumber;
                    command.Parameters.Add("p_office_size", OracleDbType.Int32).Value = office.OfficeSize;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при обновлении офиса: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task DeleteOfficeAsync(int officeId)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("office_pkg.delete_office", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_office_id", OracleDbType.Int32).Value = officeId;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при удалении офиса: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task<Office> GetOfficeAsync(int officeId)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("office_pkg.get_office", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_office_id", OracleDbType.Int32).Value = officeId;

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
                                return new Office
                                {
                                    OfficeId = reader.GetInt32(reader.GetOrdinal("office_id")),
                                    OfficeSize = reader.GetInt32(reader.GetOrdinal("office_size")),
                                    Address = new Address
                                    {
                                        Country = reader.GetString(reader.GetOrdinal("country")),
                                        City = reader.GetString(reader.GetOrdinal("city")),
                                        Street = reader.GetString(reader.GetOrdinal("street")),
                                        HouseNumber = reader.GetInt32(reader.GetOrdinal("house_number")),
                                        IndexAdd = reader.GetInt32(reader.GetOrdinal("index_add"))
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
                        throw new ApplicationException($"Ошибка при получении данных офиса: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task InsertOfficeAsync(Office office)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("office_pkg.insert_office", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_country", OracleDbType.Varchar2).Value = office.Address.Country;
                    command.Parameters.Add("p_city", OracleDbType.Varchar2).Value = office.Address.City;
                    command.Parameters.Add("p_index_add", OracleDbType.Int32).Value = office.Address.IndexAdd;
                    command.Parameters.Add("p_street", OracleDbType.Varchar2).Value = office.Address.Street;
                    command.Parameters.Add("p_house_number", OracleDbType.Int32).Value = office.Address.HouseNumber;
                    command.Parameters.Add("p_office_size", OracleDbType.Int32).Value = office.OfficeSize;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при вставке офиса: {ex.Message}", ex);
                    }
                }
            }
        }
    }
}
