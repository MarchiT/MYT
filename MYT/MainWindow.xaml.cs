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
using MYT.ImageOperations;

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
        private BitmapImage _processedImage = null;

        private ProcessedImage _processedWindow = null;
        private OriginalImage _originalWindow = null;

        private bool Load()
        {
            return _imgPath != null;
        }

        private void NotLoadedMessage()
        {
            MessageBox.Show("Image is not loaded!");
        }

        private void DisplayProcessedWindow()
        {
            if (_processedWindow == null || !_processedWindow.IsVisible)
                _processedWindow = new ProcessedImage { Owner = this };

            _processedWindow.image.Source = _processedImage;
            _processedWindow.Show();
        }

        private void DisplayOriginalWindow()
        {
            if (_originalWindow == null || !_originalWindow.IsVisible)
                _originalWindow = new OriginalImage { Owner = this };

            _originalWindow.image.Source = new BitmapImage(new Uri(_imgPath));
            _originalWindow.Show();
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

                DisplayOriginalWindow();

                ImageModifications.Initialize(_imgPath);
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                //processedImg.Source = new BitmapImage(new Uri(_imgPath));
                _processedImage = new BitmapImage(new Uri(_imgPath));
                DisplayProcessedWindow();

                ImageModifications.Initialize(_imgPath);
            } else
            {
                NotLoadedMessage();
            }
        }



        private void btnFilterComic_Click(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                //processedImg.Source = ProcessImg(MatrixFilters.Comic);
                _processedImage = ImageModifications.Filter(MatrixFilters.Comic);
                DisplayProcessedWindow();
            } else
            {
                NotLoadedMessage();
            }
        }

        private void btnFilterBlackWhite_Click(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                _processedImage = ImageModifications.Filter(MatrixFilters.BlackWhite);
                DisplayProcessedWindow();
            }
            else
            {
                NotLoadedMessage();
            }
        }

        private void btnFilterHiSatchn_Click(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                _processedImage = ImageModifications.Filter(MatrixFilters.HiSatch);
                DisplayProcessedWindow();
            }
            else
            {
                NotLoadedMessage();
            }
        }

        private void btnFilterInvert_Click(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                _processedImage = (ImageModifications.Filter(MatrixFilters.Invert));
                DisplayProcessedWindow();
            }
            else
            {
                NotLoadedMessage();
            }
        }

        private void btnLomograph_Click(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                _processedImage = ImageModifications.Filter(MatrixFilters.Lomograph);
                DisplayProcessedWindow();
            }
            else
            {
                NotLoadedMessage();
            }
        }

        private void btnPolaroid_Click(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                _processedImage = ImageModifications.Filter(MatrixFilters.Polaroid);
                DisplayProcessedWindow();
            }
            else
            {
                NotLoadedMessage();
            }
        }

        private void btnLoSatch_Click(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                _processedImage = ImageModifications.Filter(MatrixFilters.LoSatch);
                DisplayProcessedWindow();
            }
            else
            {
                NotLoadedMessage();
            }
        }

        private void btnSepia_Click(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                _processedImage = ImageModifications.Filter(MatrixFilters.Sepia);
                DisplayProcessedWindow();
            }
            else
            {
                NotLoadedMessage();
            }
        }

        private void btnPixelate_Click(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                _processedImage = ImageModifications.Pixelate(8);
                DisplayProcessedWindow();
            }
            else
            {
                NotLoadedMessage();
            }
        }

        private void btnQuality_Click(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                _processedImage = ImageModifications.Quality(5);
                DisplayProcessedWindow();
            }
            else
            {
                NotLoadedMessage();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e) //TODO fix
        {
            if (Load())
            {
                // Displays a SaveFileDialog so the user can save the Image
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
                saveFileDialog1.Title = "Save an Image File";
                saveFileDialog1.ShowDialog();

                // If the file name is not an empty string open it for saving.  
                if (saveFileDialog1.FileName != "")
                {
                    FileStream fs = (FileStream)saveFileDialog1.OpenFile();

                    //System.Drawing.Image.FromFile(@"D:\temp.png").
                    Converter.BitmapSourceToBitmap(_processedImage).
                        Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);

                    fs.Close();
                }
            }
            else
            {
                NotLoadedMessage();
            }
        }
    }
}
