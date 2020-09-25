using System;
using System.Drawing;
using System.IO;

namespace DigitRecognition.Core
{
    public class DataSet
    {
        public double[] Brightness { get; set; }
        public double[] Name { get; set; }

        public int Width { get; } = 28;
        public int Height { get; } = 28;

        public DataSet(string path)
        {
            if (int.TryParse(Path.GetFileNameWithoutExtension(path)[0].ToString(), out int index))
            {
                Name = new double[10];
                Name.SetValue(1, index);

                try
                {
                    Bitmap bitmap = new Bitmap(path);
                    if (bitmap.Size != new Size(Width, Height))
                    {
                        bitmap = bitmap.Resize(Width, Height);
                    }

                    Brightness = BitmapToArray(bitmap);
                }
                catch (Exception e)
                {
                    throw new Exception($"Error with file {path} => {e.Message}");
                }
            }
            else
                return;
        }

        public DataSet(Bitmap bitmap)
        {
            Name = new double[10];

            try
            {
                if (bitmap.Size != new Size(Width, Height))
                {
                    bitmap = bitmap.Resize(Width, Height);
                }

                Brightness = BitmapToArray(bitmap);
            }
            catch (Exception e)
            {
                throw new Exception($"Error with created Bitmap => {e.Message}");
            }
        }

        private double[] BitmapToArray(Bitmap bitmap)
        {
            double[] brightnessArray = new double[Width * Height];

            int indexArray = 0;
            for (int x = 0; x < Height; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    brightnessArray.SetValue(bitmap.GetPixel(x, y).GetBrightness(), indexArray);
                    indexArray++;
                }
            }

            return brightnessArray;
        }
    }
}
