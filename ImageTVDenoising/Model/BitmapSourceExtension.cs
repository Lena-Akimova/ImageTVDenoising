using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageTVDenoising.Model
{
    public static class BitmapSourceExtension
    {
        /// <summary>
        /// Цвет в квадратике изображения
        /// </summary>
        public static Color GetPixel(this BitmapSource bitmapImage, byte[] pixels, int stride, int x, int y)
        {
            int index = y * stride + x;

            byte red = pixels[index];
            byte green = pixels[index + 1];
            byte blue = pixels[index + 2];

            return new Color()
            {
                R = red,
                G = green,
                B = blue
            };
        }

        /// <summary>
        /// Закрашивание пикселя
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="color">r,g,b,a</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void SetPixel(this WriteableBitmap bitmap, Color color, int x, int y)
        {
            Int32Rect rect = new Int32Rect(x, y, 1, 1);
            int stride = (bitmap.PixelWidth * bitmap.Format.BitsPerPixel) / 8;
            byte[] bcol = new byte[] {color.R, color.G, color.B, color.A };
            bitmap.WritePixels(rect, bcol, stride, 0);
        }

    }
}
