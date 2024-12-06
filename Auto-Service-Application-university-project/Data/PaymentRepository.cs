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
    public class PaymentRepository
    {

        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";

        public async Task InsertPaymentAsync(Payment payment)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("payment_pkg.insert_payment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("p_bill_id", OracleDbType.Int32).Value = payment.Bill?.BillId ?? (object)DBNull.Value;
                    command.Parameters.Add("p_payment_type_id", OracleDbType.Int32).Value = payment.PaymentType?.PaymentTypeId ?? (object)DBNull.Value;
                    command.Parameters.Add("p_client_id", OracleDbType.Int32).Value = payment.Client?.ClientId ?? (object)DBNull.Value;

                    if (payment.PaymentType != null && payment.PaymentType.TypeName.Equals("card", StringComparison.OrdinalIgnoreCase))
                    {
                        command.Parameters.Add("p_number_card", OracleDbType.Int32).Value = payment.Card?.NumberCard ?? (object)DBNull.Value;
                        command.Parameters.Add("p_taken", OracleDbType.Decimal).Value = DBNull.Value;
                    }
                    else if (payment.PaymentType != null && payment.PaymentType.TypeName.Equals("cash", StringComparison.OrdinalIgnoreCase))
                    {
                        command.Parameters.Add("p_number_card", OracleDbType.Int32).Value = DBNull.Value;
                        command.Parameters.Add("p_taken", OracleDbType.Decimal).Value = payment.Cash?.Taken ?? (object)DBNull.Value;
                    }
                    else
                    {
                        command.Parameters.Add("p_number_card", OracleDbType.Int32).Value = DBNull.Value;
                        command.Parameters.Add("p_taken", OracleDbType.Decimal).Value = DBNull.Value;
                    }

                    var paymentIdParam = new OracleParameter("p_payment_id", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(paymentIdParam);

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        payment.PaymentId = Convert.ToInt32(paymentIdParam.Value.ToString());
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при вставке платежа: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("payment_pkg.update_payment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_payment_id", OracleDbType.Int32).Value = payment.PaymentId;
                    command.Parameters.Add("p_bill_id", OracleDbType.Int32).Value = payment.Bill?.BillId ?? (object)DBNull.Value;
                    command.Parameters.Add("p_payment_type_id", OracleDbType.Int32).Value = payment.PaymentType?.PaymentTypeId ?? (object)DBNull.Value;
                    command.Parameters.Add("p_client_id", OracleDbType.Int32).Value = payment.Client?.ClientId ?? (object)DBNull.Value;

                    // Специфичные параметры
                    if (payment.PaymentType != null && payment.PaymentType.TypeName.Equals("card", StringComparison.OrdinalIgnoreCase))
                    {
                        command.Parameters.Add("p_number_card", OracleDbType.Int32).Value = payment.Card?.NumberCard ?? (object)DBNull.Value;
                        command.Parameters.Add("p_taken", OracleDbType.Decimal).Value = DBNull.Value;
                    }
                    else if (payment.PaymentType != null && payment.PaymentType.TypeName.Equals("cash", StringComparison.OrdinalIgnoreCase))
                    {
                        command.Parameters.Add("p_number_card", OracleDbType.Int32).Value = DBNull.Value;
                        command.Parameters.Add("p_taken", OracleDbType.Decimal).Value = payment.Cash?.Taken ?? (object)DBNull.Value;
                    }
                    else
                    {
                        command.Parameters.Add("p_number_card", OracleDbType.Int32).Value = DBNull.Value;
                        command.Parameters.Add("p_taken", OracleDbType.Decimal).Value = DBNull.Value;
                    }

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при обновлении платежа: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task DeletePaymentAsync(int paymentId)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("payment_pkg.delete_payment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входной параметр
                    command.Parameters.Add("p_payment_id", OracleDbType.Int32).Value = paymentId;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при удалении платежа: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task<ObservableCollection<Payment>> GetAllPaymentsAsync()
        {
            var payments = new ObservableCollection<Payment>();

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("payment_pkg.get_all_payments", connection))
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
                                var payment = new Payment
                                {
                                    PaymentId = reader.GetInt32(reader.GetOrdinal("payment_id")),
                                    Bill = new Bill
                                    {
                                        BillId = reader.GetInt32(reader.GetOrdinal("bill_bill_id"))
                                        // Заполните другие свойства Bill при необходимости
                                    },
                                    Client = new Client
                                    {
                                        ClientId = reader.GetInt32(reader.GetOrdinal("client_client_id"))
                                        // Заполните другие свойства Client при необходимости
                                    },
                                    PaymentType = new PaymentType
                                    {
                                        PaymentTypeId = reader.GetInt32(reader.GetOrdinal("type_name")) // Предполагается, что type_name числовой ID, иначе скорректируйте
                                                                                                        // Заполните другие свойства PaymentType при необходимости
                                    },
                                    Card = reader.IsDBNull(reader.GetOrdinal("number_card")) ? null : new Card
                                    {
                                        NumberCard = reader.GetInt32(reader.GetOrdinal("number_card"))
                                    },
                                    Cash = reader.IsDBNull(reader.GetOrdinal("taken")) ? null : new Cash
                                    {
                                        Taken = reader.GetDecimal(reader.GetOrdinal("taken")),
                                        Given = reader.GetDecimal(reader.GetOrdinal("given"))
                                    }
                                };
                                payments.Add(payment);
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении платежей: {ex.Message}", ex);
                    }
                }
            }

            return payments;
        }

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            Payment payment = null;

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("payment_pkg.get_payment_by_id", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_payment_id", OracleDbType.Int32).Value = paymentId;

                    // Параметр OUT для курсора
                    var cursorParam = new OracleParameter("p_cursor", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(cursorParam);

                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                        {
                            if (await reader.ReadAsync())
                            {
                                payment = new Payment
                                {
                                    PaymentId = reader.GetInt32(reader.GetOrdinal("payment_id")),
                                    Bill = new Bill
                                    {
                                        BillId = reader.GetInt32(reader.GetOrdinal("bill_bill_id"))
                                        // Заполните другие свойства Bill при необходимости
                                    },
                                    Client = new Client
                                    {
                                        ClientId = reader.GetInt32(reader.GetOrdinal("client_client_id"))
                                        // Заполните другие свойства Client при необходимости
                                    },
                                    PaymentType = new PaymentType
                                    {
                                        PaymentTypeId = reader.GetInt32(reader.GetOrdinal("payment_type_id")),
                                        TypeName = reader.GetString(reader.GetOrdinal("type_name"))
                                    },
                                    Card = reader.IsDBNull(reader.GetOrdinal("number_card")) ? null : new Card
                                    {
                                        NumberCard = reader.GetInt32(reader.GetOrdinal("number_card"))
                                    },
                                    Cash = reader.IsDBNull(reader.GetOrdinal("taken")) ? null : new Cash
                                    {
                                        Taken = reader.GetDecimal(reader.GetOrdinal("taken")),
                                        Given = reader.GetDecimal(reader.GetOrdinal("given"))
                                    }
                                };
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении платежа: {ex.Message}", ex);
                    }
                }
            }

            return payment;
        }
    }
}
