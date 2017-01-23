using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using GameOfLifeClient.Connexion;
using GameOfLifeClient.Connexion.Events;
using GameOfLifeClient.Module.Menus.Annotations;
using GameOfLifeClient.Module.Menus.Models;
using GameOfLifeClient.Module.Menus.Services;
using GameOfLifeClient.Module.Menus.Views;
using GameOfLiFeClient.Modele;
using Prism.Commands;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace GameOfLifeClient.Module.Menus.ViewModels
{
    public class MainViewViewModel : INotifyPropertyChanged
    {
        private IGridCellService _gridCell;
        private IConnexion _connexion;
        private IInformationService _informationService;
        private IZoneService _zoneService;
        public GridCells GridCells { get; private set; }
        public Zone Zone { get; private set; }
        public double Scale { get; private set; }
        public ICommand MousePressedCommand { get; private set; }
        public ICommand MouseWheelCommand { get; private set; }
        public ICommand MouseMoveCommand { get; private set; }
        public ICommand RightDownCommand { get; private set; }
        public ICommand RightUpCommand { get; private set; }
        public ICommand StartPressedCommand { get; private set; }
        private bool _rightDown;
        private Point _pos;
        public double CenterX { get; private set; }
        public double CenterY { get; private set; }
        public bool ButtonClic { get; private set; }
        private int _currentGen = 0;
        private Timer _timer = new Timer(100);

        public MainViewViewModel(IGridCellService gridCell, IConnexion connexion, IInformationService informationService, IZoneService zoneService)
        {
            ButtonClic = true;
            Scale = 1;
            _gridCell = gridCell;
            MousePressedCommand = new DelegateCommand<MouseButtonEventArgs>(MousePressed);
            MouseWheelCommand = new DelegateCommand<MouseWheelEventArgs>(MouseWheel);
            MouseMoveCommand = new DelegateCommand<MouseEventArgs>(MouseMove);
            RightDownCommand = new DelegateCommand<MouseButtonEventArgs>(RightDown);
            RightUpCommand = new DelegateCommand<MouseButtonEventArgs>(RightUp);
            StartPressedCommand = new DelegateCommand(StartPressed);
            _connexion = connexion;
            _informationService = informationService;
            _zoneService = zoneService;
            Zone = new Zone();
            _connexion.SwitchCell += SwitchCell;
            _connexion.StartGenEv += StartGen;
            _timer.Elapsed += GetGen;
        }

        private void StartPressed()
        {
            ButtonClic = false;
            _connexion.StartGen(_informationService.GetCurrentGameMode().Id);
            OnPropertyChanged(nameof(ButtonClic));
        }

        private void MousePressed(MouseButtonEventArgs e)
        {
            Zone = _zoneService.InitializeZone(_connexion, _informationService.GetCurrentGameMode().Id);
            OnPropertyChanged(nameof(Zone));
            Point pos = e.GetPosition(null);
            double height = ((MainViewGame)e.Source).Height;
            double width = ((MainViewGame)e.Source).Width;
            height -= height*Scale;
            width -= width*Scale;
            height /= 2;
            width /= 2;
            _connexion.Switch((int)((pos.X - width - CenterX * Scale) / (10 * Scale)), (int)((pos.Y - height - CenterY * Scale) / (10 * Scale)),_informationService.GetCurrentPlayer().Id,_informationService.GetCurrentGameMode().Id );
        }

        private void SwitchCell(object sender, SwitchCellEvent e)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal,
                (Action) delegate()
                {
                    GridCells = _gridCell.SwitchCell(e.X,e.Y, e.IdPlayer);
                }
            );
            OnPropertyChanged(nameof(GridCells));
        }

        private void StartGen(object sender, StartGenEvent e)
        {
            _timer.Start();
        }

        private void GetGen(object sender, EventArgs e)
        {
            _currentGen++;
            List<Cell> diff;
            try
            {
                diff = _connexion.GetGen(_informationService.GetCurrentGameMode().Id, _currentGen);
                if (diff == null)
                {
                    _timer.Stop();
                    ButtonClic = true;
                    _currentGen = 0;
                    OnPropertyChanged(nameof(ButtonClic));
                    return;
                }
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal,
                    (Action) delegate()
                    {
                        GridCells = _gridCell.SwitchCell(diff);
                    }
                );
                OnPropertyChanged(nameof(GridCells));
            }
            catch (Exception)
            {
                _currentGen--;
            }
        }

        private void RightDown(MouseButtonEventArgs e)
        {
            _pos = e.GetPosition(null);
            _rightDown = true;
        }

        private void MouseMove(MouseEventArgs e)
        {
           if(!_rightDown) return;
            Point currentPos = e.GetPosition(null);
            CenterX += (currentPos.X - _pos.X)*(1/Scale);
            CenterY += (currentPos.Y - _pos.Y)*(1/Scale);
            OnPropertyChanged(nameof(CenterX));
            OnPropertyChanged(nameof(CenterY));
            _pos = currentPos;
        }

        private void RightUp(MouseButtonEventArgs e)
        {
            _rightDown = false;
        }

        private void MouseWheel(MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                Scale *= 1.1;
            }
            else
            {
                Scale /= 1.1;
            }
            OnPropertyChanged(nameof(Scale));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
