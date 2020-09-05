using System.Windows.Input;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;

namespace DigitRecognition.Core
{
    public class RecognitionViewModel : ViewModelBase
    {
        #region Private

        #endregion

        #region Public Properties
        public ICommand RecognizeNumber { get; set; }
        public ICommand ClearCanvas { get; set; }
        public ICommand AddControls { get; set; }
        #endregion

        #region Constructor

        public RecognitionViewModel()
        {
            //ListOfControls = new List<RecognitionResult>();
            RecognizeNumber = new RelayParameterizedCommand((param) => Recognize(param));

            ClearCanvas = new RelayParameterizedCommand((param) =>
            {
                ((InkCanvas)param).Strokes.Clear();
            });
        }
        #endregion

        #region Methods
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

            Random random = new Random();
            int number = random.Next();

            bitmap.Save($"test_{number}.png", ImageFormat.Png);

            Bitmap xD = Helper.ResizeBitmap(bitmap, number.ToString(), 32, 32);

        }
        #endregion
    }
}
