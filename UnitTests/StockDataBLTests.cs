using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MyStockViewApp;
using NUnit.Gui;
using System.Reflection;
using UnitTests.Mocks;

namespace UnitTests
{
    [TestFixture]
    public class StockDataBLTests
    {
        [Test]
        public void GetStocksDetailsTest()
        {
            IStockDataBusinessLogic dataBL = new GoogleFinanceDataBL();
            List<StockData> stockDetails = new List<StockData>();
            stockDetails = dataBL.GetStockDetails("");
            Assert.IsEmpty(stockDetails);

            stockDetails = dataBL.GetStockDetails("NSE:TCS,NSE:ONGC,");
            Assert.True(stockDetails.Count == 2);
            foreach (var v in stockDetails)
            {
                Assert.IsNotNullOrEmpty(v.CompanyNameShort);
                Assert.IsNotNullOrEmpty(v.Exchange);
                Assert.IsNotNull(v.value);
                Assert.IsNotNull(v.percentChanged);
                Assert.IsNotNull(v.hi52);
                Assert.IsNotNull(v.lo52);
            }
        }

        [Test]
        public void GetCompaniesListTest()
        {
            IStockDataBusinessLogic dataBL = new GoogleFinanceDataBL();
            List<string> companies = new List<string>();
            companies = dataBL.GetCompaniesList("NSE");
            Assert.IsNotEmpty(companies);

            companies = dataBL.GetCompaniesList("ABCDE");
            Assert.IsEmpty(companies);
        }
    }
}
