using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverIP = "127.0.0.1";
            int serverPort = 12333;
            SimpleClient client = new SimpleClient(serverIP, serverPort);

            client.Run();

            Console.ReadLine();
        }
    }
}
