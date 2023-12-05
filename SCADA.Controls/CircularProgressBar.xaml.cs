using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SCADA.Controls
{
    /// <summary>
    /// CircularProgressBar.xaml 的交互逻辑
    /// </summary>
    public partial class CircularProgressBar : UserControl
    {
        public CircularProgressBar()
        {
            InitializeComponent();
            this.SizeChanged += CircularProgressBar_SizeChanged;
        }

        private void CircularProgressBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateValue();
        }

        /// <summary>
        /// 进度值
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(CircularProgressBar), new PropertyMetadata(0.0, OnMyPropertyChanged));



        /// <summary>
        /// 底色
        /// </summary>
        public Brush BackColor
        {
            get { return (Brush)GetValue(BackColorProperty); }
            set { SetValue(BackColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackColorProperty =
            DependencyProperty.Register("BackColor", typeof(Brush), typeof(CircularProgressBar), new PropertyMetadata(Brushes.LightGray));


        /// <summary>
        /// 前景色
        /// </summary>
        public Brush ForeColor
        {
            get { return (Brush)GetValue(ForeColorProperty); }
            set { SetValue(ForeColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForeColorProperty =
            DependencyProperty.Register("ForeColor", typeof(Brush), typeof(CircularProgressBar), new PropertyMetadata(Brushes.Orange));


        /// <summary>
        /// 属性更改回调
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnMyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CircularProgressBar).UpdateValue();
        }

        private void UpdateValue()
        {
            // this.RenderSize.Width最终呈现的宽度大小
            this.layout.Width = Math.Min(this.RenderSize.Width, this.RenderSize.Height);
            double radius = Math.Min(this.RenderSize.Width, this.RenderSize.Height) / 2;
            if (radius <= 0)
            {
                return;
            }
            double newX = 0, newY = 0;
            double newValue = Value % 100.0;
            // 角度转弧度  Π / 180 
            newX = radius + (radius - 3) * Math.Cos((newValue * 3.6 - 90) * Math.PI / 180);
            newY = radius + (radius - 3) * Math.Sin((newValue * 3.6 - 90) * Math.PI / 180);
            string pathDataStr = "M{0} 3A{1} {1} 0 {4} 1 {2} {3}";
            pathDataStr = string.Format(pathDataStr, radius + 0.01, radius - 3, newX, newY, newValue < 50 && newValue > 0 ? 0 : 1);
            TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(Geometry));
            path.Data = (Geometry)typeConverter.ConvertFrom(pathDataStr);

            //this.Dispatcher.Invoke(() =>
            //{
            //    Number.Text = Value + "%";
            //});
        }

    }
}
