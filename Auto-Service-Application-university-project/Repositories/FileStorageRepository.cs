using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Repositories.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories
{
    public class FileStorageRepository : IFileStorageRepository
    {
        private readonly string _connectionString;

        public FileStorageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<FileStorage> GetAllFiles()
        {
            var files = new List<FileStorage>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT file_name, file_type, file_extension, upload_date, modification_date, operation_performed FROM file_storage";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var file = new FileStorage
                            {
                                FileName = reader["file_name"].ToString(),
                                FileType = reader["file_type"].ToString(),
                                FileExtension = reader["file_extension"].ToString(),
                                UploadDate = Convert.ToDateTime(reader["upload_date"]),
                                ModificationDate = Convert.ToDateTime(reader["modification_date"]),
                                OperationPerformed = reader["operation_performed"].ToString()
                            };
                            files.Add(file);
                        }
                    }
                }
            }

            return files;
        }

        public FileStorage GetFileByName(string fileName)
        {
            FileStorage file = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM file_storage WHERE file_name = :FileName";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("FileName", OracleDbType.Varchar2).Value = fileName;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            file = new FileStorage
                            {
                                FileName = reader["file_name"].ToString(),
                                FileType = reader["file_type"].ToString(),
                                FileExtension = reader["file_extension"].ToString(),
                                UploadDate = Convert.ToDateTime(reader["upload_date"]),
                                ModificationDate = Convert.ToDateTime(reader["modification_date"]),
                                OperationPerformed = reader["operation_performed"].ToString(),
                                FileContent = reader["file_content"] as byte[]
                            };
                        }
                    }
                }
            }

            return file;
        }

        public void AddFile(FileStorage file, User currentUser)
        {
            // Проверка прав доступа
            if (!currentUser.HasPermission("AddFile"))
                throw new UnauthorizedAccessException("У вас нет прав для добавления файлов.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();

                using (var command = new OracleCommand("insert_file", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("p_file_name", OracleDbType.Varchar2).Value = file.FileName;
                    command.Parameters.Add("p_file_type", OracleDbType.Varchar2).Value = file.FileType;
                    command.Parameters.Add("p_file_extension", OracleDbType.Varchar2).Value = file.FileExtension;
                    command.Parameters.Add("p_file_content", OracleDbType.Blob).Value = file.FileContent;
                    command.Parameters.Add("p_operation_performed", OracleDbType.Varchar2).Value = "Добавление";

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateFile(FileStorage file, User currentUser)
        {
            // Проверка прав доступа
            if (!currentUser.HasPermission("UpdateFile"))
                throw new UnauthorizedAccessException("У вас нет прав для обновления файлов.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();

                string query = @"UPDATE file_storage 
                                 SET file_type = :FileType, file_extension = :FileExtension, 
                                     modification_date = SYSDATE, operation_performed = :OperationPerformed, file_content = :FileContent
                                 WHERE file_name = :FileName";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("FileType", OracleDbType.Varchar2).Value = file.FileType;
                    command.Parameters.Add("FileExtension", OracleDbType.Varchar2).Value = file.FileExtension;
                    command.Parameters.Add("OperationPerformed", OracleDbType.Varchar2).Value = "Обновление";
                    command.Parameters.Add("FileContent", OracleDbType.Blob).Value = file.FileContent;
                    command.Parameters.Add("FileName", OracleDbType.Varchar2).Value = file.FileName;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteFile(string fileName, User currentUser)
        {
            // Проверка прав доступа
            if (!currentUser.HasPermission("DeleteFile"))
                throw new UnauthorizedAccessException("У вас нет прав для удаления файлов.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();

                string query = "DELETE FROM file_storage WHERE file_name = :FileName";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("FileName", OracleDbType.Varchar2).Value = fileName;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
