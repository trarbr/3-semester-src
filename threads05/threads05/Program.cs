using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace threads05
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintMonitor monitor = new PrintMonitor();
            IThreadedPrinter starPrinter = new StarPrinter(monitor);
            IThreadedPrinter hashPrinter = new HashPrinter(monitor);

            Thread starThread = new Thread(starPrinter.Print);
            Thread hashThread = new Thread(hashPrinter.Print);

            starThread.Start();
            hashThread.Start();

            Thread.Sleep(10000);

            starPrinter.RequestStop();
            hashPrinter.RequestStop();

            starThread.Join();
            hashThread.Join();

            Console.WriteLine("All printer threads stopped!");
            Console.ReadLine();
        }
    }
}
