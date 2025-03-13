using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace I_am_Hero_WPF.Converters
{
    public class ProgressConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 3 ||
                values[0] == DependencyProperty.UnsetValue ||
                values[1] == DependencyProperty.UnsetValue ||
                values[2] == DependencyProperty.UnsetValue)
                return 0;

            double value = (double)values[0];
            double min = (double)values[1];
            double max = (double)values[2];

            return (value - min) / (max - min);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
