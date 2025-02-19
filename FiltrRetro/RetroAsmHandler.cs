
// RetroAsmHandler.cs - Wersja 1.0 - Autor: Dawid Kosiński
// Ten plik zawiera definicję klasy RetroAsmHandler, która implementuje interfejs RetroEffectFoundation, korzystając z biblioteki w asemblerze.


using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace retro_assembler_JA
{
    internal class RetroAsmHandler : RetroEffectFoundation
    {


        // Konstruktor klasy.
        public RetroAsmHandler(float[] retroEffectLevel, int pixelByteSize, int startPosition, int endPosition) : base(retroEffectLevel, pixelByteSize, startPosition, endPosition)
        { 
        
        }

        // Importowanie metody z zewnętrznej biblioteki DLL napisanej w asemblerze.
        [DllImport(@"DLL\DLL_ASM.dll")]
        private static extern void retro(
            float[] pixelsData, float[] retroEffectLevel, float[] colorRatios, float[] maxRgbValues, int startPosition, int endPosition);


        // Nadpisana metoda wykonująca efekt retro na pikselach.
        public override void ApplyRetroEffect(float[] allPixels)
        {
            // Przygotowanie parametrów dla wywołania funkcji z asemblera.
            float[] maxRgbValues = { 255.0f, 255.0f, 255.0f, 255.0f };
            retro(allPixels, _retroEffectLevel, _colorBalances, maxRgbValues, _startPosition, _endPosition);
        }
    }
}
