using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace DigitRecognition.Converters
{
    class StringToProgressBarValue : MarkupExtension, IValueConverter
    {
        private static StringToProgressBarValue converter;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String newValue = value.ToString().Replace("%", "");
            if(Double.TryParse(newValue, out double result))
            {
                return result;
            }
            return default(double);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return converter ?? (converter = new StringToProgressBarValue());
        }
    }
}
