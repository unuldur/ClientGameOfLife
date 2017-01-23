using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Data;
using System.Windows.Input;
using GameOfLifeClient.Connexion;
using GameOfLifeClient.Module.Menus.Annotations;
using GameOfLifeClient.Module.Menus.Events;
using GameOfLifeClient.Module.Menus.Services;
using GameOfLiFeClient.Modele;
using Prism.Commands;
using Prism.Events;

namespace GameOfLifeClient.Module.Menus.ViewModels
{
    public class JoinModeMenuViewModel : INotifyPropertyChanged
    {
        public ICommand Return { get; private set; }
        public ICommand Join { get; private set; }
        public ICollectionView CurrentGameModes { get; private set; }

        private readonly IEventAggregator _eventAggregator;
        private readonly IConnexion _connexion;
        private readonly ICurrentGameModeService _gameModeService;
        private IInformationService _service;
        private Timer _timer;

        public JoinModeMenuViewModel(ICurrentGameModeService currentGameModeService, IInformationService service, IEventAggregator eventAggregator, IConnexion connexion)
        {
            _eventAggregator = eventAggregator;
            CurrentGameModes = new ListCollectionView(currentGameModeService.GetDetails(connexion));
            CurrentGameModes.CurrentChanged += new EventHandler(SelectedGameInfoChanged);
            _gameModeService = currentGameModeService;
            Return = new DelegateCommand(ReturnMainMenu);
            Join = new DelegateCommand(JoinGame);
            _connexion = connexion;
            _service = service;
            StartTimer();
        }

        public void StartTimer()
        {
            _timer = new Timer { Interval = 2000 };
            _timer.Start();
            _timer.Elapsed += RefreshCurrentGameModes;
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        private void RefreshCurrentGameModes(object source, ElapsedEventArgs e)
        {
            CurrentGameModes = new ListCollectionView(_gameModeService.GetDetails(_connexion));
            OnPropertyChanged("CurrentGameModes");
        }

        private void SelectedGameInfoChanged(object sender, EventArgs e)
        {
            CurrentGameMode gameMode = this.CurrentGameModes.CurrentItem as CurrentGameMode;
            if (gameMode != null)
            {
                this._eventAggregator.GetEvent<GameModeSelectedEvent>().Publish(gameMode.Name);
            }
        }


        private void ReturnMainMenu()
        {
            StopTimer();
            _eventAggregator.GetEvent<ChangeViewMainRegionEvent>().Publish(ViewNames.MainMenu);

        }

        private void JoinGame()
        {
            StopTimer();
            CurrentGameMode gameMode = this.CurrentGameModes.CurrentItem as CurrentGameMode;
            _service.SetCurrentGameMode(gameMode);
            if (_connexion.AddPlayerToGame(gameMode.Id, _service.GetCurrentPlayer().Id))
            {
                _eventAggregator.GetEvent<ChangeViewMainRegionEvent>().Publish(ViewNames.MainViewGame);
            }
            else
            {
                _eventAggregator.GetEvent<ChangeViewMainRegionEvent>().Publish(ViewNames.AttenteMenu);
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
