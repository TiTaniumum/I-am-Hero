using System;
using System.Globalization;
using System.Windows.Data;

namespace I_am_Hero_WPF.Converters
{
    public class ExpandIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "ChevronUp" : "ChevronDown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
