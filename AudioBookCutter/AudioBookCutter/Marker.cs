using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace AudioBookCutter
{
    class Marker
    {
        /*private int xValue;
        public int XValue
        {
            get { return xValue; }
            set { xValue = value; }
        }*/

        public int calculateX(int windowWidth, TimeSpan audioLength)
        {
            return (int)((this.time.TotalMilliseconds / (audioLength.TotalMilliseconds)) * windowWidth);
        }

        private TimeSpan time;
        public TimeSpan Time
        {
            get { return time; }
            set { time = value; }
        }

        public Marker(TimeSpan time)
        {
            this.time = time;
        }

        public override string ToString()
        {
            string[] temp = time.ToString().Split('.');
            return temp[0] + "." + ( 1 >= temp.Length ? new string('0',3) : temp[1].PadRight(3, '0').Substring(0,3));
        }
    }
}
