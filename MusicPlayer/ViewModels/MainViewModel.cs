using Microsoft.Win32;
using MusicPlayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MusicPlayer.ViewModels
{
    
    internal class MainViewModel: INotifyPropertyChanged
    {

        private string _currentSongTitle;
        private Song _selectedSong;

        //--------------------------------------------------------------------------------------------------
        /*
        private readonly DispatcherTimer _sliderTimer;
        private long _currentTime = 0L;


        public MainViewModel()
        {

            _sliderTimer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _sliderTimer.Tick += SliderTimer_Tick;
        }

        private void SongSeekSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            try
            {
                AudioHandler.Instance.SetPosition(SongSeekSlider.Value);
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Unable to set position: {ex.Message}");
            }
        }

        private void SliderTimer_Tick(object sender, EventArgs e)
        {
            if (_vm.Playing)
            {
                SongSeekSlider.Value += 1f;
                _vm.SeekTime = (long)SongSeekSlider.Value;
            }
        }


        */
        //--------------------------------------------------------------------------------------------------


        
        public string CurrentSongTitle
        {
            get { return _currentSongTitle; }
            set
            {
                if (_currentSongTitle != value)
                {
                    _currentSongTitle = value;
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
        
    }
}
