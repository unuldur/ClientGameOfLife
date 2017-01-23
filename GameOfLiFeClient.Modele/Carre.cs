using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameOfLiFeClient.Modele
{
    public class Carre : Polygon
    {
        public Carre(int x,int y,int id,bool zone):base(id,zone)
        {
            Points.Add(new Point(x * 10,y * 10));
            Points.Add(new Point(x * 10 + 10, y * 10));
            Points.Add(new Point(x * 10 + 10, y * 10 + 10));
            Points.Add(new Point(x * 10,y * 10 +10));
        }
    }
}
