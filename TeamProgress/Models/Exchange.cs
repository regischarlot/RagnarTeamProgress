using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProgress.Models
{
    public class Exchange
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIP { get; set; }
        public int Van { get; set; }

        public string MapAddress { get
        {
            return string.Format("{0}, {1}, {2}, {3} {4}", new object[] {Name, Address, City, State, ZIP});
        } }

        public string GoogleMapLink
        {
            get
            {
                string s = string.Format("{0}, {1}, {2} {3}", new object[] { Address, City, State, ZIP });
                return string.Format("<a target=\"_blank\" href=\"https://www.google.com/maps/place/{0}\">{1}</a>", new object[] { s.Replace(" ", "+"), s });
            }
        }

        public string FollowingLeg
        {
            get
            {
               return string.Format("<a href=\"https://www.ragnarrelay.com/race/chicago/legs/{0}\" target=\"_blank\">Leg {0}</a>", Id);
            }
        }

        public Exchange(int id, string name, string address, string city, string state, string zip, int van)
        {
            Id = id;
            Name = name;
            Address = address;
            City = city;
            State = state;
            ZIP = zip;
            Van = van;
        }
    }
}