using Auto_Service_Application_university_project.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Data
{
    public class FileStorageRepository
    {
        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";

        public FileStorageRepository(string connectionString)
        {
            connectionString = connectionString;
        }


        public async Task InsertFileAsync(FileStorage file)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("file_storage_pkg.insert_file", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Добавляем параметры
                    command.Parameters.Add("p_file_name", OracleDbType.Varchar2).Value = file.FileName;
                    command.Parameters.Add("p_file_type", OracleDbType.Varchar2).Value = file.FileType;
                    command.Parameters.Add("p_file_extension", OracleDbType.Varchar2).Value = file.FileExtension;
                    command.Parameters.Add("p_file_content", OracleDbType.Blob).Value = file.FileContent;
                    command.Parameters.Add("p_operation_performed", OracleDbType.Varchar2).Value = "insert";

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine($"Файл '{file.FileName}' успешно добавлен в базу данных.");
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine($"Ошибка при добавлении файла: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public async Task UpdateFileAsync(FileStorage file)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("file_storage_pkg.update_file", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Добавляем параметры
                    command.Parameters.Add("p_file_id", OracleDbType.Int32).Value = file.FileId;
                    command.Parameters.Add("p_file_name", OracleDbType.Varchar2).Value = file.FileName;
                    command.Parameters.Add("p_file_type", OracleDbType.Varchar2).Value = file.FileType;
                    command.Parameters.Add("p_file_extension", OracleDbType.Varchar2).Value = file.FileExtension;
                    command.Parameters.Add("p_file_content", OracleDbType.Blob).Value = file.FileContent;
                    command.Parameters.Add("p_operation_performed", OracleDbType.Varchar2).Value = "update";

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine($"Файл с ID {file.FileId} успешно обновлён.");
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine($"Ошибка при обновлении файла: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public async Task DeleteFileAsync(int fileId)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("file_storage_pkg.delete_file", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Добавляем параметр
                    command.Parameters.Add("p_file_id", OracleDbType.Int32).Value = fileId;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine($"Файл с ID {fileId} успешно удалён.");
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine($"Ошибка при удалении файла: {ex.Message}");
                        throw;
                    }
                }
            }
        }


        public async Task<FileStorage> GetFileAsync(int fileId)
        {
            FileStorage file = null;

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("file_storage_pkg.get_file", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Добавляем параметры
                    command.Parameters.Add("p_file_id", OracleDbType.Int32).Value = fileId;
                    command.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    try
                    {
                        await command.ExecuteNonQueryAsync();

                        var refCursor = (OracleRefCursor)command.Parameters["p_cursor"].Value;
                        using (var reader = refCursor.GetDataReader())
                        {
                            if (await reader.ReadAsync())
                            {
                                file = new FileStorage
                                {
                                    FileId = reader.GetInt32(reader.GetOrdinal("file_id")),
                                    FileName = reader.GetString(reader.GetOrdinal("file_name")),
                                    FileType = reader.GetString(reader.GetOrdinal("file_type")),
                                    FileExtension = reader.GetString(reader.GetOrdinal("file_extension")),
                                    UploadDate = reader.GetDateTime(reader.GetOrdinal("upload_date")),
                                    ModificationDate = reader.GetDateTime(reader.GetOrdinal("modification_date")),
                                    OperationPerformed = reader.GetString(reader.GetOrdinal("operation_performed")),
                                    FileContent = reader["file_content"] as byte[] ?? null
                                };
                                Console.WriteLine($"Файл '{file.FileName}' успешно получен.");
                            }
                            else
                            {
                                Console.WriteLine($"Файл с ID {fileId} не найден.");
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine($"Ошибка при получении файла: {ex.Message}");
                        throw;
                    }
                }
            }

            return file;
        }




    }
}
