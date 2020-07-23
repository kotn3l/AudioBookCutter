﻿using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveFormRendererLib;
using System.IO;

namespace AudioBookCutter
{
    class Audio
    {
        private string originalname;
        public string OriginalName
        {
            get { return originalname; }
            set { originalname = value; }
        }


        private AudioFileReader file;
        public AudioFileReader File
        {
            get
            {
                return file;
            }
            private set
            {
                file = value;
            }
        }
        //private IWavePlayer wavePlayer;

        private string apath;
        public string aPath
        {
            get { return apath; }
            set
            {
                if (System.IO.File.Exists(aPath))
                {
                    apath = value;
                }
                else throw new Exception();
            }
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
            return renderer.Render(this.apath, averagePeakProvider, myRendererSettings);
        }

        public Audio(string path, string originalname)
        {
            this.apath = path;
            this.originalname = Path.GetFileName(Path.GetDirectoryName(originalname));
            this.file = new AudioFileReader(aPath);
        }
    }
}
