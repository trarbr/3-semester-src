using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingHouse
{
    class Exit
    {
        private ParkingMonitor monitor;
        private volatile bool open;

        public Exit(ParkingMonitor monitor)
        {
            this.monitor = monitor;
        }

        public void Open()
        {
            Random sleepTime = new Random();

            open = true;
            while (open)
            {
                monitor.ExitCar();
                Thread.Sleep(sleepTime.Next(10, 5000));
            }
        }

        public void Close()
        {
            open = false;
        }
    }
}
