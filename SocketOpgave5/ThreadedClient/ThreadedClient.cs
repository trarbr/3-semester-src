using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadedClient
{
    class ThreadedClient
    {
        private volatile bool running;
        private StreamReader reader;
        private StreamWriter writer;

        internal void Connect(string serverIP, int serverPort)
        {
            TcpClient server = new TcpClient(serverIP, serverPort);

            NetworkStream networkStream = server.GetStream();
            writer = new StreamWriter(networkStream);
            reader = new StreamReader(networkStream);

            Console.WriteLine("Server says " + reader.ReadLine());

            running = true;

            new Thread(new ThreadStart(this.read)).Start();

            write();

            Thread.Sleep(10);

            reader.Close();
            writer.Close();
            networkStream.Close();
            server.Close();
        }

        private void read()
        {
            while (running)
            {
                // beware of io exception! if server has shut down. But how can running be true??
                string serverSays = reader.ReadLine();
                Console.WriteLine("Server said: " + serverSays);

                if (serverSays == null || serverSays.Equals("exit"))
                {
                    running = false;
                }
            }
        }

        private void write()
        {
            while (running)
            {
                Console.Write(">>> ");
                string clientSays = Console.ReadLine();
                writer.WriteLine(clientSays);
                writer.Flush();

                if (clientSays.Equals("exit"))
                {
                    running = false;
                }

                // Wait to receive answer before putting >>> on the console
                Thread.Sleep(100);
            }
        }
    }
}
