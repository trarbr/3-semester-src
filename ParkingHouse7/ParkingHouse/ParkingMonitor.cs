using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingHouse
{
    class ParkingMonitor
    {
        private const int parkingPlacesForCars = 5;
        private int parkedCars;
        private Object parkedCarsLock;

        public ParkingMonitor()
        {
            parkedCarsLock = new Object();
        }

        public void EnterCar()
        {
            lock (parkedCarsLock)
            {
                if (parkedCars < parkingPlacesForCars)
                {
                    parkedCars++;
                    Console.WriteLine("Car entered! Number of parked cars: " + parkedCars);
                }
            }
        }

        public void ExitCar()
        {
            lock (parkedCarsLock)
            {
                if (parkedCars > 0)
                {
                    parkedCars--;
                    Console.WriteLine("Car left! Number of parked cars: " + parkedCars);
                }
            }
        }
    }
}
