using System;
using System.Windows;

namespace MyStockViewApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            trayNotifyIcon = new TrayNotifyIcon();
            trayNotifyIcon.MouseClick += () =>
                                        {
                                            if (this.WindowState != WindowState.Minimized)//this.ShowInTaskbar)
                                            {
                                                this.Hide();
                                                this.WindowState = WindowState.Minimized;
                                                this.ShowInTaskbar = false;
                                            }
                                            else
                                            {
                                                this.Show();
                                                this.WindowState = WindowState.Normal;
                                                this.ShowInTaskbar = true;
                                            }
                                        };

            SetNotifyIcon();
        }

        #region Tray Icon
        private TrayNotifyIcon trayNotifyIcon;

        private void SetNotifyIcon()
        {
            try
            {
                System.IO.Stream iconStream = Application.GetResourceStream(new Uri("pack://application:,,/Common/Icon1.ico")).Stream;
                trayNotifyIcon.notifyIcon.Icon = new System.Drawing.Icon(iconStream);
                trayNotifyIcon.notifyIcon.Visible = true;
            }
            catch (Exception ex)
            {
                string str = "Failed to Set Notify Icon";
                Utilities.SetStatus(new Status(str, ex));
                Utilities.LogError(str);
            }

        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState.Minimized == WindowState)
            {
                //this.Hide();
                //this.WindowState = WindowState.Minimized;
                this.ShowInTaskbar = false;
            }
        }
        #endregion
    }
}
