using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadedServer
{
    class ThreadedServer
    {
        private IPAddress serverIP;
        private int serverPort;

        public ThreadedServer(IPAddress serverIP, int serverPort)
        {
            this.serverIP = serverIP;
            this.serverPort = serverPort;
        }

        public void ServeForever()
        {
            TcpListener listener = new TcpListener(serverIP, serverPort);

            listener.Start();

            Console.WriteLine(String.Format("Now listening on IP {0} and port {1}",
                serverIP, serverPort));

            // Keep serving forever
            while (true)
            {
                Socket client = listener.AcceptSocket();

                DateTimeHandler handler = new DateTimeHandler(client);
                ThreadStart starter = new ThreadStart(handler.Handle);
                Thread thread = new Thread(starter);

                thread.Start();
            }
        }
    }
}
