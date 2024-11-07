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
    public class OfficeRepository : IOfficeRepository
    {
        private readonly string _connectionString;

        public OfficeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Office> GetAll()
        {
            var offices = new List<Office>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT OFFICE_ID, ADDRESS_ADDRESS_ID, OFFICE_SIZE FROM OFFICE";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var office = new Office
                            {
                                OfficeId = Convert.ToInt32(reader["OFFICE_ID"]),
                                AddressAddressId = Convert.ToInt32(reader["ADDRESS_ADDRESS_ID"]),
                                OfficeSize = Convert.ToInt32(reader["OFFICE_SIZE"])
                            };
                            offices.Add(office);
                        }
                    }
                }
            }

            return offices;
        }

        public Office GetById(int id)
        {
            Office office = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT OFFICE_ID, ADDRESS_ADDRESS_ID, OFFICE_SIZE FROM OFFICE WHERE OFFICE_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            office = new Office
                            {
                                OfficeId = Convert.ToInt32(reader["OFFICE_ID"]),
                                AddressAddressId = Convert.ToInt32(reader["ADDRESS_ADDRESS_ID"]),
                                OfficeSize = Convert.ToInt32(reader["OFFICE_SIZE"])
                            };
                        }
                    }
                }
            }

            return office;
        }

        public void Add(Office office)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO OFFICE (ADDRESS_ADDRESS_ID, OFFICE_SIZE)
                                 VALUES (:AddressAddressId, :OfficeSize)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("AddressAddressId", office.AddressAddressId));
                    command.Parameters.Add(new OracleParameter("OfficeSize", office.OfficeSize));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Office office)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE OFFICE SET ADDRESS_ADDRESS_ID = :AddressAddressId, OFFICE_SIZE = :OfficeSize
                                 WHERE OFFICE_ID = :OfficeId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("AddressAddressId", office.AddressAddressId));
                    command.Parameters.Add(new OracleParameter("OfficeSize", office.OfficeSize));
                    command.Parameters.Add(new OracleParameter("OfficeId", office.OfficeId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM OFFICE WHERE OFFICE_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
