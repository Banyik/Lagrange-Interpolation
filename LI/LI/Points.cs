using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace LI
{
    internal static class Points
    {
        public static List<Point> points = new List<Point>();

        public static void AddPoint(Point point)
        {
            points.Add(point);

        }
    }
}
