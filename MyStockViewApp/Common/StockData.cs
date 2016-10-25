
namespace MyStockViewApp
{
    public class StockData
    {
        public bool IsSelected { get; set; }
        public string Exchange { get; set; }
        public string CompanyNameShort { get; set; }
        public float value { get; set; }
        public float percentChanged { get; set; }
        public float hi52 { get; set; }
        public float lo52 { get; set; }
    }
    public class StockID
    {
        public StockID(string exch, string Company)
        {
            Exchange = exch;
            CompanyNameShort = Company;
        }
        public string Exchange;
        public string CompanyNameShort;
    }
}
