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
        //private string path;
        //private ProcessStartInfo startInfo;
        private string argumentStart = "/C ffmpeg -i ";
        private string exit = " & exit";
        private StringBuilder sb = new StringBuilder();
        private ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c systeminfo")
        {
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            WorkingDirectory = @"C:\Windows\System32\"
        };

        /*public Command(string pathToFile)
        {
            this.path = pathToFile;
        }*/
        public void cutByTimeSpans(List<TimeSpan> times, Audio audio)
        {
            //Thread ffmpeg = new Thread(() => cutByTimeSpansIn(times, length));
            cutByTimeSpansIn(times, audio.File.TotalTime, audio.aPath);

        }
        private void cutByTimeSpansIn(List<TimeSpan> times, TimeSpan length, string path)
        {
            //init();
            //StringBuilder sb = new StringBuilder();
            times.OrderBy(time => time.Milliseconds);
            string fileFormat = Path.GetExtension(path);
            string argument = argumentStart + Path.GetFullPath(path);
            string end = " -c copy " + Path.GetFullPath(@"G:/EKE/szakmai gyak/AudioBookCutter\") + Path.GetFileNameWithoutExtension(path);
            string temp;
            temp = argument + " -ss 00:00:00.0 -to " + times[0] + end + 0 + fileFormat + exit;
            //addArguments(temp);
            cmd = Process.Start(processInfo);
            cmd.OutputDataReceived += (sender, args) => sb.AppendLine(temp);
            cmd.BeginOutputReadLine();
            cmd.WaitForExit();
            //cmd.WaitForExit();
            temp = "";
            int i = 1;
            while (i < times.Count)
            {
                temp = argument + " -ss " + times[i - 1] + " -to " + times[i] + end + i + fileFormat + exit;
                cmd = Process.Start(processInfo);
                cmd.OutputDataReceived += (sender, args) => sb.AppendLine(temp);
                cmd.BeginOutputReadLine();
                cmd.WaitForExit();
                temp = "";
                i++;
            }
            temp = argument + " -ss " + times[i-1] + " -to "+ length + end + i + fileFormat + exit;
            cmd = Process.Start(processInfo);
            cmd.OutputDataReceived += (sender, args) => sb.AppendLine(temp);
            cmd.BeginOutputReadLine();
            cmd.WaitForExit();
        }
        private void init()
        {
            /*cmd = new Process();
            startInfo = new ProcessStartInfo();
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.RedirectStandardInput = false;
            startInfo.RedirectStandardOutput = false;*/
            

        }
        /*private void addArguments(string argument)
        {
            startInfo.Arguments = argument;
            cmd.StartInfo = startInfo;
        }*/

        public string mergeFiles(string[] files)
        {
            //init();
            string fileFormat = Path.GetExtension(files[0]);
            string argument = argumentStart + "\"concat:";
            string filename = "output";
            for (int i = 0; i < files.Length-1; i++)
            {
                argument += Path.GetFullPath(files[i]) + "|";
            }
            argument += Path.GetFullPath(files[files.Length-1]) + "\" -acodec copy "+ Path.GetFullPath(@"G:/EKE/szakmai gyak/AudioBookCutter\") + filename + fileFormat + exit;
            cmd = Process.Start(processInfo);
            cmd.OutputDataReceived += (sender, args) => sb.AppendLine(argument);
            cmd.BeginOutputReadLine();
            cmd.WaitForExit();
            return @"G:/EKE/szakmai gyak/AudioBookCutter\" + filename + fileFormat;
        }
    }
}
