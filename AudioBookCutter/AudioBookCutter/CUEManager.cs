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
            cue.AddIndex(0, 1, (int)ordered[0].TotalMinutes, ordered[0].Seconds, ordered[0].Milliseconds/75);

            for (int i = 1; i < ordered.Count; i++)
            {
                cue.AddTrack(i + 1 + ". fejezet", "na");
                cue.AddIndex(i, 1, (int)ordered[i].TotalMinutes, ordered[i].Seconds, ordered[i].Milliseconds / 75);
            }

            cue.SaveCue(save);
        }
    }
}
