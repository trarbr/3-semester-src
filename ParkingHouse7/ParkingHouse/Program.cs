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

            Entrance entrance = new Entrance(monitor);
            Exit exit = new Exit(monitor);

            Thread entranceThread = new Thread(entrance.Open);
            Thread exitThread = new Thread(exit.Open);

            entranceThread.Start();
            exitThread.Start();

            Console.ReadLine();

            entrance.Close();
            exit.Close();

            Console.ReadLine();
        }
    }
}
