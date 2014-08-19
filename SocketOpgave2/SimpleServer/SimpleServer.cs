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

            NetworkStream networkStream = new NetworkStream(client);
            StreamWriter writer = new StreamWriter(networkStream);
            StreamReader reader = new StreamReader(networkStream);

            writer.WriteLine("Ready");
            writer.Flush();

            string input = reader.ReadLine();

            if (input == "time?")
            {
                writer.WriteLine(String.Format("{0:HH:mm:ss}", DateTime.Now));
                writer.Flush();
            }
            else
            {
                writer.WriteLine("Unkown command");
                writer.Flush();
            }

            reader.Close();
            writer.Close();
            networkStream.Close();
            client.Close();
        }

    }
}
