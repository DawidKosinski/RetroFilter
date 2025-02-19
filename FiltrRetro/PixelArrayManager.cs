
// PixelArrayManager.cs - Wersja 1.0 - Autor: Dawid Kosiński
// Ten plik zawiera metody rozszerzające dla tablic float i ich konwersji.


using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Threading.Tasks;


namespace retro_assembler_JA
{

    public static class PixelArrayManager
    {
        // Konwersja tablicy float na tablicę bajtów.
        public static byte[] ConvertToByteArr(this float[] floatArray)
        {
            // Utworzenie nowej tablicy bajtów.
            var byteArray = new byte[floatArray.Length];

            // Konwersja każdego elementu z float na bajt.
            for (int i = 0; i < byteArray.Length; i++)
            {
                byteArray[i] = (byte)floatArray[i];
            }
            return byteArray;
        }

        // Konwersja tablicy float na obraz w formacie BitmapSource.
        public static BitmapSource ConvertBgrArrayToBitmap(
                this float[] pixel, int width, int height, PixelFormat pixelForm)
        {
            // Konwersja tablicy float na tablicę bajtów.
            byte[] bytesArray = pixel.ConvertToByteArr();
            // Użycie metody rozszerzającej dla konwersji na obraz.
            return bytesArray.ConvertBgrArrayToBitmap(width, height, pixelForm);
        }


    }
}
