
// RetroEffectProducer.cs - Wersja 1.0 - Autor: Dawid Kosiński
// Ten plik zawiera klasę RetroEffectProducer, która jest odpowiedzialna za tworzenie odpowiednich obiektów RetroEffectFoundation w zależności od wybranego mechanizmu.


using System;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;



namespace retro_assembler_JA
{
    static class RetroEffectProducer
    {
        // Metoda statyczna do tworzenia obiektów RetroEffectFoundation.
        public static RetroEffectFoundation Create(RetroEffectType processingMethod, float[] retroEffectLevel, int pixelByteSize, int startPosition, int endPosition)
        {

            // Wybór odpowiedniego mechanizmu na podstawie typu.
            switch (processingMethod)
            {
                case RetroEffectType.Assembly:
                    return new RetroAsmHandler(retroEffectLevel, pixelByteSize, startPosition, endPosition);

                case RetroEffectType.Cpp:
                    return new RetroCppHandler(retroEffectLevel, pixelByteSize, startPosition, endPosition);

                default:
                    return null;
            }
        }
    }
}
