// RetroEffectType.cs - Wersja 1.0 - Autor: Dawid Kosiński
// Ten plik definiuje wyliczenie RetroEffectType, które określa dostępne typy mechanizmów do stosowania efektu retro.

namespace retro_assembler_JA
{
    // Wyliczenie reprezentujące dostępne mechanizmy efektu retro.
    enum RetroEffectType
    {
        Undefined, // Niezdefiniowany typ
        Assembly,  // Mechanizm zaimplementowany w asemblerze
        Cpp        // Mechanizm zaimplementowany w C++
    }
}