using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioBookCutter
{
    class Marker
    {
        private int xValue;
        public int XValue
        {
            get { return xValue; }
            set { xValue = value; }
        }

        private TimeSpan time;
        public TimeSpan Time
        {
            get { return time; }
            set { time = value; }
        }

        public Marker(int xValue, TimeSpan time)
        {
            this.xValue = xValue;
            this.time = time;
        }
    }
}
