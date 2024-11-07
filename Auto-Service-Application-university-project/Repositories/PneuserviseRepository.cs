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
    public class PneuserviseRepository : IPneuserviseRepository
    {
        private readonly string _connectionString;

        public PneuserviseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Pneuservise> GetAll()
        {
            var pneuservices = new List<Pneuservise>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT OFFER_ID, RADIUS_WHEEL FROM PNEUSERVISE";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pneuservise = new Pneuservise
                            {
                                OfferId = Convert.ToInt32(reader["OFFER_ID"]),
                                RadiusWheel = Convert.ToInt32(reader["RADIUS_WHEEL"])
                            };
                            pneuservices.Add(pneuservise);
                        }
                    }
                }
            }

            return pneuservices;
        }

        public Pneuservise GetById(int id)
        {
            Pneuservise pneuservise = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT OFFER_ID, RADIUS_WHEEL FROM PNEUSERVISE WHERE OFFER_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pneuservise = new Pneuservise
                            {
                                OfferId = Convert.ToInt32(reader["OFFER_ID"]),
                                RadiusWheel = Convert.ToInt32(reader["RADIUS_WHEEL"])
                            };
                        }
                    }
                }
            }

            return pneuservise;
        }

        public void Add(Pneuservise pneuservise)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO PNEUSERVISE (OFFER_ID, RADIUS_WHEEL)
                                 VALUES (:OfferId, :RadiusWheel)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("OfferId", pneuservise.OfferId));
                    command.Parameters.Add(new OracleParameter("RadiusWheel", pneuservise.RadiusWheel));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Pneuservise pneuservise)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE PNEUSERVISE SET RADIUS_WHEEL = :RadiusWheel WHERE OFFER_ID = :OfferId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("RadiusWheel", pneuservise.RadiusWheel));
                    command.Parameters.Add(new OracleParameter("OfferId", pneuservise.OfferId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM PNEUSERVISE WHERE OFFER_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
