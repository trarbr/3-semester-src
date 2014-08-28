using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace threads05
{
    class HashPrinter : IThreadedPrinter
    {
        private PrintMonitor monitor;
        private volatile bool stop_requested;

        public HashPrinter(PrintMonitor monitor)
        {
            this.monitor = monitor;
            stop_requested = false;
        }

        public void Print()
        {
            while (!stop_requested)
            {
                monitor.PrintAndCount("############################################################");
                Thread.Sleep(100);
            }
        }

        public void RequestStop()
        {
            stop_requested = true;
        }
    }
}
