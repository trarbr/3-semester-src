using System;
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
        // Warning: Cowboy coding and throwaway design
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

            // write headers, get content length
            while (response != null && response != "")
            {
                Console.WriteLine(response);
                response = reader.ReadLine();
                if (response.StartsWith("Content-Lengt"))
                {
                    contentLength = int.Parse(response.Split(' ')[1]);
                }
            }

            // read and display content
            string content = "";
            for (int i = 0; i < contentLength; i++)
            {
                content += (char)reader.Read();
            }
            Console.WriteLine(content);

            reader.Close();
            writer.Close();
            server.Close();

            Console.ReadLine();
        }
    }
}
