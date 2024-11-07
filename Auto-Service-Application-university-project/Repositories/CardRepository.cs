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
    public class CardRepository : ICardRepository
    {
        private readonly string _connectionString;

        public CardRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Card> GetAll()
        {
            var cards = new List<Card>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT PAYMENT_ID, NUMBER_CARD FROM CARD";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var card = new Card
                            {
                                PaymentId = Convert.ToInt32(reader["PAYMENT_ID"]),
                                NumberCard = reader["NUMBER_CARD"].ToString()
                            };
                            cards.Add(card);
                        }
                    }
                }
            }

            return cards;
        }

        public Card GetById(int id)
        {
            Card card = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT PAYMENT_ID, NUMBER_CARD FROM CARD WHERE PAYMENT_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            card = new Card
                            {
                                PaymentId = Convert.ToInt32(reader["PAYMENT_ID"]),
                                NumberCard = reader["NUMBER_CARD"].ToString()
                            };
                        }
                    }
                }
            }

            return card;
        }

        public void Add(Card card)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO CARD (PAYMENT_ID, NUMBER_CARD)
                                 VALUES (:PaymentId, :NumberCard)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("PaymentId", card.PaymentId));
                    command.Parameters.Add(new OracleParameter("NumberCard", card.NumberCard));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Card card)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE CARD SET NUMBER_CARD = :NumberCard
                                 WHERE PAYMENT_ID = :PaymentId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("NumberCard", card.NumberCard));
                    command.Parameters.Add(new OracleParameter("PaymentId", card.PaymentId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM CARD WHERE PAYMENT_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
