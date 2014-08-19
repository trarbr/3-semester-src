using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2Uge34
{
    class Program
    {
        static void Main(string[] args)
        {
            List<double> numbers = new List<double>() { 1, 2, 3, 4, 5, 6, 7, 8 };

            Console.WriteLine("Average " + ListeBeregner.Gennemsnit(numbers));
            Console.WriteLine("Next smallest number " + ListeBeregner.FindNextSmallestNumber(numbers));
            Console.WriteLine("Lowest quartile " + ListeBeregner.CalculateLowerQuartile(numbers));

            Console.ReadLine();
        }
    }
}
