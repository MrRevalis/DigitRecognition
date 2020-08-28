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
    using DigitRecognition.DataModels;
    using DigitRecognition.Pages;

    public class ApplicationPageConverter : MarkupExtension, IValueConverter
    {
        private static ApplicationPageConverter mConverter = null;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.RecognitionPage:
                    return new RecognitionPage();

                case ApplicationPage.LearningPage:
                    return new LearningPage();

                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mConverter ?? (mConverter = new ApplicationPageConverter());
        }
    }
}
