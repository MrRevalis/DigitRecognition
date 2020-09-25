using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DigitRecognition.AttachedProperties
{
    public class TextBoxNetworkProperty
    {
        public static readonly DependencyProperty TextBoxDigitsProperty =
            DependencyProperty.RegisterAttached(
                "TextBoxDigits",
                typeof(bool),
                typeof(TextBoxNetworkProperty),
                new PropertyMetadata(default(bool), TextBoxDigitsChanged));

        public static bool GetTextBoxDigits(DependencyObject obj)
        {
            return (bool)obj.GetValue(TextBoxDigitsProperty);
        }

        public static void SetTextBoxDigits(DependencyObject obj, bool value)
        {
            obj.SetValue(TextBoxDigitsProperty, value);
        }

        private static void TextBoxDigitsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = d as TextBox;

            textBox.PreviewTextInput += (sender, arg) =>
              {
                  bool correctConvert = char.TryParse(arg.Text, out char text);

                  if (correctConvert)
                  {

                      if (char.IsDigit(text))
                      {
                          return;
                      }

                      if(text == ',')
                      {
                          int length = (sender as TextBox).Text.Length;
                          int caret = (sender as TextBox).CaretIndex;

                          if(caret != 0)
                          {
                              if (caret >= 1 && (sender as TextBox).Text[caret - 1] != ',')
                              {
                                  if(caret < length && (sender as TextBox).Text[caret] == ',')
                                  {
                                      arg.Handled = true;
                                  }
                                  else
                                      return;
                              }
                          }
                      }
                      arg.Handled = true;
                  }

              };
        }
    }
}
