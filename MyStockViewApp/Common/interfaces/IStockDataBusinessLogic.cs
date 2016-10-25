using System.Collections.Generic;

namespace MyStockViewApp
{
    public interface IStockDataBusinessLogic
    {
        List<StockData> GetStockDetails(string Subscription);
        List<string> GetCompaniesList(string exchange);
    }
}
