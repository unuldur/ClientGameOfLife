using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GameOfLifeClient.Connexion;
using GameOfLifeClient.Module.Menus.Models;
using GameOfLiFeClient.Modele;

namespace GameOfLifeClient.Module.Menus.Services
{
    public interface IZoneService
    {
       Zone InitializeZone(IConnexion connexion,int idGame);
       Zone SwitchZone(int x, int y, int idPlayer);
    }
}
