using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingHouse
{
    class ParkingMonitor
    {
        private const int parkingPlacesForCars = 5;
        private int parkedCars;
        private Object parkedCarsLock;
        private const int parkingPlacesForTrucks = 3;
        private int parkedTrucks;
        private Object parkedTrucksLock;

        public ParkingMonitor()
        {
            parkedCarsLock = new Object();
            parkedTrucksLock = new Object();
        }

        public void EnterCar()
        {
            lock (parkedCarsLock)
            {
                // Busy waiting: 
                //if (parkedCars < parkingPlacesForCars)
                //{
                //    parkedCars++;
                //    Console.WriteLine("Car entered! Number of parked cars: " + parkedCars);
                //}

                // Wait and pulse:
                while (parkedCars == parkingPlacesForCars)
                {
                    Console.WriteLine("Too many cars, can't enter");
                    Monitor.Wait(parkedCarsLock);
                }
                if (parkedCars == 0)
                {
                    Console.WriteLine("Cars can now leave again!");
                    Monitor.PulseAll(parkedCarsLock);
                }
                parkedCars++;
                Console.WriteLine("Car entered! Number of parked cars: " + parkedCars);
            }
        }

        public void ExitCar()
        {
            lock (parkedCarsLock)
            {
                while (parkedCars == 0)
                {
                    Console.WriteLine("No cars, can't exit");
                    Monitor.Wait(parkedCarsLock);
                }
                if (parkedCars == parkingPlacesForCars)
                {
                    Console.WriteLine("Cars can now enter again!");
                    Monitor.PulseAll(parkedCarsLock);
                }
                parkedCars--;
                Console.WriteLine("Car left! Number of parked cars: " + parkedCars);
            }
        }

        public void EnterTruck()
        {
            lock (parkedTrucksLock)
            {
                while (parkedTrucks == parkingPlacesForTrucks)
                {
                    Console.WriteLine("Too many trucks, can't enter");
                    Monitor.Wait(parkedTrucksLock);
                }
                if (parkedTrucks == 0)
                {
                    Console.WriteLine("Trucks can now leave again!");
                    Monitor.PulseAll(parkedTrucksLock);
                }
                parkedTrucks++;
                Console.WriteLine("Truck entered! Number of parked trucks: " + parkedTrucks);
            }
        }

        public void ExitTruck()
        {
            lock (parkedTrucksLock)
            {
                while (parkedTrucks == 0)
                {
                    Console.WriteLine("No trucks, can't exit");
                    Monitor.Wait(parkedTrucksLock);
                }
                if (parkedTrucks == parkingPlacesForTrucks)
                {
                    Console.WriteLine("Trucks can now enter again!");
                    Monitor.PulseAll(parkedTrucksLock);
                }
                parkedTrucks--;
                Console.WriteLine("Truck left! Number of parked trucks: " + parkedTrucks);
            }
        }
    }
}
