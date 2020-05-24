using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disply
{
    public class Coordinate
    {
        int X, Y;
        public int x
        {
            get => X;
            set { if (value < 0) x = 0;
                  else
                    if (value >= Map.Width) x = 119; 
                    else
                        X = value; 
            }
        }
        public int y
        {
            get => Y;
            set {
                if (value < 0) y = 0;
                else
                  if (value >= Map.Height) y = 29;
                else
                    Y = value;
            }
        }
        public Coordinate Clone()
        {
            return new Coordinate(Y, X);
        }
        public static Coordinate operator +(Coordinate c1, Coordinate c2)
        {
            return new Coordinate(c1.x+c2.x,c1.y+c2.y);
        }
        public Coordinate() { x = 0; y = 0; }
        public Coordinate(int a, int b)
        {
            x = b;
            y = a;
        }
    }
}
