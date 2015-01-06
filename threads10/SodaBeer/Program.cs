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
            SodaBeerConsumer sodaConsumer = new SodaBeerConsumer("Soda", sodaConveyor, 500);
            SodaBeerConsumer beerConsumer = new SodaBeerConsumer("Beer", beerConveyor, 500);

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
            Console.WriteLine("Received shutdown notice.");

            // It is difficult to shut down without a thread getting stuck in waiting.
            // The reason: if any of the conveyors that the thread uses is full or empty at the
            // time of shutdown, the thread is in waiting and thus will never wake up to receive
            // the signal.
            // The below seems to work in most cases. Notable exception: Consumers too fast.
            new Thread(sodaBeerProducer.StopProduction).Start();
            new Thread(sodaBeerSplitter.StopProduction).Start();
            new Thread(sodaConsumer.StopProduction).Start();
            new Thread(beerConsumer.StopProduction).Start();

            Thread.Sleep(1000);

            producerThread.Join();
            splitterThread.Join();
            sodaConsumerThread.Join();
            beerConsumerThread.Join();
        }
    }
}
