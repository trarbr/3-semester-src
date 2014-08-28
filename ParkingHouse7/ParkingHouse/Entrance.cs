using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingHouse
{
    class Entrance
    {
        private ParkingMonitor monitor;
        private volatile bool open;
        private string vehicleType;

        public Entrance(ParkingMonitor monitor, string vehicleType)
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
                    monitor.EnterTruck();
                }
                else
                {
                    monitor.EnterCar();
                }
                Thread.Sleep(sleepTime.Next(10, 800));
            }
        }

        public void Close()
        {
            open = false;
        }
    }
}
