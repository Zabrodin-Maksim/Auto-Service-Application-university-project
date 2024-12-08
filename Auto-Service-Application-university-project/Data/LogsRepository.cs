using Auto_Service_Application_university_project.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Data
{
    public class LogsRepository
    {
        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";

        public async Task<ObservableCollection<Logs>> GetAllLogsAsync()
        {
            var logs = new ObservableCollection<Logs>();

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("logger_pkg.get_all_logs", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Добавляем параметр для REF CURSOR
                    var cursorParam = new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output);
                    command.Parameters.Add(cursorParam);

                    try
                    {
                        await command.ExecuteNonQueryAsync();

                        // Получаем REF CURSOR из параметра
                        using (var reader = ((OracleRefCursor)cursorParam.Value).GetDataReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                var log = new Logs
                                {
                                    LogId = reader.GetInt32(reader.GetOrdinal("log_id")),
                                    TableName = reader.GetString(reader.GetOrdinal("table_name")),
                                    Operation = reader.GetString(reader.GetOrdinal("operation")),
                                    ChangeDate = reader.GetDateTime(reader.GetOrdinal("change_date"))
                                };
                                logs.Add(log);
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении логов: {ex.Message}", ex);
                    }
                }
            }

            return logs;
        }
    }
}
