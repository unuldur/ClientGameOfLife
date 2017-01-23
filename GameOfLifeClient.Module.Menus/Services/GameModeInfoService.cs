using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeClient.Module.Menus.Models;

namespace GameOfLifeClient.Module.Menus.Services
{
    public class GameModeInfoService : IGameModeInfoService
    {
        private GameModeInfos _gameModeInfos = null;
        public GameModeInfos GetGameModeInfos()
        {
            if (_gameModeInfos == null)
            {
                _gameModeInfos =  new GameModeInfos()
                {
                    new GameModeInfo("SimpleMode", "Juste un Test"),
                    new GameModeInfo("Conquest", "Chacun ça zone, l'objectif est d'avoir le plus de cellules vivantes dans ça zone à la fin des 10 round de jeu. Ce joue à 2 et un round dure 500 générations.")
                };
            }
            return _gameModeInfos;
        }
    }
}
