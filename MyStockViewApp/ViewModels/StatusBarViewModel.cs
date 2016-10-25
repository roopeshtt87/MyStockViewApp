
namespace MyStockViewApp
{
    public class StatusBarViewModel : ViewModelBase
    {
        private IStatus status;
        public IStatus Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged("Status"); }
        }
        public StatusBarViewModel()
        {
            //StatusInfoManager.RegisterViewModel(this);
            statusManager.RegisterViewModel(this);
            Status = new Status();
        }
    }
}
