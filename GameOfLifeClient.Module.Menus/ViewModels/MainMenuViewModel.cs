
using System;
using System.Windows;
using System.Windows.Input;
using GameOfLifeClient.Module.Menus.Events;
using GameOfLifeClient.Module.Menus.Views;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace GameOfLifeClient.Module.Menus.ViewModels
{
    public class MainMenuViewModel
    {
        public ICommand ExitApplication { get; private set; }
        public ICommand GoCreateMode { get; private set; }
        public ICommand GoJoinMode { get; private set; }
        private IEventAggregator _eventAggregator;

        public MainMenuViewModel(IEventAggregator eventAggregator)
        {
            ExitApplication = new DelegateCommand(Exit);
            GoCreateMode = new DelegateCommand(ChangeViewCreateMode);
            GoJoinMode = new DelegateCommand(ChangeViewJoinMode);
            _eventAggregator = eventAggregator;
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }

        private void ChangeViewCreateMode()
        {
            _eventAggregator.GetEvent<ChangeViewMainRegionEvent>().Publish(ViewNames.CreateModeMenu);
        }

        private void ChangeViewJoinMode()
        {
            _eventAggregator.GetEvent<ChangeViewMainRegionEvent>().Publish(ViewNames.JoinModeMenu);
        }

    }
}
