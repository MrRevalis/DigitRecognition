using System.Windows.Input;
using DigitRecognition.Controls;
using System.Collections.Generic;

namespace DigitRecognition.ViewModel
{
    using DigitRecognition.ViewModel.Base;
    using System.Windows.Controls;

    public class RecognitionViewModel : ViewModelBase
    {
        #region Private

        #endregion
        #region Public Properties
        public ICommand RecognizeNumber { get; set; }
        public ICommand ClearCanvas { get; set; }
        public ICommand AddControls { get; set; }
        public List<RecognitionResult> ListOfControls { get; set; } = new List<RecognitionResult>();
        #endregion
        public RecognitionViewModel()
        {
            RecognizeNumber = new RelayCommand(() => Recognize());
            ClearCanvas = new RelayParameterizedCommand((param) => Clear(param));
            AddControls = new RelayParameterizedCommand((param) => Add(param));
        }

        private void Recognize()
        {
            
        }

        private void Clear(object _object)
        {

        }

        private void Add(object _object)
        {

            for (int i = 0; i < 10; i++)
            {
                ListOfControls.Add(new RecognitionResult());
                ListOfControls[i].Number = $"{i}";
            }
            StackPanel stackPanel = _object as StackPanel;

            foreach(var x in ListOfControls)
            {
                stackPanel.Children.Add(x);
            }
        }
    }
}
