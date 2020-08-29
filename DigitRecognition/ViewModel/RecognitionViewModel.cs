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
        private const int numberOfPosibilities = 10;
        #endregion
        #region Public Properties
        public ICommand RecognizeNumber { get; set; }
        public ICommand ClearCanvas { get; set; }
        public ICommand AddControls { get; set; }
        public List<RecognitionResult> ListOfControls { get; set; }
        #endregion
        public RecognitionViewModel()
        {
            ListOfControls = new List<RecognitionResult>();
            RecognizeNumber = new RelayCommand(() => Recognize());
            AddControls = new RelayParameterizedCommand((param) => Add(param));

            ClearCanvas = new RelayParameterizedCommand((param) =>
            {
                ((InkCanvas)param).Strokes.Clear();
            });

        }

        private void Recognize()
        {
            
        }

        /// <summary>
        /// Add controls to StackPanel
        /// </summary>
        /// <param name="_object">StackPanel</param>
        private void Add(object _object)
        {

            for (int i = 0; i < numberOfPosibilities; i++)
            {
                ListOfControls.Add(new RecognitionResult());
                ListOfControls[i].Number = i;
                ListOfControls[i].PosibilityNumber = i;
            }
            StackPanel stackPanel = _object as StackPanel;

            foreach(var x in ListOfControls)
            {
                stackPanel.Children.Add(x);
            }
        }
    }
}
