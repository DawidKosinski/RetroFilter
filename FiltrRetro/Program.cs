
// Program.cs - Wersja 1.0 - Autor: Dawid Kosiński
// Ten plik zawiera punkt wejścia aplikacji i uruchamia główny formularz.

using System.Windows.Forms;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace retro_assembler_JA
{
    internal static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Włączenie stylów wizualnych dla formularzy.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Uruchomienie głównego formularza aplikacji.
            Application.Run(new RetroForm());
        }
    }
}
