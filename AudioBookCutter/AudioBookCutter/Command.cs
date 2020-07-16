using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace AudioBookCutter
{
    class Command
    {
        private Process cmd;
        private string path;
        private ProcessStartInfo startInfo;

        public Command(string pathToFile)
        {
            this.path = pathToFile;
        }
        public void cutByTimeSpans(List<TimeSpan> times, TimeSpan length)
        {
            //Thread ffmpeg = new Thread(() => cutByTimeSpansIn(times, length));
            cutByTimeSpansIn(times, length);

        }
        private void cutByTimeSpansIn(List<TimeSpan> times, TimeSpan length)
        {
            init();
            times.OrderBy(time => time.Milliseconds);
            string fileFormat = Path.GetExtension(path);
            string argument = "/C ffmpeg -i " + Path.GetFullPath(path);
            string end = " -c copy " + Path.GetFullPath(@"G:/EKE/szakmai gyak/AudioBookCutter\") + Path.GetFileNameWithoutExtension(path);
            string exit = " & exit";
            string temp;
            temp = argument + " -ss 00:00:00.0 -to " + times[0] + end + 0 + fileFormat + exit;
            addArguments(temp);
            cmd.Start();
            cmd.WaitForExit();
            //string result = cmd.StandardOutput.ReadToEnd();
            temp = "";
            int i = 1;
            while (i < times.Count)
            {
                temp = argument + " -ss " + times[i - 1] + " -to " + times[i] + end + i + fileFormat + exit;
                addArguments(temp);
                cmd.Start();
                temp = "";
                i++;
            }
            temp = argument + " -ss " + times[i-1] + " -to "+ length + end + i + fileFormat + exit;
            addArguments(temp);
            cmd.Start();
        }
        private void init()
        {
            cmd = new Process();
            startInfo = new ProcessStartInfo();
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.RedirectStandardInput = false;
            startInfo.RedirectStandardOutput = false;
            
        }
        private void addArguments(string argument)
        {
            startInfo.Arguments = argument;
            cmd.StartInfo = startInfo;
        }
    }
}
