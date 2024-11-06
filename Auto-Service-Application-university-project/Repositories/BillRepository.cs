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
    public class BillRepository : IBillRepository
    {
        private readonly string _connectionString;

        public BillRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Bill> GetAll()
        {
            var bills = new List<Bill>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT BILL_ID, SERVISE_OFFER_OFFER_ID, DATE_BILL, PRICE FROM BILL";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var bill = new Bill
                            {
                                BillId = Convert.ToInt32(reader["BILL_ID"]),
                                ServiseOfferOfferId = Convert.ToInt32(reader["SERVISE_OFFER_OFFER_ID"]),
                                DateBill = Convert.ToDateTime(reader["DATE_BILL"]),
                                Price = Convert.ToDecimal(reader["PRICE"])
                            };
                            bills.Add(bill);
                        }
                    }
                }
            }

            return bills;
        }

        public Bill GetById(int id)
        {
            Bill bill = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT BILL_ID, SERVISE_OFFER_OFFER_ID, DATE_BILL, PRICE FROM BILL WHERE BILL_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bill = new Bill
                            {
                                BillId = Convert.ToInt32(reader["BILL_ID"]),
                                ServiseOfferOfferId = Convert.ToInt32(reader["SERVISE_OFFER_OFFER_ID"]),
                                DateBill = Convert.ToDateTime(reader["DATE_BILL"]),
                                Price = Convert.ToDecimal(reader["PRICE"])
                            };
                        }
                    }
                }
            }

            return bill;
        }

        public void Add(Bill bill)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO BILL (SERVISE_OFFER_OFFER_ID, DATE_BILL, PRICE)
                                 VALUES (:ServiseOfferOfferId, :DateBill, :Price)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("ServiseOfferOfferId", bill.ServiseOfferOfferId));
                    command.Parameters.Add(new OracleParameter("DateBill", bill.DateBill));
                    command.Parameters.Add(new OracleParameter("Price", bill.Price));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Bill bill)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE BILL SET SERVISE_OFFER_OFFER_ID = :ServiseOfferOfferId, DATE_BILL = :DateBill, PRICE = :Price
                                 WHERE BILL_ID = :BillId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("ServiseOfferOfferId", bill.ServiseOfferOfferId));
                    command.Parameters.Add(new OracleParameter("DateBill", bill.DateBill));
                    command.Parameters.Add(new OracleParameter("Price", bill.Price));
                    command.Parameters.Add(new OracleParameter("BillId", bill.BillId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM BILL WHERE BILL_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
