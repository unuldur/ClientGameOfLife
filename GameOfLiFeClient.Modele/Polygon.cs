using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace GameOfLiFeClient.Modele
{
    public class Polygon
    {
        public PointCollection Points { get; private set; }
        public int Id { get; private set; }
        public bool Zone { get; private set; }
        private Random _rand = new Random();

        public Polygon(int id, bool zone)
        {
            Id = id;
            Zone = zone;
            Points = new PointCollection();
        }

        /// <summary>
        /// Test si un point appartient a un segment
        /// </summary>
        /// <param name="A">le point A du segment</param>
        /// <param name="B">le point B du segment</param>
        /// <param name="O">le point a tester</param>
        /// <returns>vrai ou faux</returns>
        public bool CollisionPointinSeg(Point A, Point B, Point O)
        {
            Vector AO = new Vector(O.X - A.X, O.Y - A.Y);
            Vector AB = new Vector(B.X - A.X, B.Y - A.Y);
            double kao = Vector.Multiply(AB, AO);
            double kab = Vector.Multiply(AB, AB);
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            return Vector.CrossProduct(AO,AB) == 0 && kao >= 0 && kao <= kab;
        }

        /// <summary>
        /// Test si un segment est compris dans un autre ssegment
        /// </summary>
        /// <param name="A">le premier point du premier segment</param>
        /// <param name="B">le deuxieme point du premier segment</param>
        /// <param name="O">le premier point du deuxieme segment</param>
        /// <param name="P">le deuxieme point du deuxieme segment</param>
        /// <returns></returns>
        public bool CollisionSegInSeg(Point A, Point B, Point O, Point P)
        {
            if (!CollisionPointinSeg(A, B, O))
                return false; 
            if (!CollisionPointinSeg(A, B, P))
                return false;
            return true;
        }

        /// <summary>
        /// retourne l'index dans un polygon du point en collision avec le segment ab le plus pres du point a 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        private int CollisionSegment(Point a,Point b, Polygon p)
        {
            for (int i = 0; i < p.Points.Count; i++)
            {
                Point A = p.Points[i];
                Point B = i == p.Points.Count - 1 ? p.Points[0] : p.Points[i + 1];
                if (CollisionSegInSeg(a, b, A, B))
                {
                    Vector aA = new Vector(a.X - A.X, a.Y - A.Y);
                    Vector aB = new Vector(a.X - B.X, a.Y - B.Y);
                    if (aA.Length < aB.Length)
                    {
                        return i;
                    }
                    return i == p.Points.Count - 1 ? 0 : i + 1;
                }
            }
            return -1;
        }

        private int IntersectSegment(Point A, Point B, Point I, Point P)
        {
            Vector D = new Vector(B.X - A.X ,B.Y - A.Y), E = new Vector(P.X - I.X, P.Y - I.Y);
            double denom = D.X * E.Y - D.Y * E.X;
            if (denom == 0)
                return -1;   // erreur, cas limite
            double t = -(A.X * E.Y - I.X * E.Y - E.X * A.Y + E.X * I.Y) / denom;
            if (t < 0 || t >= 1)
                return 0;
            double u = -(-D.X * A.Y + D.X * I.Y + D.Y * A.X - D.Y * I.X) / denom;
            if (u < 0 || u >= 1)
                return 0;
            return 1;
        }

        bool Collision(Polygon polygon, int nbp, Point P)
        {
            int i;
            Point I = new Point(10000 + _rand.Next(99), 10000 + _rand.Next(99));
            int nbintersections = 0;
            for (i = 0; i < nbp; i++)
            {
                Point A = polygon.Points[i];
                Point B = i == nbp - 1 ? polygon.Points[0] : polygon.Points[i + 1];
                int iseg = IntersectSegment(A, B, I, P);
                if (iseg == -1)
                    return Collision(polygon, nbp, P);
                nbintersections += iseg;
            }
            return nbintersections % 2 == 1;
        }

        public bool? ACote(Polygon p)
        {
            int nbIn = 0;
            for (int i = 0; i < p.Points.Count; i++)
            {
                if (Collision(this, Points.Count, p.Points[i]))
                {
                    nbIn++;
                }
                Console.WriteLine(nbIn);
            }
            if (nbIn == p.Points.Count)
            {
                return null;
            }
            if (nbIn > 0)
            {
                return true;
            }
            return false;
        }

        public Polygon FusePolygon(Polygon p)
        {
            
            if (p.Id != Id || p.Zone != Zone) return null;
            Polygon poly = new Polygon(Id,Zone);
            int indexThis = 0, indexP = 0, nbPointTMe = 0, nbPointTP = 0;
            bool[] pointTraiteMe = new bool[Points.Count];
            bool[] pointTraiteP = new bool[p.Points.Count];
            bool me = true;
            bool debut = true;
            while (nbPointTMe < Points.Count || nbPointTP < p.Points.Count)
            {
                if (me || nbPointTP == p.Points.Count)
                {
                    if (pointTraiteMe[indexThis])
                    {
                        indexThis++;
                        continue;
                    }
                    pointTraiteMe[indexThis] = true;
                    nbPointTMe++;

                    Point A = Points[indexThis];
                    Point B = indexThis == Points.Count - 1 ? Points[0] : Points[indexThis + 1];
                    int index = CollisionSegment(A, B, p);
                    if (index == -1)
                    {
                        poly.Points.Add(A);
                    }
                    else
                    {
                        if (A != p.Points[index])
                        {
                            poly.Points.Add(A);
                        }
                        else
                        {
                            pointTraiteP[index] = true;
                            nbPointTP++;
                        }
                        me = false;
                        indexP = index;
                    }
                    indexThis++;
                }
                else
                {
                    if (pointTraiteP[indexP])
                    {
                        indexP = indexP == p.Points.Count - 1 ? 0 : indexP + 1;
                        continue;
                    }
                    pointTraiteP[indexP] = true;
                    nbPointTP++;

                    Point A = p.Points[indexP];
                    Point B = indexP == p.Points.Count - 1 ? p.Points[0] : p.Points[indexP + 1];
                    int index = p.CollisionSegment(A, B, this);
                    if (index == -1)
                    {
                        poly.Points.Add(A);
                    }
                    else
                    {
                        if (A != Points[index])
                        {
                            poly.Points.Add(A);
                        }
                        else
                        {
                            pointTraiteMe[index] = true;
                            nbPointTMe++;
                        }
                        me = true;
                    }
                    indexP = indexP == p.Points.Count - 1 ? 0 : indexP + 1;
                }
            }
            return poly;
        }
    }
}