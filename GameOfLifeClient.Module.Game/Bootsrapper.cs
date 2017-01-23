using System.Windows;
using GameOfLifeClient.Module.Game.Views;
using Prism.Modularity;
using Prism.Unity;

namespace GameOfLifeClient.Module.Game
{
    public class Bootsrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return new Shell();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(GameModule));
        }
    }
}