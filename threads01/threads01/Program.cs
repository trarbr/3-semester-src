using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace threads01
{
    class Program
    {
        static void Main(string[] args)
        {
            Repeater repeater = new Repeater();
            Thread thread = new Thread(repeater.Run);
            thread.Start();

            Console.ReadLine();
        }
    }
}
