using System.Windows.Input;
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
        public ICommand ChangeToRecognition { get; set; }
        public ICommand ChangeToLearning { get; set; }
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
