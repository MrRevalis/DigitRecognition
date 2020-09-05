using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitRecognition.Core
{
    public class ApplicationViewModel : ViewModelBase
    {
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.RecognitionPage;
        public ViewModelBase CurrentPageViewModel { get; set; }

        public void GoToPage(ApplicationPage page, ViewModelBase viewModel = null)
        {
            CurrentPage = page;
            CurrentPageViewModel = viewModel;
            OnPropertyChanged(nameof(CurrentPage));
        }
    }
}
