using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace threads05
{
    class PrintMonitor
    {
        private int signCounter;
        private object signCounterLock;

        public PrintMonitor()
        {
            signCounterLock = new Object();
            signCounter = 0;
        }

        internal void PrintAndCount(string signs)
        {
            lock (signCounterLock)
            {
                signCounter += 60;
                Console.WriteLine("{0} {1}", signs, signCounter);
            }
        }
    }
}
