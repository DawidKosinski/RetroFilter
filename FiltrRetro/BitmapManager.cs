
// BitmapManager.cs - Wersja 1.0 - Autor: Dawid Kosiński
// Ten plik jest odpowiedzialny za operacje na obiektach BitmapSource i Bitmap.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;



namespace retro_assembler_JA
{
    public static class BitmapManager
    {

        // Konwersja BitmapSource na Bitmap.
        public static Bitmap SourceBmp(BitmapSource source)
        {
            // Użycie strumienia pamięci do przechowywania obrazu.
            using (MemoryStream outStream = new MemoryStream())
            {
                // Stworzenie kodera PNG.
                BitmapEncoder imageEncoder = new PngBitmapEncoder();

                // Dodanie ramki obrazu i zapis do strumienia.
                imageEncoder.Frames.Add(BitmapFrame.Create(source));
                imageEncoder.Save(outStream);

                // Utworzenie i zwrócenie obiektu Bitmap.
                System.Drawing.Bitmap bmpImage = new System.Drawing.Bitmap(outStream);
                return new Bitmap(bmpImage);
            }
        }


        // Konwersja BitmapSource na tablicę pikseli w formacie BGR.
        public static float[] BitmapToBgrArray(this BitmapSource sourceBmp)
        {
            // Obliczenie długości linii obrazu w bajtach.
            int rowLength = sourceBmp.PixelWidth * (sourceBmp.Format.BitsPerPixel / 8);

            // Utworzenie tablicy bajtów na piksele.
            byte[] pixelByteArray = new byte[sourceBmp.PixelHeight * rowLength];

            // Kopiowanie pikseli do tablicy bajtów.
            sourceBmp.CopyPixels(pixelByteArray, rowLength, 0);

            // Konwersja bajtów na tablicę float.
            float[] floatArrayPixels = pixelByteArray.ToFloatArray();

            return floatArrayPixels;
        }


        // Konwersja Bitmap na BitmapSource.
        public static BitmapSource ConvertToBitmapSource(System.Drawing.Bitmap source)
        {
            // Użycie strumienia pamięci do przechowywania obrazu.
            using (MemoryStream mem = new MemoryStream())
            {
                // Zapis Bitmap do strumienia jako PNG.
                source.Save(mem, ImageFormat.Png);
                mem.Position = 0;

                // Utworzenie i inicjalizacja BitmapImage.
                BitmapImage bmpImage = new BitmapImage();
                bmpImage.BeginInit();
                bmpImage.StreamSource = mem;
                bmpImage.CacheOption = BitmapCacheOption.OnLoad;
                bmpImage.EndInit();

                // Zwrócenie utworzonego obiektu BitmapSource.
                return bmpImage;
            }
        }
    }
}
