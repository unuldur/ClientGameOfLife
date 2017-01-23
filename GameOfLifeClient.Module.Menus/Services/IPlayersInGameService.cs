using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeClient.Connexion;
using GameOfLifeClient.Module.Menus.Models;
using GameOfLiFeClient.Modele;

namespace GameOfLifeClient.Module.Menus.Services
{
    public interface IPlayersInGameService
    {
        PlayersInGame GetPlayers(IConnexion connexion, int idGame);
        PlayersInGame DeletePlayer(int id);
        PlayersInGame AddPlayers(Player p);
    }
}
