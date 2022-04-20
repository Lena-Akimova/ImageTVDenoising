using ImageTVDenoising.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageTVDenoising
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitmapImage resultImage1;
        private BitmapImage resultImage2;
        private ImageSource noisyImage;
        private BitmapImage sourceImage;
        private float lambda = 4f;

        private ImageOperations imageGradientOperations;
        private ImageOperations imageBundleOperations;

        private readonly ImageGradientDenoising ImageGradientDenoising = new ImageGradientDenoising();
        private readonly ImageBundleDenoising ImageBundleDenoising = new ImageBundleDenoising();



        public MainWindow()
        {
            InitializeComponent();
            imageGradientOperations = new ImageOperations(ImageGradientDenoising);
            imageBundleOperations = new ImageOperations(ImageBundleDenoising);
            LambdaTbx.Text = $"{lambda}";
            noisyImage = NoisedImage.Source;
        }

        /// <summary>
        /// Открыть картинку из источника
        /// </summary>
        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.bmp;)|*.png;*.jpeg;*.jpg;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                var img = openFileDialog.FileName;
                sourceImage = new BitmapImage(new Uri(img, UriKind.Absolute));
                SourceImage.Source = IOImage.OpenImage(img);
            }
        }

     

        /// <summary>
        /// Шумоподавление
        /// </summary>
        private void DenoiseBtn_Click(object sender, RoutedEventArgs e)
        {
            //Берем шумную картинку
            if (noisyImage != null)
            {
                var t1 = new DateTime();
                var cf1 = 0f;
                var i1 = 0;

                var t2 = new DateTime();
                var cf2 = 0f;
                var i2 = 0;

                var RegularisationPar = float.TryParse(LambdaTbx.Text.Trim(), out lambda);

                if (!RegularisationPar)
                {
                    lambda = 4;
                    LambdaTbx.Text = $"{lambda}";
                }


                resultImage1 = imageGradientOperations.TVDenoiseImage(sourceImage, lambda, out t1, out cf1, out i1);
                resultImage2 = imageBundleOperations.TVDenoiseImage(sourceImage, lambda, out t2, out cf2, out i2);

                //Кладем в результат
                DeNoisedImage1.Source = resultImage1;
                DeNoisedImage2.Source = resultImage2;


            }
        }


        /// <summary>
        /// Зашумить 
        /// </summary>
        private void NoiseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sourceImage != null)
            {
                noisyImage = (BitmapImage)ImageOperations.NoiseImage(sourceImage);
                NoisedImage.Source = noisyImage; 
            }
        }

        private void SaveNoiseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (noisyImage != null)
            {
                var res = IOImage.DownLoadImage(noisyImage);
                if (!res.Succes)
                    MessageBox.Show(res.Message);
            }
        }

        private void SaveDeNoise1Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveDeNoise2Btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
