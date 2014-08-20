using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimpleServer
{
    class SimpleServer
    {
        private IPAddress serverIP;
        private int serverPort;

        public SimpleServer(IPAddress serverIP, int serverPort)
        {
            this.serverIP = serverIP;
            this.serverPort = serverPort;
        }

        public void Run()
        {
            TcpListener listener = new TcpListener(serverIP, serverPort);

            listener.Start();

            Console.WriteLine("Now listening on " + serverIP + " " + serverPort);

            Socket client = listener.AcceptSocket();

            IPEndPoint clientEndPoint = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("Client connected! " + clientEndPoint.Address + " " + clientEndPoint.Port);

            DateTimeHandler handler = new DateTimeHandler(client);
            handler.Handle();
        }
    }
}
