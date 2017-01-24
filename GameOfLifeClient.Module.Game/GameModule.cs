using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeClient.Module.Game.Services;
using GameOfLifeClient.Module.Game.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace GameOfLifeClient.Module.Game
{
    public class GameModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public GameModule(IUnityContainer container, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<IGridCellService, GridCellService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IRectangleService, RectangleService>(new ContainerControlledLifetimeManager());
            _regionManager.RegisterViewWithRegion("MainRegion", () => _container.Resolve<MainViewGame>());
        }
    }
}
