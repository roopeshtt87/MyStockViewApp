using System.Windows.Threading;

namespace MyStockViewApp
{
    public class ViewModelBase : NotifyPropertyChanged
    {
        protected IDataManager dataManager = null;
        protected ILogManager logger = null;
        protected IStatusInfoManager statusManager = null;
        protected ViewModelMediator mediator = ViewModelMediator.GetMediator();
        public ViewModelBase()
        {
            dataManager = mediator.dataManager;
            logger = mediator.logManager;
            statusManager = mediator.statusManager;
        }
        public Dispatcher currentDispatcher = Dispatcher.CurrentDispatcher;

    }
}
