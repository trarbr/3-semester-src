using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWcfConsole.PersonWcfService;

namespace TestWcfConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //PersonWcfServiceClient proxy = new PersonWcfServiceClient();

            //PersonDTO[] persons = proxy.GetAllePersoner();

            //foreach (PersonDTO person in persons)
            //{
            //    Console.WriteLine("{0} {1} {2} {3}", 
            //        person.Adresse, person.Distrikt, person.Fornavn, person.Efternavn);
            //}

            //Console.ReadLine();

            CalculationService.CalculationServiceClient proxy2 = new CalculationService.CalculationServiceClient();

            Console.WriteLine(proxy2.IntegerAddition(3, 5));

            Console.ReadLine();
        }
    }
}
