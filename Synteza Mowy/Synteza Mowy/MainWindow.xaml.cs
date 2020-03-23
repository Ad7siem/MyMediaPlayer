using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Windows.Controls;
using System;
using System.IO;
using Microsoft.Win32;


namespace Synteza_Mowy
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpeechSynthesizer syntezator;
        List<InstalledVoice> glosy;
        private string sciezkaPliku = null;
        private OpenFileDialog openFileDialog;

        public MainWindow()
        {
            InitializeComponent();

            openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Wybierz plik tekstowy";
            openFileDialog.DefaultExt = "txt";
            openFileDialog.Filter = "Pliki tekstowe (*.txt)|*.txt|Pliki tekstowe (*.doc)|*.doc|Pliki tekstowe (*.docx)|*.docx|Pliki tekstowe (*.odt)|*.odt";
            openFileDialog.FilterIndex = 1;

            syntezator = new SpeechSynthesizer();
            glosy = syntezator.GetInstalledVoices().ToList();
            foreach (InstalledVoice _glos in glosy)
                glos.Items.Add(_glos.VoiceInfo.Description);
            if (glos.Items.Count > 0) glos.SelectedIndex = 0;
        }

        private void Czytaj_Click(object sender, RoutedEventArgs e)
        {
            Button btnSender = sender as Button;
            btnSender.IsEnabled = false;
            syntezator.SelectVoice(glosy[glos.SelectedIndex].VoiceInfo.Name);
            syntezator.Volume = (int)glosnosc.Value; //zakres 0 - 100
            syntezator.Rate = (int)szybkosc.Value; //zakres -10 -10
            syntezator.SpeakCompleted += (object _sender, SpeakCompletedEventArgs _e) => { btnSender.IsEnabled = true; };
            syntezator.SpeakAsync(tekst.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            szybkosc.Value = 0;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            glosnosc.Value = 75;
        }

        private void OtworzPlik_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(sciezkaPliku))
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(sciezkaPliku);
                openFileDialog.FileName = Path.GetFileName(sciezkaPliku);
            }
            bool? wynik = openFileDialog.ShowDialog();
            if (wynik.HasValue && wynik.Value)
            {
                sciezkaPliku = openFileDialog.FileName;
                tekst.Text = File.ReadAllText(sciezkaPliku);
                statusBarText.Text = Path.GetFileName(openFileDialog.FileName);
                
            }
        }
    }
}
