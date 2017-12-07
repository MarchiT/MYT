using ImageProcessor;
using ImageProcessor.Imaging.Filters.Photo;
using ImageProcessor.Imaging.Formats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MYT.ImageOperations
{
    static class ImageModifications
    {
        private static ImageFactory _factory = null;

        //Preps the in stream for the upcoming operations
        public static void Initialize(String path)
        {
            byte[] photoBytes = File.ReadAllBytes(path);

            using (MemoryStream inStream = new MemoryStream(photoBytes))
            {
                _factory = new ImageFactory(preserveExifData: true).Load(inStream);
            }
        }

        //Recives an IMartixFilter parameter and applies it 
        public static BitmapImage Filter(IMatrixFilter filter)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                _factory.Filter(filter).Save(outStream);

                return Converter.ToBitmap(outStream);
            }
        }

        //Changes the image quality to the given percentage
        public static BitmapImage Quality(int percentage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                _factory.Quality(percentage).Save(outStream);

                return Converter.ToBitmap(outStream);
            }
        }

        //Pixelates the image to pixels according to the given pixelSize
        public static BitmapImage Pixelate(int pixelSize)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                _factory.Pixelate(pixelSize).Save(outStream);

                return Converter.ToBitmap(outStream);
            }
        }
    }
}
