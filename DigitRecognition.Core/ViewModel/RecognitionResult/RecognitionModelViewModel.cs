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
        public int Number { get; set; }
        public double NumberPosibility { get; set; }
    }
}
