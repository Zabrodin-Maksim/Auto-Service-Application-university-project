using Auto_Service_Application_university_project.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Data
{
    public class SparePartRepository
    {
        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";

        private OfficeRepository _officeRepository = new OfficeRepository();

        public async Task InsertSparePartAsync(SparePart sparePart)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("spare_part_pkg.insert_spare_part", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("p_speciality", OracleDbType.Varchar2).Value = sparePart.Speciality;
                    command.Parameters.Add("p_price", OracleDbType.Decimal).Value = sparePart.Price;
                    command.Parameters.Add("p_stock_availability", OracleDbType.Char).Value = sparePart.StockAvailability;
                    command.Parameters.Add("p_office_id", OracleDbType.Int32).Value = sparePart.Office.OfficeId;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при вставке SparePart: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task UpdateSparePartAsync(SparePart sparePart)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("spare_part_pkg.update_spare_part", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_spare_part_id", OracleDbType.Int32).Value = sparePart.SparePartId;
                    command.Parameters.Add("p_speciality", OracleDbType.Varchar2).Value = sparePart.Speciality;
                    command.Parameters.Add("p_price", OracleDbType.Decimal).Value = sparePart.Price;
                    command.Parameters.Add("p_stock_availability", OracleDbType.Char).Value = sparePart.StockAvailability;
                    command.Parameters.Add("p_office_id", OracleDbType.Int32).Value = sparePart.Office.OfficeId;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при обновлении SparePart: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task DeleteSparePartAsync(int sparePartId)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("spare_part_pkg.delete_spare_part", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_spare_part_id", OracleDbType.Int32).Value = sparePartId;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при удалении SparePart: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task<SparePart> GetSparePartByIdAsync(int sparePartId)
        {
            SparePart sparePart = null;

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("spare_part_pkg.get_spare_part_by_id", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_spare_part_id", OracleDbType.Int32).Value = sparePartId;

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
                                sparePart = new SparePart
                                {
                                    SparePartId = reader.GetInt32(reader.GetOrdinal("spare_part_id")),
                                    Speciality = reader.GetString(reader.GetOrdinal("speciality")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("price")),
                                    StockAvailability = reader.GetChar(reader.GetOrdinal("stock_availability")),
                                    Office = await _officeRepository.GetOfficeAsync(reader.GetInt32(reader.GetOrdinal("office_office_id")))
                                };
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении SparePart: {ex.Message}", ex);
                    }
                }
            }

            return sparePart;
        }

        public async Task<ObservableCollection<SparePart>> GetAllSparePartsAsync()
        {
            var spareParts = new ObservableCollection<SparePart>();

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("spare_part_pkg.get_all_spare_parts", connection))
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
                                var sparePart = new SparePart
                                {
                                    SparePartId = reader.GetInt32(reader.GetOrdinal("spare_part_id")),
                                    Speciality = reader.GetString(reader.GetOrdinal("speciality")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("price")),
                                    //StockAvailability = reader.GetChar(reader.GetOrdinal("stock_availability")),
                                    Office = await _officeRepository.GetOfficeAsync(reader.GetInt32(reader.GetOrdinal("office_office_id")))
                                };

                                spareParts.Add(sparePart);
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении всех SpareParts: {ex.Message}", ex);
                    }
                }
            }

            return spareParts;
        }

        public async Task<ObservableCollection<SparePart>> GetSparePartsByOfficeAsync(int officeId)
        {
            var spareParts = new ObservableCollection<SparePart>();

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("spare_part_pkg.get_spare_parts_by_office", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_office_id", OracleDbType.Int32).Value = officeId;

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
                                var sparePart = new SparePart
                                {
                                    SparePartId = reader.GetInt32(reader.GetOrdinal("spare_part_id")),
                                    Speciality = reader.GetString(reader.GetOrdinal("speciality")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("price")),
                                    //StockAvailability = reader.GetChar(reader.GetOrdinal("stock_availability")),
                                    Office = await _officeRepository.GetOfficeAsync(reader.GetInt32(reader.GetOrdinal("office_office_id")))
                                };

                                spareParts.Add(sparePart);
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении SpareParts по OfficeId: {ex.Message}", ex);
                    }
                }
            }

            return spareParts;
        }
    }
}
