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
        ProcessStartInfo startInfo;
        private string argumentStart = "-i ";
        private string workingDir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        private string temp = @"\temp\";
        private string cut = @"\cut\";

        public void cutByTimeSpans(List<TimeSpan> times, Audio audio, string save)
        {
            cutByTimeSpansIn(times, audio.File.TotalTime, audio.aPath, save);
        }
        private void cutByTimeSpansIn(List<TimeSpan> times, TimeSpan length, string path, string save)
        {
            init();
            List<TimeSpan> ordered = new List<TimeSpan>(times.OrderBy(time => time.TotalMilliseconds));
            string fileFormat = Path.GetExtension(path);
            string argument = argumentStart + "\"" + Path.GetFullPath(path) + "\"";
            string end = " -c copy " + "\"" + workingDir + cut + save;
            string temp;
            temp = argument + " -ss 00:00:00.0 -to " + ordered[0] + end + 0 + fileFormat + "\"";
            Execute(temp);
            temp = "";
            int i = 1;
            while (i < ordered.Count)
            {
                temp = argument + " -ss " + ordered[i - 1] + " -to " + ordered[i] + end + i + fileFormat + "\"";
                Execute(temp);
                temp = "";
                i++;
            }
            temp = argument + " -ss " + ordered[i-1] + " -to "+ length + end + i + fileFormat + "\"";
            Execute(temp);
        }
        private void init()
        {
            startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\ffmpeg.exe";

            Directory.CreateDirectory(workingDir + temp);
            Directory.CreateDirectory(workingDir + cut);
        }
        public string mergeFiles(string[] files)
        {
            init();
            string fileFormat = Path.GetExtension(files[0]);
            string argument = argumentStart + "\"concat:";
            string filename = Path.GetDirectoryName(files[0]);
            for (int i = 0; i < files.Length - 1; i++)
            {
                argument += Path.GetFullPath(files[i]) + "|";
            }
            argument += Path.GetFullPath(files[files.Length - 1]) + "\" -acodec copy " + "\"" + workingDir + temp + filename + "_merged" + fileFormat + "\"";
            

            /*string output;
            using (Process exeProcess = Process.Start(startInfo))
            {
                string error = exeProcess.StandardError.ReadToEnd();
                output = exeProcess.StandardError.ReadToEnd();
                exeProcess.WaitForExit();
            }
            return output;*/
            return Execute(argument);
        }
        private string Execute(string argument)
        {
            startInfo.Arguments = argument;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            string output;
            using (Process exeProcess = Process.Start(startInfo))
            {
                string error = exeProcess.StandardError.ReadToEnd();
                output = exeProcess.StandardError.ReadToEnd();
                exeProcess.WaitForExit();
            }
            return output;
        }
    }
}
