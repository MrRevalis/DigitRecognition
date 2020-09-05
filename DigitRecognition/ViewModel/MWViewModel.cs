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
        public ICommand ChangePage { get; set; }
        #endregion

        public MWViewModel(Window window)
        {
            mWindow = window;
            ChangePage = new RelayParameterizedCommand(async (param) => await Change(param));
        }

        private async Task Change(object param)
        {
            if (int.TryParse(param.ToString(), out int page))
            {
                if (page == 0)
                    IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.RecognitionPage);
                else
                    IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.LearningPage);
            }
            await Task.Delay(1);
        }
    }
}
