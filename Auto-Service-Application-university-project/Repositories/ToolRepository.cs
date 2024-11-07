using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Repositories.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Repositories
{
    public class ToolRepository : IToolRepository
    {
        private readonly string _connectionString;

        public ToolRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Tool> GetAll()
        {
            var tools = new List<Tool>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT TOOL_ID, SPECIALITY, PRICE, CHECK_DATE, OFFICE_OFFICE_ID FROM TOOL";
                using (var command = new OracleCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var tool = new Tool
                            {
                                ToolId = Convert.ToInt32(reader["TOOL_ID"]),
                                Speciality = reader["SPECIALITY"].ToString(),
                                Price = Convert.ToDecimal(reader["PRICE"]),
                                CheckDate = Convert.ToDateTime(reader["CHECK_DATE"]),
                                OfficeOfficeId = Convert.ToInt32(reader["OFFICE_OFFICE_ID"])
                            };
                            tools.Add(tool);
                        }
                    }
                }
            }

            return tools;
        }

        public Tool GetById(int id)
        {
            Tool tool = null;

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT TOOL_ID, SPECIALITY, PRICE, CHECK_DATE, OFFICE_OFFICE_ID FROM TOOL WHERE TOOL_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tool = new Tool
                            {
                                ToolId = Convert.ToInt32(reader["TOOL_ID"]),
                                Speciality = reader["SPECIALITY"].ToString(),
                                Price = Convert.ToDecimal(reader["PRICE"]),
                                CheckDate = Convert.ToDateTime(reader["CHECK_DATE"]),
                                OfficeOfficeId = Convert.ToInt32(reader["OFFICE_OFFICE_ID"])
                            };
                        }
                    }
                }
            }

            return tool;
        }

        public void Add(Tool tool)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO TOOL (SPECIALITY, PRICE, CHECK_DATE, OFFICE_OFFICE_ID)
                                 VALUES (:Speciality, :Price, :CheckDate, :OfficeOfficeId)";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Speciality", tool.Speciality));
                    command.Parameters.Add(new OracleParameter("Price", tool.Price));
                    command.Parameters.Add(new OracleParameter("CheckDate", tool.CheckDate));
                    command.Parameters.Add(new OracleParameter("OfficeOfficeId", tool.OfficeOfficeId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Tool tool)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE TOOL SET SPECIALITY = :Speciality, PRICE = :Price, 
                                 CHECK_DATE = :CheckDate, OFFICE_OFFICE_ID = :OfficeOfficeId
                                 WHERE TOOL_ID = :ToolId";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Speciality", tool.Speciality));
                    command.Parameters.Add(new OracleParameter("Price", tool.Price));
                    command.Parameters.Add(new OracleParameter("CheckDate", tool.CheckDate));
                    command.Parameters.Add(new OracleParameter("OfficeOfficeId", tool.OfficeOfficeId));
                    command.Parameters.Add(new OracleParameter("ToolId", tool.ToolId));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM TOOL WHERE TOOL_ID = :Id";
                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));
                    command.ExecuteNonQuery();
                }
            }
        }
    }

}
