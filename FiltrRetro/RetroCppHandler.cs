
// RetroCppHandler.cs - Wersja 1.0 - Autor: Dawid Kosiński
// Ten plik zawiera definicję klasy RetroCppHandler, która implementuje interfejs RetroEffectFoundation, korzystając z biblioteki w C++.


using System;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace retro_assembler_JA
{
    internal class RetroCppHandler : RetroEffectFoundation
    {

        // Konstruktor klasy.
        public RetroCppHandler(float[] retroEffectLevel, int pixelByteSize, int startPosition, int endPosition) : base(retroEffectLevel, pixelByteSize, startPosition, endPosition)
        { 
        
        }


        // Importowanie metody z zewnętrznej biblioteki DLL napisanej w C++.
        [DllImport(@"Dll/DLL_CPP.dll", EntryPoint = "retroCpp")]
        private static extern void RetroCppAlgorithm(float[] pixelsData, int size, float[] retroFactors, float[] colorRatios, int pixelByteSize, int startPosition, int endPosition);



        // Nadpisana metoda wykonująca efekt retro na pikselach.
        public override void ApplyRetroEffect(float[] allPixels)
        {
            // Wywołanie algorytmu retro napisanego w C++.
            RetroCppAlgorithm(allPixels, allPixels.Length, _retroEffectLevel, _colorBalances, _bytesPerPixel, _startPosition, _endPosition);

        }
    }
}
