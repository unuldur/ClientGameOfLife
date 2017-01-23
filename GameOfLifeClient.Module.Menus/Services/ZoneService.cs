using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeClient.Connexion;
using GameOfLifeClient.Module.Menus.Models;
using GameOfLiFeClient.Modele;

namespace GameOfLifeClient.Module.Menus.Services
{
    class ZoneService  : IZoneService
    {
        private Zone _zone;
        public Zone InitializeZone(IConnexion connexion,int idGame)
        {
            _zone = new Zone();
            _zone.AddRange(connexion.GetZone(idGame));
            return _zone;
        }

        public Zone SwitchZone(int x, int y, int idPlayer)
        {
            throw new NotImplementedException();
        }
    }
}
