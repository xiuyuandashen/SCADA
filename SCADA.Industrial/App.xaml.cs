using SCADA.Industrial.Base;
using SCADA.Industrial.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SCADA.Industrial
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            GlobalMonitor.Start(SuccessAction: () =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    new MainWindow().Show();
                });
            }, ErrorAction: (errorMsg) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show(errorMsg,"应用程序启动异常");
                    Application.Current.Shutdown();
                });
            });

        }

        protected override void OnExit(ExitEventArgs e)
        {
            GlobalMonitor.Stop();
            base.OnExit(e);
        }
    }
}
