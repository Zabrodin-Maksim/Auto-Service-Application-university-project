using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Services;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Data
{
    public class UserRepository
    {
        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";

        private readonly PasswordHasher _passwordHasher = new PasswordHasher();

        public async Task AddUser(User newUser)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("users_pkg.insert_user", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_username", OracleDbType.Varchar2).Value = newUser.Username;
                    command.Parameters.Add("p_password", OracleDbType.Varchar2).Value = newUser.Password;
                    command.Parameters.Add("p_name", OracleDbType.Varchar2).Value = newUser.Name;
                    command.Parameters.Add("p_phone", OracleDbType.Int64).Value = newUser.Phone;
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

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("users_pkg.authenticate_user", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;
                    command.Parameters.Add("p_password", OracleDbType.Varchar2).Value = password;

                    // Выходные параметры
                    var userIdParam = new OracleParameter("p_user_id", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(userIdParam);

                    var nameParam = new OracleParameter("p_name", OracleDbType.Varchar2, 40)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(nameParam);


                    try
                    {
                        await command.ExecuteNonQueryAsync();

                        // Получение выходных параметров
                        int userId = Convert.ToInt32(userIdParam.Value.ToString());
                        string name = nameParam.Value.ToString();


                        return new User
                        {
                            UserId = userId,
                            Username = username,
                            Name = name,
                            Password = password,
                            // Дополнительно можно загрузить Address, если необходимо
                        };
                    }
                    catch (OracleException ex)
                    {
                        // Обработка ошибок, например, неверный логин/пароль
                        throw new ApplicationException($"Authentication failed: {ex.Message}", ex);
                    }
                }
            }
}
