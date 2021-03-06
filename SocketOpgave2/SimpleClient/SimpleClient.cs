﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimpleClient
{
    class SimpleClient
    {
        internal void Connect(string serverIP, int serverPort)
        {
            TcpClient server = new TcpClient(serverIP, serverPort);

            NetworkStream networkStream = server.GetStream();
            StreamWriter writer = new StreamWriter(networkStream);
            StreamReader reader = new StreamReader(networkStream);

            Console.WriteLine("Server says " + reader.ReadLine());

            string userInput = Console.ReadLine();

            writer.WriteLine(userInput);
            writer.Flush();

            Console.WriteLine("Server says " + reader.ReadLine());

            reader.Close();
            writer.Close();
            networkStream.Close();
            server.Close();
        }
    }
}
