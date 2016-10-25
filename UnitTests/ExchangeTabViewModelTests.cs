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
    public class ExchangeTabViewModelTests
    {

        [Test]
        public void ConfiguredExchangesInitTest()
        {
            MockDataManager mDm = new MockDataManager();
            ViewModelMediator.GetMediator().dataManager = mDm;
            mDm.stockDataBL = new MockStockDataBusinessLogic();
            mDm.ExchangesConfigured.Add("NSE");
            ExchangeTabViewModel vm = new ExchangeTabViewModel();
            Assert.AreEqual(vm.ListOfExchanges.Count, 1);
            Assert.AreSame(vm.ListOfExchanges[0], "NSE");
        }

        [Test]
        public void SelectionTest()
        {
            IStockDataBusinessLogic dataBL = new MockStockDataBusinessLogic();
            MockDataManager mDm = new MockDataManager();
            ViewModelMediator.GetMediator().dataManager = mDm;
            mDm.stockDataBL = new MockStockDataBusinessLogic();
            mDm.ExchangesConfigured.Add("NSE");
            ExchangeTabViewModel vm = new ExchangeTabViewModel();
            MockStockListViewModel listVm = new MockStockListViewModel();
            ViewModelMediator.GetMediator().RegisterForAction("SelectedExchange", false, listVm);
            vm.SelectedExchange = "NSE";
            Assert.AreSame(vm.SelectedExchange, listVm.SelectedExchange);
        }
    }
}
