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

            decimal eurAmount = valutaService.FromDkkToEur(dkkAmount);

            Console.WriteLine("In EUR, that is " + eurAmount.ToString("N2"));
            Console.ReadLine();

            Console.Write("Enter ISO code you want exchange rate for: ");
            string iso = Console.ReadLine();

            decimal exchangeRate = valutaService.GetExchangeRate(iso);

            Console.WriteLine("The exchange rate is " + exchangeRate.ToString("N2"));
            Console.ReadLine();
        }
    }
}
