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
using Microsoft.Win32;
using System.IO;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using ImageProcessor.Imaging.Filters.EdgeDetection;
using ImageProcessor.Imaging.Filters.Photo;
using System.Drawing;
using System.Windows.Interop;

namespace MYT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string _imgPath = null;

        private bool Load()
        {
            return _imgPath != null;
        }

        private BitmapImage ProcessImg(IMatrixFilter filter)
        {
            byte[] photoBytes = File.ReadAllBytes(_imgPath);
            // Format is automatically detected though can be changed.
            ISupportedImageFormat format = new JpegFormat { Quality = 70 };
            //System.Drawing.Size size = new System.Drawing.Size(150, 0);
            using (MemoryStream inStream = new MemoryStream(photoBytes))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        imageFactory.Load(inStream)
                                    //.Resize(size)
                                    .Format(format)
                                    .Filter(filter)
                                    .Save(outStream);
                    }

                    return ToBitmap(outStream);
                }
            }
        }

        private BitmapImage ToBitmap(MemoryStream outStream)
        {
            outStream.Seek(0, SeekOrigin.Begin);

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = outStream;
            bitmapImage.EndInit();

            return bitmapImage;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            if (op.ShowDialog() == true)
            {
                _imgPath = op.FileName;
                sourceImg.Source = new BitmapImage(new Uri(_imgPath));
                processedImg.Source = new BitmapImage(new Uri(_imgPath));
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                processedImg.Source = new BitmapImage(new Uri(_imgPath));
            }
        }

        private void btnFilterComic_Click(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                processedImg.Source = ProcessImg(MatrixFilters.Comic);
            }
        }

        private void btnFilterBlackWhite_Click(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                processedImg.Source = ProcessImg(MatrixFilters.BlackWhite);
            }
        }

        private void btnFilterHiSatchn_Click(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                processedImg.Source = ProcessImg(MatrixFilters.HiSatch);
            }
        }

        private void btnFilterInvert_Click(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                processedImg.Source = ProcessImg(MatrixFilters.Invert);
            }
        }

        private void btnASCII_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
