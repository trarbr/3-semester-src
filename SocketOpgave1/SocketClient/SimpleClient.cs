using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketClient
{
    class SimpleClient
    {
        private string serverIP;
        private int serverPort;

        public SimpleClient(string serverIP, int serverPort)
        {
            this.serverIP = serverIP;
            this.serverPort = serverPort;
        }

        public void Run()
        {
            TcpClient server = new TcpClient(serverIP, serverPort);

            NetworkStream networkStream = server.GetStream();
            StreamWriter writer = new StreamWriter(networkStream);
            StreamReader reader = new StreamReader(networkStream);

            writer.WriteLine("Hello server");
            writer.Flush();

            Console.WriteLine(reader.ReadLine());

            reader.Close();
            writer.Close();
            networkStream.Close();
            server.Close();
        }
    }
}
