using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeClient.Module.Menus.Models;
using GameOfLiFeClient.Modele;

namespace GameOfLifeClient.Module.Menus.Services
{
    public interface IGridCellService
    {
        GridCells SwitchCell(int x, int y, int idPlayer);
        GridCells SwitchCell(List<Cell> c);
    }
}
