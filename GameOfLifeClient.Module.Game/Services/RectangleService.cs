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
            _grid = AjoutRec(_grid, rec);
            return _grid;
        }

        private Rectangles AjoutRec(Rectangles recs,Rectangle rec)
        {
            Rectangles rectangles = new Rectangles();
            bool addRec = true;
            foreach (var rectangle in recs)
            {
                bool res = rectangle.Collision(rec);
                if (res && addRec)
                {
                    if (rectangle.In(rec))
                    {
                        addRec = false;
                        rectangles.Add(rectangle);
                        continue;
                    }
                    Rectangle r = rectangle.Fuze(rec);
                    if (r == null)
                    {
                        rectangles.Add(rectangle);
                    }
                    else
                    {
                        rectangles = AjoutRec(rectangles, r);
                        rec = rectangles[rectangles.Count - 1];
                        addRec = false;
                    }
                }
                else
                {
                    rectangles.Add(rectangle);
                }

            }
            if (addRec) rectangles.Add(rec);
            return rectangles;
        }
    }
}
