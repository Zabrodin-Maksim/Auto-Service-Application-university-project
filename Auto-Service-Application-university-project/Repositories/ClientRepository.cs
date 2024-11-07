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
    public class ClientRepository : IClientRepository
    {
        private readonly string _connectionString;

        public ClientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Client> GetAll()
        {
            var clients = new List<Client>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT name_customer, address_address_id, phone FROM client";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var client = new Client
                            {
                                NameCustomer = reader["name_customer"].ToString(),
                                AddressAddressId = Convert.ToInt32(reader["address_address_id"]),
                                Phone = reader["phone"].ToString()
                            };
                            clients.Add(client);
                        }
                    }
                }
            }

            return clients;
        }

        public Client GetByPhone(string phone)
        {
            Client client = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT name_customer, address_address_id, phone FROM client WHERE phone = :Phone";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("Phone", OracleDbType.Varchar2).Value = phone;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            client = new Client
                            {
                                NameCustomer = reader["name_customer"].ToString(),
                                AddressAddressId = Convert.ToInt32(reader["address_address_id"]),
                                Phone = reader["phone"].ToString()
                            };
                        }
                    }
                }
            }

            return client;
        }

        public void Add(Client client, User currentUser)
        {
            // Проверка прав доступа (Требования 10, 22, 23)
            if (!currentUser.HasPermission("AddClient"))
                throw new UnauthorizedAccessException("У вас нет прав для добавления клиента.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction(); // Начало транзакции (Требование 16)

                try
                {
                    // Вызов хранимой процедуры для вставки данных (Требования 5, 12)
                    using (var command = new OracleCommand("insert_client", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_name_customer", OracleDbType.Varchar2).Value = client.NameCustomer;
                        command.Parameters.Add("p_address_address_id", OracleDbType.Int32).Value = client.AddressAddressId;
                        command.Parameters.Add("p_phone", OracleDbType.Varchar2).Value = client.Phone;

                        command.ExecuteNonQuery();
                    }

                    // Фиксация транзакции
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Откат транзакции при ошибке
                    transaction.Rollback();
                    throw; // Повторно выбрасываем исключение
                }
            }
        }

        public void Update(Client client, User currentUser)
        {
            // Проверка прав доступа (Требования 10, 22, 23)
            if (!currentUser.HasPermission("UpdateClient"))
                throw new UnauthorizedAccessException("У вас нет прав для обновления клиента.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Получаем client_id по телефону
                    int clientId = GetClientIdByPhone(client.Phone, connection);

                    if (clientId == 0)
                        throw new Exception("Клиент не найден.");

                    // Вызов хранимой процедуры для обновления данных (Требования 5, 12)
                    using (var command = new OracleCommand("update_client", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_client_id", OracleDbType.Int32).Value = clientId;
                        command.Parameters.Add("p_name_customer", OracleDbType.Varchar2).Value = client.NameCustomer;
                        command.Parameters.Add("p_address_address_id", OracleDbType.Int32).Value = client.AddressAddressId;
                        command.Parameters.Add("p_phone", OracleDbType.Varchar2).Value = client.Phone;

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

        public void Delete(string phone, User currentUser)
        {
            // Проверка прав доступа (Требования 10, 22, 23)
            if (!currentUser.HasPermission("DeleteClient"))
                throw new UnauthorizedAccessException("У вас нет прав для удаления клиента.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Получаем client_id по телефону
                    int clientId = GetClientIdByPhone(phone, connection);

                    if (clientId == 0)
                        throw new Exception("Клиент не найден.");

                    // Вызов хранимой процедуры для удаления данных (Требования 5, 12)
                    using (var command = new OracleCommand("delete_client", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_client_id", OracleDbType.Int32).Value = clientId;

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

        private int GetClientIdByPhone(string phone, OracleConnection connection)
        {
            string query = "SELECT client_id FROM client WHERE phone = :Phone";
            using (var command = new OracleCommand(query, connection))
            {
                command.Parameters.Add("Phone", OracleDbType.Varchar2).Value = phone;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader["client_id"]);
                    }
                }
            }
            return 0;
        }
    }
}
