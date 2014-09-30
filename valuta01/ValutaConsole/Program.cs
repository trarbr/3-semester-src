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
            string amountAsString;

            //Console.Write("Amount of DKK to convert to EUR: ");
            //amountAsString = Console.ReadLine();
            //decimal dkkAmount = decimal.Parse(amountAsString);
            //decimal eurAmount = valutaService.FromDkkToEur(dkkAmount);
            //Console.WriteLine("In EUR, that is " + eurAmount.ToString("N2"));
            //Console.ReadLine();

            //Console.Write("Enter ISO code you want exchange rate for: ");
            //string iso = Console.ReadLine();

            //decimal exchangeRate = valutaService.GetExchangeRate(iso);

            //Console.WriteLine("The exchange rate is " + exchangeRate.ToString("N2"));
            //Console.ReadLine();

            //ValutaWcfService.Valuta[] valutas = valutaService.GetValutas();
            //foreach (ValutaWcfService.Valuta valuta in valutas)
            //{
            //    Console.WriteLine(String.Format("{0} ({1}): {2}", 
            //        valuta.Iso, valuta.Name, valuta.ExchangeRate.ToString("N2")));
            //}
            //Console.ReadLine();

            //Console.Write("Amount to convert: ");
            //amountAsString = Console.ReadLine();
            //decimal fromAmount = decimal.Parse(amountAsString);
            //Console.Write("From ISO code: ");
            //string fromIso = Console.ReadLine();
            //Console.Write("To ISO code: ");
            //string toIso = Console.ReadLine();
            //decimal toAmount = valutaService.ConvertFromIsoToIso(fromIso, toIso, fromAmount);
            //Console.WriteLine(String.Format("{0} {1} is {2} {3}", 
            //    fromAmount.ToString("N2"), fromIso, toAmount.ToString("N2"), toIso));
            //Console.ReadLine();

            //valutaService.ConvertFromIsoToIso("EUR", "USD", 100);

            //string[] conversions = valutaService.GetDoneConversions();
            //foreach (string conversion in conversions)
            //{
            //    Console.WriteLine(conversion);
            //}
            //Console.ReadLine();

            foreach (ValutaWcfService.Valuta valuta in valutaService.GetValutas())
            {
                Console.WriteLine(valuta.ExchangeRate);
            }

            Console.ReadLine();

            ValutaWcfService.Valuta valutaToEdit = valutaService.GetValutas()[3];

            valutaToEdit.ExchangeRate = 1000m;

            valutaService.SetValutaExchangeRate(valutaToEdit);

            foreach (ValutaWcfService.Valuta valuta in valutaService.GetValutas())
            {
                Console.WriteLine(valuta.ExchangeRate);
            }

            Console.ReadLine();

            ValutaWcfService.Valuta newValuta = new ValutaWcfService.Valuta()
            {
                Name = "Rusland",
                Iso = "RUB",
                ExchangeRate = 14.8300m,
            };

            valutaService.AddValuta(newValuta);

            foreach (ValutaWcfService.Valuta valuta in valutaService.GetValutas())
            {
                Console.WriteLine(valuta.ExchangeRate);
            }

            Console.ReadLine();

            Console.WriteLine(valutaService.ConvertFromIsoToIso("CAD", "RUB", 1));
            Console.ReadLine();
        }
    }
}
