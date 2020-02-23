using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetworkScanner
{
    class InvertVisibilityConverter : BaseValueConverter<InvertVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (Visibility)value;
            if (visibility == Visibility.Collapsed)
            {
                return Visibility.Visible;
            }
            else if (visibility == Visibility.Visible)
            {
                return Visibility.Collapsed;
            }

            return Visibility.Hidden;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
