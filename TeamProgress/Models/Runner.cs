using System;
using System.Data;
using System.Data.SqlClient;

namespace TeamProgress.Models
{
    public class Runner: Person
    {
        public Runner()
        {
        }

        public Runner(int id, string name, string displayname, double pace, string cell, string email, string contact, eType type) : base(id, name, displayname, pace, cell, email, contact, type)
        {
        }

        public Boolean Update(int id, string name, string displayname, float pace, string cell, string email, string emergencycontact, int type)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    // New record
                    string s = "update [Ragnar].[dbo].[Runner] set Name = @Name, DisplayName = @DisplayName, Pace = @Pace, Cell = @Cell, Email = @Email, EmergencyContact = @EmergencyContact, Type = @type where RunnerID = @RunnerID";
                    using (SqlCommand qry = new SqlCommand(s, conn))
                    {
                        qry.Parameters.Add("@RunnerID", SqlDbType.Int).Value = id;
                        qry.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                        qry.Parameters.Add("@DisplayName", SqlDbType.NVarChar).Value = displayname;
                        qry.Parameters.Add("@Pace", SqlDbType.Float).Value = pace;
                        qry.Parameters.Add("@Cell", SqlDbType.NVarChar).Value = cell;
                        qry.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                        qry.Parameters.Add("@EmergencyContact", SqlDbType.NVarChar).Value = emergencycontact;
                        qry.Parameters.Add("@Type", SqlDbType.Int).Value = type;
                        qry.ExecuteNonQuery();
                    }
                    conn.Close();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}