using BDAS2_University_Project.Models;
using BDAS2_University_Project.Repositories.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User GetByUsername(string username)
        {
            User user = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT user_id, username, password_hash, role, is_active FROM users WHERE username = :Username";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("Username", OracleDbType.Varchar2).Value = username;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                UserId = Convert.ToInt32(reader["user_id"]),
                                Username = reader["username"].ToString(),
                                PasswordHash = reader["password_hash"].ToString(),
                                Role = reader["role"].ToString(),
                                IsActive = reader["is_active"].ToString() == "Y"
                            };
                        }
                    }
                }
            }

            return user;
        }

        public void Add(User user)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                using (var command = new OracleCommand("INSERT INTO users (user_id, username, password_hash, role, is_active) VALUES (users_user_id_seq.NEXTVAL, :Username, :PasswordHash, :Role, :IsActive)", connection))
                {
                    command.Parameters.Add("Username", OracleDbType.Varchar2).Value = user.Username;
                    command.Parameters.Add("PasswordHash", OracleDbType.Varchar2).Value = user.PasswordHash;
                    command.Parameters.Add("Role", OracleDbType.Varchar2).Value = user.Role;
                    command.Parameters.Add("IsActive", OracleDbType.Char).Value = user.IsActive ? 'Y' : 'N';

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(User user)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                using (var command = new OracleCommand("UPDATE users SET password_hash = :PasswordHash, role = :Role, is_active = :IsActive WHERE username = :Username", connection))
                {
                    command.Parameters.Add("PasswordHash", OracleDbType.Varchar2).Value = user.PasswordHash;
                    command.Parameters.Add("Role", OracleDbType.Varchar2).Value = user.Role;
                    command.Parameters.Add("IsActive", OracleDbType.Char).Value = user.IsActive ? 'Y' : 'N';
                    command.Parameters.Add("Username", OracleDbType.Varchar2).Value = user.Username;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(string username)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                using (var command = new OracleCommand("DELETE FROM users WHERE username = :Username", connection))
                {
                    command.Parameters.Add("Username", OracleDbType.Varchar2).Value = username;

                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<User> GetAll()
        {
            var users = new List<User>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT user_id, username, password_hash, role, is_active FROM users";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                UserId = Convert.ToInt32(reader["user_id"]),
                                Username = reader["username"].ToString(),
                                PasswordHash = reader["password_hash"].ToString(),
                                Role = reader["role"].ToString(),
                                IsActive = reader["is_active"].ToString() == "Y"
                            };
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }
    }
}
