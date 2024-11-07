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
    public class CashRepository : ICashRepository
    {
        private readonly string _connectionString;

        public CashRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Cash> GetAll()
        {
            var cashList = new List<Cash>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT PAYMENT_ID, TAKEN, GIVEN FROM CASH";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cash = new Cash
                            {
                                PaymentId = Convert.ToInt32(reader["PAYMENT_ID"]),
                                Taken = Convert.ToDecimal(reader["TAKEN"]),
                                Given = Convert.ToDecimal(reader["GIVEN"])
                            };
                            cashList.Add(cash);
                        }
                    }
                }
            }

            return cashList;
        }

        public Cash GetById(int id)
        {
            Cash cash = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT PAYMENT_ID, TAKEN, GIVEN FROM CASH WHERE PAYMENT_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cash = new Cash
                            {
                                PaymentId = Convert.ToInt32(reader["PAYMENT_ID"]),
                                Taken = Convert.ToDecimal(reader["TAKEN"]),
                                Given = Convert.ToDecimal(reader["GIVEN"])
                            };
                        }
                    }
                }
            }

            return cash;
        }

        public void Add(Cash cash)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO CASH (PAYMENT_ID, TAKEN, GIVEN)
                                 VALUES (:PaymentId, :Taken, :Given)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("PaymentId", cash.PaymentId));
                    command.Parameters.Add(new OracleParameter("Taken", cash.Taken));
                    command.Parameters.Add(new OracleParameter("Given", cash.Given));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Cash cash)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE CASH SET TAKEN = :Taken, GIVEN = :Given
                                 WHERE PAYMENT_ID = :PaymentId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Taken", cash.Taken));
                    command.Parameters.Add(new OracleParameter("Given", cash.Given));
                    command.Parameters.Add(new OracleParameter("PaymentId", cash.PaymentId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM CASH WHERE PAYMENT_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
