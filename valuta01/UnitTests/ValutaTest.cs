using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValutaWcfService;

namespace UnitTests
{
    [TestClass]
    public class ValutaTest
    {
        [TestMethod]
        public void Test_FromDkkToEur()
        {
            IValutaService service = new ValutaService();
            decimal expectedEur = 13.40500542902719875601549619m;

            decimal actualEur = service.FromDkkToEur(100);

            Assert.AreEqual(expectedEur, actualEur);
        }

        [TestMethod]
        public void Test_GetExchangeRate()
        {
            IValutaService service = new ValutaService();
            decimal expectedRate = 492.27m;

            decimal actualRate = service.GetExchangeRate("CAD");

            Assert.AreEqual(expectedRate, actualRate);
        }

        [TestMethod]
        public void Test_ConvertFromIsoToIso()
        {
            IValutaService service = new ValutaService();
            decimal expectedAmount = 100;

            decimal actualAmount = service.ConvertFromIsoToIso("EUR", "USD", 100);

            Assert.AreEqual(expectedAmount, actualAmount);
        }
    }
}
