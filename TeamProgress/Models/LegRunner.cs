using System;

namespace TeamProgress.Models
{
    public class LegRunner : Leg
    {
        public int TeamID { get; set; }
        public int? LegRunnerID { get; set; }
        public int? RunnerID { get; set; }
        public string RunnerName { get; set; }
        public string RunnerPace { get; set; }
        public string RunnerCell { get; set; }
        public DateTime? StartTime { get; set; }
        public Boolean StartTimeEstimated { get; set; }
        public DateTime? EndTime { get; set; }
        public Boolean EndTimeEstimated { get; set; }
        public String LegTime { get; set; }
        public string TruePace { get; set; }

        public LegRunner()
        {
            
        }

        public LegRunner(int team, int leg, int order, int? legrunner, int? runner, double distance, int van, int difficulty, DateTime? starttime, bool starttime_estimated, DateTime? endtime, bool endtime_estimated, string legtime, string truepace, string runnername, string runnerpace, string runnercell)
        {
            StartTime = starttime;
            EndTime = endtime;
            RunnerID = runner;
            LegRunnerID = legrunner;
            TeamID = team;
            LegID = leg;
            Order = order;
            Van = van;
            Distance = distance;
            Difficulty = difficulty;
            StartTimeEstimated = starttime_estimated;
            EndTimeEstimated = endtime_estimated;
            LegTime = legtime;
            TruePace = truepace;
            RunnerName = runnername;
            RunnerPace = runnerpace;
            RunnerCell = runnercell;
        }
    }
}
