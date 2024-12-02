using Auto_Service_Application_university_project.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Data
{
    public class ReservationRepository
    {

        private readonly string connectionString = "User Id=st67280;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)))";

        public async Task InsertReservationAsync(Reservation reservation)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("reservation_pkg.insert_reservation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Входные параметры
                    command.Parameters.Add("p_date_reservace", OracleDbType.Date).Value = reservation.DateReservace;
                    command.Parameters.Add("p_office_office_id", OracleDbType.Int32).Value = reservation.Office.OfficeId;
                    command.Parameters.Add("p_client_client_id", OracleDbType.Int32).Value = reservation.Client.ClientId;

                    try
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при вставке резервации: {ex.Message}", ex);
                    }
                }
            }
        }

        public async Task<ObservableCollection<Reservation>> GetAllReservationsAsync()
        {
            var reservations = new ObservableCollection<Reservation>();

            using (var connection = new OracleConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("reservation_pkg.get_all_reservations", connection))
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
                                var reservation = new Reservation
                                {
                                    ReservationId = reader.GetInt32(reader.GetOrdinal("reservation_id")),
                                    DateReservace = reader.GetDateTime(reader.GetOrdinal("date_reservace")),
                                    Client = new Client
                                    {
                                        ClientId = reader.GetInt32(reader.GetOrdinal("client_id")),
                                        ClientName = reader.GetString(reader.GetOrdinal("name_customer"))
                                    },
                                    Office = new Office
                                    {
                                        OfficeId = reader.GetInt32(reader.GetOrdinal("office_id")),
                                        Address = new Address
                                        {
                                            Country = reader.GetString(reader.GetOrdinal("country")),
                                            City = reader.GetString(reader.GetOrdinal("city")),
                                            Street = reader.GetString(reader.GetOrdinal("street")),
                                            HouseNumber = reader.GetInt32(reader.GetOrdinal("house_number")),
                                            IndexAdd = reader.GetInt32(reader.GetOrdinal("index_add"))
                                        }
                                    }
                                };
                                reservations.Add(reservation);
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        throw new ApplicationException($"Ошибка при получении списка резерваций: {ex.Message}", ex);
                    }
                }
            }

            return reservations;
        }
    }
}
