using SCADA.Industrial.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SCADA.Industrial
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        /// <summary>
        /// 拖动窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MovingWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Cursor = Cursors.Hand;
                this.DragMove();
            }
            if(e.LeftButton == MouseButtonState.Released)
            {
                this.Cursor = Cursors.Arrow;
            }
                
        }


        private bool isMaximized = false;
        private double restoreTop;
        private double restoreLeft;
        private double restoreWidth;
        private double restoreHeight;

        /// <summary>
        /// 窗口最大化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {


            if (isMaximized)
            {
                // 还原Border的边距以及Radius
                RootBorder.Margin = new Thickness(10);
                RootBorder.CornerRadius = new CornerRadius(20);

                // 还原窗口的位置和大小
                this.Left = restoreLeft;
                this.Top = restoreTop;
                this.Width = restoreWidth;
                this.Height = restoreHeight;
                isMaximized = false;
                //this.WindowState = WindowState.Normal;
                return;
            }
            else
            {
                //最大化去除Border的边距以及Radius
                RootBorder.Margin = new Thickness(0);
                RootBorder.CornerRadius = new CornerRadius(0);
                // 记录当前窗口的位置和大小，然后最大化窗口
                restoreLeft = this.Left;
                restoreTop = this.Top;
                restoreWidth = this.Width;
                restoreHeight = this.Height;
                // SystemParameters.WorkArea 获取windows工作区大小 以防覆盖windows任务栏
                this.Left = SystemParameters.WorkArea.Left;
                this.Top = SystemParameters.WorkArea.Top;
                this.Width = SystemParameters.WorkArea.Width;
                this.Height = SystemParameters.WorkArea.Height;
                isMaximized = true;
                //this.WindowState = WindowState.Maximized;
            }

        }

        /// <summary>
        /// 窗口最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// 结束程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
