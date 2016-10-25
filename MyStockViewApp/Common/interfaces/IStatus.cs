
namespace MyStockViewApp
{
    public interface IStatus
    {
        string StatusMessage { get; }
        string StatusDetails { get; }
        StatusSeverity Severity { get; }
        bool IsSuccessful { get; }

        void SetSuccess();
    }
}
