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

namespace SCADA.Industrial.View
{
    /// <summary>
    /// SystemMonitor.xaml 的交互逻辑
    /// </summary>
    public partial class SystemMonitor : UserControl
    {
        public SystemMonitor()
        {
            InitializeComponent();
            DataContext = new SystemMonitorViewModel();
        }

        /// <summary>
        /// 滚轮事件缩放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            // 缩放后的宽高
            double newWidth = mainView.ActualWidth + e.Delta;
            double newHeight = mainView.ActualHeight + e.Delta;
            // 如果缩放后大小过小，需要进行限制
            if (newWidth < 500) newWidth = 500;
            if (newHeight < 100) newHeight = 100;
            // 赋值控件新的大小
            mainView.Width = newWidth;
            mainView.Height = newHeight;

            // 设置canvas的左边距为控件窗体的实际宽度减去canvas宽度的一半 这样canvas缩放会以中心点进行缩放
            mainView.SetValue(Canvas.LeftProperty, (this.RenderSize.Width - this.mainView.Width) / 2);
        }

        bool _isMove = false;
        // 初始canvas位置
        Point _downPoint = new Point(0, 0);
        // 初始canvas左上距离边距
        double left = 0, top = 0;

        /// <summary>
        /// 鼠标左键按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isMove = true;
            _downPoint = e.GetPosition(sender as Canvas);
            left = double.Parse(mainView.GetValue(Canvas.LeftProperty).ToString());
            top = double.Parse(mainView.GetValue(Canvas.TopProperty).ToString());
            // 截断事件冒泡
            e.Handled = true;
            // 鼠标指定到这个元素上
            (sender as Canvas).CaptureMouse();
        }
        /// <summary>
        /// 鼠标左键抬起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isMove = false;
            // 截断事件冒泡
            e.Handled = true;
            // 释放鼠标
            (sender as Canvas).ReleaseMouseCapture();
        }

        /// <summary>
        /// 移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMove)
            {
                Point currentPoint = e.GetPosition(sender as Canvas);

                mainView.SetValue(Canvas.LeftProperty, left + (currentPoint.X - _downPoint.X));
                mainView.SetValue(Canvas.TopProperty, top + (currentPoint.Y - _downPoint.Y));
                // 截断事件冒泡
                e.Handled = true;
            }
        }
    }
}
