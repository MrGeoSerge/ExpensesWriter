using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ExpensesWriter.Converters
{
    public class PercentToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var percents = (int)value;

            if (percents < 90)
                return "LightGreen";
            else if (percents >= 90 && percents <= 100)
                return "Yellow";
            else
                return "#f5b8a9";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
