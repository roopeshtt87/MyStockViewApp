using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MyStockViewApp
{
    public class ExchangeTabViewModel : ViewModelBase, IExchangeSelection 
    {
        public ObservableCollection<string> ListOfExchanges { get; set; }
        public ObservableCollection<string> ListOfAllExchanges { get; set; }
        
        public ICommand AddExchCommand { get; set; }
        public ICommand DeleteExchCommand { get; set; }
        
        private string newExchSymbol;
        public string NewExchSymbol
        {
            get { return newExchSymbol;}
            set
            {
                newExchSymbol = value;
                OnPropertyChanged("NewExchSymbol");
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
        

        public ExchangeTabViewModel()
        {
            try
            {
                ListOfAllExchanges = new ObservableCollection<string>();
                ListOfExchanges = new ObservableCollection<string>();


                AddExchCommand = new RelayCommand(ExecuteAddExchCommand);
                DeleteExchCommand = new RelayCommand(ExecuteDeleteExchCommand);
                ExchangeListCallback exchangeListCallback = new ExchangeListCallback((List<string> list) =>
                                                                                     {
                                                                                         this.currentDispatcher.BeginInvoke((Action)delegate()
                                                                                            {
                                                                                                list.ForEach(i => ListOfAllExchanges.Add(i));
                                                                                            });
                                                                                     });
                this.PropertyChanged += (obj, args) =>
                                        {
                                            if (args.PropertyName == "SelectedExchange")
                                            {
                                                mediator.Action = new PropertyAction() { PropertyName = "SelectedExchange", value = SelectedExchange };
                                            }
                                        };

                dataManager.GetAllExchanges(exchangeListCallback);
                dataManager.GetExchangesConfigured().ForEach(i => ListOfExchanges.Add(i));

                mediator.RegisterForAction("SelectedExchange", true, this);
            }
            catch (Exception ex)
            {
                string str = "Failed to initialize ExchangeTabViewModel";
                statusManager.SetStatus(new Status(str, ex));
                logger.Write(str + ex.Message);
            }
        }
        public void ExecuteAddExchCommand(object obj)
        {
            try
            {
                if (ListOfExchanges.Contains(NewExchSymbol))
                {
                    statusManager.SetStatus(new Status("Exchange symbol already added !!", StatusSeverity.Information, null));
                }
                else if(String.IsNullOrWhiteSpace(NewExchSymbol))
                {
                    statusManager.SetStatus(new Status("Select an exchange symbol !!", StatusSeverity.Information, null));
                }
                else
                {
                    ListOfExchanges.Add(NewExchSymbol);
                }
            }
            catch (Exception ex)
            {
                string str = "Failed to add new exchange\n";
                statusManager.SetStatus(new Status(str, ex));
                logger.Write(str + ex.Message);
            }
        }
        public void ExecuteDeleteExchCommand(object obj)
        {
            try
            {
                if (ListOfExchanges.Contains(SelectedExchange))
                {
                    ListOfExchanges.Remove(SelectedExchange);
                }
                else
                {
                    statusManager.SetStatus(new Status("Exchange symbol does not exists !!", StatusSeverity.Information, null));
                }
            }
            catch (Exception ex)
            {
                string str = "Failed to delete exchange";
                statusManager.SetStatus(new Status(str, ex));
                logger.Write(str + ex.Message);
            }

        }

    }
}
