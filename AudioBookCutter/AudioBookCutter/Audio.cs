using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveFormRendererLib;

namespace AudioBookCutter
{
    class Audio
    {
        private string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public Image audioWave(int Width)
        {
            MaxPeakProvider maxPeakProvider = new MaxPeakProvider();
            RmsPeakProvider rmsPeakProvider = new RmsPeakProvider(200); // e.g. 200
            SamplingPeakProvider samplingPeakProvider = new SamplingPeakProvider(200); // e.g. 200
            AveragePeakProvider averagePeakProvider = new AveragePeakProvider(3); // e.g. 4

            StandardWaveFormRendererSettings myRendererSettings = new StandardWaveFormRendererSettings();
            myRendererSettings.Width = Width > 50 ? Width - 50 : Width;
            myRendererSettings.TopHeight = 64;
            myRendererSettings.BottomHeight = 64;

            WaveFormRenderer renderer = new WaveFormRenderer();
            return renderer.Render(this.path, averagePeakProvider, myRendererSettings);
        }

        public Audio(string path)
        {
            this.path = path;
        }

    }
}
