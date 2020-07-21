using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
