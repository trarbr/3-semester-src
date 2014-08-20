using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimpleServer
{
    class MathHandler
    {
        private Socket client;

        public MathHandler(Socket client)
        {
            this.client = client;
        }

        public void Handle()
        {
            NetworkStream networkStream = new NetworkStream(client);
            StreamWriter writer = new StreamWriter(networkStream);
            StreamReader reader = new StreamReader(networkStream);

            string output = "Ready";

            bool clientConnected = true;
            while (clientConnected)
            {
                writer.WriteLine(output);
                writer.Flush();

                string[] input = reader.ReadLine().Trim().ToLower().Split(' ');

                string command = input[0];
                switch (command)
                {
                    case "add":
                        output = add(input);
                        break;
                    case "sub":
                        output = sub(input);
                        break;
                    case "exit":
                        Console.WriteLine("Client disconnected");
                        clientConnected = false;
                        break;
                    default:
                        output = "Unkown command";
                        break;
                }
            }

            reader.Close();
            writer.Close();
            networkStream.Close();
            client.Close();
        }

        private string add(string[] input)
        {
            string output = "";
            try
            {
                int x = int.Parse(input[1]);
                int y = int.Parse(input[2]);
                output = "sum " + (x + y);
            }
            catch
            {
                output = "usage: sum <int> <int>";
            }

            return output;
        }

        private string sub(string[] input)
        {
            string output = "";
            try
            {
                int x = int.Parse(input[1]);
                int y = int.Parse(input[2]);
                output = "difference  " + (x - y);
            }
            catch
            {
                output = "usage: sub <int> <int>";
            }

            return output;
        }
    }
}