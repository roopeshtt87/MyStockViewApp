using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading;

namespace MyStockViewApp
{
    public class StockListViewModel : ViewModelBase, IExchangeSelection
    {
        public ObservableCollection<StockData> StockDetailsList { get; set; }
        public ObservableCollection<string> ListOfAllCompanies { get; set; }
        private string newSymbol;
        public string NewSymbol
        {
            get { return newSymbol; }
            set
            {
                newSymbol = value;
                OnPropertyChanged("NewSymbol");
            }
        }
        private string selectedExchange;
        public string SelectedExchange
        {
            get { return selectedExchange; }
            set
            {
                selectedExchange = value;
                OnPropertyChanged("SelectedExchange");
            }
        }

        BackgroundWorker updateThread = new BackgroundWorker();

        public StockListViewModel()
        {

            mediator.RegisterForAction("SelectedExchange", false, this);

            AddCommand = new RelayCommand(ExecuteAddCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
            StockDetailsList = new ObservableCollection<StockData>();
            ListOfAllCompanies = new ObservableCollection<string>();

            updateThread.WorkerSupportsCancellation = true;
            #region SelectedExchange
            this.PropertyChanged += (obj, args) =>
                                    {
                                        if (args.PropertyName == "SelectedExchange")
                                        {
                                            try
                                            {
                                                this.currentDispatcher.BeginInvoke((Action)delegate()
                                                {
                                                    ListOfAllCompanies.Clear();
                                                    BackgroundWorker bw = new BackgroundWorker();
                                                    bw.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
                                                    {
                                                        e.Result = dataManager.GetCompanySymbols(SelectedExchange);
                                                    });
                                                    bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler((object sender, RunWorkerCompletedEventArgs e) =>
                                                    {
                                                        (e.Result as List<string>).ForEach(i => ListOfAllCompanies.Add(i));
                                                    });
                                                    bw.RunWorkerAsync();
                                                    StockDetailsList.Clear();
                                                    dataManager.GetStocks(SelectedExchange).ForEach(i => StockDetailsList.Add(i));
                                                });

                                            }
                                            catch (Exception ex)
                                            {
                                                string str = "Failed to get stock information";
                                                statusManager.SetStatus(new Status(str, ex));
                                                logger.Write(str + ex.Message);
                                            }
                                        }
                                    };
            #endregion

            #region Update stocks
            try
            {
                updateThread.DoWork += (object sender, DoWorkEventArgs e) =>
                                        {

                                            while (true)
                                            {
                                                try
                                                {
                                                    Thread.Sleep(5000);
                                                    this.currentDispatcher.BeginInvoke((Action)delegate()
                                                    {
                                                        List<string> selectedItems = new List<string>();
                                                        selectedItems = StockDetailsList.Where(s => s.IsSelected == true).Select(s => s.CompanyNameShort).ToList();

                                                        StockDetailsList.Clear();
                                                        List<StockData> stockList = dataManager.GetStocks(SelectedExchange);

                                                        foreach (StockData s in stockList)
                                                        {
                                                            if (selectedItems.Contains(s.CompanyNameShort))
                                                                s.IsSelected = true;
                                                            StockDetailsList.Add(s);
                                                        }
                                                    });

                                                }
                                                catch (Exception ex)
                                                {
                                                    string str = "Failed to update stock information";
                                                    statusManager.SetStatus(new Status(str, ex));
                                                    logger.Write(str + ex.Message);
                                                }
                                            }
                                        };
                updateThread.RunWorkerAsync();
            #endregion
            }
            catch (Exception ex)
            {
                string str = "Failed to initialize the view";
                statusManager.SetStatus(new Status(str, ex));
                logger.Write(str + ex.Message);
            }
        }

        public ICommand AddCommand {get; set;}
        public void ExecuteAddCommand(object obj)
        {
            try
            {
                if (StockDetailsList.Where(i => i.CompanyNameShort == NewSymbol).ToList().Count != 0)
                {
                    statusManager.SetStatus(new Status("Stock symbol already added !!", StatusSeverity.Information, null));
                }
                else
                {
                    dataManager.Subscribe(SelectedExchange, NewSymbol);
                    StockDetailsList.Add(new StockData() { Exchange = SelectedExchange, CompanyNameShort = NewSymbol });
                }
            }
            catch (Exception ex)
            {
                string str = "Failed to add new exchange\n";
                statusManager.SetStatus(new Status(str, ex));
                logger.Write(str + ex.Message);
            }
        }

        public ICommand DeleteCommand { get; set; }
        public void ExecuteDeleteCommand(object obj)
        {
            try
            {
                List<string> selectedItems = new List<string>();
                selectedItems = StockDetailsList.Where(s => s.IsSelected == true).Select(s => s.CompanyNameShort).ToList();
                foreach (string sym in selectedItems)
                {
                    StockDetailsList.Remove(StockDetailsList.Single(i => i.CompanyNameShort == sym));
                    dataManager.Unsubscribe(SelectedExchange, sym);
                }
            }
            catch (Exception ex)
            {
                string str = "Failed to delete stock(company) symbol\n";
                statusManager.SetStatus(new Status(str, ex));
                logger.Write(str + ex.Message);
            }
        }
    }
}
