using Data.Database_layer;
using Data.Model_layer;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Model.Model_layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Model_layer.Booking;

namespace DataAccess.Database_layer
{
    public class TimeslotDatabaseAccess : ITimeslotDatabaseAccess
    {
        private readonly string _connectionString;

        public TimeslotDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("WeDoLaundry");
        }

        public TimeslotDatabaseAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool DecreaseAvailability(int id)
        {
            int numberOfRowsModified = 0;
            string SQL_string = "UPDATE TimeSlots SET [availability] = [availability] - 1 WHERE id = @Id";
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                SqlParameter slotParam = new("@Id", id);
                command.Parameters.Add(slotParam);

                con.Open();

                numberOfRowsModified = command.ExecuteNonQuery();

                con.Close();
            }

            return (numberOfRowsModified > 0);
        }

        public List<TimeSlot> GetAll()
        {
            List<TimeSlot>? returnList = new();

            string SQL_string = "SELECT * FROM TimeSlots";
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                returnList = new();
                while (reader.Read())
                {
                    returnList.Add(GetTimeslotReader(reader));
                }
                con.Close();
            }
            return returnList;
        }

        public TimeSlot Get(int id)
        {
            TimeSlot timeslot = new();

            string SQL_string = "SELECT * from TimeSlots WHERE id = @Id";
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                SqlParameter slotParam = new("@Id", id);
                command.Parameters.Add(slotParam);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    timeslot = GetTimeslotReader(reader);
                }
                con.Close();
            }
            return timeslot;
        }

        public int Create(TimeSlot timeslot)
        {
            int insertedId = -1;

            string insertString = "INSERT INTO TimeSlots([date], slot, availability) OUTPUT INSERTED.ID VALUES(@Date, @Slot, @Availability)";

            using (SqlConnection con = new(_connectionString))
            using (SqlCommand cmd = new(insertString, con))
            {

                SqlParameter dateParam = new("@Date", timeslot.Date);
                cmd.Parameters.Add(dateParam);
                SqlParameter slotParam = new("@Slot", timeslot.Slot);
                cmd.Parameters.Add(slotParam);
                SqlParameter availabilityParam = new("@Availability", timeslot.Availability);
                cmd.Parameters.Add(availabilityParam);

                con.Open();
                insertedId = (int) cmd.ExecuteScalar();

                con.Close();
            }
            return insertedId;
        }

        public bool Delete(int id)
        {
            int numberOfRowsModified = 0;

            string deleteSqlString = "DELETE FROM TimeSlots WHERE id = @Id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand command = new(deleteSqlString, conn))
            {
                SqlParameter idParam = new("@Id", id);
                command.Parameters.Add(idParam);

                if (conn != null)
                {
                    conn.Open();
                    numberOfRowsModified = command.ExecuteNonQuery();
                }
            }
            return (numberOfRowsModified > 0);
        }
        public TimeSlot? GetByDateAndSlot(DateTime date, string slot)
        {
            TimeSlot timeslot = new();

            string SQL_string = "SELECT * from TimeSlots WHERE date = @date and slot = @slot";
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                SqlParameter dateParam = new("@date", date);
                command.Parameters.Add(dateParam);
                SqlParameter slotParam = new("@slot", slot);
                command.Parameters.Add(slotParam);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    timeslot = GetTimeslotReader(reader);
                }
                con.Close();
            }
            return timeslot;
        }

        private TimeSlot GetTimeslotReader(SqlDataReader reader)
        {
            TimeSlot returnTimeslot;
            int id = reader.GetInt32(reader.GetOrdinal("id"));
            DateTime date = reader.GetDateTime(reader.GetOrdinal("date"));
            string slot = reader.GetString(reader.GetOrdinal("slot"));
            int availability = reader.GetInt32(reader.GetOrdinal("availability"));

            returnTimeslot = new(id, date, slot, availability);

            return returnTimeslot;
        }

    }
}
