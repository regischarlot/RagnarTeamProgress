using System;

namespace TeamProgress.Models
{
    public class Team: IDisposable
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public void Dispose()
        {
        }
    }
}

