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
            List<Rectangle> rectangles = new List<Rectangle>();
            rectangles.Add(rec);
            foreach (var rectangle in _grid)
            {
                if (!rectangle.In(rec)) continue;
                var rectanglesSep = rectangle.Separe(false,(int)rec.Origin.X);
                if (rectanglesSep != null)
                {
                    rectangles.Add(rectanglesSep[0]);
                }
                rectanglesSep = rectangle.Separe(false, (int)rec.Origin.X + rec.Width);
                if (rectanglesSep != null)
                {
                    rectangles.Add(rectanglesSep[1]);
                }
                rectanglesSep = rectangle.Separe(true, (int)rec.Origin.Y);
                if (rectanglesSep != null)
                {
                    Rectangle haut = rectanglesSep[0];
                    rectanglesSep = haut.Separe(false, (int)rec.Origin.X);
                    if (rectanglesSep != null)
                    {
                        haut = rectanglesSep[1];
                    }
                    rectanglesSep = haut.Separe(false, (int)rec.Origin.X + rec.Width);
                    if (rectanglesSep != null)
                    {
                        haut = rectanglesSep[0];
                    }
                    rectangles.Add(haut);
                }
                rectanglesSep = rectangle.Separe(true, (int)rec.Origin.Y + rec.Width);
                if (rectanglesSep != null)
                {
                    Rectangle bas = rectanglesSep[1];
                    rectanglesSep = bas.Separe(false, (int)rec.Origin.X);
                    if (rectanglesSep != null)
                    {
                        bas = rectanglesSep[1];
                    }
                    rectanglesSep = bas.Separe(false, (int)rec.Origin.X + rec.Width);
                    if (rectanglesSep != null)
                    {
                        bas = rectanglesSep[0];
                    }
                    rectangles.Add(bas);
                }
                _grid.Remove(rectangle);
                rectangles.Remove(rec);
                break;
            }
            foreach (Rectangle t in rectangles)
            {
                Rectangle rect = t;
                _grid = AjoutRec(_grid,ref rect);
                _grid.Add(rect);
            }
            
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
