using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1Uge34
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person("Frank", 80, 1.70);
            Console.WriteLine(String.Format("{0}, {1}, {2}", person.Name, person.Weight, person.Height));

            Console.ReadLine();
        }
    }
}
