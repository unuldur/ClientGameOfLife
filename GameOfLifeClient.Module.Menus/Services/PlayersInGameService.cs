using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using GameOfLifeClient.Connexion;
using GameOfLifeClient.Module.Menus.Models;
using GameOfLiFeClient.Modele;

namespace GameOfLifeClient.Module.Menus.Services
{
    class PlayersInGameService : IPlayersInGameService
    {
        
        private readonly PlayersInGame _players = new PlayersInGame();

        public PlayersInGame GetPlayers(IConnexion connexion, int idGame)
        {
            List<Player> players = connexion.GetPlayerInGame(idGame);
            _players.AddRange(players);
            
            return  _players;
        }

        public PlayersInGame DeletePlayer(int id)
        {
           Player p = _players.ToList().Find(player => player.Id == id);
            _players.Remove(p);
            return _players;
        }

        public PlayersInGame AddPlayers(Player p)
        {
            _players.Add(p);
            return _players;
        }
    }
}
