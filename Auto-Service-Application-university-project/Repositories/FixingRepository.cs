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
    public class FixingRepository : IFixingRepository
    {
        private readonly string _connectionString;

        public FixingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Fixing> GetAll()
        {
            var fixings = new List<Fixing>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT OFFER_ID, SPECIALITY FROM FIXING";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var fixing = new Fixing
                            {
                                OfferId = Convert.ToInt32(reader["OFFER_ID"]),
                                Speciality = reader["SPECIALITY"].ToString()
                            };
                            fixings.Add(fixing);
                        }
                    }
                }
            }

            return fixings;
        }

        public Fixing GetById(int id)
        {
            Fixing fixing = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT OFFER_ID, SPECIALITY FROM FIXING WHERE OFFER_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            fixing = new Fixing
                            {
                                OfferId = Convert.ToInt32(reader["OFFER_ID"]),
                                Speciality = reader["SPECIALITY"].ToString()
                            };
                        }
                    }
                }
            }

            return fixing;
        }

        public void Add(Fixing fixing)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO FIXING (OFFER_ID, SPECIALITY)
                                 VALUES (:OfferId, :Speciality)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("OfferId", fixing.OfferId));
                    command.Parameters.Add(new OracleParameter("Speciality", fixing.Speciality));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Fixing fixing)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE FIXING SET SPECIALITY = :Speciality
                                 WHERE OFFER_ID = :OfferId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Speciality", fixing.Speciality));
                    command.Parameters.Add(new OracleParameter("OfferId", fixing.OfferId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM FIXING WHERE OFFER_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
