using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient client = new WebClient();

            string response = client.DownloadString("http://webservicedemo.datamatiker-skolen.dk/RegneWcfService.svc/RESTjson/Add?a=5&b=8");
            Console.WriteLine(response);

            // Profit?!
            Console.ReadLine();
        }
    }
}
