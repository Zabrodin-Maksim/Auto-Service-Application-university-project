using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Repositories.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_UnAuto_Service_Application_university_projectiversity_Project.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly string _connectionString;

        public PaymentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Payment> GetAll()
        {
            var payments = new List<Payment>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT payment_id, bill_bill_id, client_client_id, payment_type_id FROM payment";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var payment = new Payment
                            {
                                PaymentId = Convert.ToInt32(reader["payment_id"]),
                                BillBillId = Convert.ToInt32(reader["bill_bill_id"]),
                                ClientClientId = Convert.ToInt32(reader["client_client_id"]),
                                PaymentTypeId = Convert.ToInt32(reader["payment_type_id"])
                            };
                            payments.Add(payment);
                        }
                    }
                }
            }

            return payments;
        }

        public Payment GetByBillId(int billId)
        {
            Payment payment = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT payment_id, bill_bill_id, client_client_id, payment_type_id FROM payment WHERE bill_bill_id = :BillId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("BillId", OracleDbType.Int32).Value = billId;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            payment = new Payment
                            {
                                PaymentId = Convert.ToInt32(reader["payment_id"]),
                                BillBillId = Convert.ToInt32(reader["bill_bill_id"]),
                                ClientClientId = Convert.ToInt32(reader["client_client_id"]),
                                PaymentTypeId = Convert.ToInt32(reader["payment_type_id"])
                            };
                        }
                    }
                }
            }

            return payment;
        }

        public void Add(Payment payment, User currentUser)
        {
            // Проверка прав доступа
            if (!currentUser.HasPermission("AddPayment"))
                throw new UnauthorizedAccessException("У вас нет прав для добавления платежа.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Вызов хранимой процедуры для вставки данных
                    using (var command = new OracleCommand("insert_payment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_bill_bill_id", OracleDbType.Int32).Value = payment.BillBillId;
                        command.Parameters.Add("p_client_client_id", OracleDbType.Int32).Value = payment.ClientClientId;
                        command.Parameters.Add("p_payment_type_id", OracleDbType.Int32).Value = payment.PaymentTypeId;

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Update(Payment payment, User currentUser)
        {
            // Проверка прав доступа
            if (!currentUser.HasPermission("UpdatePayment"))
                throw new UnauthorizedAccessException("У вас нет прав для обновления платежа.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Вызов хранимой процедуры для обновления данных
                    using (var command = new OracleCommand("update_payment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_payment_id", OracleDbType.Int32).Value = payment.PaymentId;
                        command.Parameters.Add("p_bill_bill_id", OracleDbType.Int32).Value = payment.BillBillId;
                        command.Parameters.Add("p_client_client_id", OracleDbType.Int32).Value = payment.ClientClientId;
                        command.Parameters.Add("p_payment_type_id", OracleDbType.Int32).Value = payment.PaymentTypeId;

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Delete(int billId, User currentUser)
        {
            // Проверка прав доступа
            if (!currentUser.HasPermission("DeletePayment"))
                throw new UnauthorizedAccessException("У вас нет прав для удаления платежа.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Получаем payment_id по bill_id
                    int paymentId = GetPaymentIdByBillId(billId, connection);

                    if (paymentId == 0)
                        throw new Exception("Платеж не найден.");

                    // Вызов хранимой процедуры для удаления данных
                    using (var command = new OracleCommand("delete_payment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_payment_id", OracleDbType.Int32).Value = paymentId;

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private int GetPaymentIdByBillId(int billId, OracleConnection connection)
        {
            string query = "SELECT payment_id FROM payment WHERE bill_bill_id = :BillId";
            using (var command = new OracleCommand(query, connection))
            {
                command.Parameters.Add("BillId", OracleDbType.Int32).Value = billId;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader["payment_id"]);
                    }
                }
            }
            return 0;
        }
    }
}
