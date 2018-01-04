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
    /// Interaction logic for MainWindow.xaml:
    /// - initializes every button with the corresponding logic
    /// - displays the Processed and Original Windows
    /// - returns a message dialog for specific illegal actions
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Declare the original image path and the current image for processing
        private string _imgPath = null;
        private BitmapImage _processedImage = null;
        //Declare the two separate Windows for images
        private ProcessedImage _processedWindow = null;
        private OriginalImage _originalWindow = null;

        //Checks for errors
        private bool Load()
        {
            return _imgPath != null;
        }

        //Message dialog in case Load() returns false
        private void NotLoadedMessage()
        {
            MessageBox.Show("Image is not loaded!");
        }

        private void DisplayProcessedWindow()
        {
            //Initializes new Window for the first time if not yet loaded
            //Or if incorrectly closed, opens new Window with the same image in process
            if (_processedWindow == null || !_processedWindow.IsVisible)
                _processedWindow = new ProcessedImage { Owner = this };

            //Sets the right image for the window and displays it
            _processedWindow.image.Source = _processedImage;
            _processedWindow.Show();
        }

        private void DisplayOriginalWindow()
        {
            //The same logic applies for the Original Image Window 
            if (_originalWindow == null || !_originalWindow.IsVisible)
                _originalWindow = new OriginalImage { Owner = this };

            _originalWindow.image.Source = new BitmapImage(new Uri(_imgPath));
            _originalWindow.Show();
        }


        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            //Opens a File System Dialog
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            //Filters the seen results by the appropriate extensions
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            
            //Loads the chosen image and displays it in a separate window
            //Prepares the newly loaded image for the upcoming modifications
            if (op.ShowDialog() == true)
            {
                _imgPath = op.FileName;

                DisplayOriginalWindow();

                ImageModifications.Initialize(_imgPath);
            }
        }

        //Resets the current image back to its original state
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                _processedImage = new BitmapImage(new Uri(_imgPath));
                DisplayProcessedWindow();

                ImageModifications.Initialize(_imgPath);
            } else
            {
                NotLoadedMessage();
            }
        }


        //Applies the Comic filter
        private void btnFilterComic_Click(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                _processedImage = ImageModifications.Filter(MatrixFilters.Comic);
                DisplayProcessedWindow();
            } else
            {
                NotLoadedMessage();
            }
        }

        //Applies the Black & White filter
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

        //Applies the HiSatchn filter
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

        //Applies the Invert filter
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

        //Applies the Lomograph filter
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

        //Applies the Polaroid filter
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

        //Applies the LoSatchn filter
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

        //Applies the Sepia filter
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

        //Applies the Pixelate function
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

        //Applies the Quality function
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

        //Saves the already processed image
        private void btnSave_Click(object sender, RoutedEventArgs e)
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
                    
                    //Converts the ImageSource image to Bitmap in order to save in the opened FileStream
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

        //Asks before closing the application
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to close?", "MYT", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Cancel || result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

    }
}
