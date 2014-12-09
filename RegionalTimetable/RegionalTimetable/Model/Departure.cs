using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetableApp.Model
{
    class Departure
    {
        public readonly string Time;
        public readonly string City;

        public Departure(string time, string city)
        {
            Time = time;
            City = city;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", City, Time);
        }
    }
}
