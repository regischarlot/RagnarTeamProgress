using System;

namespace TeamProgress.Models
{
    public enum TeType { Runner, Driver }

    public class Person : DataAccess
    {
        public int? Id { get; set; }
        public String Name { get; set; }
        public String DisplayName { get; set; }
        public Double Pace { get; set; }
        public String Cell { get; set; }
        public String Email { get; set; }
        public String EmergencyContact { get; set; }
        public TeType Type { get; set; }

        public Person()
        {
        }
    }
}