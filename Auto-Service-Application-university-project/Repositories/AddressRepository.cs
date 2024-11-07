using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Repositories.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly string _connectionString;

        public AddressRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Address> GetAll()
        {
            var addresses = new List<Address>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT ADDRESS_ID, COUNTRY, CITY, INDEX_ADD, STREET, HOUSE_NUMBER FROM ADDRESS";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var address = new Address
                            {
                                AddressId = Convert.ToInt32(reader["ADDRESS_ID"]),
                                Country = reader["COUNTRY"].ToString(),
                                City = reader["CITY"].ToString(),
                                IndexAdd = reader["INDEX_ADD"].ToString(),
                                Street = reader["STREET"].ToString(),
                                HouseNumber = reader["HOUSE_NUMBER"].ToString()
                            };
                            addresses.Add(address);
                        }
                    }
                }
            }

            return addresses;
        }

        public Address GetById(int id)
        {
            Address address = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT ADDRESS_ID, COUNTRY, CITY, INDEX_ADD, STREET, HOUSE_NUMBER FROM ADDRESS WHERE ADDRESS_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            address = new Address
                            {
                                AddressId = Convert.ToInt32(reader["ADDRESS_ID"]),
                                Country = reader["COUNTRY"].ToString(),
                                City = reader["CITY"].ToString(),
                                IndexAdd = reader["INDEX_ADD"].ToString(),
                                Street = reader["STREET"].ToString(),
                                HouseNumber = reader["HOUSE_NUMBER"].ToString()
                            };
                        }
                    }
                }
            }

            return address;
        }

        public void Add(Address address)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO ADDRESS (COUNTRY, CITY, INDEX_ADD, STREET, HOUSE_NUMBER)
                                 VALUES (:Country, :City, :IndexAdd, :Street, :HouseNumber)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Country", address.Country));
                    command.Parameters.Add(new OracleParameter("City", address.City));
                    command.Parameters.Add(new OracleParameter("IndexAdd", address.IndexAdd));
                    command.Parameters.Add(new OracleParameter("Street", address.Street));
                    command.Parameters.Add(new OracleParameter("HouseNumber", address.HouseNumber));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Address address)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE ADDRESS SET COUNTRY = :Country, CITY = :City, INDEX_ADD = :IndexAdd, STREET = :Street, HOUSE_NUMBER = :HouseNumber
                                 WHERE ADDRESS_ID = :AddressId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Country", address.Country));
                    command.Parameters.Add(new OracleParameter("City", address.City));
                    command.Parameters.Add(new OracleParameter("IndexAdd", address.IndexAdd));
                    command.Parameters.Add(new OracleParameter("Street", address.Street));
                    command.Parameters.Add(new OracleParameter("HouseNumber", address.HouseNumber));
                    command.Parameters.Add(new OracleParameter("AddressId", address.AddressId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM ADDRESS WHERE ADDRESS_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
