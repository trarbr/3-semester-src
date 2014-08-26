﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverName = "webservicedemo.datamatiker-skolen.dk";
            int serverPort = 80;

            string request = String.Format(
                "GET /RegneWcfService.svc/RESTjson/Add?a={0}&b={1} HTTP/1.1\n", 3, 7);
            request += "Host: webservicedemo.datamatiker-skolen.dk\n";
            request += "\n";

            Console.WriteLine(request);

            TcpClient server = new TcpClient(serverName, serverPort);

            NetworkStream networkStream = server.GetStream();
            StreamWriter writer = new StreamWriter(networkStream);
            StreamReader reader = new StreamReader(networkStream);

            writer.Write(request);
            writer.Flush();

            Console.WriteLine("sent");

            string response = reader.ReadLine();

            int contentLength = 0;

            // throwaway headers, get content
            while (response != null && response != "")
            {
                Console.WriteLine(response);
                response = reader.ReadLine();
                if (response.StartsWith("Content-Lengt"))
                {
                    contentLength = int.Parse(response.Split(' ')[1]);
                }
            }

            // parse response
            string amount = "";
            for (int i = 0; i < contentLength; i++)
            {
                amount += (char)reader.Read();
            }
            Console.WriteLine(amount);

            reader.Close();
            writer.Close();
            server.Close();

            Console.ReadLine();
        }
    }
}
