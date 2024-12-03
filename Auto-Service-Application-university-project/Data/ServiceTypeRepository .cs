using Auto_Service_Application_university_project.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Data
{
    public class ServiceTypeRepository
    {
        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";

        public async Task<ObservableCollection<ServiceType>> GetAllServiceTypesAsync()
        {
            var serviceTypes = new ObservableCollection<ServiceType>();

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("SELECT service_type_id, type_name FROM service_type ORDER BY service_type_id", connection))
                {
                    command.CommandType = CommandType.Text;

                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var serviceType = new ServiceType
                                {
                                    ServiceTypeId = reader.GetInt32(reader.GetOrdinal("service_type_id")),
                                    TypeName = reader.GetString(reader.GetOrdinal("type_name"))
                                };
                                serviceTypes.Add(serviceType);
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении типов услуг: {ex.Message}", ex);
                    }
                }
            }

            return serviceTypes;
        }
    }
}
