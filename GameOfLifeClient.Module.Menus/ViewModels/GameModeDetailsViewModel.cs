using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeClient.Module.Menus.Annotations;
using GameOfLifeClient.Module.Menus.Models;

namespace GameOfLifeClient.Module.Menus.ViewModels
{
    public class GameModeDetailsViewModel : INotifyPropertyChanged
    {
        private GameModeInfo _gameModeInfo;
        public GameModeInfo CurrentGameModeInfo
        {
            get { return _gameModeInfo; }
            set
            {
                _gameModeInfo = value;
                OnPropertyChanged("CurrentGameModeInfo");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
