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

        public void Serve()
        {
            TcpListener listener = new TcpListener(serverIP, serverPort);

            listener.Start();

            Console.WriteLine(String.Format("Now listening on IP {0} and port {1}",
                serverIP, serverPort));

            while (true)
            {
                Socket client = listener.AcceptSocket();

                // Difference - is the DateTimeHandler initialized in main thread or new thread?
                DateTimeHandler handler = new DateTimeHandler(client);
                ThreadStart starter = new ThreadStart(handler.Handle);
                // ThreadStart starter = new ThreadStart(new DateTimeHandler().Handle);

                Thread thread = new Thread(starter);

                thread.Start();
            }
        }
    }
}
