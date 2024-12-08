using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auto_Service_Application_university_project.Models;
using Dapper;

namespace Auto_Service_Application_university_project.Data
{
    public class LogsRepository
    {
        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";

        public async Task<ObservableCollection<Logs>> GetAllLogsAsync()
        {
            using (IDbConnection db = new OracleConnection(connectionString))
            {
                var logs = new ObservableCollection<Logs>();

                var parameters = new DynamicParameters();
                parameters.Add("p_cursor", dbType: DbType.Object, direction: ParameterDirection.Output);

                await db.ExecuteAsync("logger_pkg.get_all_logs", parameters, commandType: CommandType.StoredProcedure);

                using (var reader = ((OracleRefCursor)parameters.Get<OracleRefCursor>("p_cursor")).GetDataReader())
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

                return logs;
            }
        }
    }
}
