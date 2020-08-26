using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using Serilog;

namespace AudioBookCutter
{
    class Command
    {
        ProcessStartInfo startInfo;
        private string argumentStart = " -i ";
        private string workingDir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        private string temp = @"\temp\";
        private string command = "[COMMAND] ";

        public Command(ILogger log)
        {
            Log.Logger = log;
        }
        public void cutByTimeSpans(List<TimeSpan> times, TimeSpan totalTime, Audio audio, string save)
        {
            cutByTimeSpansIn(times, totalTime, audio.aPath, save);
        }
        private void cutByTimeSpansIn(List<TimeSpan> times, TimeSpan length, string path, string save)
        {
            init();
            List<TimeSpan> ordered = new List<TimeSpan>(times.OrderBy(time => time.TotalMilliseconds));
            Log.Information(command + "{0} will be cut in these timestamps: {1}", Path.GetFileName(path), ordered);
            string fileFormat = Path.GetExtension(path);
            string argument = argumentStart + "\"" + Path.GetFullPath(path) + "\"";
            string end = " -c copy -copyts -avoid_negative_ts 1 " + "\"" + save;
            string temp;
            temp = "-ss 00:00:00.0" + argument + " -to " + ordered[0] + end + 1.ToString().PadLeft(3, '0') + fileFormat + "\"";
            Execute(temp);
            Log.Information(command + "Cut from 00:00:00.0 to {0} done", ordered[0]);
            temp = "";
            int i = 1;
            while (i < ordered.Count)
            {
                temp = "-ss " + ordered[i - 1] + argument + " -to " + ordered[i] + end + (i+1).ToString().PadLeft(3, '0') + fileFormat + "\"";
                Execute(temp);
                Log.Information(command + "Cut from {0} to {1} done", ordered[i - 1], ordered[i]);
                temp = "";
                i++;
            }
            temp = "-ss " + ordered[i-1] + argument + " -to "+ length + end + (i+1).ToString().PadLeft(3, '0') + fileFormat + "\"";
            Execute(temp);
            Log.Information(command + "Cut from {0} to {1} done", ordered[i - 1], length);
            Log.Information(command + "{0} files were saved", i+1);
        }
        private void init()
        {
            startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            try
            {
                startInfo.FileName = workingDir + "\\ffmpeg.exe";
                Log.Information(command + "FFmpeg.exe found");
            }
            catch (Exception e)
            {
                Log.Error(e, "Command init failed");
            }
            Directory.CreateDirectory(workingDir + temp);
        }
        public string mergeFiles(string[] files)
        {
            emptyTemp();
            init();
            string fileFormat = Path.GetExtension(files[0]);
            string filename = Path.GetFileName(Path.GetDirectoryName(files[0]));
            string output = workingDir + temp + filename + "_merged" + fileFormat;
            string text = "";
            for (int i = 0; i < files.Length; i++)
            {
                text += "file \'" +Path.GetFullPath(files[i]) + "\'\n";
            }
            string textOut = workingDir + temp + filename + ".txt";
            File.WriteAllText(textOut, text);
            string argument = " -f concat -safe 0" + argumentStart + "\"" + textOut + "\"" + " -acodec copy " + "\"" + output + "\"";
            Execute(argument);
            Log.Information(command + "Multiple files merged, result: {0}", Path.GetFileName(output));
            return output;
        }
        private string Execute(string argument)
        {
            startInfo.Arguments = argument;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            string output;
            using (Process exeProcess = Process.Start(startInfo))
            {
                output = exeProcess.StandardError.ReadToEnd();
                exeProcess.WaitForExit();
            }
            Log.Warning(command + "\n" + output);
            return output;
        }

        public void emptyTemp()
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(workingDir + temp);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
                Log.Information(command + "Emptied temp folder");
            }
            catch (IOException)
            {
                return;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
