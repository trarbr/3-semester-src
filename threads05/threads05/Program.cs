using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace threads05
{
    class Program
    {
        static int counter;

        static void Main(string[] args)
        {
            Thread thread1 = new Thread(printStars);
            Thread thread2 = new Thread(printHashes);

            thread1.Start();
            thread2.Start();

            Console.ReadLine();
        }

        static void printStars()
        {
            for (int i = 0; i < 100; i++)
            {
                lock ((object)counter)
                {
                    counter += 60;
                    Console.WriteLine("************************************************************ " + counter);
                    Thread.Sleep(100);
                }
            }
        }

        static void printHashes()
        {
            for (int i = 0; i < 100; i++)
            {
                lock ((object)counter)
                {
                    counter += 60;
                    Console.WriteLine("############################################################ " + counter);
                    Thread.Sleep(100);
                }
            }
        }

    }
}
