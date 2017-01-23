using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeClient.Module.Game.Modele;

namespace GameOfLifeClient.Module.Game.Services
{
    public interface IPolygonService
    {
        Polygons AddPolygon(int x, int y, int id);
    }
}
