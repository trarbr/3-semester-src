using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ThreadedServer
{
    class WebHandler
    {
        private Socket client;
        private NetworkStream stream;
        private StreamWriter writer;
        private StreamReader reader;
        private IService service;

        public WebHandler(Socket client)
        {
            this.client = client;
            stream = null;
            writer = null;
            reader = null;
            service = null;
        }

        public void Handle()
        {
            Console.WriteLine(String.Format("Client connected! {0}", client.RemoteEndPoint));

            stream = new NetworkStream(client);
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);

            service = new DateTimeService();

            string request = readHttpRequest();

            respondToRequest(request);

            reader.Close();
            writer.Close();
            stream.Close();
            client.Close();
        }

        private void respondToRequest(string request)
        {
            string content = service.ResolveRequest(request) + "\n";
            int contentLength = Encoding.UTF8.GetByteCount(content);

            string response = "HTTP/1.1 200 OK" + Environment.NewLine;
            response += "Content-Type: text/plain" + Environment.NewLine;
            response += "Content-Length: " + contentLength + Environment.NewLine; 
            response += "\n";
            response += content;

            sendMessage(response);
        }

        private string readHttpRequest()
        {
            string request;

            try
            {
                string[] message = receiveMessage().Split(' ');

                request = message[1];
            }
            catch
            {
                request = null;
            }

            return request;
        }

        private string receiveMessage()
        {
            string input;

            try
            {
                input = reader.ReadLine();
            }
            catch
            {
                input = null;
            }

            return input;
        }

        private void sendMessage(string response)
        {
            writer.Write(response);
            writer.Flush();
        }
    }
}
