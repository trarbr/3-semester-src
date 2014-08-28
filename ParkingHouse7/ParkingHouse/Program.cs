using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            ParkingMonitor monitor = new ParkingMonitor();

            Entrance carEntrance = new Entrance(monitor, "Car");
            Exit carExit = new Exit(monitor, "Car");
            Entrance truckEntrance = new Entrance(monitor, "Truck");
            Exit truckExit = new Exit(monitor, "Truck");

            Thread carEntranceThread = new Thread(carEntrance.Open);
            Thread carExitThread = new Thread(carExit.Open);
            Thread truckEntranceThread = new Thread(truckEntrance.Open);
            Thread truckExitThread = new Thread(truckExit.Open);

            carEntranceThread.Start();
            carExitThread.Start();
            truckEntranceThread.Start();
            truckExitThread.Start();

            Console.ReadLine();

            carEntrance.Close();
            carExit.Close();
            truckEntrance.Close();
            truckExit.Close();

            Console.ReadLine();
        }
    }
}
