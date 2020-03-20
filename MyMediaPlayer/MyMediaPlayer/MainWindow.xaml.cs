using System;
using System.Windows;
using System.Windows.Threading;

namespace MyMediaPlayer
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            inicjujTimer();
        }

        private void inicjujTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(250);
            timer.Tick += (object sender, EventArgs e) =>
            {
                if (mediaElement.Source == null) lbOpis.Content = "<Nie wybrano pliku>";
                {
                    lbOpis.Content = mediaElement.Source.ToString();
                    if (mediaElement.NaturalDuration.HasTimeSpan)
                        lbOpis.Content += String.Format(" - {0} / {1}", mediaElement.Position.ToString(@"mm\:ss"),
                            mediaElement.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                }
            };
            timer.Start();
        }

        #region Przyciski
        private void btnOdtwarzaj_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }

        private void btnWstrzymaj_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        private void btnZatrzymaj_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
        }
        #endregion
    }
}
