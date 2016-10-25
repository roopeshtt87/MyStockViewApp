using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyStockViewApp;
using System.Collections.ObjectModel;

namespace UnitTests.Mocks
{
    public class MockStockListViewModel : IExchangeSelection
    {
        public ObservableCollection<StockData> StockDetailsList { get; set; }
        public ObservableCollection<string> ListOfAllCompanies { get; set; }
        public string NewSymbol;
        public string SelectedExchange
        {
            get;
            set;
        }
        //BackgroundWorker updateThread = new BackgroundWorker();

        public MockStockListViewModel()
        {

            //mediator.RegisterForAction("SelectedExchange", false, this);

            //AddCommand = new RelayCommand(ExecuteAddCommand);
            //DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
            StockDetailsList = new ObservableCollection<StockData>();
            ListOfAllCompanies = new ObservableCollection<string>();

            //updateThread.WorkerSupportsCancellation = true;
            //#region SelectedExchange
            //this.PropertyChanged += (obj, args) =>
            //{
            //    if (args.PropertyName == "SelectedExchange")
            //    {
            //        try
            //        {
            //            this.currentDispatcher.BeginInvoke((Action)delegate()
            //            {
            //                ListOfAllCompanies.Clear();
            //                BackgroundWorker bw = new BackgroundWorker();
            //                bw.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
            //                {
            //                    e.Result = dataManager.GetCompanySymbols(SelectedExchange);
            //                });
            //                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler((object sender, RunWorkerCompletedEventArgs e) =>
            //                {
            //                    (e.Result as List<string>).ForEach(i => ListOfAllCompanies.Add(i));
            //                });
            //                bw.RunWorkerAsync();
            //                StockDetailsList.Clear();
            //                dataManager.GetStocks(SelectedExchange).ForEach(i => StockDetailsList.Add(i));
            //            });

            //        }
            //        catch (Exception ex)
            //        {
            //            string str = "Failed to get stock information";
            //            statusManager.SetStatus(new Status(str, ex));
            //            logger.Write(str + ex.Message);
            //        }
            //    }
            //};
            //#endregion

            //#region Update stocks
            //try
            //{
            //    updateThread.DoWork += (object sender, DoWorkEventArgs e) =>
            //    {

            //        while (true)
            //        {
            //            try
            //            {
            //                Thread.Sleep(5000);
            //                this.currentDispatcher.BeginInvoke((Action)delegate()
            //                {
            //                    List<string> selectedItems = new List<string>();
            //                    selectedItems = StockDetailsList.Where(s => s.IsSelected == true).Select(s => s.CompanyNameShort).ToList();

            //                    StockDetailsList.Clear();
            //                    List<StockData> stockList = dataManager.GetStocks(SelectedExchange);

            //                    foreach (StockData s in stockList)
            //                    {
            //                        if (selectedItems.Contains(s.CompanyNameShort))
            //                            s.IsSelected = true;
            //                        StockDetailsList.Add(s);
            //                    }
            //                });

            //            }
            //            catch (Exception ex)
            //            {
            //                string str = "Failed to update stock information";
            //                statusManager.SetStatus(new Status(str, ex));
            //                logger.Write(str + ex.Message);
            //            }
            //        }
            //    };
            //    updateThread.RunWorkerAsync();
            //#endregion
            //}
            //catch (Exception ex)
            //{
            //    string str = "Failed to initialize the view";
            //    statusManager.SetStatus(new Status(str, ex));
            //    logger.Write(str + ex.Message);
            //}
        }

        //public ICommand AddCommand { get; set; }
        //public void ExecuteAddCommand(object obj)
        //{
        //    try
        //    {
        //        if (StockDetailsList.Where(i => i.CompanyNameShort == NewSymbol).ToList().Count != 0)
        //        {
        //            statusManager.SetStatus(new Status("Stock symbol already added !!", StatusSeverity.Information, null));
        //        }
        //        else
        //        {
        //            dataManager.Subscribe(SelectedExchange, NewSymbol);
        //            StockDetailsList.Add(new StockData() { Exchange = SelectedExchange, CompanyNameShort = NewSymbol });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = "Failed to add new exchange\n";
        //        statusManager.SetStatus(new Status(str, ex));
        //        logger.Write(str + ex.Message);
        //    }
        //}

