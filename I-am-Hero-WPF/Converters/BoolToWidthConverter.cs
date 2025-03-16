using System;
using System.Globalization;
using System.Windows.Data;

namespace I_am_Hero_WPF.Converters
{
    public class BoolToWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isExpanded && parameter is string param)
            {
                var sizes = param.Split(',');
                if (sizes.Length == 2 && double.TryParse(sizes[0], out double collapsedWidth) && double.TryParse(sizes[1], out double expandedWidth))
                {
                    return isExpanded ? expandedWidth : collapsedWidth;
                }
            }
            return 30;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
