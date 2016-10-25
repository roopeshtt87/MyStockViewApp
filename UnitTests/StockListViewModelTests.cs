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
    public class StockListViewModelTests
    {

        [Test]
        public void InitTest()
        {
            MockDataManager mDm = new MockDataManager();
            mDm.ExchangesConfigured.Add("ABC");
            mDm.ExchangesConfigured.Add("DEF");
            ViewModelMediator.GetMediator().dataManager = mDm;
            mDm.stockDataBL = new MockStockDataBusinessLogic();
            mDm.ExchangesConfigured.Add("NSE");
            StockListViewModel listVm = new StockListViewModel();
            listVm.SelectedExchange = "NSE";
            Assert.IsTrue(listVm.StockDetailsList.Count >0);
            Assert.IsTrue(listVm.ListOfAllCompanies.Count > 0);
        }

        [Test]
        public void GetStocksTest()
        {
            MockDataManager mDm = new MockDataManager();
            mDm.ExchangesConfigured.Add("ABC");
            mDm.ExchangesConfigured.Add("DEF");
            mDm.Stocks.Add(new StockData() { CompanyNameShort = "TCS", Exchange = "NSE", value = 200, hi52 = 300, lo52 = 100, percentChanged = 1 });
            mDm.Stocks.Add(new StockData() { CompanyNameShort = "ONGC", Exchange = "NSE", value = 210, hi52 = 310, lo52 = 110, percentChanged = 2 });
            mDm.Stocks.Add(new StockData() { CompanyNameShort = "KSCL", Exchange = "NSE", value = 220, hi52 = 320, lo52 = 120, percentChanged = 3 });
            ViewModelMediator.GetMediator().dataManager = mDm;
            mDm.stockDataBL = new MockStockDataBusinessLogic();
            mDm.ExchangesConfigured.Add("NSE");

            StockListViewModel listVm = new StockListViewModel();
            listVm.SelectedExchange = "NSE";
            Thread.Sleep(10000);
            Assert.IsTrue(listVm.StockDetailsList.Count == 3);
        }

    }
}
