using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LI
{
    public partial class Form1 : Form
    {
        Graphics canvasG;
        Graphics sliderG;
        Slider currentSlider;
        public Form1()
        {
            InitializeComponent();
            sliderCanvas.Invalidate();
        }

        private void sliderCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    currentSlider = null;
                    foreach (var slider in Sliders.sliders)
                    {
                        if (currentSlider == null &&
                           slider.P0.X - 5 <= e.X && slider.P1.X + 5 > e.X &&
                           slider.P0.Y - 15 <= e.Y && slider.P1.Y + 15 > e.Y)
                        {
                            currentSlider = slider;
                            break;
                        }
                    }
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Right:
                    break;
                case MouseButtons.Middle:
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                    break;
            }
            
        }

        private void sliderCanvas_Paint(object sender, PaintEventArgs e)
        {
            sliderG = e.Graphics;
            sliderG.DrawLine(Pens.Black, new Point(10, sliderCanvas.Height / 2), new Point(sliderCanvas.Width - 10, sliderCanvas.Height / 2));
            foreach (var slider in Sliders.sliders)
            {
                sliderG.DrawLine(Pens.Black, slider.P0, slider.P1);
                sliderG.DrawString(slider.Pos.ToString(), DefaultFont, Brushes.Black, slider.P0);
            }
            foreach (var slider in Sliders.intervalSliders)
            {
                sliderG.DrawLine(Pens.Black, slider.P0, slider.P1);
                sliderG.DrawString(slider.Pos.ToString(), DefaultFont, Brushes.Black, slider.P0);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            sliderCanvas.Invalidate();
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (Sliders.clickCount < 10)
                    {
                        Sliders.AddSlider(sliderCanvas);
                        Points.AddPoint(new Point(e.X, e.Y));
                        sliderCanvas.Invalidate();
                        canvas.Invalidate();
                    }
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Right:
                    Point currentPoint = Point.Empty;
                    foreach (var point in Points.points)
                    {
                        if (
                           point.X - 3 <= e.X && point.X + 6 > e.X &&
                           point.Y - 3 <= e.Y && point.Y + 6 > e.Y)
                        {
                            currentPoint = point;
                            break;
                        }
                    }
                    if (currentPoint != Point.Empty)
                    {
                        int index = Points.points.IndexOf(currentPoint);
                        if (Sliders.sliders.Count > 0)
                        {
                            Sliders.sliders.RemoveAt(0);
                        }
                        else
                        {
                            Sliders.intervalSliders.RemoveAt(index);
                        }
                        Sliders.clickCount--;
                        Points.points.Remove(currentPoint);
                        sliderCanvas.Invalidate();
                        canvas.Invalidate();
                    }
                    break;
                case MouseButtons.Middle:
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                    break;
            }
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            canvasG = e.Graphics;
            foreach (var point in Points.points)
            {
                canvasG.DrawRectangle(Pens.Black, new Rectangle(new Point(point.X - 3, point.Y - 3), new Size(3, 3)));
            }
            if(Points.points.Count > 1)
            {
                Curve.DrawCurve(canvasG);
            }
        }

        private void sliderCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            int limit0 = 10;
            int limit1 = sliderCanvas.Width - 10;
            if(currentSlider != null)
            {
                if(Sliders.sliders.Count > 2)
                {
                    if (currentSlider.Equals(Sliders.sliders.First()))
                    {
                        limit1 = Sliders.sliders[1].P0.X;
                    }
                    else if(currentSlider.Equals(Sliders.sliders.Last()))
                    {
                        limit0 = Sliders.sliders[Sliders.sliders.Count - 2].P0.X;
                    }
                    else
                    {
                        int index = Sliders.sliders.IndexOf(currentSlider);
                        limit0 = Sliders.sliders[index - 1].P0.X;
                        limit1 = Sliders.sliders[index + 1].P0.X;
                    }
                }
                if(e.X > limit0 && e.X < limit1)
                {
                    currentSlider.P0 = new Point(e.X, currentSlider.P0.Y);
                    currentSlider.P1 = new Point(e.X, currentSlider.P1.Y);
                    currentSlider.Pos = Math.Round(Math.Abs(1-(Sliders.scale + (e.X-10)) / Sliders.scale), 2);
                    sliderCanvas.Invalidate();
                    canvas.Invalidate();
                }
            }
        }

        private void sliderCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            currentSlider = null;
            sliderCanvas.Invalidate();
        }
    }
}
