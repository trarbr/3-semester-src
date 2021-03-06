﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ThreadedServer
{
    class DateTimeHandler
    {
        private Socket client;
        List<string> commands;

        public DateTimeHandler(Socket client)
        {
            this.client = client;
            commands = new List<string>();
        }

        public void Handle()
        {
            IPEndPoint clientEndPoint = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine(String.Format("Client connected! IP: {0} Port: {1}",
                clientEndPoint.Address, clientEndPoint.Port));

            NetworkStream networkStream = new NetworkStream(client);
            StreamWriter writer = new StreamWriter(networkStream);
            StreamReader reader = new StreamReader(networkStream);

            writer.WriteLine("Ready");
            writer.Flush();

            bool clientConnected = true;

            while (clientConnected)
            {
                string input = reader.ReadLine().Trim().ToLower();

                commands.Add(input);

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
                    case "commands?":
                        string commandHistory = "";
                        foreach (var command in commands)
                        {
                            commandHistory += command + ", ";
                        }
                        writer.WriteLine(commandHistory);
                        writer.Flush();
                        break;
                    case "exit":
                        Console.WriteLine(String.Format("Client disconnected! IP: {0} Port {1}",
                            clientEndPoint.Address, clientEndPoint.Port));
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
