using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageTVDenoising.Model
{
    class IOImage
    {
        public static BitmapImage OpenImage(string path)
        {
            return new BitmapImage(new Uri(path, UriKind.Absolute));
        }

        /// <summary>
        /// Скачать картинку на диск
        /// </summary>
        public static Result DownLoadImage(ImageSource bitmapImage, string path = "")
        {
            var res = new Result();
            path = path == "" ? $"img{DateTime.Now.Ticks}.png" : path;
            try
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)bitmapImage));
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    encoder.Save(stream);
                }
            }
            catch (Exception ex) {
                res.Succes = false;
                res.Message = ex.Message;
            }
            return res;
        }
    }
}
