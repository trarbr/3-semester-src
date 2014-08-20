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

        public void Serve()
        {
            TcpListener listener = new TcpListener(serverIP, serverPort);

            listener.Start();

            Console.WriteLine(String.Format("Now listening on IP {0} and port {1}", 
                serverIP, serverPort));

            Socket client = listener.AcceptSocket();

            IPEndPoint clientEndPoint = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine(String.Format("Client connected! IP: {0} Port: {1}", 
                clientEndPoint.Address, clientEndPoint.Port));

            MathHandler handler = new MathHandler(client);
            handler.Handle();
        }
    }
}
