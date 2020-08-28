﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitRecognition.Controls
{
    /// <summary>
    /// Interaction logic for RecognitionResult.xaml
    /// </summary>
    public partial class RecognitionResult : UserControl
    {
        public RecognitionResult()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }
        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register(
                   nameof(Number), typeof(int), typeof(RecognitionResult), new FrameworkPropertyMetadata(null));

        public double PosibilityNumber
        {
            get { return (int)GetValue(PosibilityNumberProperty); }
            set { SetValue(PosibilityNumberProperty, value); }
        }
        public static readonly DependencyProperty PosibilityNumberProperty = DependencyProperty.Register(
                   nameof(PosibilityNumber), typeof(double), typeof(RecognitionResult), new FrameworkPropertyMetadata(null));

    }
}
