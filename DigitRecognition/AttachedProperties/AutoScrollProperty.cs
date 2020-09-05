using System;
using System.Windows;
using System.Windows.Controls;

namespace DigitRecognition.AttachedProperties
{
    public class AutoScrollProperty
    {
        public static bool GetAutoScrollSettings(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoScrollSettingsProperty);
        }

        public static void SetAutoScrollSettings(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoScrollSettingsProperty, value);
        }

        public static readonly DependencyProperty AutoScrollSettingsProperty =
            DependencyProperty.RegisterAttached("AutoScrollSettings", typeof(bool), typeof(AutoScrollProperty), new PropertyMetadata(default(bool), AutoScrollSettingsChanged));

        private static void AutoScrollSettingsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = d as TextBox;

            textBox.TextChanged += (sender, arg) =>
              {
                  if (textBox.Text.Length <= 0)
                      return;
                  textBox.ScrollToEnd();
              };
        }
    }
}
