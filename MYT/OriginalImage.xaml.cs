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
using System.Windows.Shapes;

namespace MYT
{
    /// <summary>
    /// Interaction logic for OriginalImage.xaml
    /// </summary>
    public partial class OriginalImage : Window
    {
        public OriginalImage()
        {
            InitializeComponent();
        }

        //Initialize logic for the newly opened Original Image Window
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Normal;
            image.Focus();
        }
    }
}
