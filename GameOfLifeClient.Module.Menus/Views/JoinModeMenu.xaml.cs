using System;
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
    /// Logique d'interaction pour JoinModeMenu.xaml
    /// </summary>
    public partial class JoinModeMenu : UserControl
    {
        public JoinModeMenu(JoinModeMenuViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
