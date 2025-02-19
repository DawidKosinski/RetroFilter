# Retro Image Filter - Porównanie implementacji w C++ i Asemblerze

## Opis projektu  
Celem projektu było porównanie implementacji algorytmu filtru retro obrazu w języku wysokiego poziomu (C++) oraz asemblerze. Głównym założeniem było zbadanie różnic w wydajności obu implementacji oraz analiza czasu wykonania algorytmu w zależności od różnych konfiguracji wątków.  

Aplikacja pozwala użytkownikowi na:  
✔ Wczytanie obrazu w formacie BMP  
✔ Wybór biblioteki obliczeniowej (Asembler lub C++)  
✔ Ustawienie liczby wątków  
✔ Przetworzenie obrazu z zastosowaniem filtru retro  
✔ Zapisanie przetworzonego obrazu na dysku  

## Interfejs użytkownika  
Aplikacja została zaprojektowana jako program okienkowy. Użytkownik może wczytać obraz BMP, ustawić parametry przetwarzania i zobaczyć efekt zastosowania filtru retro.  

**Założenia dotyczące plików wejściowych:**  
Pliki wejściowe muszą być w formacie BMP.  

## Technologie  
- **C# (.NET)** - interfejs użytkownika oraz zarządzanie aplikacją  
- **Asembler (x86-64, SIMD)** - wydajna implementacja filtru  
- **C++** - alternatywna implementacja filtru  
- **WinAPI** - obsługa okienkowa aplikacji  
- **Wielowątkowość** - optymalizacja wydajności

## Implementacja algorytmu  

### Biblioteka Asemblerowa  
Asemblerowa wersja algorytmu wykorzystuje instrukcje SIMD oraz operacje na rejestrach do szybkiego przetwarzania pikseli. Kluczowe operacje obejmują:  
- Inicjalizację rejestrów i wczytywanie współczynników efektu retro  
- Obliczanie długości zakresu pikseli do przetworzenia  
- Nakładanie efektu retro poprzez operacje arytmetyczne na pikselach  
- Zamianę co 11. i 12. piksela na biały kolor  
- Zapisanie przetworzonych wartości z powrotem do pamięci  

### Implementacja w C++

Kod w **C++** implementuje ten sam algorytm, jednak działa na tablicach `float[]` i wykonuje obliczenia na poziomie CPU, bez bezpośredniego użycia SIMD.  

## Autor
Dawid Kosiński


 ![Image Alt](https://github.com/DawidKosinski/RetroFilter/blob/44f78dc22f65c39345595d2a308d1f18a4d9a288/Screenshots/Effect.png)

![Image Alt](https://github.com/DawidKosinski/RetroFilter/blob/44f78dc22f65c39345595d2a308d1f18a4d9a288/Screenshots/Screenshot.png)
