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
    public class OfficeRepository
    {
        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";


        public async Task<ObservableCollection<Office>> GetAllOfficesAsync()
        {
            var offices = new ObservableCollection<Office>();

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("office_pkg.get_all_offices", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Выходные параметры
                    var cursorParam = new OracleParameter("p_cursor", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(cursorParam);

                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                        {
                            while (await reader.ReadAsync())
                            {
                                var office = new Office
                                {
                                    OfficeId = reader.GetInt32(reader.GetOrdinal("office_id")),
                                    OfficeSize = reader.GetInt32(reader.GetOrdinal("office_size")),
                                    Address = new Address
                                    {
                                        Country = reader.GetString(reader.GetOrdinal("country")),
                                        City = reader.GetString(reader.GetOrdinal("city")),
                                        Street = reader.GetString(reader.GetOrdinal("street")),
                                        HouseNumber = reader.GetInt32(reader.GetOrdinal("house_number")),
                                        IndexAdd = reader.GetInt32(reader.GetOrdinal("index_add"))
                                    }
                                };
                                offices.Add(office);
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении списка офисов: {ex.Message}", ex);
                    }
                }
            }

            return offices;
        }

    }
}
