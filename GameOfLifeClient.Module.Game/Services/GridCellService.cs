using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeClient.Module.Game.Modele;
using GameOfLiFeClient.Modele;

namespace GameOfLifeClient.Module.Game.Services
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

        public GridCells SwitchCell(Cell c)
        {
            Cell c2 = _grid.ToList().Find(cell => cell.X == c.X && cell.Y == c.Y && c.IdPlayer == cell.IdPlayer);
            if (c2 == null)
            {
                _grid.Add(c);
            }
            else
            {
                _grid.Remove(c2);
            }
            return _grid;
        }
    }
}
