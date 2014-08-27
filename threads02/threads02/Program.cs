using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace threads02
{
    class Program
    {
        static void Main(string[] args)
        {
            Repeater repeater = new Repeater();
            Thread thread1 = new Thread(repeater.Run);
            Thread thread2 = new Thread(repeater.RunAlternative);

            thread1.Start();
            thread2.Start();

            Console.ReadLine();
        }
    }
}
