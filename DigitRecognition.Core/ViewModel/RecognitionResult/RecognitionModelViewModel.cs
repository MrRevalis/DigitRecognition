using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace DigitRecognition.Core
{
    public class RecognitionModelViewModel : ViewModelBase
    {
        public string Number { get; set; }
        public string NumberPosibility { get; set; }
    }
}
