using System.Windows.Controls;
using DigitRecognition.Core;

namespace DigitRecognition.Pages
{
    public class BasePage<VM> : BasePage where VM : ViewModelBase, new()
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

        public BasePage() : base()
        {
            this.ViewModel = IoC.Get<VM>();
        }

        public BasePage(VM specifiedViewModel = null) : base()
        {
            if (specifiedViewModel != null)
                ViewModel = specifiedViewModel;
            else
                ViewModel = IoC.Get<VM>();
        }
    }
    public class BasePage : UserControl
    {
        public BasePage() { }
    }
}
