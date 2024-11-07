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
    public class EmployerRepository : IEmployerRepository
    {
        private readonly string _connectionString;

        public EmployerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Employer> GetAll()
        {
            var employers = new List<Employer>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT name_employee, speciality, phone, office_office_id, employer_employer_id, address_address_id FROM employer";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var employer = new Employer
                            {
                                NameEmployee = reader["name_employee"].ToString(),
                                Speciality = reader["speciality"].ToString(),
                                Phone = reader["phone"].ToString(),
                                OfficeOfficeId = Convert.ToInt32(reader["office_office_id"]),
                                EmployerEmployerId = reader["employer_employer_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["employer_employer_id"]),
                                AddressAddressId = Convert.ToInt32(reader["address_address_id"])
                            };
                            employers.Add(employer);
                        }
                    }
                }
            }

            return employers;
        }

        public Employer GetByPhone(string phone)
        {
            Employer employer = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT name_employee, speciality, phone, office_office_id, employer_employer_id, address_address_id FROM employer WHERE phone = :Phone";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("Phone", OracleDbType.Varchar2).Value = phone;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employer = new Employer
                            {
                                NameEmployee = reader["name_employee"].ToString(),
                                Speciality = reader["speciality"].ToString(),
                                Phone = reader["phone"].ToString(),
                                OfficeOfficeId = Convert.ToInt32(reader["office_office_id"]),
                                EmployerEmployerId = reader["employer_employer_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["employer_employer_id"]),
                                AddressAddressId = Convert.ToInt32(reader["address_address_id"])
                            };
                        }
                    }
                }
            }

            return employer;
        }

        public void Add(Employer employer, User currentUser)
        {
            // Проверка прав доступа
            if (!currentUser.HasPermission("AddEmployer"))
                throw new UnauthorizedAccessException("У вас нет прав для добавления сотрудника.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Вызов хранимой процедуры для вставки данных
                    using (var command = new OracleCommand("insert_employer", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_name_employee", OracleDbType.Varchar2).Value = employer.NameEmployee;
                        command.Parameters.Add("p_speciality", OracleDbType.Varchar2).Value = employer.Speciality;
                        command.Parameters.Add("p_phone", OracleDbType.Varchar2).Value = employer.Phone;
                        command.Parameters.Add("p_office_office_id", OracleDbType.Int32).Value = employer.OfficeOfficeId;
                        command.Parameters.Add("p_employer_employer_id", OracleDbType.Int32).Value = employer.EmployerEmployerId.HasValue ? (object)employer.EmployerEmployerId.Value : DBNull.Value;
                        command.Parameters.Add("p_address_address_id", OracleDbType.Int32).Value = employer.AddressAddressId;

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

        public void Update(Employer employer, User currentUser)
        {
            // Проверка прав доступа
            if (!currentUser.HasPermission("UpdateEmployer"))
                throw new UnauthorizedAccessException("У вас нет прав для обновления сотрудника.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Получаем employer_id по телефону
                    int employerId = GetEmployerIdByPhone(employer.Phone, connection);

                    if (employerId == 0)
                        throw new Exception("Сотрудник не найден.");

                    // Вызов хранимой процедуры для обновления данных
                    using (var command = new OracleCommand("update_employer", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_employer_id", OracleDbType.Int32).Value = employerId;
                        command.Parameters.Add("p_name_employee", OracleDbType.Varchar2).Value = employer.NameEmployee;
                        command.Parameters.Add("p_speciality", OracleDbType.Varchar2).Value = employer.Speciality;
                        command.Parameters.Add("p_phone", OracleDbType.Varchar2).Value = employer.Phone;
                        command.Parameters.Add("p_office_office_id", OracleDbType.Int32).Value = employer.OfficeOfficeId;
                        command.Parameters.Add("p_employer_employer_id", OracleDbType.Int32).Value = employer.EmployerEmployerId.HasValue ? (object)employer.EmployerEmployerId.Value : DBNull.Value;
                        command.Parameters.Add("p_address_address_id", OracleDbType.Int32).Value = employer.AddressAddressId;

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

        public void Delete(string phone, User currentUser)
        {
            // Проверка прав доступа
            if (!currentUser.HasPermission("DeleteEmployer"))
                throw new UnauthorizedAccessException("У вас нет прав для удаления сотрудника.");

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Получаем employer_id по телефону
                    int employerId = GetEmployerIdByPhone(phone, connection);

                    if (employerId == 0)
                        throw new Exception("Сотрудник не найден.");

                    // Вызов хранимой процедуры для удаления данных
                    using (var command = new OracleCommand("delete_employer", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("p_employer_id", OracleDbType.Int32).Value = employerId;

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

        private int GetEmployerIdByPhone(string phone, OracleConnection connection)
        {
            string query = "SELECT employer_id FROM employer WHERE phone = :Phone";
            using (var command = new OracleCommand(query, connection))
            {
                command.Parameters.Add("Phone", OracleDbType.Varchar2).Value = phone;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader["employer_id"]);
                    }
                }
            }
            return 0;
        }
    }
}
