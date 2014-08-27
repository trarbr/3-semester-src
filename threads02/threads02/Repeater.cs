using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace threads02
{
    class Repeater
    {
        public void Run()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("C# trådning er nemt!");
                Thread.Sleep(500);
            }
        }

        public void RunAlternative()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Også med flere tråde");
                Thread.Sleep(100);
            }
        }
    }
}
