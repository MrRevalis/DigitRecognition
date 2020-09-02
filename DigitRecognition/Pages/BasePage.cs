using System.Windows.Controls;
using DigitRecognition.Core;

namespace DigitRecognition.Pages
{
    public class BasePage<VM> : Page where VM : ViewModelBase, new()
    {
        private VM viewModel;
        public VM ViewModel
        {
            get { return viewModel; }
            set
            {
                if (viewModel == value)
                    return;
                viewModel = value;
                this.DataContext = viewModel;
            }
        }

        public BasePage()
        {

            this.ViewModel = new VM();

        }
    }
}
