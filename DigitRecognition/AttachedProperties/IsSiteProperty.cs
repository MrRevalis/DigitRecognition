using System.Windows;
using System.Windows.Controls;

namespace DigitRecognition.AttachedProperties
{
    public class IsSiteProperty
    {
        public static readonly DependencyProperty IsSiteChoosenProperty =
            DependencyProperty.RegisterAttached(
                "IsSiteChoosen",
                typeof(bool),
                typeof(IsSiteProperty),
                new PropertyMetadata(default(bool), IsSiteChoosenChanged));

        public static bool GetIsSiteChoosen(DependencyObject d)
        {
            return (bool)d.GetValue(IsSiteChoosenProperty);
        }

        public static void SetIsSiteChoosen(DependencyObject d, bool value)
        {
            d.SetValue(IsSiteChoosenProperty, value);
        }

        private static void IsSiteChoosenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Button button = d as Button;
            bool wartosc = GetIsSiteChoosen(button);
        }
    }
}
