using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyStockViewApp;

namespace UnitTests.Mocks
{
    public class MockDataManager : IDataManager
    {
        public IStatusInfoManager statusManager = Utilities.StatusManager;
        public ILogManager logger = Utilities.LogManager;
        public IStockDataBusinessLogic stockDataBL;

        public List<StockData> Stocks = new List<StockData>();
        public List<string> AllExchanges = new List<string>();
        public List<string> ExchangesConfigured = new List<string>();
        public Dictionary<string, List<string>> ExchangeCompaniesMap = new Dictionary<string, List<string>>();

        public string Subscription = String.Empty;
        //private readonly BackgroundWorker workerThread = new BackgroundWorker();
        private Object locker = new object();

        public MockDataManager()
        {
            AllExchanges.Add("NSE");
            AllExchanges.Add("NASDAQ");
            AllExchanges.Add("BOM");

        }

        public void Subscribe(string Exchange, string symbol)
        {
            //ExchangesConfigured.Add(Exchange);
            //string stock = Exchange + ':' + symbol + ',';
            //if (Subscription.Contains(stock) == false)
            //    Subscription += stock;
            //BackgroundWorker GetStockSymbolsWorker = new BackgroundWorker();
            //GetStockSymbolsWorker.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
            //{
            //    try
            //    {
            //        GetCompanySymbols(Exchange);
            //        SettingsManager.WriteToFile(Exchange, symbol);
            //    }
            //    catch (Exception ex)
            //    {
            //        string str = "Error while subscribing to the stock.\n";
            //        statusManager.SetStatus(new Status(str, ex));
            //        logger.Write(str + ex.Message);
            //    }
            //});
            //GetStockSymbolsWorker.RunWorkerAsync();

        }

        public void Unsubscribe(string Exchange, string symbol)
        {
            //try
            //{
            //    string stock = Exchange + ':' + symbol + ',';
            //    if (Subscription.Contains(stock))
            //        Subscription = Subscription.Replace(stock, "");
            //    SettingsManager.DeleteFromFile(Exchange, symbol);
            //}
            //catch (Exception ex)
            //{
            //    string str = "Error while unsubscribing the stock.\n";
            //    statusManager.SetStatus(new Status(str, ex));
            //    logger.Write(str + ex.Message);
            //}

        }
        public List<StockData> GetStocks(string Exchange)
        {
            List<StockData> filteredStockData = new List<StockData>();
            lock (locker)
            {
                filteredStockData = Stocks.Where(s => s.Exchange == Exchange).ToList();
            }
            return filteredStockData;
        }

        public List<string> GetExchangesConfigured()
        {
            return ExchangesConfigured;
        }
        public void GetAllExchanges(ExchangeListCallback exchangeListCallback)
        {
            exchangeListCallback(this.AllExchanges);           
        }

        public List<string> GetCompanySymbols(string exchange)
        {
                if (ExchangeCompaniesMap.ContainsKey(exchange))
                    return ExchangeCompaniesMap[exchange];
                return new List<string>();
        }

        public void SetSubscription(string sub)
        {
            this.Subscription = sub;
        }
    }

}
