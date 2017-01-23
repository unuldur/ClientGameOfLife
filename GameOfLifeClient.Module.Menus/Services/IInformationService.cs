using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLiFeClient.Modele;

namespace GameOfLifeClient.Module.Menus.Services
{
    public interface IInformationService
    {
        Player GetCurrentPlayer();
        void SetCurrentPlayer(Player p);
        CurrentGameMode GetCurrentGameMode();
        void SetCurrentGameMode(CurrentGameMode gameMode);
        void SetCurrentGameMode(int id, string name);

    }
}
