using System;

namespace TeamProgress.Models
{
    public class LegRunner : Leg
    {
        public int TeamID { get; set; }
        public int? LegRunnerID { get; set; }

        public int? Runner1ID { get; set; }
        public string Runner1Name { get; set; }
        public string Runner1Pace { get; set; }
        public string Runner1Cell { get; set; }
        public int? Runner2ID { get; set; }
        public string Runner2Name { get; set; }
        public string Runner2Pace { get; set; }
        public string Runner2Cell { get; set; }
        public double? Pace { get; set; }
        public string TruePace { get; set; }


        public DateTime? StartTime { get; set; }
        public Boolean StartTimeEstimated { get; set; }
        public DateTime? EndTime { get; set; }
        public Boolean EndTimeEstimated { get; set; }
        public String LegTime { get; set; }
        public string LegMap { get; set; }

        public LegRunner()
        {
            
        }
    }
}
