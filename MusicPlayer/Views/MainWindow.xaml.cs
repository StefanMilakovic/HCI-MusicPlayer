using Microsoft.Win32;
using MusicPlayer.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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

using MusicPlayer.ViewModels;

using System.IO;
using MaterialDesignThemes.Wpf;
using System.Windows.Threading;
using System.Windows.Media.Animation;


namespace MusicPlayer.Views
{
    public partial class MainWindow : Window
    {
        private Song currentSong;
        private bool playing = false;

        private List<string> songList;
        private int currentSongIndex;

        private DispatcherTimer timer;


        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            mediaElement.Volume = 0.5;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;


            //za slajder
            mediaElement.MediaOpened += MediaElement_MediaOpened;
            

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


        private void LoadSongs(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Audio Files (*.mp3;*.wav)|*.mp3;*.wav"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                songList = openFileDialog.FileNames.ToList();
                currentSongIndex = 0;
                mediaElement.Source = new Uri(songList[currentSongIndex]);

                var viewModel = DataContext as MainViewModel;
                if (viewModel != null)
                {
                    string songTitle = System.IO.Path.GetFileName(songList[currentSongIndex]);
                    viewModel.CurrentSongTitle = songTitle;
                    viewModel.SelectedSong = new Song
                    {
                        FilePath = songList[currentSongIndex],
                        Title = System.IO.Path.GetFileNameWithoutExtension(songList[currentSongIndex])
                    };
                }
            }
        }

        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (mediaElement.CanPause && playing)
            {
                mediaElement.Pause();
                playing = false;
                PlayPauseIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.PlayCircle;
            }
            else if (!playing)
            {
                mediaElement.Play();
                playing = true;
                PlayPauseIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.PauseCircle;
            }
        }

        private void PreviousSong_Click(object sender, RoutedEventArgs e)
        {
            if (songList.Count > 0)
            {
                currentSongIndex = (currentSongIndex - 1 + songList.Count) % songList.Count;
                mediaElement.Source = new Uri(songList[currentSongIndex]);
                mediaElement.Play();

                var viewModel = DataContext as MainViewModel;
                if (viewModel != null)
                {
                    string songTitle = System.IO.Path.GetFileName(songList[currentSongIndex]);
                    viewModel.CurrentSongTitle = songTitle;
                    viewModel.SelectedSong = new Song
                    {
                        FilePath = songList[currentSongIndex],
                        Title = System.IO.Path.GetFileNameWithoutExtension(songList[currentSongIndex])
                    };
                }
            }
        }

        private void NextSong_Click(object sender, RoutedEventArgs e)
        {
            if (songList.Count > 0)
            {
                currentSongIndex = (currentSongIndex + 1) % songList.Count;
                mediaElement.Source = new Uri(songList[currentSongIndex]);
                mediaElement.Play();

                var viewModel = DataContext as MainViewModel;
                if (viewModel != null)
                {
                    string songTitle = System.IO.Path.GetFileName(songList[currentSongIndex]);
                    viewModel.CurrentSongTitle = songTitle;
                    viewModel.SelectedSong = new Song
                    {
                        FilePath = songList[currentSongIndex],
                        Title = System.IO.Path.GetFileNameWithoutExtension(songList[currentSongIndex])
                    };

                }
            }
        }


        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaElement != null)
            {
                mediaElement.Volume = e.NewValue / 100;
            }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            var viewModel = DataContext as MainViewModel;
            if (viewModel != null && mediaElement.NaturalDuration.HasTimeSpan)
            {
                TimeSpan currentTime = mediaElement.Position;
                viewModel.CurrentTime = currentTime.ToString(@"mm\:ss");

                TimeSpan totalDuration = mediaElement.NaturalDuration.TimeSpan;
                viewModel.TotalDuration = totalDuration.ToString(@"mm\:ss");
            }
        }
        
        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            NextSong_Click(sender, e);
        }

        
        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            var duration = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
            SeekSlider.Maximum = duration;

            timer.Start();
            var viewModel = DataContext as MainViewModel;
            if (viewModel != null && mediaElement.NaturalDuration.HasTimeSpan)
            {
                TimeSpan totalDuration = mediaElement.NaturalDuration.TimeSpan;

                viewModel.TotalDuration = totalDuration.ToString(@"mm\:ss");
            }
        }
        

        private void SeekSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaElement != null && e.NewValue != mediaElement.Position.TotalSeconds)
            {
                mediaElement.Position = TimeSpan.FromSeconds(e.NewValue);
            }
        }

    }
}