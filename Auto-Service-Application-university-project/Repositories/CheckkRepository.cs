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
    public class CheckkRepository : ICheckkRepository
    {
        private readonly string _connectionString;

        public CheckkRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Checkk> GetAll()
        {
            var checkkList = new List<Checkk>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT OFFER_ID, SPECIALITY FROM CHECKK";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var checkk = new Checkk
                            {
                                OfferId = Convert.ToInt32(reader["OFFER_ID"]),
                                Speciality = reader["SPECIALITY"].ToString()
                            };
                            checkkList.Add(checkk);
                        }
                    }
                }
            }

            return checkkList;
        }

        public Checkk GetById(int id)
        {
            Checkk checkk = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT OFFER_ID, SPECIALITY FROM CHECKK WHERE OFFER_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            checkk = new Checkk
                            {
                                OfferId = Convert.ToInt32(reader["OFFER_ID"]),
                                Speciality = reader["SPECIALITY"].ToString()
                            };
                        }
                    }
                }
            }

            return checkk;
        }

        public void Add(Checkk checkk)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO CHECKK (OFFER_ID, SPECIALITY)
                                 VALUES (:OfferId, :Speciality)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("OfferId", checkk.OfferId));
                    command.Parameters.Add(new OracleParameter("Speciality", checkk.Speciality));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Checkk checkk)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE CHECKK SET SPECIALITY = :Speciality
                                 WHERE OFFER_ID = :OfferId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Speciality", checkk.Speciality));
                    command.Parameters.Add(new OracleParameter("OfferId", checkk.OfferId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM CHECKK WHERE OFFER_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
