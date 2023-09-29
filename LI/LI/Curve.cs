using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LI
{
    internal static class Curve
    {
        public static void DrawCurve(Graphics g)
        {
            //for (int i = 0; i < Points.points.Count; i++)
            //{
            //    g.DrawLine(Pens.Black, Points.points[i - 1], Points.points[i]);
            //}
            DrawParametricCurve2D(g, Pens.Black,
                t => CalculateLagrangeX(t),
                t => CalculateLagrangeY(t),
                0, 1);
        }

        public static void DrawParametricCurve2D(this Graphics g,
            Pen pen, Func<double, double> X, Func<double, double> Y,
            double a, double b, double scale = 1.0,
            double cX = 0, double cY = 0, double n = 300)
        {
            double t = a;
            double h = (b - a) / n;
            PointF p0 = new PointF((float)(scale * X(t) + cX),
                                   (float)(scale * Y(t) + cY));
            while (t < b)
            {
                t += h;
                PointF p1 = new PointF((float)(scale * X(t) + cX),
                                       (float)(scale * Y(t) + cY));
                g.DrawLine(pen, p0, p1);
                p0 = p1;
            }
        }

        static double CalculateLagrangeX(double t)
        {
            double ret = 0;
            for (int i = 0; i < Points.points.Count; i++)
            {
                double xj = Points.points[i].X;
                double temp = 1;

                for (int j = 0; j < Points.points.Count; j++)
                {
                    if (i != j)
                        temp *= (t - Sliders.GetSliders()[j].Pos) / (Sliders.GetSliders()[i].Pos - Sliders.GetSliders()[j].Pos); 
                }
                temp *= xj;
                ret += temp;
            }
            return ret;
        }
        static double CalculateLagrangeY(double t)
        {
            double ret = 0;
            for (int i = 0; i < Points.points.Count; i++)
            {
                double yj = Points.points[i].Y;
                double temp = 1;

                for (int j = 0; j < Points.points.Count; j++)
                {
                    if(i != j)
                        temp *= (t - Sliders.GetSliders()[j].Pos) / (Sliders.GetSliders()[i].Pos - Sliders.GetSliders()[j].Pos);
                }
                temp *= yj;
                ret += temp;
            }
            return ret;
        }
    }
}
