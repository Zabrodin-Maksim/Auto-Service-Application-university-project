using Auto_Service_Application_university_project.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Data
{
    public class OracleRepository
    {
        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";
        public async Task<ObservableCollection<OracleObject>> GetSystemObjectsAsync()
        {
            var objects = new ObservableCollection<OracleObject>();

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT object_name, object_type FROM all_objects WHERE OWNER = 'ST67280'";

                using (var command = new OracleCommand(query, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    //command.Parameters.Add("owner", OracleDbType.Varchar2).Value = owner;

                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                objects.Add(new OracleObject
                                {
                                    ObjectName = reader.GetString(reader.GetOrdinal("object_name")),
                                    ObjectType = reader.GetString(reader.GetOrdinal("object_type"))
                                });
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении системных объектов: {ex.Message}", ex);
                    }
                }
            }

            return objects;
        }
    }
}
