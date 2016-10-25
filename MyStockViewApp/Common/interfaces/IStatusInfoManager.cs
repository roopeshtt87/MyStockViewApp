
namespace MyStockViewApp
{
    public interface IStatusInfoManager
    {
        void RegisterViewModel(StatusBarViewModel vm);
        void SetStatus(IStatus status);
    }
}
