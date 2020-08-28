using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DigitRecognition.AttachedProperties
{
    public class StackPanelProperty
    {
        public static readonly DependencyProperty StackPanelCommandProperty =
            DependencyProperty.RegisterAttached(
                "StackPanelCommand",
                typeof(ICommand),
                typeof(StackPanelProperty),
                new PropertyMetadata(default(ICommand), StackPanelCommandChanged)
                );
        public static void SetStackPanelCommand(DependencyObject _object, ICommand value)
        {
            _object.SetValue(StackPanelCommandProperty, value);
        }

        public static ICommand GetStackPanelCommand(DependencyObject _object)
        {
            return (ICommand)_object.GetValue(StackPanelCommandProperty);
        }
        private static void StackPanelCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StackPanel stackPanel = d as StackPanel;

            stackPanel.Initialized += (sender, args) =>
            {
                object command = stackPanel.GetValue(StackPanelParameterProperty);
                GetStackPanelCommand(stackPanel).Execute(command);
            };
        }

        //Command

        public static readonly DependencyProperty StackPanelParameterProperty =
            DependencyProperty.RegisterAttached(
                "StackPanelParameter",
                typeof(object),
                typeof(StackPanelProperty),
                new PropertyMetadata(null)
        );
        public static void SetStackPanelParameter(DependencyObject _object, object value)
        {
            _object.SetValue(StackPanelParameterProperty, value);
        }

        public static object GetStackPanelParameter(DependencyObject _object)
        {
            return (object)_object.GetValue(StackPanelParameterProperty);
        }
    }
}
