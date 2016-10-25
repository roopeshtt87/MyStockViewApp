using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyStockViewApp;

namespace UnitTests.Mocks
{
    public class MockStockDataBusinessLogic : IStockDataBusinessLogic
    {

        public List<string> GetCompaniesList(string exchange)
        {
            List<string> companies = new List<string>();
            if (exchange == "NSE")
            {
                companies.Add("TCS");
                companies.Add("ONGC");
                companies.Add("KSCL");
            }
            return companies;
        }

        public List<StockData> GetStockDetails(string Subscription)
        {
            List<StockData> stockDetails = new List<StockData>();
            stockDetails.Add(new StockData() { CompanyNameShort = "TCS", Exchange = "NSE", value = 200, hi52 = 300, lo52 = 100, percentChanged = 1 });
            stockDetails.Add(new StockData() { CompanyNameShort = "ONGC", Exchange = "NSE", value = 210, hi52 = 310, lo52 = 110, percentChanged = 2 });
            stockDetails.Add(new StockData() { CompanyNameShort = "KSCL", Exchange = "NSE", value = 220, hi52 = 320, lo52 = 120, percentChanged = 3 });
            return stockDetails;
        }
    }
}
