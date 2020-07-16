using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AudioBookCutter
{
    class Command
    {
        private Process cmd;
        private string path;

        public Command(string pathToFile)
        {
            cmd = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            cmd.StartInfo = startInfo;
            this.path = pathToFile;
        }
        public void cutByTimeSpans(List<TimeSpan> times)
        {
            times.OrderBy(time => time.Milliseconds);
            string fileFormat = path.Split('.')[1];
            string argument = "ffmpeg -i " + path;
            string end = " -c copy somewhere";
            string temp;
            temp = argument + " -ss 00:00:00.0 -to " + times[0] + end + 0 + fileFormat;
            cmd.Start();
            int i = 1;
            while (i < times.Count)
            {
                temp = argument + " -ss " + times[i-1] + " -to " + times[i] + end + i + fileFormat;
                cmd.Start();
                i++;
            }
        }
  
    }
}
