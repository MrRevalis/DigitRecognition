using System.Windows.Input;
using System.Drawing;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Drawing.Imaging;
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
            RecognizeNumber = new RelayParameterizedCommand((param) => Recognize(param));

            ClearCanvas = new RelayParameterizedCommand((param) =>
            {
                ((InkCanvas)param).Strokes.Clear();
                RecognitionModel.Instance.ClearPosibilities();
            });
        }
        #endregion

        #region Methods
        private void Recognize(object _object)
        {
            if (!IoC.Get<LearningViewModel>().IsRunning)
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
            //bitmap.Save($"newfile_{DateTime.Now.Millisecond}.bmp", ImageFormat.Bmp);
            DataSet dataSet = new DataSet(bitmap);

            var results = IoC.Get<Network>().Calculate(dataSet.Brightness);

            RecognitionModel.Instance.ChangePosibilities(results);
            }
        }
        #endregion
    }
}
