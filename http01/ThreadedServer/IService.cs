using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadedServer
{
    interface IService
    {
        string Help { get; }

        string ResolveRequest(string request);
    }
}
