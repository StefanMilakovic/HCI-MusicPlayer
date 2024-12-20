using Microsoft.Win32;
using MusicPlayer.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicPlayer.Views
{
    public partial class MainWindow : Window
    {
        private Song currentSong;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        
        
        private void LoadSong(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Audio Files (*.mp3;*.wav)|*.mp3;*.wav";
            if (openFileDialog.ShowDialog() == true)
            {
                // Kreiraj novi objekat pesme
                currentSong = new Song
                {
                    FilePath = openFileDialog.FileName,
                    Title = System.IO.Path.GetFileNameWithoutExtension(openFileDialog.FileName)
                };

                // Podesi MediaElement da učita pesmu
                mediaElement.Source = new Uri(currentSong.FilePath);
                mediaElement.Play();
            }
        }
        
        
    }
}