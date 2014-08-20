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

            // put this part on its own thread
            server.Serve();

            string userInput = Console.ReadLine();

            if (userInput.Equals("exit"))
            {
                // request server shut down:
                // call out to all clients that they should shut down
                // meaning clients have to be threaded as well
                // unclean shutdown can be forced anyway with CTRL+c
            }

        }
    }
}
