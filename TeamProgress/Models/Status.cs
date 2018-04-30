using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using ADM;


namespace TeamProgress.Models
{
    public class Pair
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Pair(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    
    public class Status : DataAccess
    {
        public List<Runner> Runners;
        public List<LegRunner> LegRunners;
        public List<Pair> RunnerTypes;
        public List<Exchange> Exchanges;
        public Team TeamDefinition;
        public String DisplayRunner;
        public String DisplayTeam;
        public string Error;

        /// <summary>
        ///    Constructor
        ///
        /// </summary>
        public Status()
        {
            Runners = new List<Runner>();
            LegRunners = new List<LegRunner>();
            RunnerTypes = new List<Pair>();
            TeamDefinition = new Team();
            DisplayRunner = String.Empty;
            DisplayTeam = String.Empty;
            Exchanges = new List<Exchange>();
        }

        /// <summary>
        ///    Load()
        ///
        /// </summary>
        public void Load(int team)
        {
            const int defaultpace = 12;
            Error = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    DisplayRunner = string.Empty;
                    //
                    // A. Runner Types
                    RunnerTypes.Clear();
                    RunnerTypes.Add(new Pair((int) eType.Runner, "Runner"));
                    RunnerTypes.Add(new Pair((int) eType.Driver, "Driver"));
                    //
                    // A. Team
                    //
                    using (SqlCommand myCommand = new SqlCommand(string.Format("SELECT [TeamID],[Name] FROM [Ragnar].[dbo].[Team] WHERE [TeamID]={0}", team), conn))
                    {
                        using (SqlDataReader rdr = myCommand.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                TeamDefinition.Id = rdr["TeamID"].ToInt32();
                                TeamDefinition.Name = rdr["Name"].ToString();
                            }
                        }
                    }
                    //
                    // B. Runners
                    //
                    using (SqlCommand myCommand = new SqlCommand(string.Format("SELECT [RunnerID],[Name],[DisplayName],[Pace],[Cell],[Email],[EmergencyContact], [Type] FROM [Ragnar].[dbo].[Runner] order by Name", team), conn))
                    {
                        SqlDataReader rdr = myCommand.ExecuteReader();
                        while (rdr.Read())
                            Runners.Add(new Runner(rdr["RunnerID"].ToInt32(), rdr["Name"].ToString(),
                                rdr["DisplayName"].ToString(), rdr["Pace"].ToDouble(), rdr["Cell"].ToString(),
                                rdr["Email"].ToString(), rdr["EmergencyContact"].ToString(),
                                (eType) rdr["Type"].ToInt32()));
                        rdr.Close();
                    }
                    //
                    // C. Leg Runners
                    //
                    using (SqlCommand myCommand = new SqlCommand(string.Format("SELECT " +
                                                                               "    A.[LegID], " +
                                                                               "    A.[Order], " +
                                                                               "    B.[LegRunnerID], " +
                                                                               "    A.[Distance], " +
                                                                               "    A.[Van], " +
                                                                               "    A.[Difficulty], " +
                                                                               "    B.[StartTime], " +
                                                                               "    B.[EndTime], " +
                                                                               "    B.[RunnerID] Runner1ID, " +
                                                                               "    C.[Name] Runner1Name, " +
                                                                               "    C.[Pace] Runner1Pace, " +
                                                                               "    C.[Cell] Runner1Cell, " +
                                                                               "    B.[Runner2ID] Runner2ID, " +
                                                                               "    D.[Name] Runner2Name, " +
                                                                               "    D.[Pace] Runner2Pace, " +
                                                                               "    D.[Cell] Runner2Cell " +
                                                                               "FROM  " +
                                                                               "    [Ragnar].[dbo].[Leg] A " +
                                                                               "    left outer join [Ragnar].[dbo].[LegRunner] B on (A.LegID = b.LegID and b.TeamID={0}) " +
                                                                               "    left outer join [Ragnar].[dbo].[Runner] C on (B.RunnerID = C.RunnerID) " +
                                                                               "    left outer join [Ragnar].[dbo].[Runner] D on (B.Runner2ID = D.RunnerID) " +
                                                                               "order by " +
                                                                               "    A.[Order]", team), conn))
                    {
                        SqlDataReader rdr = myCommand.ExecuteReader();
                        bool rds_est = true, rde_est = true;
                        DateTime? rds = null, rde = null, prev_rde = null;
                        int cnt = 0;
                        while (rdr.Read())
                        {
                            // Distance
                            double dist = rdr["Distance"].ToDouble();
                            // Runner Pace
                            double pace = defaultpace;
                            int? runner1 = rdr["Runner1ID"].ToNullableInt32();
                            int? runner2 = rdr["Runner2ID"].ToNullableInt32();
                            Runner p = getRunner(runner1);
                            if (p != null)
                                pace = p.Pace != 0 ? p.Pace : defaultpace;
                            // Read dates well
                            DateTime? ds = null;
                            if (rdr["StartTime"] != DBNull.Value)
                                ds = rdr["StartTime"].ToDateTime();
                            DateTime? de = null;
                            if (rdr["EndTime"] != DBNull.Value)
                                de = rdr["EndTime"].ToDateTime();

                            if (ds != null)
                            {
                                rds = ds;
                                rds_est = false;
                            }
                            else
                            {
                                rds = prev_rde;
                                rds_est = true;
                            }

                            if (de != null)
                            {
                                rde = de;
                                rde_est = false;
                            }
                            else
                            {
                                rde = null;
                                rde_est = true;
                                if (!pace.Equals(0))
                                    rde = rds.ToDateTime().AddHours(dist*pace/60);
                            }
                            TimeSpan ts = rde.ToDateTime().Subtract(rds.ToDateTime());
                            string legtime = string.Format("{0:D2}:{1:D2}:{2:D2}", new object[] {ts.Hours, ts.Minutes, ts.Seconds});
                            string truepace = string.Empty;
                            if (!rds_est && !rde_est)
                                truepace = string.Format("{0:F2}", ((ts.Hours*3600 + ts.Minutes*60 + ts.Seconds)/dist)/60);
                            //
                            // Display Runner
                            if ((rdr["StartTime"] != DBNull.Value && rdr["EndTime"] == DBNull.Value) ||
                                (string.IsNullOrEmpty(DisplayRunner) && rdr["EndTime"] == DBNull.Value))
                                DisplayRunner = string.Format("<table height='100%' width='100%'>" +
                                                              "  <tr>" +
                                                              "    <td rowspan=\"2\" style=\"font-size:30pt; font-weight: bold; padding-right:20px;\">{0}</td>" +
                                                              "    <td style=\"font-size:11pt; font-weight: bold; padding-right:20px;\">Est. {1}</td>" +
                                                              "  </tr>" +
                                                              "  <tr>" +
                                                              "    <td style=\"font-size:11pt; font-weight: bold;\">Cell {2}</td>" +
                                                              "  </tr>" +
                                                              "</table>",
                                    new object[]
                                    {
                                        rdr["Runner1Name"].ToString(), rde.ToDateTime().ToString("hh:mm tt"),
                                        rdr["Runner1Cell"].ToString()
                                    });
                            //
                            // Display Team
                            DisplayTeam = string.Format("<table height='100%' width='100%'><tr><td style=\"text-align:center; vertical-align:middle; font-size:20pt; \">{0}</td></tr></table>", new object[] {TeamDefinition.Name});
                            //
                            // Add leg runner
                            LegRunners.Add(new LegRunner
                            {
                                StartTime = rds,
                                EndTime = rde,
                                Runner1ID = runner1,
                                Runner2ID = runner2,
                                LegRunnerID = rdr["LegRunnerID"].ToNullableInt32(),
                                TeamID = team,
                                LegID = rdr["LegID"].ToInt32(),
                                Order = rdr["Order"].ToInt32(),
                                Van = rdr["Van"].ToInt32(),
                                Distance = dist,
                                Difficulty = rdr["Difficulty"].ToInt32(),
                                StartTimeEstimated = rds_est,
                                EndTimeEstimated = rde_est,
                                LegTime = legtime,
                                TruePace = truepace,
                                Runner1Name = rdr["Runner1Name"].ToString(),
                                Runner1Pace = rdr["Runner1Pace"].ToString(),
                                Runner1Cell = rdr["Runner1Cell"].ToString(),
                                Runner2Name = rdr["Runner2Name"].ToString(),
                                Runner2Pace = rdr["Runner2Pace"].ToString(),
                                Runner2Cell = rdr["Runner2Cell"].ToString(),
                                LegMap = $"Leg {++cnt}"
                            });
                            prev_rde = rde;
                        }
                        rdr.Close();
                    }
                    //
                    // D. Exchanges
                    //
                    using (SqlCommand myCommand = new SqlCommand("SELECT [ID],[Name],[Address],[City],[State],[ZIP],[Van] FROM [Ragnar].[dbo].[Exchanges] order by ID", conn))
                    {
                        SqlDataReader rdr = myCommand.ExecuteReader();
                        while (rdr.Read())
                            Exchanges.Add(new Exchange(rdr["ID"].ToInt32(), rdr["Name"].ToString(), rdr["Address"].ToString(), rdr["City"].ToString(), rdr["State"].ToString(), rdr["ZIP"].ToString(), rdr["Van"].ToInt32()));
                        rdr.Close();
                    }

                    //
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
        }

        /// <summary>
        ///    Destructor
        ///
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        ///    Update()
        ///
        /// </summary>
        public Boolean Update(int legId, int? legRunnerId, string field, string value)
        {
            try
            {
                DateTime? d = null;
                int? r = null;
                if (field.Equals("StartTime") || field.Equals("EndTime"))
                {
                    if (!value.Equals("null"))
                        d = DateTime.ParseExact(value.Replace("\"", ""), "yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else if (field.Equals("Runner1ID") || field.Equals("Runner2ID"))
                {
                    if (!value.Equals("null"))
                        r = Int32.Parse(value, CultureInfo.InvariantCulture);
                    if (field.Equals("Runner1ID"))
                        field = "RunnerID";
                }
                //
                // B. Update database
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    // New record
                    string s;
                    if (legRunnerId == null)
                        s = "insert into [Ragnar].[dbo].[LegRunner] (TeamID, LegID, " + field + ") values (@teamid, @legid, @value)";
                    else
                        s = "update [Ragnar].[dbo].[LegRunner] set " + field + " = @value where LegRunnerID = @legrunnerid";
                    using (SqlCommand qry = new SqlCommand(s, conn))
                    {
                        qry.Parameters.Add("@teamid", SqlDbType.Int).Value = 1;
                        qry.Parameters.Add("@legid", SqlDbType.Int).Value = legId;
                        qry.Parameters.Add("@legrunnerid", SqlDbType.Int).Value = (object)legRunnerId ?? DBNull.Value;
                        if (field.Equals("StartTime") || field.Equals("EndTime"))
                            qry.Parameters.Add("@value", SqlDbType.DateTime).Value = (object)d ?? DBNull.Value;
                        else if (field.Equals("RunnerID") || field.Equals("Runner1ID") || field.Equals("Runner2ID"))
                            qry.Parameters.Add("@value", SqlDbType.Int).Value = (object)r ?? DBNull.Value;
                        qry.ExecuteNonQuery();
                    }
                    conn.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        ///    getRunner()
        ///
        /// </summary>
        private Runner getRunner(int? value)
        {
            if (value != null)
                return Runners.Find(x => x.Id == value);
            return null;
        }

    }
}