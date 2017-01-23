using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeClient.Module.Menus.Models;
using GameOfLifeClient.Module.Menus.Services;
using GameOfLiFeClient.Modele;

namespace GameOfLifeClient.Module.Menus.Services
{
    class GridCellService : IGridCellService
    {
        private readonly GridCells _grid = new GridCells();

        public GridCells SwitchCell(int x, int y, int idPlayer)
        {
            Cell c = _grid.ToList().Find(cell => cell.X == x && cell.Y == y);
            if (c == null)
            {
                _grid.Add(new Cell(x, y, idPlayer));
            }
            else
            {
                _grid.Remove(c);
            }
            return _grid;
        }
        public GridCells SwitchCell(List<Cell> cells)
        {
            foreach (var c in cells)
            {
                Cell c2 = _grid.ToList().Find(cell => cell.X == c.X && cell.Y == c.Y);
                if (c2 == null)
                {
                    _grid.Add(c);
                }
                else
                {
                    _grid.Remove(c2);
                }
            }
            
            return _grid;
        }
    }
}
