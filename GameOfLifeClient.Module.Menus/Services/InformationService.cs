using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLiFeClient.Modele;

namespace GameOfLifeClient.Module.Menus.Services
{
    public class InformationService : IInformationService
    {
        private Player _player;
        private CurrentGameMode _gameMode;

        public Player GetCurrentPlayer()
        {
            return _player;
        }

        public void SetCurrentPlayer(Player p)
        {
            _player = p;
        }

        public CurrentGameMode GetCurrentGameMode()
        {
            return _gameMode;
        }

        public void SetCurrentGameMode(CurrentGameMode gameMode)
        {
            _gameMode = gameMode;
        }

        public void SetCurrentGameMode(int id, string name)
        {
            _gameMode = new CurrentGameMode(id,name);
        }
    }
}
