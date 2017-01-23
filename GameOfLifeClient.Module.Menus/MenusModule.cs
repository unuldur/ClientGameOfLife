using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using GameOfLifeClient.Connexion;
using GameOfLifeClient.Module.Menus.Controllers;
using GameOfLifeClient.Module.Menus.Services;
using GameOfLifeClient.Module.Menus.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace GameOfLifeClient.Module.Menus
{
    public class MenusModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public MenusModule(IUnityContainer container, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<object, CreateModeMenu>(ViewNames.CreateModeMenu);
            _container.RegisterType<object, MainMenu>(ViewNames.MainMenu);
            _container.RegisterType<object, JoinModeMenu>(ViewNames.JoinModeMenu);
            _container.RegisterType<object, AttenteMenu>(ViewNames.AttenteMenu);
            _container.RegisterType<object, MainViewGame>(ViewNames.MainViewGame);

            _container.RegisterType<IGameModeInfoService, GameModeInfoService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ICurrentGameModeService, CurrentGameModeService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IConnexion, Connexion.Connexion>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IInformationService, InformationService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IPlayersInGameService, PlayersInGameService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IGridCellService, GridCellService>();
            _container.RegisterType<IZoneService, ZoneService>();
            _container.RegisterType<IClient, ClientAsynchronous>();

            _container.Resolve<MainRegionController>();
            _container.Resolve<Connexion.Connexion>();
            
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, () => _container.Resolve<ConnexionMenu>());
            
        }
    }
}
