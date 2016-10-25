using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MyStockViewApp;
using NUnit.Gui;
using System.Reflection;
using UnitTests.Mocks;
using System.Threading;

namespace UnitTests
{

    [TestFixture]
    public class DataManagerTests
    {

        [Test]
        public void DMGetStocksTest()
        {
            IStockDataBusinessLogic dataBL = new MockStockDataBusinessLogic();
            DataManager dataManager = new DataManager();
            dataManager.GetCompanySymbols("NSE");
            dataManager.Subscribe("NSE", "TCS");
            dataManager.Subscribe("NSE", "ONGC");
            dataManager.Subscribe("NSE", "KSCL");
            Thread.Sleep(6000);
            List<StockData> stocks = dataManager.GetStocks("NSE");
            Assert.AreEqual(stocks.Count, 3);
            //Assert.AreEqual(stocks[0].CompanyNameShort, "TCS");
            Assert.IsNotEmpty(stocks.Where(i => i.CompanyNameShort =="TCS"));
            Assert.IsNotEmpty(stocks.Where(i => i.CompanyNameShort == "ONGC"));
            Assert.IsNotEmpty(stocks.Where(i => i.CompanyNameShort == "KSCL"));
        }


    }
}
