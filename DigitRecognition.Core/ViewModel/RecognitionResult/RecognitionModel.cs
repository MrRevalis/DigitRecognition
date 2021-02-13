using System;
using System.Collections.ObjectModel;

namespace DigitRecognition.Core
{
    public class RecognitionModel
    {
        public static RecognitionModel Instance { get; set; } =  new RecognitionModel();
        public ObservableCollection<RecognitionModelViewModel> RecognitionModelCollection { get; set; }

        public RecognitionModel()
        {
            RecognitionModelCollection = new ObservableCollection<RecognitionModelViewModel>();

            for (int i = 0; i < 10; i++)
            {
                var x = new RecognitionModelViewModel();
                x.Number = $"Number {i}";
                x.NumberPosibility = $"{0}%";

                RecognitionModelCollection.Add(x);
            }
        }

        public void ChangePosibilities(double[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                RecognitionModelCollection[i].NumberPosibility = $"{Math.Round(data[i] * 100, 2)}%";
            }
        }

        public void ClearPosibilities()
        {
            for (int i = 0; i < RecognitionModelCollection.Count; i++)
            {
                RecognitionModelCollection[i].NumberPosibility = $"{0}%";
            }
        }
    }
}
