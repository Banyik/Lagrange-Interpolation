using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LI
{
    internal class Slider
    {
        Point p0;
        Point p1;
        double pos;
        public Slider(Point p0, Point p1, double pos)
        {
            this.p0 = p0;
            this.p1 = p1;
            this.pos = pos;
        }

        public Point P0 { get => p0; set => p0 = value; }
        public Point P1 { get => p1; set => p1 = value; }
        public double Pos { get => pos; set => pos = value; }

        public override bool Equals(object obj)
        {
            if(obj == null || !(obj is Slider)) return false;
            Slider other = (Slider)obj;
            if (other.p0 == p0 && other.p1 == p1 && other.pos == pos) return true;
            else return false;
        }
    }
}
