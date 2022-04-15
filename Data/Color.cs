using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Color
    {
        public static string PickColor()
        {
            string[] _color = { "#FF165D", "#FF9A00", "#F6F7D7", "#3EC1D3", "#2D4059", "#EA5455", "#F07B3F", "#FFD460", "#8675A9", "#00B8A9" };
            Random rnd = new Random();
            return _color[rnd.Next(10)];
        }
    }

    //kolory dla kulek (10)
    //jasnoczerwony - #FF165D
    //pomaranczowy - #FF9A00
    //kremowy - #F6F7D7
    //blekitny - #3EC1D3
    //szaro-granatowy - #2D4059
    //bladoczerwony - #EA5455
    //cieplopomaranczowy - #F07B3F
    //bladozolty - #FFD460
    //bladofioletowy - #8675A9
    //turkusowy - #00B8A9
}
