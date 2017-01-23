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
    class CurrentGameModeService:ICurrentGameModeService
    {
        public CurrentGameModes GetDetails(IConnexion connexion)
        {
            List<CurrentGameMode> currentGameMode = connexion.GetGames();
            CurrentGameModes currentGameModes = new CurrentGameModes();
            currentGameModes.AddRange(currentGameMode);
            return currentGameModes;
        }
    }
}
