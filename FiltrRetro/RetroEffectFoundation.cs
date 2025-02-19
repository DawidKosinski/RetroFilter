
// RetroEffectFoundation.cs - Wersja 1.0 - Autor: Dawid Kosiński
// Ten plik definiuje abstrakcyjną klasę RetroEffectFoundation, która jest bazą dla klas implementujących różne mechanizmy efektu retro.


using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace retro_assembler_JA
{
    public abstract class RetroEffectFoundation
    {

        // Pola do przechowywania współczynników i indeksów przetwarzania.
        protected int _bytesPerPixel;
        protected int _startPosition;
        protected int _endPosition;
        protected float[] _retroEffectLevel;
        protected float[] _colorBalances = { 0.11f, 0.58f, 0.30f, 0.0f };


        // Konstruktor klasy abstrakcyjnej.
        public RetroEffectFoundation(float[] retroEffectLevel, int pixelByteSize, int startPosition, int endPosition)
        {
            _bytesPerPixel = pixelByteSize;
            _retroEffectLevel = retroEffectLevel;
            _startPosition = startPosition;
            _endPosition = endPosition;
        }

        // Abstrakcyjna metoda do wykonania efektu retro, wymagana do implementacji w klasach pochodnych.
        public abstract void ApplyRetroEffect(float[] allPixels);
    }
}
