using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetable
{
    public interface ITokenizer
    {
        char GetCurrent();
        bool MoveNext();
    }
}
