using System.Windows.Input;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DigitRecognition.Core;

namespace DigitRecognition.ViewModel
{
    using DigitRecognition.Controls;

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
            RecognizeNumber = new RelayParameterizedCommand((param) => Recognize(param));
            AddControls = new RelayParameterizedCommand((param) => Add(param));

            ClearCanvas = new RelayParameterizedCommand((param) =>
            {
                ((InkCanvas)param).Strokes.Clear();
            });

        }

        private void Recognize(object _object)
        {
            InkCanvas inkCanvas = _object as InkCanvas;
            int width = (int)inkCanvas.ActualWidth;
            int height = (int)inkCanvas.ActualHeight;
            int dpi = 96;

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(width, height, dpi, dpi, PixelFormats.Default);
            renderTargetBitmap.Render(inkCanvas);

            BmpBitmapEncoder bitmapEncoder = new BmpBitmapEncoder();
            bitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            MemoryStream memoryStream = new MemoryStream();
            bitmapEncoder.Save(memoryStream);

            Bitmap bitmap = new Bitmap(memoryStream);
            bitmap.Save("test.png", ImageFormat.Png);

            Bitmap xD = Helper.ResizeBitmap(bitmap,32 ,32);

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
