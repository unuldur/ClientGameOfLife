using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GameOfLiFeClient.Modele;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Polygon p = new Polygon(1,true);
            p.Points.Add(new Point(0,0));
            p.Points.Add(new Point(2, 0));
            p.Points.Add(new Point(2, 1));
            p.Points.Add(new Point(1, 1));
            p.Points.Add(new Point(1,2));
            p.Points.Add(new Point(0,2));
            Polygon p2 = new Polygon(1,true);
            p2.Points.Add(new Point(1,1));
            p2.Points.Add(new Point(2, 1));
            p2.Points.Add(new Point(2, 2));
            p2.Points.Add(new Point(1, 2));
            Polygon result = p.FusePolygon(p2);
            foreach (var point in result.Points)
            {
                Console.WriteLine(point);
            }
            Console.ReadKey();
        }
    }
}
