using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ThreadedServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress serverIP = IPAddress.Parse("127.0.0.1");
            int serverPort = 12333;
            ThreadedServer server = new ThreadedServer(serverIP, serverPort);

            server.Serve();

            Console.ReadLine();
        }
    }
}
