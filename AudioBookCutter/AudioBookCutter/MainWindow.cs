using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using WaveFormRendererLib;
using System.IO;
using System.Drawing.Imaging;
using NAudio.Wave.SampleProviders;
using System.Threading;

namespace AudioBookCutter
{
    public partial class MainWindow : Form
    {
        private WaveOutEvent output;
        private AudioFileReader file;
        private string pathAudio = null;
        private Image wave;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void audioWave()
        {
            MaxPeakProvider maxPeakProvider = new MaxPeakProvider();
            RmsPeakProvider rmsPeakProvider = new RmsPeakProvider(200); // e.g. 200
            SamplingPeakProvider samplingPeakProvider = new SamplingPeakProvider(200); // e.g. 200
            AveragePeakProvider averagePeakProvider = new AveragePeakProvider(3); // e.g. 4

            StandardWaveFormRendererSettings myRendererSettings = new StandardWaveFormRendererSettings();
            myRendererSettings.Width = this.Width > 50 ? this.Width - 50 : this.Width;
            myRendererSettings.TopHeight = 64;
            myRendererSettings.BottomHeight = 64;

            WaveFormRenderer renderer = new WaveFormRenderer();
            String audioFilePath = pathAudio;
            wave = renderer.Render(audioFilePath, averagePeakProvider, myRendererSettings);
            audioWaveImage.Image = wave;
        }

        private void MainWindow_ResizeEnd(object sender, EventArgs e)
        {
            if (pathAudio != null)
            {
                audioWaveImage.Width = this.Width;
                Thread t = new Thread(() => audioWave());
                t.Start();
            }

        }

        private void openAudio_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "mp3 fájlok|*.mp3|WAV fájlok|*.wav";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pathAudio = openFileDialog1.FileName;
                string[] text = new AudioFileReader(pathAudio).TotalTime.ToString().Split('.');
                trackLength.Text = text[0] + "." + text[1].Substring(0,3);
                audioWave();
            }
        }
    }
}
