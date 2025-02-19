
// RetroEffectController.cs - Wersja 1.0 - Autor: Dawid Kosiński
// Ten plik zawiera klasę RetroEffectController, która zarządza procesem aplikowania efektu retro na obraz, wykorzystując mechanizmy zaimplementowane w innych klasach.

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;



namespace retro_assembler_JA
{
    class RetroEffectController
    {
        // Pola do przechowywania obrazu, interfejsów do efektu retro i zadań.
        private int _threadCount;
        private float[] _allPixelArray;
        private BitmapSource _sourceBmp;
        private List<Task> _processingTasks = new List<Task>();
        private List<RetroEffectFoundation> _retroEffectHandlers = new List<RetroEffectFoundation>();

        // Stałe do obliczeń.
        const int _bitsPerByte = 8;


        // Prywatny konstruktor domyślny - nie jest używany.
        private RetroEffectController() { 
        
        }


        // Konstruktor inicjalizujący RetroManager.
        public RetroEffectController(BitmapSource bmpImage, RetroEffectType processingMethod, float retroFactors, int numberOfThreads)
        {

            // Inicjalizacja i przygotowanie obiektów do przetwarzania.
            _sourceBmp = bmpImage;
            _allPixelArray = GetPixelArray(bmpImage);
            _threadCount = numberOfThreads;
            int segmentLength = CalculateSegmentSize();
            float[] retroEffectLevel = { 0, retroFactors, 2 * retroFactors, 0 };
            for (int segmentIndex = 0; segmentIndex < _threadCount; segmentIndex++)   
            {
                int currentPartIndex = segmentIndex;
                int segmentEnd;
                if (segmentIndex + 1 == _threadCount)
                {
                    segmentEnd = _allPixelArray.Length;
                }
                else
                {
                    segmentEnd = segmentLength * (currentPartIndex + 1) - 1;
                }
                _retroEffectHandlers.Add(RetroEffectProducer.Create(processingMethod, retroEffectLevel, bmpImage.Format.BitsPerPixel / _bitsPerByte,segmentLength * currentPartIndex, segmentEnd));
                _processingTasks.Add(new Task(() => _retroEffectHandlers[currentPartIndex].ApplyRetroEffect(_allPixelArray)));
            }
        }



        // Metoda do pobrania pikseli z BitmapSource.
        private float[] GetPixelArray(BitmapSource bmpImage)
        {
            // Wykorzystanie metody rozszerzającej z klasy Bmp.
            return bmpImage.BitmapToBgrArray();
        }




        // Metoda wykonująca efekt retro i zwracająca wynikowy obraz.
        public BitmapSource ApplyRetroEffect(out System.TimeSpan timeTaken)
        {

            // Użycie stopera do mierzenia czasu wykonania.
            Stopwatch timer = new Stopwatch();
            timer.Start();


            // Uruchomienie zadań w sposób równoległy.
            Parallel.ForEach(_processingTasks, (task) => task.Start());
            Task.WaitAll(_processingTasks.ToArray());

            timer.Stop();
            timeTaken = timer.Elapsed;

            // Konwersja przetworzonych pikseli z powrotem na obraz.
            return _allPixelArray.ConvertBgrArrayToBitmap(_sourceBmp.PixelWidth, _sourceBmp.PixelHeight, _sourceBmp.Format);
        }



        // Metoda dostosowująca długość fragmentu obrazu do przetwarzania przez wątek.
        private int CalculateSegmentSize()
        {
            // Obliczenie i dostosowanie długości fragmentu.
            int segmentLength = _allPixelArray.Length / _threadCount;

            while (segmentLength % (_sourceBmp.Format.BitsPerPixel / _bitsPerByte) != 0) segmentLength++;

            return segmentLength;
        }


    }
}