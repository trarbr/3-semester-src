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
        private string vehicleType;

        public Exit(ParkingMonitor monitor, string vehicleType)
        {
            this.monitor = monitor;
            this.vehicleType = vehicleType;
            open = true;
        }

        public void Open()
        {
            Random sleepTime = new Random();

            while (open)
            {
                if (vehicleType.Equals("Truck"))
                {
                    monitor.ExitTruck();
                }
                else 
                {
                    monitor.ExitCar();
                }
                Thread.Sleep(sleepTime.Next(10, 500));
            }
        }

        public void Close()
        {
            open = false;
        }
    }
}
