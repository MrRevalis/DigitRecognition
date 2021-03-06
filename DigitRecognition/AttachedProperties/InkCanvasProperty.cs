﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;

namespace DigitRecognition.AttachedProperties
{
    public class InkCanvasProperty
    {
        public static readonly DependencyProperty InkCanvasSettingsProperty =
            DependencyProperty.RegisterAttached(
                "InkCanvasSettings",
                typeof(ICommand),
                typeof(InkCanvasProperty),
                new PropertyMetadata(new PropertyChangedCallback(InkCanvasSettingsChanged))
                );

        public static void SetInkCanvasSettings(DependencyObject _object, ICommand value)
        {
            _object.SetValue(InkCanvasSettingsProperty, value);
        }

        public static ICommand GetInkCanvasSettings(DependencyObject _object)
        {
            return (ICommand)_object.GetValue(InkCanvasSettingsProperty);
        }


        private static void InkCanvasSettingsChanged(DependencyObject _object, DependencyPropertyChangedEventArgs e)
        {
            InkCanvas inkCanvas = _object as InkCanvas;
            inkCanvas.Background = System.Windows.Media.Brushes.Black;


            DrawingAttributes drawingAttributes = inkCanvas.DefaultDrawingAttributes;
            //drawingAttributes.Color = Color.FromRgb(255, 255, 0);
            drawingAttributes.Color = Colors.White;
            drawingAttributes.Width = 15D;
            drawingAttributes.Height = 15D;

            inkCanvas.MouseUp += (sender, args) =>
            {
                GetInkCanvasSettings(inkCanvas).Execute(inkCanvas);
                args.Handled = true;
            };
        }
    }
}
