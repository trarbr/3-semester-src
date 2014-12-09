using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetable.LexicalAnalysis
{
    public interface ICharGenerator
    {
        char GetCurrent();
        bool MoveNext();
    }
}
