using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace threads03
{
    class Program
    {
        static void Main(string[] args)
        {
            Thermo thermo = new Thermo();
            Thread thread = new Thread(thermo.Run);

            thread.Start();

            while (thread.IsAlive)
            {
                Thread.Sleep(10000);
                Console.WriteLine("Thread is alive");
            }

            Console.WriteLine("Alarm thread terminated");

            Console.ReadLine();
        }
    }
}
