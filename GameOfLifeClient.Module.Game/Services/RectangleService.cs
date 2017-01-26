using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GameOfLifeClient.Module.Game.Modele;
using GameOfLiFeClient.Modele;

namespace GameOfLifeClient.Module.Game.Services
{
    class RectangleService : IRectangleService
    {
        private Rectangles _grid = new Rectangles();
        public Rectangles AddRectangle(int x, int y, int id)
        {
            Rectangle rec = new Rectangle(new Point(x*10,y*10),10,10,false,id);
            foreach (var rectangle in _grid)
            {
                if (rectangle.In(rec))
                {
                    return _grid;
                }
            }
            _grid = AjoutRec(_grid,ref rec);
            _grid.Add(rec);
            return _grid;
        }

        private Rectangles AjoutRec(Rectangles recs, ref Rectangle rec)
        {
            Rectangles rectangles = new Rectangles();
            if (recs.Count == 0) return recs;
            foreach (var rectangle in recs)
            {
                Rectangle r = rectangle.Fuze(rec);
                if (r == null)
                {
                    rectangles.Add(rectangle);
                }
                else
                {
                    rectangles = AjoutRec(rectangles,ref r);
                    rec = r;       
                }
            }
            return rectangles;
        }
    }
}
