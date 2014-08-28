using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace threads05
{
    interface IThreadedPrinter
    {
        void Print();
        void RequestStop();
    }
}
