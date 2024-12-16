using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Services;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Auto_Service_Application_university_project.Data
{
    public class UserRepository
    {
        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";

        private OfficeRepository _officeRepository = new OfficeRepository();

        private readonly PasswordHasher _passwordHasher = new PasswordHasher();

        public async Task AddUser(User newUser)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("users_pkg.insert_user", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    string hashedPassword = _passwordHasher.HashPassword(newUser.Password);

                    // Входные параметры
                    command.Parameters.Add("p_username", OracleDbType.Varchar2).Value = newUser.Username;
                    command.Parameters.Add("p_password", OracleDbType.Varchar2).Value = hashedPassword;
                    command.Parameters.Add("p_name", OracleDbType.Varchar2).Value = newUser.Name;
                    command.Parameters.Add("p_phone", OracleDbType.Int32).Value = newUser.Phone;
                    command.Parameters.Add("p_country", OracleDbType.Varchar2).Value = newUser.Address.Country;
                    command.Parameters.Add("p_city", OracleDbType.Varchar2).Value = newUser.Address.City;
                    command.Parameters.Add("p_index_add", OracleDbType.Int32).Value = newUser.Address.IndexAdd;
                    command.Parameters.Add("p_street", OracleDbType.Varchar2).Value = newUser.Address.Street;
                    command.Parameters.Add("p_house_number", OracleDbType.Int32).Value = newUser.Address.HouseNumber;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        // Обработка ошибок, связанных с хранимыми процедурами
                        throw new ApplicationException($"Error inserting user: {ex.Message}", ex);
                    }
                }

            }
        }

        public async Task<ObservableCollection<User>> GetAllUsersAsync()
        {
            var users = new ObservableCollection<User>();

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("users_pkg.get_all_users", connection))
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
                                var user = new User
                                {
                                    UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                                    Username = reader.GetString(reader.GetOrdinal("username")),
                                    Password = reader.GetString(reader.GetOrdinal("password")),
                                    Name = reader.GetString(reader.GetOrdinal("name")),
                                    Phone = reader.GetInt32(reader.GetOrdinal("phone")),
                                    RoleId = reader.GetInt32(reader.GetOrdinal("role_id")),
                                    Address = new Address
                                    {
                                        Country = reader.GetString(reader.GetOrdinal("country")),
                                        City = reader.GetString(reader.GetOrdinal("city")),
                                        Street = reader.GetString(reader.GetOrdinal("street")),
                                        HouseNumber = reader.GetInt32(reader.GetOrdinal("house_number")),
                                        IndexAdd = reader.GetInt32(reader.GetOrdinal("index_add"))
                                    }
                                };
                                users.Add(user);
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении пользователей: {ex.Message}", ex);
                    }
                }
            }

            return users;
        }

        public async Task<User> AuthenticateUserAsync(string username, string userPassword)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("users_pkg.authenticate_user", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;

                    // Выходные параметры
                    var usernameOutParam = new OracleParameter("p_username_out", OracleDbType.Varchar2, 30)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(usernameOutParam);

                    var passwordOutParam = new OracleParameter("p_password", OracleDbType.Varchar2, 100)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(passwordOutParam);

                    var userIdOutParam = new OracleParameter("p_user_id", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(userIdOutParam);

                    var nameOutParam = new OracleParameter("p_name", OracleDbType.Varchar2, 40)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(nameOutParam);

                    var roleIdParam = new OracleParameter("p_role_id", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(roleIdParam);

                    var phoneOutParam = new OracleParameter("p_phone", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(phoneOutParam);

                    try
                    {
                        await command.ExecuteNonQueryAsync();

                        // Получение выходных параметров
                        string usernameOut = usernameOutParam.Value.ToString();
                        string password = passwordOutParam.Value.ToString();
                        int userId = Convert.ToInt32(userIdOutParam.Value.ToString());
                        string name = nameOutParam.Value.ToString();
                        int roleId = Convert.ToInt32(roleIdParam.Value.ToString());
                        int phone = Convert.ToInt32(phoneOutParam.Value.ToString());

                        if (_passwordHasher.VerifyPassword(userPassword, password))
                        {
                            return new User
                            {
                                UserId = userId,
                                Username = usernameOut,
                                Password = password,
                                Name = name,
                                RoleId = roleId,
                                Phone = phone
                                // Дополнительно можно загрузить Address, если необходимо
                            };
                        }else
                        {
                            return null;
                        }

                        
                    }
                    catch (OracleException ex)
                    {
                        // Обработка ошибок, например, неверный логин
                        throw new ApplicationException($"Ошибка при аутентификации: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("users_pkg.update_user", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    string hashedPassword = _passwordHasher.HashPassword(user.Password);

                    // Входные параметры
                    command.Parameters.Add("p_user_id", OracleDbType.Int32).Value = user.UserId;
                    command.Parameters.Add("p_username", OracleDbType.Varchar2).Value = user.Username;
                    command.Parameters.Add("p_password", OracleDbType.Varchar2).Value = hashedPassword; // Отправляем хэш
                    command.Parameters.Add("p_name", OracleDbType.Varchar2).Value = user.Name;
                    command.Parameters.Add("p_phone", OracleDbType.Int32).Value = user.Phone;
                    command.Parameters.Add("p_country", OracleDbType.Varchar2).Value = user.Address.Country;
                    command.Parameters.Add("p_city", OracleDbType.Varchar2).Value = user.Address.City;
                    command.Parameters.Add("p_index_add", OracleDbType.Int32).Value = user.Address.IndexAdd;
                    command.Parameters.Add("p_street", OracleDbType.Varchar2).Value = user.Address.Street;
                    command.Parameters.Add("p_house_number", OracleDbType.Int32).Value = user.Address.HouseNumber;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при обновлении пользователя: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("users_pkg.delete_user", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_user_id", OracleDbType.Int32).Value = userId;

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
                            throw new ApplicationException($"Ошибка при удалении пользователя: {ex.Message}", ex);
                        }
                    }
                }
            }
        }

        public async Task DeleteEmployerAsync(int employerId)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new OracleCommand("employer_pkg.delete_employer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("p_employer_id", OracleDbType.Int32).Value = employerId;

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
                            throw new ApplicationException($"Ошибка при удалении Employer: {ex.Message}", ex);
                        }
                    }
                }
            }
        }

        public async Task<ObservableCollection<Employer>> GetAllEmployersAsync()
        {
            var employers = new ObservableCollection<Employer>();

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new OracleCommand("employer_pkg.get_all_employers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    var cursorParam = new OracleParameter("p_cursor", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(cursorParam);

                    try
                    {
                        // Выполняем команду
                        await command.ExecuteNonQueryAsync();

                        // Извлекаем REF CURSOR из выходного параметра
                        var refCursor = (OracleRefCursor)cursorParam.Value;
                        using (var reader = refCursor.GetDataReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                var emp = new Employer
                                {
                                    EmployerId = reader.GetInt32(reader.GetOrdinal("employer_id")),
                                    Speciality = reader.IsDBNull(reader.GetOrdinal("speciality")) ? null : reader.GetString(reader.GetOrdinal("speciality")),
                                    NameEmployee = reader.IsDBNull(reader.GetOrdinal("name_employee")) ? null : reader.GetString(reader.GetOrdinal("name_employee")),
                                    
                                    Phone = reader.IsDBNull(reader.GetOrdinal("phone")) ? 0 : reader.GetInt32(reader.GetOrdinal("phone")),
                                    Office = new Office
                                    {
                                        OfficeId = reader.IsDBNull(reader.GetOrdinal("office_office_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("office_office_id"))
                                        // Дополнительно можно загрузить данные офиса при необходимости
                                    },
                                    Address = new Address
                                    {
                                        AddressId = reader.IsDBNull(reader.GetOrdinal("address_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("address_id")),
                                        Country = reader.IsDBNull(reader.GetOrdinal("country")) ? null : reader.GetString(reader.GetOrdinal("country")),
                                        City = reader.IsDBNull(reader.GetOrdinal("city")) ? null : reader.GetString(reader.GetOrdinal("city")),
                                        Street = reader.IsDBNull(reader.GetOrdinal("street")) ? null : reader.GetString(reader.GetOrdinal("street")),
                                        HouseNumber = reader.IsDBNull(reader.GetOrdinal("house_number")) ? 0 : reader.GetInt32(reader.GetOrdinal("house_number")),
                                        IndexAdd = reader.IsDBNull(reader.GetOrdinal("index_add")) ? 0 : reader.GetInt32(reader.GetOrdinal("index_add"))
                                    }
                                };

                                if (!reader.IsDBNull(reader.GetOrdinal("employer_employer_id")))
                                {
                                    var supervisorId = reader.GetInt32(reader.GetOrdinal("employer_employer_id"));
                                    if (supervisorId != 0) // Если 0 не валидный ID
                                    {
                                        emp.Supervisor = new Employer { EmployerId = supervisorId };
                                    }
                                    else
                                    {
                                        emp.Supervisor = null;
                                    }
                                }
                                else
                                {
                                    emp.Supervisor = null;
                                }

                                employers.Add(emp);
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении списка Employer: {ex.Message}", ex);
                    }
                }
            }

            return employers;
        }

        public async Task UpdateEmployerAsync(Employer employer)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new OracleCommand("employer_pkg.update_employer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("p_employer_id", OracleDbType.Int32).Value = employer.EmployerId;
                    command.Parameters.Add("p_name_employee", OracleDbType.Varchar2).Value = employer.NameEmployee;
                    command.Parameters.Add("p_speciality", OracleDbType.Varchar2).Value = employer.Speciality;
                    command.Parameters.Add("p_phone", OracleDbType.Int32).Value = employer.Phone;
                    command.Parameters.Add("p_office_id", OracleDbType.Int32).Value = employer.Office.OfficeId;
                    command.Parameters.Add("p_employer_employer_id", OracleDbType.Int32).Value =
                        (employer.Supervisor != null) ? (object)employer.Supervisor.EmployerId : DBNull.Value;
                    command.Parameters.Add("p_address_id", OracleDbType.Int32).Value = employer.Address.AddressId;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при обновлении Employer: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task AddEmployerAsync(Employer employer)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new OracleCommand("employer_pkg.insert_employer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("p_name_employee", OracleDbType.Varchar2).Value = employer.NameEmployee;
                    command.Parameters.Add("p_speciality", OracleDbType.Varchar2).Value = employer.Speciality;
                    command.Parameters.Add("p_phone", OracleDbType.Int32).Value = employer.Phone;
                    command.Parameters.Add("p_office_id", OracleDbType.Int32).Value = employer.Office.OfficeId;
                    command.Parameters.Add("p_employer_employer_id", OracleDbType.Int32).Value =
                        (employer.Supervisor != null) ? (object)employer.Supervisor.EmployerId : DBNull.Value;
                    command.Parameters.Add("p_address_id", OracleDbType.Int32).Value = employer.Address.AddressId;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при вставке Employer: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task AssignRoleAsync(int userId, int roleId)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("users_pkg.assign_role", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_user_id", OracleDbType.Int32).Value = userId;
                    command.Parameters.Add("p_role_id", OracleDbType.Int32).Value = roleId;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при назначении роли: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task InsertEmployerAsync(int userId, int officeId, string speciality)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("users_pkg.insert_employer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_user_id", OracleDbType.Int32).Value = userId;
                    command.Parameters.Add("p_office_id", OracleDbType.Int32).Value = officeId;
                    command.Parameters.Add("p_speciality", OracleDbType.Varchar2).Value = speciality;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при вставке работодателя: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task<Employer> GetEmployerByPhoneAsync(long phone)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("users_pkg.get_employer_by_phone", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_phone", OracleDbType.Int32).Value = phone;

                    // Выходные параметры
                    var cursorParam = new OracleParameter("p_cursor", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(cursorParam);

                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var employer = new Employer
                                {
                                    EmployerId = reader.GetInt32(reader.GetOrdinal("employer_id")),
                                    Speciality = reader.IsDBNull(reader.GetOrdinal("speciality")) ? null : reader.GetString(reader.GetOrdinal("speciality")),
                                    NameEmployee = reader.GetString(reader.GetOrdinal("name_employee")),
                                    Phone = reader.GetInt32(reader.GetOrdinal("phone")),
                                    Office = await _officeRepository.GetOfficeAsync( reader.GetInt32(reader.GetOrdinal("office_office_id"))),
                                    // EmployerEmployerId убран
                                    Address = new Address
                                    {
                                        AddressId = reader.GetInt32(reader.GetOrdinal("address_address_id")),
                                        Country = reader.IsDBNull(reader.GetOrdinal("country")) ? null : reader.GetString(reader.GetOrdinal("country")),
                                        City = reader.IsDBNull(reader.GetOrdinal("city")) ? null : reader.GetString(reader.GetOrdinal("city")),
                                        IndexAdd = reader.GetInt32(reader.GetOrdinal("index_add")),
                                        Street = reader.IsDBNull(reader.GetOrdinal("street")) ? null : reader.GetString(reader.GetOrdinal("street")),
                                        HouseNumber = reader.GetInt32(reader.GetOrdinal("house_number"))
                                    }
                                };
                                return employer;
                            }
                            else
                            {
                                Debug.WriteLine($"[INFO] Employer not aut");
                                // Если работодатель с таким телефоном не найден
                                return null;
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении данных работодателя: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task<Employer> GetEmployerAsync(int employerId)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("employer_pkg.get_employer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_employer_id", OracleDbType.Int32).Value = employerId;

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
                                return new Employer
                                {
                                    EmployerId = reader.GetInt32(reader.GetOrdinal("employer_id")),
                                    NameEmployee = reader.GetString(reader.GetOrdinal("name_employee")),
                                    Speciality = reader.IsDBNull(reader.GetOrdinal("speciality")) ? null : reader.GetString(reader.GetOrdinal("speciality")),
                                    Phone = reader.GetInt32(reader.GetOrdinal("phone")),
                                    Office = new Office
                                    {
                                        OfficeId = reader.GetInt32(reader.GetOrdinal("office_office_id"))
                                        // Дополнительно можно загрузить данные офиса при необходимости
                                    },
                                    Supervisor = new Employer
                                    {
                                        EmployerId = reader.IsDBNull(reader.GetOrdinal("employer_employer_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("employer_employer_id"))
                                        // Дополнительно можно загрузить данные руководителя при необходимости
                                    },
                                    Address = new Address
                                    {
                                        Country = reader.GetString(reader.GetOrdinal("country")),
                                        City = reader.GetString(reader.GetOrdinal("city")),
                                        Street = reader.GetString(reader.GetOrdinal("street")),
                                        HouseNumber = reader.GetInt32(reader.GetOrdinal("house_number")),
                                        IndexAdd = reader.GetInt32(reader.GetOrdinal("index_add"))
                                    }
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении данных работника: {ex.Message}", ex);
                    }
                }
            }
        }
    }
}
