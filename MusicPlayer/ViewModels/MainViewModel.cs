using Microsoft.Win32;
using MusicPlayer.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MusicPlayer.ViewModels
{

    internal class MainViewModel: INotifyPropertyChanged
    {
        private string _currentSongTitle;
        private Song _selectedSong;
        private string _currentTime;
        private string _totalDuration;

        public string CurrentSongTitle
        {
            get { return _currentSongTitle; }
            set
            {
                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(value);

                if (_currentSongTitle != fileNameWithoutExtension)
                {
                    _currentSongTitle = fileNameWithoutExtension;
                    OnPropertyChanged();
                }
            }
        }

        public Song SelectedSong
        {
            get { return _selectedSong; }
            set
            {
                if (_selectedSong != value)
                {
                    _selectedSong = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public string CurrentTime
        {
            get { return _currentTime; }
            set
            {
                if (_currentTime != value)
                {
                    _currentTime = value;
                    OnPropertyChanged();
                }
            }
        }


        public string TotalDuration
        {
            get { return _totalDuration; }
            set
            {
                if (_totalDuration != value)
                {
                    _totalDuration = value;
                    OnPropertyChanged();
                }
            }
        }
        

        
    }
}
