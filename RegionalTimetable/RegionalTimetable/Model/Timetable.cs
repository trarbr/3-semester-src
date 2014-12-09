using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegionalTimetableApp.Model
{
    class Timetable
    {
        public readonly string RouteNumber;
        public readonly List<Departure> Departures;

        public Timetable(string routeNumber)
        {
            RouteNumber = routeNumber;
            Departures = new List<Departure>();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(RouteNumber);
            foreach (var departure in Departures)
            {
                stringBuilder.AppendLine(departure.ToString());
            }
            return stringBuilder.ToString();
        }
    }
}
