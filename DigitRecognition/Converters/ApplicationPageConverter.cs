using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using DigitRecognition.Core;

namespace DigitRecognition.Converters
{
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
