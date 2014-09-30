using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValutaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ValutaWcfService.IValutaService valutaService = new ValutaWcfService.ValutaServiceClient();

            Console.Write("Amount of DKK to convert to EUR: ");
            string amountAsString = Console.ReadLine();
            decimal dkkAmount = decimal.Parse(amountAsString);

            decimal eurAmount = valutaService.FromDkkToEur(100);

            Console.WriteLine("In EUR, that is " + eurAmount.ToString("N2"));

            Console.ReadLine();
        }
    }
}
