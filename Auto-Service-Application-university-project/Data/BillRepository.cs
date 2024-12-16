using Auto_Service_Application_university_project.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Auto_Service_Application_university_project.Data
{
    public class BillRepository
    {
        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";

        private ServisOfferRepository _offerRepository = new ServisOfferRepository();

        public async Task InsertBillAsync(Bill bill)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("bill_pkg.insert_bill", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_servise_offer_id", OracleDbType.Int32).Value = bill.ServiceOffer.OfferId;
                    command.Parameters.Add("p_date_bill", OracleDbType.Date).Value = bill.DateBill;
                    command.Parameters.Add("p_price", OracleDbType.Decimal).Value = bill.Price;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при вставке Bill: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task UpdateBillAsync(Bill bill)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("bill_pkg.update_bill", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_bill_id", OracleDbType.Int32).Value = bill.BillId;
                    command.Parameters.Add("p_servise_offer_id", OracleDbType.Int32).Value = bill.ServiceOffer.OfferId;
                    command.Parameters.Add("p_date_bill", OracleDbType.Date).Value = bill.DateBill;
                    command.Parameters.Add("p_price", OracleDbType.Decimal).Value = bill.Price;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при обновлении Bill: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task DeleteBillAsync(int billId)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("bill_pkg.delete_bill", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_bill_id", OracleDbType.Int32).Value = billId;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        if (ex.Number == 2292) // ORA-02292
                        {
                            MessageBox.Show("Unable to delete the record because it is linked to other data. Please delete the related records first.");
                        }
                        else
                        {
                            throw new ApplicationException($"Ошибка при удалении Bill: {ex.Message}", ex);
                        }
                    }
                }
            }
        }

        public async Task<Bill> GetBillByIdAsync(int billId)
        {
            Bill bill = null;

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("bill_pkg.get_bill_by_id", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_bill_id", OracleDbType.Int32).Value = billId;

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
                            if (await reader.ReadAsync())
                            {
                                bill = new Bill
                                {
                                    BillId = reader.GetInt32(reader.GetOrdinal("bill_id")),
                                    ServiceOffer = await _offerRepository.GetServiceOfferAsync( reader.GetInt32(reader.GetOrdinal("servise_offer_offer_id"))),
                                    DateBill = reader.GetDateTime(reader.GetOrdinal("date_bill")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("price"))
                                };
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении Bill: {ex.Message}", ex);
                    }
                }
            }

            return bill;
        }

        public async Task<ObservableCollection<Bill>> GetAllBillsAsync()
        {
            var bills = new ObservableCollection<Bill>();

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("bill_pkg.get_all_bills", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Добавляем выходной параметр для REF CURSOR
                    var cursorParam = new OracleParameter("p_cursor", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(cursorParam);

                    try
                    {
                        // Выполняем процедуру
                        await command.ExecuteNonQueryAsync();

                        // Извлекаем REF CURSOR
                        var refCursor = (OracleRefCursor)cursorParam.Value;
                        using (var reader = refCursor.GetDataReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                var bill = new Bill
                                {
                                    BillId = reader.GetInt32(reader.GetOrdinal("bill_id")),
                                    DateBill = reader.GetDateTime(reader.GetOrdinal("date_bill")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("price"))
                                };

                                // Получаем servise_offer_offer_id
                                int serviseOfferId = reader.GetInt32(reader.GetOrdinal("servise_offer_offer_id"));

                                // Получаем связанный ServiceOffer
                                if (serviseOfferId > 0)
                                {
                                    var serviceOffer = await _offerRepository.GetServiceOfferAsync(serviseOfferId);
                                    bill.ServiceOffer = serviceOffer;
                                }
                                else
                                {
                                    bill.ServiceOffer = null;
                                }

                                bills.Add(bill);
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine($"OracleException при получении всех биллов: {ex.Message}");
                        throw new ApplicationException($"Ошибка при получении всех биллов: {ex.Message}", ex);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception при получении всех биллов: {ex.Message}");
                        throw;
                    }
                }
            }

            return bills;
        }

        public async Task<ObservableCollection<Bill>> GetBillsByOfficeIdAsync(int officeId)
        {
            var bills = new ObservableCollection<Bill>();

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("bill_pkg.get_bills_by_office_id", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входной параметр
                    var officeIdParam = new OracleParameter("p_office_id", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Input,
                        Value = officeId
                    };
                    command.Parameters.Add(officeIdParam);

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
                            while (await reader.ReadAsync())
                            {
                                var bill = new Bill
                                {
                                    BillId = reader.GetInt32(reader.GetOrdinal("bill_id")),
                                    ServiceOffer = await _offerRepository.GetServiceOfferAsync( reader.GetInt32(reader.GetOrdinal("servise_offer_offer_id"))),
                                    DateBill = reader.GetDateTime(reader.GetOrdinal("date_bill")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("price"))
                                };
                                bills.Add(bill);
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении счетов по office_id: {ex.Message}", ex);
                    }
                }
            }

            return bills;
        }
    }
}
