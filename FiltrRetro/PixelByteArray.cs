
// PixelByteArray.cs - Wersja 1.0 - Autor: Dawid Kosiński
// Ten plik zawiera metody rozszerzające dla tablic bajtów i ich konwersji.


using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;


namespace retro_assembler_JA
{
    public static class PixelByteArray
    {

        // Konwersja tablicy bajtów na tablicę float.
        public static float[] ToFloatArray(this byte[] byteArray)
        {
            // Utworzenie nowej tablicy float.
            var newFloatArray = new float[byteArray.Length];

            // Konwersja każdego elementu z bajtu na float.
            for (int i = 0; i < newFloatArray.Length; i++)
            {
                newFloatArray[i] = (float)byteArray[i];
            }
            return newFloatArray;
        }


        // Konwersja tablicy bajtów na obraz w formacie BitmapSource.
        public static BitmapSource ConvertBgrArrayToBitmap(
           this byte[] pixelsData, int width, int height, PixelFormat pixelForm)
        {

            // Stałe dla formatowania obrazu.
            const int byteSizeInBits = 8;
            const int imageDpi = 96;

            // Tworzenie obrazu zapisywalnego.
            WriteableBitmap bmpImage = new WriteableBitmap(width, height, imageDpi, imageDpi, pixelForm, null);

            // Zapisanie pikseli do obrazu.
            bmpImage.WritePixels(new Int32Rect(0, 0, width, height), pixelsData, width * (bmpImage.Format.BitsPerPixel / byteSizeInBits), 0);

            return bmpImage;
        }

    }
}
