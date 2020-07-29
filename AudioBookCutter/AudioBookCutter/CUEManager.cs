using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CueSharp;

namespace AudioBookCutter
{
    class CUEManager
    {
        public void saveMarkers(List<TimeSpan> markers, string save, Audio audio)
        {
            CueSheet cue = new CueSheet();
            List<TimeSpan> ordered = new List<TimeSpan>(markers.OrderBy(time => time.TotalMilliseconds));
            cue.Title = Path.GetFileNameWithoutExtension(audio.aPath);
            cue.AddTrack("1. fejezet", "na", DataType.AUDIO);
            cue.AddIndex(0, 1, 0, 0, 0);
            int j = 1;
            for (int i = 0; i < ordered.Count; i++)
            {
                j++;
                cue.AddTrack(j + ". fejezet", "na");
                cue.AddIndex(i + 1, 1, (int)ordered[i].TotalMinutes, ordered[i].Seconds, (int)(ordered[i].Milliseconds / (999 / 75d)));
            }

            cue.SaveCue(save);
        }

        public List<Marker> openMarkers(string path)
        {
            CueSheet cue = new CueSheet(path);
            List<Marker> markers = new List<Marker>();
            for (int i = 1; i < cue.Tracks.Length; i++)
            {
                TimeSpan fromMins = TimeSpan.FromMinutes(cue[i][0].Minutes);
                markers.Add(new Marker(new TimeSpan( 
                    0, (int)fromMins.TotalHours,
                    fromMins.Minutes,
                    cue[i][0].Seconds,
                    (int)(cue[i][0].Frames * (1000/75d)))));
            }
            return markers;
        }
    }
}
