using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadedServer
{
    class DateTimeService : IService
    {
        public string Help { get; private set; }

        public DateTimeService()
        {
            Help = "time? | date? | exit";
        }

        public string ResolveRequest(string request)
        {
            string response = null;

            switch (request)
            {
                case "/time":
                    response = String.Format("{0:HH:mm:ss}", DateTime.Now);
                    break;
                case "/date":
                    response = String.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    break;
                default:
                    response = "Unkown command";
                    break;
            }

            return response;
        }
    }
}
