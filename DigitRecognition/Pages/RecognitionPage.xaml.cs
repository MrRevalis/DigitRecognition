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
using DigitRecognition.Core;

namespace DigitRecognition.Pages
{
    using DigitRecognition.ViewModel;
    /// <summary>
    /// Interaction logic for RecognitionPage.xaml
    /// </summary>
    public partial class RecognitionPage : BasePage<RecognitionViewModel>
    {
        public RecognitionPage()
        {
            InitializeComponent();
        }

        public RecognitionPage(RecognitionViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }
    }
}
