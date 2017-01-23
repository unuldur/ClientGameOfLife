using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GameOfLifeClient.Connexion;
using GameOfLifeClient.Module.Menus.Events;
using GameOfLifeClient.Module.Menus.Services;
using GameOfLiFeClient.Modele;
using Prism.Commands;
using Prism.Events;

namespace GameOfLifeClient.Module.Menus.ViewModels
{
    public class ConnexionMenuViewModel
    {
        public ICommand Connexion { get; private set; }
        private readonly IEventAggregator _eventAggregator;
        private readonly IConnexion _connexion;
        private IInformationService _service;
        public String MyName { get;  set; }
        public ConnexionMenuViewModel(IEventAggregator eventAggregator, IConnexion connexion, IInformationService service)
        {
            _eventAggregator = eventAggregator;
            _connexion = connexion;
            _service = service;
            Connexion = new DelegateCommand(Connect);
        }

        private void Connect()
        {
            if(String.IsNullOrEmpty(MyName)) return;
            Player p = _connexion.ConnexionPlayer(MyName);
            if (p == null)
            {
                MyName = "";
                return;
            }
            _service.SetCurrentPlayer(p);
            _eventAggregator.GetEvent<ChangeViewMainRegionEvent>().Publish(ViewNames.MainMenu);
        }
    }
}
