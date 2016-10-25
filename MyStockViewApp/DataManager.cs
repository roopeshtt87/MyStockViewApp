using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Threading;

namespace MyStockViewApp
{
    public class DataManager : IDataManager
    {
        private IStatusInfoManager statusManager = Utilities.StatusManager;
        private ILogManager logger = Utilities.LogManager;
        private IStockDataBusinessLogic stockDataBL;

        private List<StockData> Stocks = new List<StockData>();
        private List<string> AllExchanges = new List<string>();
        private List<string> ExchangesConfigured =null;
        private Dictionary<string, List<string>> ExchangeCompaniesMap = new Dictionary<string, List<string>>();

        private string Subscription = String.Empty;
        private readonly BackgroundWorker workerThread = new BackgroundWorker();
        private Object locker = new object();
       
        public DataManager()
        {
            Initialize();
            
            stockDataBL = new GoogleFinanceDataBL();
            workerThread.DoWork +=  (object sender, DoWorkEventArgs e)=>
                                    {
                                        while (true)
                                        {
                                            try
                                            {
                                                Thread.Sleep(5000);
                                                lock (locker)
                                                {
                                                    Stocks = stockDataBL.GetStockDetails(Subscription);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                string str = "Failed to get stock information\n";
                                                statusManager.SetStatus(new Status(str, ex));
                                                logger.Write(str + ex.Message);
                                            }
                                        }
                                    };
            workerThread.RunWorkerAsync();

        }
        
        public void Subscribe(string Exchange, string symbol)
        {
            if (ExchangesConfigured == null)
                ExchangesConfigured = new List<string>();
            ExchangesConfigured.Add(Exchange);
            string stock = Exchange + ':' + symbol + ',';
            if (Subscription.Contains(stock) == false)
                Subscription += stock;
            BackgroundWorker GetStockSymbolsWorker = new BackgroundWorker();
            GetStockSymbolsWorker.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
                                            {
                                                try
                                                {
                                                    GetCompanySymbols(Exchange);
                                                    SettingsManager.WriteToFile(Exchange, symbol);
                                                }
                                                catch (Exception ex)
                                                {
                                                    string str = "Error while subscribing to the stock.\n";
                                                    statusManager.SetStatus(new Status(str, ex));
                                                    logger.Write(str + ex.Message);
                                                }
                                            });
            GetStockSymbolsWorker.RunWorkerAsync();
            
        }
        
        public void Unsubscribe(string Exchange, string symbol)
        {
            try
            {
                string stock = Exchange + ':' + symbol + ',';
                if (Subscription.Contains(stock))
                    Subscription = Subscription.Replace(stock, "");
                SettingsManager.DeleteFromFile(Exchange, symbol);
            }
            catch (Exception ex)
            {
                string str = "Error while unsubscribing the stock.\n";
                statusManager.SetStatus(new Status(str, ex));
                logger.Write(str + ex.Message);
            }
            
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
            lock (locker)
            {
                if (ExchangesConfigured == null)
                {
                    ExchangesConfigured = new List<string>();
                    if (!String.IsNullOrWhiteSpace(Subscription))
                    {
                        string[] stockIds = Subscription.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string stock in stockIds)
                        {
                            string e = stock.Substring(0, stock.IndexOf(':'));
                            if (!ExchangesConfigured.Contains(e))
                                ExchangesConfigured.Add(e);
                        }
                    }
                }
            }

            return ExchangesConfigured;
        }
        public void GetAllExchanges(ExchangeListCallback exchangeListCallback)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) => 
                                                {
                                                    try
                                                    {
                                                        this.AllExchanges = GoogleFinanceDataBL.GetExchanges();
                                                        exchangeListCallback(this.AllExchanges);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        string str = "Error in getting the exchange info.\n";
                                                        statusManager.SetStatus(new Status(str, ex));
                                                        logger.Write(str + ex.Message);
                                                    }                                                    
                                                });
            bw.RunWorkerAsync();
        }

        public List<string> GetCompanySymbols(string exchange)
        {
            try
            {
                if (exchange == null)
                    return new List<string>();
                if (ExchangeCompaniesMap.ContainsKey(exchange))
                    return ExchangeCompaniesMap[exchange];
                else
                    return ExchangeCompaniesMap[exchange] = stockDataBL.GetCompaniesList(exchange);
            }
            catch (Exception ex)
            {
                string str = "Failed to get stock symbols";
                throw new Exception(str + ex.Message);
            }
        }

        void Initialize()
        {
            try
            {
                InitSubscription();
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
                {
                    GetExchangesConfigured().ForEach(i => this.GetCompanySymbols(i));
                });
                bw.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                string str = "Error initializing.\n";
                statusManager.SetStatus(new Status(str, ex));
                logger.Write(str + ex.Message);
            }   
            
        }
        void InitSubscription()
        {
            this.Subscription = SettingsManager.ReadFile();
        }
    }
}
