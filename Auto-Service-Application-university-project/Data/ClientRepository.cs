using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Services;
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
    class ClientRepository
    {
        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";

        public async Task<ObservableCollection<Client>> GetAllClientsAsync()
        {
            var clients = new ObservableCollection<Client>();

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("client_pkg.get_all_clients", connection))
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
                                var client = new Client
                                {
                                    ClientId = reader.GetInt32(reader.GetOrdinal("client_id")),
                                    ClientName = reader.GetString(reader.GetOrdinal("name_customer")),
                                    Phone = reader.GetInt32(reader.GetOrdinal("phone")),
                                    Address = new Address
                                    {
                                        Country = reader.GetString(reader.GetOrdinal("country")),
                                        City = reader.GetString(reader.GetOrdinal("city")),
                                        Street = reader.GetString(reader.GetOrdinal("street")),
                                        HouseNumber = reader.GetInt32(reader.GetOrdinal("house_number")),
                                        IndexAdd = reader.GetInt32(reader.GetOrdinal("index_add"))
                                    }
                                };
                                clients.Add(client);
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении списка клиентов: {ex.Message}", ex);
                    }
                }
            }

            return clients;
        }

        public async Task UpdateClientAsync(Client client)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("client_pkg.update_client", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_client_id", OracleDbType.Int32).Value = client.ClientId;
                    command.Parameters.Add("p_name_customer", OracleDbType.Varchar2).Value = client.ClientName;
                    command.Parameters.Add("p_country", OracleDbType.Varchar2).Value = client.Address.Country;
                    command.Parameters.Add("p_city", OracleDbType.Varchar2).Value = client.Address.City;
                    command.Parameters.Add("p_index_add", OracleDbType.Int32).Value = client.Address.IndexAdd;
                    command.Parameters.Add("p_street", OracleDbType.Varchar2).Value = client.Address.Street;
                    command.Parameters.Add("p_house_number", OracleDbType.Int32).Value = client.Address.HouseNumber;
                    command.Parameters.Add("p_phone", OracleDbType.Int64).Value = client.Phone;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при обновлении клиента: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task InsertClientAsync(Client client)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("client_pkg.insert_client", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_name_customer", OracleDbType.Varchar2).Value = client.ClientName;
                    command.Parameters.Add("p_country", OracleDbType.Varchar2).Value = client.Address.Country;
                    command.Parameters.Add("p_city", OracleDbType.Varchar2).Value = client.Address.City;
                    command.Parameters.Add("p_index_add", OracleDbType.Int32).Value = client.Address.IndexAdd;
                    command.Parameters.Add("p_street", OracleDbType.Varchar2).Value = client.Address.Street;
                    command.Parameters.Add("p_house_number", OracleDbType.Int32).Value = client.Address.HouseNumber;
                    command.Parameters.Add("p_phone", OracleDbType.Int64).Value = client.Phone;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        // Обработка ошибок, связанных с хранимыми процедурами
                        throw new ApplicationException($"Ошибка при вставке клиента: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task DeleteClientAsync(int clientId)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("client_pkg.delete_client", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_client_id", OracleDbType.Int32).Value = clientId;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при удалении клиента: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task<Client> GetClientPublicAsync(int clientId)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("client_pkg.get_client_public", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_client_id", OracleDbType.Int32).Value = clientId;

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
                                return new Client
                                {
                                    ClientId = reader.GetInt32(reader.GetOrdinal("client_id")),
                                    ClientName = reader.GetString(reader.GetOrdinal("name_customer")),
                                    Phone = reader.GetInt32(reader.GetOrdinal("phone")),
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
                        throw new ApplicationException($"Ошибка при получении публичных данных клиента: {ex.Message}", ex);
                    }
                }
            }
        }
    }
}
