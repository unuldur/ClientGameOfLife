using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeClient.Module.Game.Modele;
using GameOfLiFeClient.Modele;

namespace GameOfLifeClient.Module.Game.Services
{
    public interface IGridCellService
    {
        GridCells SwitchCell(int x, int y, int idPlayer);
        GridCells SwitchCell(Cell c);
    }
}
