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
            Rectangles rectangles = new Rectangles();
            foreach (var rectangle in _grid)
            {
                bool res = rectangle.Collision(rec);
                if (res && !rectangle.In(rec))
                {
                    Rectangle r = rectangle.Fuze(rec);
                    if (r == null)
                    {
                        rectangles.Add(rectangle);
                    }
                    else
                    {
                        rectangles.Add(r);
                        rec = r;
                    }
                }
                else
                {
                    rectangles.Add(rectangle);
                }
                
            }
            rectangles.Add(rec);
            _grid = rectangles;
            return _grid;
        }
    }
}
