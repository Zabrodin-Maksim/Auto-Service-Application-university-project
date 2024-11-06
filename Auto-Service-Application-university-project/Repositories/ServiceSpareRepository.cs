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
    public class ServiceSpareRepository : IServiceSpareRepository
    {
        private readonly string _connectionString;

        public ServiceSpareRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<ServiceSpare> GetAll()
        {
            var serviceSpares = new List<ServiceSpare>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT SPARE_PART_SPARE_PART_ID, SERVISE_OFFER_OFFER_ID FROM SERVICE_SPARE";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var serviceSpare = new ServiceSpare
                            {
                                SparePartSparePartId = Convert.ToInt32(reader["SPARE_PART_SPARE_PART_ID"]),
                                ServiseOfferOfferId = Convert.ToInt32(reader["SERVISE_OFFER_OFFER_ID"])
                            };
                            serviceSpares.Add(serviceSpare);
                        }
                    }
                }
            }

            return serviceSpares;
        }

        public ServiceSpare GetByIds(int sparePartId, int serviseOfferId)
        {
            ServiceSpare serviceSpare = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"SELECT SPARE_PART_SPARE_PART_ID, SERVISE_OFFER_OFFER_ID 
                                 FROM SERVICE_SPARE 
                                 WHERE SPARE_PART_SPARE_PART_ID = :SparePartId AND SERVISE_OFFER_OFFER_ID = :ServiseOfferId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("SparePartId", sparePartId));
                    command.Parameters.Add(new OracleParameter("ServiseOfferId", serviseOfferId));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            serviceSpare = new ServiceSpare
                            {
                                SparePartSparePartId = Convert.ToInt32(reader["SPARE_PART_SPARE_PART_ID"]),
                                ServiseOfferOfferId = Convert.ToInt32(reader["SERVISE_OFFER_OFFER_ID"])
                            };
                        }
                    }
                }
            }

            return serviceSpare;
        }

        public void Add(ServiceSpare serviceSpare)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO SERVICE_SPARE (SPARE_PART_SPARE_PART_ID, SERVISE_OFFER_OFFER_ID)
                                 VALUES (:SparePartId, :ServiseOfferId)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("SparePartId", serviceSpare.SparePartSparePartId));
                    command.Parameters.Add(new OracleParameter("ServiseOfferId", serviceSpare.ServiseOfferOfferId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(ServiceSpare serviceSpare, int oldSparePartId, int oldServiseOfferId)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE SERVICE_SPARE 
                                 SET SPARE_PART_SPARE_PART_ID = :NewSparePartId, SERVISE_OFFER_OFFER_ID = :NewServiseOfferId
                                 WHERE SPARE_PART_SPARE_PART_ID = :OldSparePartId AND SERVISE_OFFER_OFFER_ID = :OldServiseOfferId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("NewSparePartId", serviceSpare.SparePartSparePartId));
                    command.Parameters.Add(new OracleParameter("NewServiseOfferId", serviceSpare.ServiseOfferOfferId));
                    command.Parameters.Add(new OracleParameter("OldSparePartId", oldSparePartId));
                    command.Parameters.Add(new OracleParameter("OldServiseOfferId", oldServiseOfferId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int sparePartId, int serviseOfferId)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"DELETE FROM SERVICE_SPARE 
                                 WHERE SPARE_PART_SPARE_PART_ID = :SparePartId AND SERVISE_OFFER_OFFER_ID = :ServiseOfferId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("SparePartId", sparePartId));
                    command.Parameters.Add(new OracleParameter("ServiseOfferId", serviseOfferId));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
