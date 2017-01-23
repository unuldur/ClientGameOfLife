using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using GameOfLifeClient.Connexion;
using GameOfLifeClient.Connexion.Events;
using GameOfLifeClient.Module.Menus.Annotations;
using GameOfLifeClient.Module.Menus.Events;
using GameOfLifeClient.Module.Menus.Models;
using GameOfLifeClient.Module.Menus.Services;
using Prism.Events;

namespace GameOfLifeClient.Module.Menus.ViewModels
{
    public class AttenteMenuViewModel : INotifyPropertyChanged
    {
        public ICollectionView Players { get; private set; }
        private IEventAggregator _eventAggregator;
        private IConnexion _connexion;
        private IPlayersInGameService _servicePlayer;
        private IInformationService _serviceInfo;

        public AttenteMenuViewModel(IEventAggregator eventAggregator,IInformationService serviceInfo, IConnexion connexion, IPlayersInGameService servicePlayer)
        {
            _eventAggregator = eventAggregator;
            _connexion = connexion;
            _servicePlayer = servicePlayer;
            _serviceInfo = serviceInfo;
            Players = new ListCollectionView(servicePlayer.GetPlayers(connexion,serviceInfo.GetCurrentGameMode().Id));
            _connexion.NewPlayer += RefreshPlayers;
            _connexion.DeletePLayer += DeletePlayer;
            _connexion.StartGame += Start;
        }

        private void RefreshPlayers(object source, NewPlayerEvent e)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal,
                (Action) delegate()
                {
                    Players = new ListCollectionView(_servicePlayer.AddPlayers(e.Player));
                }
           );
            OnPropertyChanged(nameof(Players));
        }

        private void DeletePlayer(object source, DeletePlayerEvent e)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal,
                (Action)delegate ()
                {
                    Players = new ListCollectionView(_servicePlayer.DeletePlayer(e.IdPlayer));
                }
            );
            OnPropertyChanged(nameof(Players));
        }

        private void Start(object sender, StartGameEvent e)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal,
                (Action)delegate ()
                {
                    _eventAggregator.GetEvent<ChangeViewMainRegionEvent>().Publish(ViewNames.MainViewGame);
                }
            );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
