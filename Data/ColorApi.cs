using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class ColorApi
    {
        public static ColorApi CreateColor(ColorApi? color = default)
        {
            return new Color();
        }

        public abstract string PickColor();

        internal class Color : ColorApi
        {

            internal Color()
            {
            }

            public override string PickColor()
            {
                string[] _color = { "#FF165D", "#FF9A00", "#F6F7D7", "#3EC1D3", "#2D4059", "#EA5455", "#F07B3F", "#FFD460", "#8675A9", "#00B8A9" };
                Random rnd = new Random();
                return _color[rnd.Next(_color.Length)];
            }
        }
    }
}
