using System;
using System.Windows.Forms;

namespace MyStockViewApp
{
    public class TrayNotifyIcon : IDisposable
    {
        public NotifyIcon notifyIcon;
        
        public delegate void MouseClickHandler();
        public event MouseClickHandler MouseClick;

        public TrayNotifyIcon() 
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.MouseClick += new MouseEventHandler(targetNotifyIcon_MouseClick);
        }

        public void targetNotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            //notifyIconMousePosition = System.Windows.Forms.Control.MousePosition; 
            MouseClick();
            
        }
        
        #region IDisposable Members

        private bool _IsDisposed = false;

        ~TrayNotifyIcon()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool IsDisposing)
        {
            if (_IsDisposed)
                return;

            if (IsDisposing)
            {
                notifyIcon.Dispose();
            }

            _IsDisposed = true;

        #endregion
        }
    }
}
