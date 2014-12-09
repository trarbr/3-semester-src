using RegionalTimetableApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetableApp.Parsing
{
    class ParseResult
    {
        public readonly RegionalTimetable RegionalTimetable;
        public readonly List<string> Errors;

        public ParseResult(RegionalTimetable regionalTimetable, List<string> errors)
        {
            RegionalTimetable = regionalTimetable;
            Errors = errors;
        }
    }
}
