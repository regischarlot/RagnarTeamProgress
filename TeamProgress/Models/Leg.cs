namespace TeamProgress.Models
{
    public class Leg
    {
        public int LegID { get; set; }
        public int Order { get; set; }
        public int Van { get; set; }
        public double Distance { get; set; }
        public int Difficulty { get; set; }

        public Leg()
        {
            
        }

        public Leg(int id, int order, int van, double distance, int difficulty)
        {
            LegID = id;
            Order = order;
            Van = van;
            Distance = distance;
            Difficulty = difficulty;
        }
    }
}
