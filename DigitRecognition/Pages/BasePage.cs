using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DigitRecognition.Pages
{
    using DigitRecognition.ViewModel.Base;
    public class BasePage<VM> : Page where VM : ViewModelBase, new()
    {
        public Network NeuralNetwork { get; set; } = new Network();
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
            //this.ViewModel = new VM();

            this.ViewModel = (VM)Activator.CreateInstance(
                typeof(VM),
                new object[] { NeuralNetwork }
                );
        }
    }
}
