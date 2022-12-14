using Data.Database_layer;
using Data.Model_layer;
using DataAccess.Exceptions;
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

        // true = increase
        // false = decrease
        public bool ModifyAvailability(int id, bool mode, int value)
        {
            int numberOfRowsModified = 0;
            string SQL_string;
            if (mode) // increase
            {
                SQL_string = "UPDATE TimeSlots SET [availability] = [availability] + @value WHERE id = @Id";
            }
            else // decrease
            {
                SQL_string = "UPDATE TimeSlots SET [availability] = [availability] - @value WHERE id = @Id AND availability > 0";
            }
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                SqlParameter slotParam = new("@Id", id);
                command.Parameters.Add(slotParam);
                SqlParameter valueParam = new("@value", value);
                command.Parameters.Add(valueParam);

                con.Open();

                numberOfRowsModified = command.ExecuteNonQuery();

                con.Close();
            }
            if (numberOfRowsModified > 0)
            {
                return true;
            } else {
                throw new SlotNotAvailable("Slot is not available.");
            }
        }

        public List<TimeSlot> GetAll()
        {
            List<TimeSlot>? returnList = new();

            string SQL_string = "SELECT * FROM TimeSlots order by [date] asc, slot asc";
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

        public List<TimeSlot> GetByDate(DateTime date)
        {
            List<TimeSlot> returnList = new();

            string SQL_string = "SELECT * from TimeSlots WHERE date = @date";
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                SqlParameter dateParam = new("@date", date);
                command.Parameters.Add(dateParam);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    returnList.Add(GetTimeslotReader(reader));
                }
                con.Close();
            }
            return returnList;
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
