using Data.Database_layer;
using Data.Model_layer;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Model.Model_layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public bool DecreaseAvailability(TimeSlot timeslot)
        {
            int numberOfRowsModified = 0;
            string SQL_string = "UPDATE TimeSlots SET [availability] = [availability] - 1 WHERE date = @Date AND slot = @Slot";
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                SqlParameter dateParam = new("@Date", new DateTime(timeslot.Date.Year, timeslot.Date.Month, timeslot.Date.Day));
                command.Parameters.Add(dateParam);
                SqlParameter slotParam = new("@Slot", timeslot.Slot);
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

        public TimeSlot Get(DateOnly date, String slot)
        {
            TimeSlot timeslot = new();

            string SQL_string = "SELECT * from TimeSlots WHERE [date] = @date and slot = @slot";
            using (SqlConnection con = new(_connectionString))
            using (SqlCommand command = new(SQL_string, con))
            {
                SqlParameter dateParam = new("@Date", new DateTime(date.Year, date.Month, date.Day));
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
            DateOnly date = DateOnly.Parse(reader.GetDateTime(reader.GetOrdinal("date")).ToShortDateString());
            string slot = reader.GetString(reader.GetOrdinal("slot"));
            int availability = reader.GetInt32(reader.GetOrdinal("availability"));

            returnTimeslot = new(date, slot, availability);

            return returnTimeslot;
        }

    }
}
