using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimpleServer
{
    class DateTimeHandler
    {
        private Socket client;

        public DateTimeHandler(Socket client)
        {
            this.client = client;
        }

        public void Handle()
        {
            NetworkStream networkStream = new NetworkStream(client);
            StreamWriter writer = new StreamWriter(networkStream);
            StreamReader reader = new StreamReader(networkStream);

            writer.WriteLine("Ready");
            writer.Flush();

            bool clientConnected = true;

            while (clientConnected)
            {
                string input = reader.ReadLine().Trim().ToLower();

                switch (input)
                {
                    case "time?":
                        writer.WriteLine(String.Format("{0:HH:mm:ss}", DateTime.Now));
                        writer.Flush();
                        break;
                    case "date?":
                        writer.WriteLine(String.Format("{0:yyyy-MM-dd}", DateTime.Now));
                        writer.Flush();
                        break;
                    case "exit":
                        Console.WriteLine("Client disconnected");
                        clientConnected = false;
                        break;
                    default:
                        writer.WriteLine("Unkown command");
                        writer.Flush();
                        break;
                }
            }

            reader.Close();
            writer.Close();
            networkStream.Close();
            client.Close();
        }
    }
}
