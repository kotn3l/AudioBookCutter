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
        private IWavePlayer wavePlayer;
        private AudioFileReader file = null;
        private Audio audio = null;

        public MainWindow()
        {
            InitializeComponent();
            audioWaveImage.Width = this.Width;
        }
        private void audioWave()
        {
            audioWaveImage.Image = audio.audioWave(this.Width);
        }

        private void MainWindow_ResizeEnd(object sender, EventArgs e)
        {
            if (audio != null)
            {
                audioWaveImage.Width = this.Width;
                Thread t = new Thread(() => audioWave());
                t.Start();
                timeLocation();
            }

        }

        private void openAudio_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "mp3 fájlok|*.mp3|WAV fájlok|*.wav";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                audio = new Audio(openFileDialog1.FileName);
                file = new AudioFileReader(audio.Path);
                trackLength.Text = FormatTimeSpan(file.TotalTime);
                audioWave();
            }
        }

        private static string FormatTimeSpan(TimeSpan ts)
        {
            return string.Format("{0:D2}:{1:D2}.{2:D2}", (int)ts.TotalMinutes, ts.Seconds, ts.Milliseconds);
        }

        private void timeLocation()
        {
            if (timer1.Enabled == true)
            {
                pictureBox1.Location = new Point((int)((file.CurrentTime.TotalMilliseconds / (file.TotalTime.TotalMilliseconds)) * this.Width), pictureBox1.Location.Y);
            }
            else
            {
                pictureBox1.Location = new Point(0, pictureBox1.Location.Y);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (audio != null && file != null)
            //{
                now.Text = FormatTimeSpan(file.CurrentTime);
                //pictureBox1.Location = new Point((int)((file.CurrentTime.TotalMilliseconds  / (file.TotalTime.TotalMilliseconds)) * this.Width), pictureBox1.Location.Y);
                timeLocation();
            //}
        }

        private void start_Click(object sender, EventArgs e)
        {
            BeginPlayback();
        }

        private void BeginPlayback()
        {
            wavePlayer = CreateWavePlayer();
            wavePlayer.Init(file);
            wavePlayer.PlaybackStopped += OnPlaybackStopped;
            wavePlayer.Play();
            //EnableButtons(true);
            timer1.Enabled = true; // timer for updating current time label
        }

        void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            //CleanUp();
            //EnableButtons(false);
            timer1.Enabled = false;
            timeLocation();
            now.Text = "00:00.00";
        }


        private IWavePlayer CreateWavePlayer()
        {
            //switch (comboBoxOutputDriver.SelectedIndex)
            //{
                //case 2:
                    return new WaveOutEvent();
                //case 1:
                    //return new WaveOut(WaveCallbackInfo.FunctionCallback());
                //default:
                    //return new WaveOut();
            //}
        }

        private void CleanUp()
        {
            if (file != null)
            {
                file.Dispose();
                file = null;
            }
            if (wavePlayer != null)
            {
                wavePlayer.Dispose();
                wavePlayer = null;
            }
        }
    }
}
