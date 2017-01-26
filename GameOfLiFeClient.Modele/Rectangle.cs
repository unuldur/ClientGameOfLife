using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameOfLiFeClient.Modele
{
    public class Rectangle
    {
        public Point Origin { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool Zone { get; private set; }
        public int Id { get; private set; }

        public List<Rectangle> RectanglesTouche { get; private set; }

        public Rectangle(Point origin, int width, int height, bool zone, int id)
        {
            Origin = origin;
            Width = width;
            Height = height;
            Zone = zone;
            Id = id;
            RectanglesTouche = new List<Rectangle>();
        }

        public Rectangle(bool zone, int id)
        {
            Zone = zone;
            Id = id;
        }

        public bool Collision(Rectangle rec)
        {
            if (rec.Id != Id || rec.Zone != Zone) return false;
            return !(rec.Origin.X > Origin.X + Width) && !(rec.Origin.X + rec.Width < Origin.X) && !(rec.Origin.Y > Origin.Y + Height) && !(rec.Origin.Y + rec.Height < Origin.Y);
        }

        public bool In(Rectangle rec)
        {
            return rec.Origin.X >= Origin.X && rec.Origin.Y >= Origin.Y && rec.Origin.X + rec.Width <= Origin.X + Width &&
                   rec.Origin.Y + rec.Height <= Origin.Y + Height;
        }

        public bool CollisionPoint(Point p)
        {
            return p.X >= Origin.X
                   && p.X < Origin.X + Width
                   && p.Y >= Origin.Y
                   && p.Y < Origin.Y + Height;
        }

        public Rectangle Fuze(Rectangle rec)
        {
            if (rec.CollisionPoint(new Point(Origin.X + Width * 0.5, Origin.Y - 1)) && rec.Width == Width)
            {
                return new Rectangle(rec.Origin,Width,Height + rec.Height,Zone,Id);
            }
            if (rec.CollisionPoint(new Point(Origin.X + Width * 0.5, Origin.Y + Height + 1)) && rec.Width == Width)
            {
                return new Rectangle(Origin, Width, Height + rec.Height, Zone, Id);
            }
            if (rec.CollisionPoint(new Point(Origin.X - 1, Origin.Y + Height * 0.5)) && rec.Height == Height)
            {
                return new Rectangle(rec.Origin, Width + rec.Width, Height , Zone, Id);
            }
            if (rec.CollisionPoint(new Point(Origin.X + Width + 1, Origin.Y + Height * 0.5)) && rec.Height == Height)
            {
                return new Rectangle(Origin, Width + rec.Width, Height, Zone, Id);
            }
            return null;
        }
    }
}
