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

        private readonly PasswordHasher _passwordHasher = new PasswordHasher();

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
    }
}
