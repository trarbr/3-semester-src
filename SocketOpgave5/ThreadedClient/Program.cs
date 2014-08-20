using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadedClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverIP = "127.0.0.1";
            int serverPort = 12333;
            ThreadedClient client = new ThreadedClient();

            client.Connect(serverIP, serverPort);

            Console.ReadLine();
        }
    }
}
