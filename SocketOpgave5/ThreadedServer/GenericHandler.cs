using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ThreadedServer
{
    class GenericHandler
    {
        private Socket client;
        private NetworkStream stream;
        private StreamWriter writer;
        private StreamReader reader;
        private IService service;
        private bool clientConnected;

        public GenericHandler(Socket client)
        {
            this.client = client;
            stream = null;
            writer = null;
            reader = null;
            service = null;
            clientConnected = false;
        }

        public void Handle()
        {
            IPEndPoint clientEndPoint = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine(String.Format("Client connected! {0}", client.RemoteEndPoint));

            stream = new NetworkStream(client);
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);

            clientConnected = true;

            // Type of service depends on first command
            resolveService();

            runService();

            Console.WriteLine(String.Format("Client disconnected! {0}", client.RemoteEndPoint));

            reader.Close();
            writer.Close();
            stream.Close();
            client.Close();
        }

        private void resolveService()
        {
            while (clientConnected && service == null)
            {
                sendMessage("datetime | other services (none at this moment)");

                string requestedService = receiveMessage();
                if (requestedService == null)
                {
                    clientConnected = false;
                }
                else if (requestedService == "datetime")
                {
                    service = new DateTimeService();
                }
            }
        }

        private void runService()
        {
            sendMessage(service.Help);

            while (clientConnected)
            {
                string request = receiveMessage();

                string response = service.ResolveRequest(request);
                if (response == null)
                {
                    clientConnected = false;
                }
                else
                {
                    sendMessage(response);
                }
            }
        }


        private void sendMessage(string message)
        {
            writer.WriteLine(message);
            writer.Flush();
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
    }
}
