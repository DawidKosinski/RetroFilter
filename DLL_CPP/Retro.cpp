
// Retro.cpp - Wersja 1.0 - Autor: Dawid Kosi�ski
// Ta biblioteka w C++ implementuje algorytm nak�adania efektu retro na obraz.

using namespace std;

const float maxValue = 255.0f;		// Maksymalna warto�� dla koloru piksela.


// Funkcja pomocnicza do obliczania minimum z dw�ch liczb.
float min(float a, float b)
{
	return a < b ? a : b;
}


// G��wna funkcja nak�adaj�ca efekt retro.
void Retro(float* pixelsData, int size, float* retroEffectLevel, float* colorRatios, int pixelByteSize, int startPosition, int endPosition) {

	for (int i = startPosition; i < endPosition && i < size; i += pixelByteSize) {

		// Obliczenie warto�ci piksela na podstawie wsp�czynnik�w kolor�w.
		float pixel = pixelsData[i] * colorRatios[0] + pixelsData[i + 1] * colorRatios[1] + pixelsData[i + 2] * colorRatios[2];


		// Co 11 piksel dwa piksele bia�e
		if (i / pixelByteSize % 11 == 0 || (i / pixelByteSize) % 11 == 1) {
			// Ustawienie pikseli na bia�y kolor.
			pixelsData[i] = maxValue;
			pixelsData[i + 1] = maxValue;  
			pixelsData[i + 2] = maxValue;  
		}
		else {
			// Modyfikacja piksela zgodnie z efektem retro.
			pixelsData[i] = pixel;
			pixelsData[i + 1] = min(pixel + retroEffectLevel[1], maxValue);
			pixelsData[i + 2] = min(pixel + retroEffectLevel[2], maxValue);
		}
	}
}

// Eksportowana funkcja wywo�ywana z C#.
extern "C" __declspec(dllexport) void retroCpp(float* pixelsData, int size, float* retroEffectLevel, float* colorRatios, int pixelByteSize, int startPosition, int endPosition) {

	Retro(pixelsData, size, retroEffectLevel, colorRatios, pixelByteSize, startPosition, endPosition);

}