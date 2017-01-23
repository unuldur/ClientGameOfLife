using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using GameOfLifeClient.Module.Game.Annotations;
using GameOfLifeClient.Module.Game.Modele;
using GameOfLifeClient.Module.Game.Services;
using GameOfLifeClient.Module.Game.Views;
using Prism.Commands;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace GameOfLifeClient.Module.Game.ViewModels
{
    public class MainViewGameViewModel : INotifyPropertyChanged
    {
        private IPolygonService _polygonService;
        public Polygons Polygons { get; set; }
        public double Scale { get; private set; }
        public ICommand MousePressedCommand { get; private set; }
        public ICommand MouseWheelCommand { get; private set; }
        public ICommand MouseMoveCommand { get; private set; }
        public ICommand RightDownCommand { get; private set; }
        public ICommand RightUpCommand { get; private set; }
        private bool _rightDown;
        private Point _pos;
        public double CenterX { get; private set; }
        public double CenterY { get; private set; }

        public MainViewGameViewModel(IPolygonService polygonService)
        {
            Scale = 1;
            _polygonService = polygonService;
            MousePressedCommand = new DelegateCommand<MouseButtonEventArgs>(MousePressed);
            MouseWheelCommand = new DelegateCommand<MouseWheelEventArgs>(MouseWheel);
            MouseMoveCommand = new DelegateCommand<MouseEventArgs>(MouseMove);
            RightDownCommand = new DelegateCommand<MouseButtonEventArgs>(RightDown);
            RightUpCommand = new DelegateCommand<MouseButtonEventArgs>(RightUp);
        }

        private void MousePressed(MouseButtonEventArgs e)
        {
            Point pos = e.GetPosition(null);
            double height = ((MainViewGame)e.Source).Height;
            double width = ((MainViewGame)e.Source).Width;
            height -= height*Scale;
            width -= width*Scale;
            height /= 2;
            width /= 2;
            Polygons = _polygonService.AddPolygon((int)((pos.X-width-CenterX*Scale)/(10*Scale)),(int)((pos.Y-height-CenterY*Scale) / (10 * Scale)),0);
            foreach (var polygon in Polygons)
            {
                Console.WriteLine(polygon.Points.ToString());
            }
            OnPropertyChanged(nameof(Polygons));
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
