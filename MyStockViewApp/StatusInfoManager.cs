using System.ComponentModel;
using System.Threading;

namespace MyStockViewApp
{
    public class StatusInfoManager : IStatusInfoManager
    {
        StatusBarViewModel StatusInfoVM = null;
        BackgroundWorker fadeoutTimer = new BackgroundWorker();
        public StatusInfoManager()
        {
            fadeoutTimer.WorkerSupportsCancellation = true;
            fadeoutTimer.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) => { Thread.Sleep(6000); });
            fadeoutTimer.RunWorkerCompleted += new RunWorkerCompletedEventHandler((object sender, RunWorkerCompletedEventArgs e) => { ResetStatus();  });
        }

        public void RegisterViewModel(StatusBarViewModel vm)
        {
            StatusInfoVM = vm;
        }

        public void SetStatus(IStatus status)//, StatusBarViewModel StatusInfoVM)
        {
            if (StatusInfoVM != null)
            {
                if( !fadeoutTimer.IsBusy)
                {
                    StatusInfoVM.Status = status;
                    fadeoutTimer.RunWorkerAsync();
                }
            }
        }

        void ResetStatus()
        {
            if (StatusInfoVM != null)
            {
                StatusInfoVM.Status = new Status();
            }
        }
    }
    //public static class StatusInfoManager //: IStatusInfoManager
    //{
    //    static StatusBarViewModel StatusInfoVM;
    //    public static void RegisterViewModel(StatusBarViewModel vm)
    //    {
    //        StatusInfoVM = vm;
    //    }
    //    //static StatusInfoManager()
    //    //{
    //    //    Utilities.statusManager = this;
    //    //}

    //    //StatusInfoManager(StatusBarViewModel statusInfoVM)
    //    //{
    //    //    //StatusInfoVM = statusInfoVM;
    //    //    Utilities.statusManager = this;
    //    //}

    //    public static void SetStatus(IStatus status)//, StatusBarViewModel StatusInfoVM)
    //    {
    //        if(StatusInfoVM != null)
    //        {
    //            StatusInfoVM.Status = status;
    //        }
    //    }
    //}
}
