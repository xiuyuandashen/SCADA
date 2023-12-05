using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SCADA.Industrial.Converts
{
    /// <summary>
    /// 根据数值下标展示不同的颜色笔刷
    /// </summary>
    public class IndexColorConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                if (intValue % 2 == 0)
                {
                    SolidColorBrush solidColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#667099"));
                    return solidColorBrush;
                }
                else
                {
                    SolidColorBrush solidColorBrush = new SolidColorBrush(Colors.Transparent);
                    return solidColorBrush;
                }
            }

            // 如果无法转换，可以返回默认值或抛出异常，根据需要处理
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
