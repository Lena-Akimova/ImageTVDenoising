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
    class ImageOperations
    {
        private IImageDenoising ImageDenoising;
        public ImageOperations(IImageDenoising _imageDenoising)
        {
            ImageDenoising = _imageDenoising;
        }


        public BitmapImage TVDenoiseImage(BitmapImage bitmapImage, float par, out DateTime time, out float cf, out int iterations)
        {
            return ImageDenoising.TVDenoiseImage(bitmapImage,par, out time, out cf, out iterations);
        }

        /// <summary>
        /// Зашумление изображения
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
        public static BitmapSource NoiseImage(BitmapImage bitmapImage)
        {
            var listPixels = GetPixels(bitmapImage);
            var rand = new Random();
            var listNoisedPixels = new List<Pixel>(listPixels.Count);
            Color newColor = new Color();

            foreach(var cp in listPixels)
            {
                var rr = rand.Next(100);
                if( rr < 40)
                {
                    var noise = (rand.NextDouble() + rand.NextDouble() + rand.NextDouble() + rand.NextDouble() - 2) * 20;
                    newColor = new Color() { R = (byte)(cp.Color.R+noise), G = (byte)(cp.Color.G + noise), B = (byte)(cp.Color.B + noise) };
                    listNoisedPixels.Add(new Pixel() { Color = newColor, Point = cp.Point });
                }
                listNoisedPixels.Add(new Pixel() { Color = cp.Color, Point = cp.Point });
            }

            var noisedImage = new WriteableBitmap(bitmapImage.PixelWidth, bitmapImage.PixelHeight, 96, 96, PixelFormats.Bgra32, null);

            foreach(var np in listNoisedPixels)
            {
                noisedImage.SetPixel(np.Color, (int)np.Point.X, (int)np.Point.Y);
            }

            return noisedImage;
        }

        /// <summary>
        /// Пикcели для работы
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
        protected static List<Pixel> GetPixels(BitmapSource bitmapImage)
        {
            //Шаг растр. изображения
            int stride = (bitmapImage.PixelWidth * bitmapImage.Format.BitsPerPixel) / 8;
            int size = bitmapImage.PixelHeight * stride;
            byte[] pixels = new byte[size];
            bitmapImage.CopyPixels(pixels, stride, 0);

            var list = new List<Pixel>(bitmapImage.PixelWidth * bitmapImage.PixelHeight);

            for (int y = 0; y <= bitmapImage.Height; y++)
            {
                for (int x = 0; x <= bitmapImage.Width; x++)
                {
                    list.Add(new Pixel()
                    {
                        Color = bitmapImage.GetPixel(pixels, stride, x, y),
                        Point = new System.Windows.Point(x, y)
                    });
                }
            }
            return list;
        }


    }

    interface IImageDenoising
    {
        /// <summary>
        /// Total Variation шумоподавление
        /// </summary>
        /// <param name="bitmapImage">Картинка к изменению</param>
        /// <param name="par">Параметр регуляризации</param>
        /// <param name="time">Время выполнения</param>
        /// <param name="cf">Значение целевой функции</param>
        /// <param name="iterations">Количество итераций</param>
        /// <returns>Измененное изображение</returns>
        BitmapImage TVDenoiseImage(BitmapImage bitmapImage, float par, out DateTime time, out float cf, out int iterations);
    }

    class ImageGradientDenoising : IImageDenoising
    { 
        public BitmapImage TVDenoiseImage(BitmapImage bitmapImage, float par, out DateTime time, out float cf, out int iterations)
        {
            time = default;
            cf = 0;
            iterations = 0;
            return null;
        }
    }

    class ImageBundleDenoising : IImageDenoising
    {
        public BitmapImage TVDenoiseImage(BitmapImage bitmapImage, float par, out DateTime time, out float cf, out int iterations)
        {
            time = default;
            cf = 0;
            iterations = 0;
            return null;
        }
    }
}
