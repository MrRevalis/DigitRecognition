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
                x.Number = i;
                x.NumberPosibility = i;

                RecognitionModelCollection.Add(x);
            }
        }
    }
}
