
// Form1.cs - Wersja 1.0 - Autor: Dawid Kosiński
// Ten plik zawiera definicję głównego okna formularza aplikacji, obsługę zdarzeń oraz logikę interfejsu użytkownika.


using System;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection.Emit;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;



namespace retro_assembler_JA
{
    public partial class RetroForm : Form
    {
        // Deklaracja zmiennych dla zarządzania czasem, obrazami i pikselami.
        private Bitmap originalBitmap;
        private Bitmap retroBitmap;
        private RetroEffectController retroEffectController;
        private Stopwatch timer;
        private float[] pixelsData;

        // Konstruktor formularza.
        public RetroForm()
        {
            InitializeComponent();
            timer = new Stopwatch();
            // Ustawienie domyślnej liczby wątków.
            threadsNumber.Value = System.Environment.ProcessorCount;
            threads.Text = (threadsNumber.Value).ToString();
        }


        // Metoda do otwierania obrazu.
        public void openImage()
        {
            // Konfiguracja okna dialogowego do otwierania plików.
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "Plik graficzny (*.bmp)|*.BMP; *.bmp",
                FileName = "*.bmp"
            };
            openDialog.ShowDialog();

            // Wczytanie wybranego obrazu.
            if (openDialog.FileName != "*.bmp")
            {
                Image image = Image.FromFile(openDialog.FileName);
                openDialog.Dispose();
                originalBitmap = (Bitmap)image;
            }
        }


        private Bitmap ScaleBitmap(Bitmap bmp, PictureBox picBox)
        {

            float ratio = 1.0f;
            int thumbHeight = 0;
            int thumbWidth = 0;

            if (bmp.Height > picBox.Height || bmp.Width > picBox.Width)
            {
                Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

                if (bmp.Height >= bmp.Width){

                    ratio = (((float)bmp.Width) / ((float)bmp.Height));
                    thumbHeight = picBox.Height;
                    thumbWidth = (int)((thumbHeight) * (ratio));
                }
                else{

                    ratio = (((float)bmp.Height) / ((float)bmp.Width));
                    thumbWidth = picBox.Width;
                    thumbHeight = (int)((thumbWidth) * (ratio));
                }


                Image myThumbnail = bmp.GetThumbnailImage(thumbWidth, thumbHeight, myCallback, IntPtr.Zero);

                return new Bitmap(myThumbnail);
            }

            return bmp;
        }


        private void CS_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Pomocnicza metoda dla procesu skalowania obrazów.
        public bool ThumbnailCallback()
        {
            return false;
        }


        // Obsługa zdarzenia kliknięcia przycisku zapisu obrazu.
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Zapisanie przetworzonego obrazu do pliku.
            if (retroBitmap != null)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.FileName = "obrazRetro";
                saveDialog.Filter = "Plik graficzny (*.bmp)|*.BMP; *.bmp";
                saveDialog.ShowDialog();
                retroBitmap.Save(saveDialog.FileName);
                saveDialog.Dispose();
            }
        }



        // Obsługa zdarzenia kliknięcia przycisku wczytywania obrazu.
        private void LoadButton_Click(object sender, EventArgs e)
        {
            openImage();
            pictureBefore.Image = ScaleBitmap(originalBitmap, pictureBefore);
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        // Obsługa zdarzenia przesunięcia suwaka wątków.
        private void threads_Scroll(object sender, EventArgs e)
        {
            threads.Text = (threadsNumber.Value).ToString();

        }


        // Obsługa zdarzenia kliknięcia przycisku rozpoczęcia.
        private void startButton_Click(object sender, EventArgs e)
        {
            if ((ASM.Checked || CS.Checked) && !pictureBefore.Image.Equals(null)){

                String retroMechanism = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Name;
                BitmapSource bs = BitmapManager.ConvertToBitmapSource(originalBitmap);
                pixelsData = BitmapManager.BitmapToBgrArray(bs);
                double[] wyniki = new double[320];
                RetroEffectController manager = null;
                if (ASM.Checked)
                {
                    manager = new RetroEffectController(bs, RetroEffectType.Assembly, 20, threadsNumber.Value);
                }
                if (CS.Checked)
                {
                    manager = new RetroEffectController(bs, RetroEffectType.Cpp, 20, threadsNumber.Value);
                }

                TimeSpan t;
                BitmapSource bsa = manager.ApplyRetroEffect(out t);
                pictureAfter.Image = ScaleBitmap(BitmapManager.SourceBmp(bsa), pictureAfter);
                retroBitmap = BitmapManager.SourceBmp(bsa);
                time.Text = t.ToString();
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
 }
