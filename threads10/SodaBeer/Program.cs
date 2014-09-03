using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SodaBeer
{
    class Program
    {
        static void Main(string[] args)
        {
            RunSimulation();

            Console.WriteLine("Simulation ended.");
            Console.ReadLine();
        }

        static void RunSimulation()
        {
            Conveyor sodaBeerConveyor = new Conveyor("SodaBeer", 50);
            Conveyor beerConveyor = new Conveyor("Beer", 30);
            Conveyor sodaConveyor = new Conveyor("Soda", 30);

            SodaBeerProducer sodaBeerProducer = new SodaBeerProducer(sodaBeerConveyor);
            SodaBeerSplitter sodaBeerSplitter = new SodaBeerSplitter(sodaBeerConveyor, sodaConveyor, beerConveyor);
            SodaBeerConsumer sodaConsumer = new SodaBeerConsumer("Soda", sodaConveyor);
            SodaBeerConsumer beerConsumer = new SodaBeerConsumer("Beer", beerConveyor);

            Thread producerThread = new Thread(sodaBeerProducer.StartProduction);
            Thread splitterThread = new Thread(sodaBeerSplitter.StartProduction);
            Thread sodaConsumerThread = new Thread(sodaConsumer.StartProduction);
            Thread beerConsumerThread = new Thread(beerConsumer.StartProduction);

            Console.WriteLine("Press enter to end simulation.");

            producerThread.Start();
            splitterThread.Start();
            sodaConsumerThread.Start();
            beerConsumerThread.Start();

            Console.ReadLine();

            sodaBeerProducer.StopProduction();
            sodaBeerSplitter.StopProduction();
            sodaConsumer.StopProduction();
            beerConsumer.StopProduction();

            producerThread.Join();
            splitterThread.Join();
            sodaConsumerThread.Join();
            beerConsumerThread.Join();
        }
    }
}
