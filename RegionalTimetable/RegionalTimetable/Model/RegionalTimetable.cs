using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetableApp.Model
{
    class RegionalTimetable
    {
        public readonly List<Timetable> Timetables;

        public RegionalTimetable()
        {
            Timetables = new List<Timetable>();
        }
    }
}
