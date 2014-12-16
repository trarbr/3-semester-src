using RegionalTimetableApp.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetableApp.Parsing
{
    interface ITokenGenerator
    {
        Token GetCurrent();
        bool MoveNext();
    }
}
