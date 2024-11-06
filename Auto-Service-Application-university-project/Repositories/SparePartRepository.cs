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
    public class SparePartRepository : ISparePartRepository
    {
        private readonly string _connectionString;

        public SparePartRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<SparePart> GetAll()
        {
            var spareParts = new List<SparePart>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT SPARE_PART_ID, SPECIALITY, PRICE, STOCK_AVAILABILITY, OFFICE_OFFICE_ID FROM SPARE_PART";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var sparePart = new SparePart
                            {
                                SparePartId = Convert.ToInt32(reader["SPARE_PART_ID"]),
                                Speciality = reader["SPECIALITY"].ToString(),
                                Price = Convert.ToDecimal(reader["PRICE"]),
                                StockAvailability = Convert.ToInt32(reader["STOCK_AVAILABILITY"]),
                                OfficeOfficeId = Convert.ToInt32(reader["OFFICE_OFFICE_ID"])
                            };
                            spareParts.Add(sparePart);
                        }
                    }
                }
            }

            return spareParts;
        }

        public SparePart GetById(int id)
        {
            SparePart sparePart = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT SPARE_PART_ID, SPECIALITY, PRICE, STOCK_AVAILABILITY, OFFICE_OFFICE_ID FROM SPARE_PART WHERE SPARE_PART_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sparePart = new SparePart
                            {
                                SparePartId = Convert.ToInt32(reader["SPARE_PART_ID"]),
                                Speciality = reader["SPECIALITY"].ToString(),
                                Price = Convert.ToDecimal(reader["PRICE"]),
                                StockAvailability = Convert.ToInt32(reader["STOCK_AVAILABILITY"]),
                                OfficeOfficeId = Convert.ToInt32(reader["OFFICE_OFFICE_ID"])
                            };
                        }
                    }
                }
            }

            return sparePart;
        }

        public void Add(SparePart sparePart)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO SPARE_PART (SPECIALITY, PRICE, STOCK_AVAILABILITY, OFFICE_OFFICE_ID)
                                 VALUES (:Speciality, :Price, :StockAvailability, :OfficeOfficeId)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Speciality", sparePart.Speciality));
                    command.Parameters.Add(new OracleParameter("Price", sparePart.Price));
                    command.Parameters.Add(new OracleParameter("StockAvailability", sparePart.StockAvailability));
                    command.Parameters.Add(new OracleParameter("OfficeOfficeId", sparePart.OfficeOfficeId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(SparePart sparePart)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE SPARE_PART SET SPECIALITY = :Speciality, PRICE = :Price, 
                                 STOCK_AVAILABILITY = :StockAvailability, OFFICE_OFFICE_ID = :OfficeOfficeId
                                 WHERE SPARE_PART_ID = :SparePartId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Speciality", sparePart.Speciality));
                    command.Parameters.Add(new OracleParameter("Price", sparePart.Price));
                    command.Parameters.Add(new OracleParameter("StockAvailability", sparePart.StockAvailability));
                    command.Parameters.Add(new OracleParameter("OfficeOfficeId", sparePart.OfficeOfficeId));
                    command.Parameters.Add(new OracleParameter("SparePartId", sparePart.SparePartId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM SPARE_PART WHERE SPARE_PART_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
