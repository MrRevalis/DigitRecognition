using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DigitRecognition.ViewModel
{
    using DigitRecognition.DataModels;
    using DigitRecognition.ViewModel.Base;
    public class MWViewModel : ViewModelBase
    {
        #region Private
        private Window mWindow;
        private string windowTitle = "Digit Recognition";
        private ApplicationPage currentPage = ApplicationPage.RecognitionPage;
        #endregion

        #region Public Properties
        public string WindowTitle{ get { return windowTitle; }}
        public ApplicationPage CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
        #endregion

        public MWViewModel(Window window) 
        {
            mWindow = window;
        }
    }
}
