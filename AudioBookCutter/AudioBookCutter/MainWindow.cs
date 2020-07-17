﻿using System;
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
        private Audio audio = null;
        private Command ffmpeg;

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
                trackLength.Text = FormatTimeSpan(audio.File.TotalTime);
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
                seeker.Location = new Point((int)((audio.File.CurrentTime.TotalMilliseconds / (audio.File.TotalTime.TotalMilliseconds)) * this.Width), seeker.Location.Y);
            }
            else
            {
                seeker.Location = new Point(0, seeker.Location.Y);
            }
        }

        private double locationTime()
        {
            if (audio != null)
            {
                return audio.File.TotalTime.TotalMilliseconds * (seeker.Location.X / (double)audioWaveImage.Width);
            }
            else return 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            now.Text = FormatTimeSpan(audio.File.CurrentTime);
            timeLocation();
        }

        private void start_Click(object sender, EventArgs e)
        {
            if (audio != null)
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
            if (seeker.Location.X == 0)
            {
                wavePlayer.Init(audio.File);
            }
            else
            {
                var trimmed = new OffsetSampleProvider(audio.File);
                trimmed.SkipOver = TimeSpan.FromMilliseconds(locationTime());
                wavePlayer.Init(trimmed);
            }
            wavePlayer.PlaybackStopped += OnPlaybackStopped;
            wavePlayer.Play();
            timer1.Enabled = true;
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
            if (audio.File != null)
            {
                audio.File.Dispose();
                //file = null;
            }
            if (wavePlayer != null)
            {
                wavePlayer.Dispose();
                //wavePlayer = null;
            }
        }

        private void cut_Click(object sender, EventArgs e)
        {
            ffmpeg = new Command(audio.aPath);
            List<TimeSpan> times = new List<TimeSpan>();
            times.Add(new TimeSpan(0, 1, 31));
            times.Add(new TimeSpan(0, 3, 0));
            ffmpeg.cutByTimeSpans(times, audio.File.TotalTime);
        }

        private void audioWaveImage_Click(object sender, EventArgs e)
        {
            if (wavePlayer.PlaybackState == PlaybackState.Playing)
            {
                //file.WaveFormat.
            }
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            seeker.Location = new Point(coordinates.X, seeker.Location.Y);
        }
    }
}
