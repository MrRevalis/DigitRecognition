using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DigitRecognition.AttachedProperties
{
    public class FrameHistoryProperty
    {
        public static readonly DependencyProperty FrameSettingsProperty =
            DependencyProperty.RegisterAttached(
                "FrameSettings",
                typeof(bool),
                typeof(FrameHistoryProperty),
                new PropertyMetadata(default(bool), FrameSettingsChanged)
                );

        public static void SetFrameSettings(DependencyObject _object, bool value)
        {
            _object.SetValue(FrameSettingsProperty, value);
        }

        public static bool GetFrameSettings(DependencyObject _object)
        {
            return (bool)_object.GetValue(FrameSettingsProperty);
        }


        private static void FrameSettingsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Frame form = d as Frame;

            MessageBox.Show(e.NewValue.ToString());
        }
    }
}
