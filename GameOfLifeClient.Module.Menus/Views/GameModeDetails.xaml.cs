﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameOfLifeClient.Module.Menus.ViewModels;

namespace GameOfLifeClient.Module.Menus.Views
{
    /// <summary>
    /// Logique d'interaction pour GameModeDetails.xaml
    /// </summary>
    public partial class GameModeDetails : UserControl
    {
        public GameModeDetails( GameModeDetailsViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }


    }
}
