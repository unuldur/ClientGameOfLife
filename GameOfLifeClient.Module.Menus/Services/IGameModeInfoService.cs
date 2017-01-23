using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeClient.Module.Menus.Models;

namespace GameOfLifeClient.Module.Menus.Services
{
    public interface IGameModeInfoService
    {
        GameModeInfos GetGameModeInfos();
    }
}
