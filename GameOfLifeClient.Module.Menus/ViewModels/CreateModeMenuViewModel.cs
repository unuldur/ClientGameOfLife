using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GameOfLifeClient.Connexion;
using GameOfLifeClient.Module.Menus.Events;
using GameOfLifeClient.Module.Menus.Models;
using GameOfLifeClient.Module.Menus.Services;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace GameOfLifeClient.Module.Menus.ViewModels
{
    public class CreateModeMenuViewModel
    {
        public ICommand Return { get; private set; }
        public ICommand Create { get; private set; }
        private IEventAggregator _eventAggregator;
        private IConnexion _connexion;
        private IInformationService _service;
        public ICollectionView GameModeInfos { get; private set; } 

        public CreateModeMenuViewModel(IGameModeInfoService gameModeInfoService,IInformationService service,IEventAggregator eventAggregator,IConnexion connexion)
        {
            _eventAggregator = eventAggregator;
            Return = new DelegateCommand(ReturnMainMenu);
            Create = new DelegateCommand(CreateMode);
            _connexion = connexion;
            GameModeInfos = new ListCollectionView(gameModeInfoService.GetGameModeInfos());
            this.GameModeInfos.CurrentChanged += new EventHandler(SelectedGameInfoChanged);
            _service = service;
        }
        

        private void ReturnMainMenu()
        {
            _eventAggregator.GetEvent<ChangeViewMainRegionEvent>().Publish(ViewNames.MainMenu);

        }

        private void CreateMode()
        {
            int id = _connexion.InitGameMode(((GameModeInfo)GameModeInfos.CurrentItem).Name);
            _service.SetCurrentGameMode(id, ((GameModeInfo)GameModeInfos.CurrentItem).Name);
            _eventAggregator.GetEvent<ChangeViewMainRegionEvent>().Publish(ViewNames.AttenteMenu);
        }

        private void SelectedGameInfoChanged(object sender, EventArgs e)
        {
            GameModeInfo gameMode = this.GameModeInfos.CurrentItem as GameModeInfo;
            if (gameMode != null)
            {
                this._eventAggregator.GetEvent<GameModeSelectedEvent>().Publish(gameMode.Name);
            }
        }
    }
}
