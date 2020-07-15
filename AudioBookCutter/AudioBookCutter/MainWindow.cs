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
                buttonChange(false);
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
            now.Text = FormatTimeSpan(file.CurrentTime);
            timeLocation();
        }

        private void start_Click(object sender, EventArgs e)
        {
            if (audio != null && file != null)
            {
                BeginPlayback();
            }
        }

        private void pause_Click(object sender, EventArgs e)
        {
            wavePlayer.Pause();
            buttonChange(false);
        }

        private void stop_Click(object sender, EventArgs e)
        {
            wavePlayer.Stop();
            buttonChange(false);
        }

        private void buttonChange(bool playing)
        {
            if (playing && timer1.Enabled)
            {
                start.Enabled = false;
                pause.Enabled = true;
                stop.Enabled = true;
            }
            else if (!playing && timer1.Enabled)
            {
                start.Enabled = true;
                pause.Enabled = false;
                stop.Enabled = true;
            }
            else
            {
                start.Enabled = true;
                pause.Enabled = false;
                stop.Enabled = false;
            }
        }

        private void BeginPlayback()
        {
            wavePlayer = CreateWavePlayer();
            wavePlayer.Init(file);
            wavePlayer.PlaybackStopped += OnPlaybackStopped;
            wavePlayer.Play();
            timer1.Enabled = true; // timer for updating current time label
            buttonChange(true);
        }

        void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            CleanUp();
            timer1.Enabled = false;
            buttonChange(false);
            timeLocation();
            now.Text = "00:00.00";
        }

        private IWavePlayer CreateWavePlayer()
        {
            return new WaveOutEvent();
        }

        private void CleanUp()
        {
            if (file != null)
            {
                file.Dispose();
                //file = null;
            }
            if (wavePlayer != null)
            {
                wavePlayer.Dispose();
                //wavePlayer = null;
            }
        }
    }
}
