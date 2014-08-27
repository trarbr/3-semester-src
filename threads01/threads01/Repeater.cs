using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace threads01
{
    class Repeater
    {
        public void Run()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("C# trådning er nemt!");
            }

        }
    }
}
