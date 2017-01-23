using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeClient.Module.Game.Modele;
using GameOfLiFeClient.Modele;

namespace GameOfLifeClient.Module.Game.Services
{
    class PolygonService : IPolygonService
    {
        private Polygons _grid = new Polygons();
        public Polygons AddPolygon(int x, int y, int id)
        {
            Carre pol = new Carre(x,y,id,false);
            Polygons polygons = new Polygons();
            bool notDifferent = true;
            foreach (var polygon in _grid)
            {
                bool? res = polygon.ACote(pol);
                Console.WriteLine(res);
                if (res == null) return _grid;
                if (res == true)
                {
                    Polygon p = polygon.FusePolygon(pol);
                    polygons.Add(p);
                    notDifferent = false;
                    break;
                }
                polygons.Add(polygon);
            }
            if(notDifferent) polygons.Add(pol);
            _grid = polygons;
            return _grid;
        }
    }
}
