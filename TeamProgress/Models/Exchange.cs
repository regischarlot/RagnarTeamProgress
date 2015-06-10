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