        //public ICommand DeleteCommand { get; set; }
        //public void ExecuteDeleteCommand(object obj)
        //{
        //    try
        //    {
        //        List<string> selectedItems = new List<string>();
        //        selectedItems = StockDetailsList.Where(s => s.IsSelected == true).Select(s => s.CompanyNameShort).ToList();
        //        foreach (string sym in selectedItems)
        //        {
        //            StockDetailsList.Remove(StockDetailsList.Single(i => i.CompanyNameShort == sym));
        //            dataManager.Unsubscribe(SelectedExchange, sym);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = "Failed to delete stock(company) symbol\n";
        //        statusManager.SetStatus(new Status(str, ex));
        //        logger.Write(str + ex.Message);
        //    }
        //}

    }

    //public class MockStockListViewModel : IDataManager
    //{
    //    public IStatusInfoManager statusManager = Utilities.StatusManager;
    //    public ILogManager logger = Utilities.LogManager;
    //    public IStockDataBusinessLogic stockDataBL;

    //    public List<StockData> Stocks = new List<StockData>();
    //    public List<string> AllExchanges = new List<string>();
    //    public List<string> ExchangesConfigured = new List<string>();
    //    public Dictionary<string, List<string>> ExchangeCompaniesMap = new Dictionary<string, List<string>>();

    //    public string Subscription = String.Empty;
    //    //private readonly BackgroundWorker workerThread = new BackgroundWorker();
    //    private Object locker = new object();

    //    public MockStockListViewModel()
    //    {
    //        AllExchanges.Add("NSE");
    //        AllExchanges.Add("NASDAQ");
    //        AllExchanges.Add("BOM");

    //    }

    //    public void Subscribe(string Exchange, string symbol)
    //    {
    //        //ExchangesConfigured.Add(Exchange);
    //        //string stock = Exchange + ':' + symbol + ',';
    //        //if (Subscription.Contains(stock) == false)
    //        //    Subscription += stock;
    //        //BackgroundWorker GetStockSymbolsWorker = new BackgroundWorker();
    //        //GetStockSymbolsWorker.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
    //        //{
    //        //    try
    //        //    {
    //        //        GetCompanySymbols(Exchange);
    //        //        SettingsManager.WriteToFile(Exchange, symbol);
    //        //    }
    //        //    catch (Exception ex)
    //        //    {
    //        //        string str = "Error while subscribing to the stock.\n";
    //        //        statusManager.SetStatus(new Status(str, ex));
    //        //        logger.Write(str + ex.Message);
    //        //    }
    //        //});
    //        //GetStockSymbolsWorker.RunWorkerAsync();

    //    }

    //    public void Unsubscribe(string Exchange, string symbol)
    //    {
    //        //try
    //        //{
    //        //    string stock = Exchange + ':' + symbol + ',';
    //        //    if (Subscription.Contains(stock))
    //        //        Subscription = Subscription.Replace(stock, "");
    //        //    SettingsManager.DeleteFromFile(Exchange, symbol);
    //        //}
    //        //catch (Exception ex)
    //        //{
    //        //    string str = "Error while unsubscribing the stock.\n";
    //        //    statusManager.SetStatus(new Status(str, ex));
    //        //    logger.Write(str + ex.Message);
    //        //}

    //    }
    //    public List<StockData> GetStocks(string Exchange)
    //    {
    //        List<StockData> filteredStockData = new List<StockData>();
    //        lock (locker)
    //        {
    //            filteredStockData = Stocks.Where(s => s.Exchange == Exchange).ToList();
    //        }
    //        return filteredStockData;
    //    }

    //    public List<string> GetExchangesConfigured()
    //    {
    //        return ExchangesConfigured;
    //    }
    //    public void GetAllExchanges(ExchangeListCallback exchangeListCallback)
    //    {
    //        exchangeListCallback(this.AllExchanges);           
    //    }

    //    public List<string> GetCompanySymbols(string exchange)
    //    {
    //            if (ExchangeCompaniesMap.ContainsKey(exchange))
    //                return ExchangeCompaniesMap[exchange];
    //            return new List<string>();
    //    }

    //    public void SetSubscription(string sub)
    //    {
    //        this.Subscription = sub;
    //    }
    //}
}
