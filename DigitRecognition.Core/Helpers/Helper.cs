using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace DigitRecognition.Core
{
    public class Helper
    {
        public static List<double> BitmapToArray(Bitmap bitmap)
        {
            return null;
        }

        public static Bitmap ResizeBitmap(Bitmap sourceBitmap, string name, int width, int height)
        {
            /*Bitmap resizedBitmap = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedBitmap))
            {
                graphics.DrawImage(sourceBitmap, 0, 0, width, height);
            }
            resizedBitmap.Save($"resized_{name}.png", ImageFormat.Png);
            return resizedBitmap;*/

            Bitmap resized = new Bitmap(sourceBitmap, new Size(width, height));
            resized.Save($"resized_{name}.png", ImageFormat.Png);
            return resized;

        }
    }
}
