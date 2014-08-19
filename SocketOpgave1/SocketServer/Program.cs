using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress serverIP = IPAddress.Parse("127.0.0.1");
            int serverPort = 12333;
            SimpleServer server = new SimpleServer(serverIP, serverPort);

            server.Run();

            Console.ReadLine();
        }
    }
}
