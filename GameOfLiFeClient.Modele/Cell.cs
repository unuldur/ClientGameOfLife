using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLiFeClient.Modele
{
    public class Cell
    {
        public Cell(int x, int y, int idPlayer)
        {
            X = x;
            Y = y;
            IdPlayer = idPlayer;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public int IdPlayer { get; private set; }
    }
}
