﻿using System.Windows;
using DigitRecognition.Core;

namespace DigitRecognition
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IoC.Setup();

            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }
    }
}
