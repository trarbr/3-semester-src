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
            ValutaService service = new ValutaService();
            decimal expectedEur = 13.40500542902719875601549619m;

            decimal actualEur = service.FromDkkToEur(100);

            Assert.AreEqual(expectedEur, actualEur);
        }
    }
}
