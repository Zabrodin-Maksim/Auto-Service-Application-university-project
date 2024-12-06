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
    public class PaymentTypeRepository
    {
        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";

        public async Task<ObservableCollection<PaymentType>> GetAllPaymentTypesAsync()
        {
            var paymentTypes = new ObservableCollection<PaymentType>();

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("payment_type_pkg.get_all_payment_types", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

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
                                var paymentType = new PaymentType
                                {
                                    PaymentTypeId = reader.GetInt32(reader.GetOrdinal("payment_type_id")),
                                    TypeName = reader.GetString(reader.GetOrdinal("type_name"))
                                };

                                paymentTypes.Add(paymentType);
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении типов платежей: {ex.Message}", ex);
                    }
                }
            }

            return paymentTypes;
        }
    }
}
