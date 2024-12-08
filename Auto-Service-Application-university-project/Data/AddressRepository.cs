using Auto_Service_Application_university_project.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Auto_Service_Application_university_project.Data
{
    public class AddressRepository
    {
        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";

        public async Task InsertAddressAsync(Address address)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("address_pkg.insert_address", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_country", OracleDbType.Varchar2).Value = address.Country;
                    command.Parameters.Add("p_city", OracleDbType.Varchar2).Value = address.City;
                    command.Parameters.Add("p_index_add", OracleDbType.Int32).Value = address.IndexAdd;
                    command.Parameters.Add("p_street", OracleDbType.Varchar2).Value = address.Street;
                    command.Parameters.Add("p_house_number", OracleDbType.Int32).Value = address.HouseNumber;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        // Процедура не возвращает address_id, поэтому AddressId остается неизменным
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при вставке адреса: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task UpdateAddressAsync(Address address)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("address_pkg.update_address", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_address_id", OracleDbType.Int32).Value = address.AddressId;
                    command.Parameters.Add("p_country", OracleDbType.Varchar2).Value = address.Country;
                    command.Parameters.Add("p_city", OracleDbType.Varchar2).Value = address.City;
                    command.Parameters.Add("p_index_add", OracleDbType.Int32).Value = address.IndexAdd;
                    command.Parameters.Add("p_street", OracleDbType.Varchar2).Value = address.Street;
                    command.Parameters.Add("p_house_number", OracleDbType.Int32).Value = address.HouseNumber;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при обновлении адреса: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task DeleteAddressAsync(int addressId)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("address_pkg.delete_address", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входной параметр
                    command.Parameters.Add("p_address_id", OracleDbType.Int32).Value = addressId;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        if (ex.Number == 2292) // ORA-02292
                        {
                            MessageBox.Show("Unable to delete the record because it is linked to other data. Please delete the related records first.");
                        }
                        else
                        {
                            throw new ApplicationException($"Ошибка при удалении адреса: {ex.Message}", ex);
                        }
                    }
                }
            }
        }

        public async Task<Address> GetAddressByIdAsync(int addressId)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("address_pkg.get_address_by_id", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входной параметр
                    command.Parameters.Add("p_address_id", OracleDbType.Int32).Value = addressId;

                    // Выходной параметр (Ref Cursor)
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
                                return new Address
                                {
                                    AddressId = reader.GetInt32(reader.GetOrdinal("address_id")),
                                    Country = reader.GetString(reader.GetOrdinal("country")),
                                    City = reader.GetString(reader.GetOrdinal("city")),
                                    IndexAdd = reader.GetInt32(reader.GetOrdinal("index_add")),
                                    Street = reader.GetString(reader.GetOrdinal("street")),
                                    HouseNumber = reader.GetInt32(reader.GetOrdinal("house_number"))
                                };
                            }

                            // Если запись не найдена
                            return null;
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении адреса: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task<ObservableCollection<Address>> GetAllAddressesAsync()
        {
            var addresses = new ObservableCollection<Address>();

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("address_pkg.get_all_addresses", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Выходной параметр (Ref Cursor)
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
                                var address = new Address
                                {
                                    AddressId = reader.GetInt32(reader.GetOrdinal("address_id")),
                                    Country = reader.GetString(reader.GetOrdinal("country")),
                                    City = reader.GetString(reader.GetOrdinal("city")),
                                    IndexAdd = reader.GetInt32(reader.GetOrdinal("index_add")),
                                    Street = reader.GetString(reader.GetOrdinal("street")),
                                    HouseNumber = reader.GetInt32(reader.GetOrdinal("house_number"))
                                };

                                addresses.Add(address);
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении всех адресов: {ex.Message}", ex);
                    }
                }
            }

            return addresses;
        }
    }
}

