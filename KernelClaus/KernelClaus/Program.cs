using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KernelClaus
{
    class Program
    {
        static int years;
        static int elfCounter;
        static int reindeerCounter;
        static Mutex theDoor;
        static SemaphoreSlim elfDoorSemaphore;
        static SemaphoreSlim santaSemaphore;
        static SemaphoreSlim elfSemaphore;
        static SemaphoreSlim reindeerSemaphore;
        
        static void Main(string[] args)
        {
            theDoor = new Mutex();
            elfDoorSemaphore = new SemaphoreSlim(1);
            santaSemaphore = new SemaphoreSlim(0);
            elfSemaphore = new SemaphoreSlim(0);
            reindeerSemaphore = new SemaphoreSlim(9);

            Thread santaThread = new Thread(goSanta);
            santaThread.Start();

            Thread elfStarter = new Thread(startElves);
            elfStarter.Start();

            Thread reindeerStarter = new Thread(startReindeer);
            reindeerStarter.Start();

            Console.ReadLine();
            santaThread.Abort();
            elfStarter.Abort();
            reindeerStarter.Abort();

            Console.WriteLine("Everyone is dead. No one believes in Santa anymore.");
            Console.ReadLine();
        }

        private static void startReindeer(object obj)
        {
            for (int i = 0; i < 9; i++)
            {
                new Thread(goReindeer).Start();
            }
        }

        private static void startElves(object obj)
        {
            while (true)
            {
                Thread.Sleep(100);
                new Thread(goElves).Start();
            }
        }

        static void goSanta()
        {
            while (true)
            {
                santaSemaphore.Wait();
                Console.WriteLine("Santa got signal!");
                theDoor.WaitOne();
                if (reindeerCounter == 9)
                {
                    reindeerCounter = 0;
                    prepSleigh();
                    reindeerSemaphore.Release(9);
                }
                else
                {
                    elfSemaphore.Release(3);
                    helpElves();
                }

                theDoor.ReleaseMutex();
            }
        }

        static void goReindeer()
        {
            while (true)
            {
                Thread.Sleep(1000);
                theDoor.WaitOne();
                reindeerCounter++;
                if (reindeerCounter == 9)
                {
                    santaSemaphore.Release();
                }
                theDoor.ReleaseMutex();
                reindeerSemaphore.Wait();
                getHitched();
            }
        }

        static void goElves()
        {
            elfDoorSemaphore.Wait();
            theDoor.WaitOne();
            elfCounter++;   
            if (elfCounter == 3)
            {
                // Bug: Santa doesn't wake up?!
                santaSemaphore.Release();
                Console.WriteLine("3 elves");
            }
            else
            {
                elfDoorSemaphore.Release();
            }
            theDoor.ReleaseMutex();
            elfSemaphore.Wait();
            getHelp();
            theDoor.WaitOne();
            elfCounter--;
            if (elfCounter == 0)
            {
                elfDoorSemaphore.Release();
            }
            theDoor.ReleaseMutex();
            Console.WriteLine("Elf done");
        }

        private static void getHelp()
        {
            Console.WriteLine("Getting help");
        }

        private static void helpElves()
        {
            Console.WriteLine("Helping elves");
        }

        private static void prepSleigh()
        {
            Console.WriteLine("Prepping sleigh");
            Console.WriteLine("Year {0} passed", years++);
        }

        private static void getHitched()
        {
            Console.WriteLine("Getting hitched");
        }
    }
}
