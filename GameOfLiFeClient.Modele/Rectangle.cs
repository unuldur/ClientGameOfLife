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

        public bool In(Rectangle rec)
        {
            return rec.Origin.X >= Origin.X && rec.Origin.Y >= Origin.Y && rec.Origin.X + rec.Width <= Origin.X + Width &&
                   rec.Origin.Y + rec.Height <= Origin.Y + Height;
        }


        public Rectangle Fuze(Rectangle rec)
        {
            if (Origin.X == rec.Origin.X && Width == rec.Width && Origin.Y == rec.Origin.Y + rec.Height)
            {
                return new Rectangle(rec.Origin,Width,Height + rec.Height,Zone,Id);
            }
            if (Origin.X == rec.Origin.X && Width == rec.Width && Origin.Y + Height == rec.Origin.Y)
            {
                return new Rectangle(Origin, Width, Height + rec.Height, Zone, Id);
            }
            if (Origin.Y == rec.Origin.Y && Height == rec.Height && Origin.X == rec.Origin.X + rec.Width)
            {
                return new Rectangle(rec.Origin, Width + rec.Width, Height , Zone, Id);
            }
            if (Origin.Y == rec.Origin.Y && Height == rec.Height && Origin.X + Width == rec.Origin.X )
            {
                return new Rectangle(Origin, Width + rec.Width, Height, Zone, Id);
            }
            return null;
        }

        public Rectangle[] Separe(bool horizontale, int pos)
        {
            Rectangle[] rectangles= new Rectangle[2];
            if (horizontale)
            {
                if(pos <= Origin.Y || pos >= Origin.Y+Height) return null;
                rectangles[0] = new Rectangle(Origin, Width,(int)(pos - Origin.Y), Zone, Id);
                rectangles[1] = new Rectangle(new Point(Origin.X, pos), Width, (int)(Origin.Y + Height - pos), Zone, Id);
            }
            else
            {
                if (pos <= Origin.X || pos >= Origin.X + Width) return null;
                rectangles[0] = new Rectangle(Origin, (int)(pos - Origin.X), Height, Zone, Id);
                rectangles[1] = new Rectangle(new Point(pos, Origin.Y), (int)(Origin.X + Width - pos), Height, Zone, Id);
            }
            return rectangles;
        } 
    }
}
