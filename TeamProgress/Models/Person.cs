using System;

namespace TeamProgress.Models
{
    public enum eType { Runner, Driver }

    public class Person : DataAccess
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String DisplayName { get; set; }
        public Double Pace { get; set; }
        public String Cell { get; set; }
        public String Email { get; set; }
        public String EmergencyContact { get; set; }
        public eType Type { get; set; }

        public Person()
        {
        }

        public Person(int id, string name, string displayname, double pace, string cell, string email, string contact, eType type)
        {
            Id = id;
            Name = name;
            DisplayName = displayname;
            Pace = pace;
            Cell = cell;
            Email = email;
            EmergencyContact = contact;
            Type = type;
        }
    }
}