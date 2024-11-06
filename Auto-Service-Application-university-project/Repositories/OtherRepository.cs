using BDAS2_University_Project.Models;
using BDAS2_University_Project.Repositories.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories
{
    public class OtherRepository : IOtherRepository
    {
        private readonly string _connectionString;

        public OtherRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Other> GetAll()
        {
            var others = new List<Other>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT OFFER_ID, SPECIALITY FROM OTHER";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var other = new Other
                            {
                                OfferId = Convert.ToInt32(reader["OFFER_ID"]),
                                Speciality = reader["SPECIALITY"].ToString()
                            };
                            others.Add(other);
                        }
                    }
                }
            }

            return others;
        }

        public Other GetById(int id)
        {
            Other other = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT OFFER_ID, SPECIALITY FROM OTHER WHERE OFFER_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            other = new Other
                            {
                                OfferId = Convert.ToInt32(reader["OFFER_ID"]),
                                Speciality = reader["SPECIALITY"].ToString()
                            };
                        }
                    }
                }
            }

            return other;
        }

        public void Add(Other other)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO OTHER (OFFER_ID, SPECIALITY)
                                 VALUES (:OfferId, :Speciality)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("OfferId", other.OfferId));
                    command.Parameters.Add(new OracleParameter("Speciality", other.Speciality));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Other other)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE OTHER SET SPECIALITY = :Speciality
                                 WHERE OFFER_ID = :OfferId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Speciality", other.Speciality));
                    command.Parameters.Add(new OracleParameter("OfferId", other.OfferId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM OTHER WHERE OFFER_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
