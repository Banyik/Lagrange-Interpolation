using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace LI
{
    internal static class Sliders
    {
        public static List<Slider> sliders = new List<Slider>();
        public static List<Slider> intervalSliders = new List<Slider>();
        public static double scale = 0;
        public static int clickCount = 0;
        public static void AddSlider(PictureBox sliderCanvas)
        {
            if(scale == 0)
            {
                scale = (double)sliderCanvas.Width - 20;
            }
            clickCount++;
            sliders.Clear();
            if (clickCount > 0 && intervalSliders.Count == 0)
            {
                intervalSliders.Add(new Slider(new Point(10, sliderCanvas.Height / 2 + 5), new Point(10, sliderCanvas.Height / 2 - 5), Math.Round(0.0, 2)));
            }
            if (clickCount > 1 && intervalSliders.Count == 1)
            {
                intervalSliders.Add(new Slider(new Point(sliderCanvas.Width - 10, sliderCanvas.Height / 2 + 5), new Point(sliderCanvas.Width - 10, sliderCanvas.Height / 2 - 5), Math.Round(1.0, 2)));
            }
            if (clickCount > 2)
            {
                int index = clickCount - 1;
                for (int i = 1; i < index; i++)
                {
                    sliders.Add(new Slider(new Point(sliderCanvas.Width / index * i, sliderCanvas.Height / 2 + 5), new Point(sliderCanvas.Width / index * i, sliderCanvas.Height / 2 - 5), Math.Round((scale / index / scale) * i, 2)));
                }
            }
        }

        public static List<Slider> GetSliders()
        {
            List<Slider> temp = new List<Slider>();
            temp.Add(intervalSliders[0]);
            if(sliders.Count > 0)
            {
                temp.AddRange(sliders);
            }
            temp.Add(intervalSliders.Last());
            return temp;
        }
    }
}
