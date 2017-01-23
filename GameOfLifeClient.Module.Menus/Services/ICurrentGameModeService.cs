using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeClient.Connexion;
using GameOfLifeClient.Module.Menus.Models;

namespace GameOfLifeClient.Module.Menus.Services
{
    public interface ICurrentGameModeService
    {
        CurrentGameModes GetDetails(IConnexion connexion);
    }
}
