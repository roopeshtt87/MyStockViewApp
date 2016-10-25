using System.Collections.Generic;

namespace MyStockViewApp
{
    public delegate void ExchangeListCallback(List<string> list);

    public interface IDataManager
    {
        List<string> GetExchangesConfigured();
        void GetAllExchanges(ExchangeListCallback exchangeListCallback);
        List<StockData> GetStocks(string Exchange);
        void Subscribe(string Exchange, string symbol);
        void Unsubscribe(string Exchange, string symbol);
        List<string> GetCompanySymbols(string Exchange);
    }

    public interface IExchangeSelection
    {
        string SelectedExchange
        {
            get;
            set;
        }
    }

}
