using System.Windows.Input;
using System.Windows;
using DigitRecognition.Core;
using System.Threading.Tasks;

namespace DigitRecognition.ViewModel
{

    public class MWViewModel : ViewModelBase
    {
        #region Private
        private Window mWindow;
        private string windowTitle = "Digit Recognition";
        #endregion

        #region Public Properties
        public string WindowTitle { get { return windowTitle; } }
        public bool LearningPageBool { get; set; }
        public bool RecognitionPageBool { get; set; }
        public ICommand ChangePage { get; set; }
        public ICommand Minimalize { get; set; }
        public ICommand CloseProgram { get; set; }
        #endregion

        public MWViewModel(Window window)
        {
            mWindow = window;
            ChangePage = new RelayParameterizedCommand(async (param) => await Change(param));

            Minimalize = new RelayCommand(() => window.WindowState = WindowState.Minimized);
            CloseProgram = new RelayCommand(() => window.Close());

            LearningPageBool = false;
            RecognitionPageBool = true;
        }

        private async Task Change(object param)
        {
            if (int.TryParse(param.ToString(), out int page))
            {
                if (page == 0)
                {
                    IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.RecognitionPage);
                    LearningPageBool = false;
                    RecognitionPageBool = true;
                }
                else
                {
                    IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.LearningPage);
                    LearningPageBool = true;
                    RecognitionPageBool = false;
                }
                RefreshData();
            }
            await Task.Delay(1);
        }

        private void RefreshData()
        {
            OnPropertyChanged(nameof(LearningPageBool));
            OnPropertyChanged(nameof(RecognitionPageBool));
        }
    }
}
