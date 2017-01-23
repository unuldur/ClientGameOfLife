using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GameOfLifeClient.Connexion;
using GameOfLifeClient.Connexion.Events;
using GameOfLifeClient.Module.Menus.Events;
using GameOfLifeClient.Module.Menus.Models;
using GameOfLifeClient.Module.Menus.Services;
using GameOfLifeClient.Module.Menus.ViewModels;
using GameOfLifeClient.Module.Menus.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace GameOfLifeClient.Module.Menus.Controllers
{
    class MainRegionController
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IUnityContainer _container;
        private readonly IGameModeInfoService _service;
        private readonly IConnexion _connexion;

        public MainRegionController( IRegionManager regionManager,
                                    IEventAggregator eventAggregator,
                                    IUnityContainer container,
                                    IGameModeInfoService service,
                                    IConnexion connexion)
        {
            if (regionManager == null) throw new ArgumentNullException("regionManager");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
            if (container == null) throw new ArgumentNullException("container");
            if (service == null) throw new ArgumentNullException("service");
            if (connexion == null) throw new ArgumentNullException("connexion");
            this._container = container;
            this._regionManager = regionManager;
            this._eventAggregator = eventAggregator;
            this._service = service;
            _connexion = connexion;
            _connexion.ErrorGames += ErrorGames;
            this._eventAggregator.GetEvent<GameModeSelectedEvent>().Subscribe(GameModeSelected, true);
            this._eventAggregator.GetEvent<ChangeViewMainRegionEvent>().Subscribe(ChangeViewMain, true);
        }

        

        private void GameModeSelected(String gameModeName)
        {
            if (String.IsNullOrEmpty(gameModeName)) return;

            GameModeInfo gameMode = _service.GetGameModeInfos().FirstOrDefault(gamemode => gamemode.Name == gameModeName);

            IRegion regionGameModeDetails = _regionManager.Regions[RegionNames.RegionGameModeDetails];
            if (regionGameModeDetails == null) return;

            GameModeDetails view = regionGameModeDetails.GetView(ViewNames.GameModeDetails) as GameModeDetails;

            if (view == null)
            {
                view = _container.Resolve<GameModeDetails>();
                regionGameModeDetails.Add(view, ViewNames.GameModeDetails);
            }
            else
            {
                regionGameModeDetails.Activate(view);
            }

            GameModeDetailsViewModel viewModel = view.DataContext as GameModeDetailsViewModel;

            if (viewModel != null)
            {
                viewModel.CurrentGameModeInfo = gameMode;
            }
        }

        private void ChangeViewMain(String viewName)
        {
            if (String.IsNullOrEmpty(viewName)) return;

            _regionManager.Regions[RegionNames.MainRegion].RemoveAll();
            _regionManager.RequestNavigate(RegionNames.MainRegion,viewName);

        }

        private void ErrorGames(object sender, ErrorGamesEvent e)
        {
            Application.Current.Dispatcher.Invoke((Action) delegate
            {
                _regionManager.Regions[RegionNames.MainRegion].RemoveAll();
                _regionManager.RequestNavigate(RegionNames.MainRegion, ViewNames.MainMenu);
            });
        }
        
    }
}
