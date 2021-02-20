using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;

namespace DigitRecognition.Core
{
    public class DataSet
    {
        public double[] Brightness { get; set; }
        public double[] Name { get; set; }

        public DataSet(FileInfo file)
        {
            try
            {
                //Get Colors
                Bitmap image = (Bitmap)Image.FromFile(file.FullName);
                if (image.Width != 28 && image.Height != 28)
                {
                    image = new Bitmap(image, new Size(28, 28));
                }

                int width = image.Width;
                int height = image.Height;
                double[] colorArray = new double[width * height];

                Rectangle rectangle = new Rectangle(0, 0, width, height);
                BitmapData bmpData = image.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                int[] dataArray = new int[width * height - 1];
                Marshal.Copy(bmpData.Scan0, dataArray, 0, dataArray.Length);
                image.UnlockBits(bmpData);

                for (int i = 0; i < dataArray.Length; i++)
                {
                    colorArray[i] = Color.FromArgb(dataArray[i]).GetBrightness();
                }
                Brightness = colorArray;
                //image.Save($"newfile_{DateTime.Now.Millisecond}.bmp", ImageFormat.Bmp);
                image.Dispose();
                //Get name value
                double[] nameArray = new double[10];
                string fileName = file.Name.Replace(file.Extension, " ");
                if (int.TryParse(fileName[0].ToString(), out int number))
                {
                    nameArray[number] = 1;
                }
                Name = nameArray;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error => {e.Message}");
            }
        }

        public DataSet(Bitmap image)
        {
            if (image.Width != 28 && image.Height != 28)
            {
                image = new Bitmap(image, new Size(28, 28));
            }

            int width = image.Width;
            int height = image.Height;
            double[] colorArray = new double[width * height];

            Rectangle rectangle = new Rectangle(0, 0, width, height);
            BitmapData bmpData = image.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int[] dataArray = new int[width * height - 1];
            Marshal.Copy(bmpData.Scan0, dataArray, 0, dataArray.Length);
            image.UnlockBits(bmpData);

            for (int i = 0; i < dataArray.Length; i++)
            {
                colorArray[i] = Color.FromArgb(dataArray[i]).GetBrightness();
            }
            Brightness = colorArray;
        }

        //CSV Read
        public DataSet(string[] dataLine)
        {
            Brightness = dataLine.Reverse().Skip(1).Select(x => double.Parse(x) / 255).ToArray();

            Name = new double[10];
            Name[int.Parse(dataLine.Last())] = 1;
        }
    }
}