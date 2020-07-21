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
        private Audio audio = null;
        private Command ffmpeg;
        private List<Marker> markers;
        private List<PictureBox> pmarkers;

        public MainWindow()
        {
            InitializeComponent();
            audioWaveImage.Width = this.Width-16;
            markers = new List<Marker>();
            pmarkers = new List<PictureBox>();
            label1.SendToBack();
            label2.SendToBack();
            label3.SendToBack();
        }
        private void audioWave()
        {
            audioWaveImage.Image = audio.audioWave(this.Width-16);
        }

        private void MainWindow_ResizeEnd(object sender, EventArgs e)
        {
            if (audio != null)
            {
                audioWaveImage.Width = this.Width-16;
                Thread t = new Thread(() => audioWave());
                t.Start();
                timeLocation();
            }
        }

        private void openAudio_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "mp3 fájlok|*.mp3|WAV fájlok|*.wav";
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileNames.Length > 1)
                {
                    ffmpeg = new Command();
                    string result = ffmpeg.mergeFiles(openFileDialog1.FileNames);
                    MessageBox.Show(result);
                }
                audio = new Audio(openFileDialog1.FileNames[0]);
                trackLength.Text = FormatTimeSpan(audio.File.TotalTime);
                audioWave();
                buttonChange(false);
                enableOtherControls();
            }
        }

        private void enableOtherControls()
        {
            markerCurrent.Enabled = true;
            markerOther.Enabled = true;
            markerHour.Enabled = true;
            markerMinute.Enabled = true;
            markerSeconds.Enabled = true;
            markerMiliseconds.Enabled = true;
        }

        private static string FormatTimeSpan(TimeSpan ts)
        {
            return string.Format("{0:D2}:{1:D2}.{2:D2}", (int)ts.TotalMinutes, ts.Seconds, ts.Milliseconds);
        }

        private void timeLocation()
        {
            if (timer1.Enabled == true)
            {
                seeker.Location = new Point((int)((audio.File.CurrentTime.TotalMilliseconds / (audio.File.TotalTime.TotalMilliseconds)) * this.Width-16), seeker.Location.Y);
            }
            else
            {
                seeker.Location = new Point(0, seeker.Location.Y);
            }
            for (int i = 0; i < pmarkers.Count; i++)
            {
                pmarkers[i].Location = new Point(markers[i].calculateX(this.Width - 16, audio.File.TotalTime), pmarkers[i].Location.Y);
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
            ffmpeg = new Command();
            List<TimeSpan> times = new List<TimeSpan>();
            for (int i = 0; i < markers.Count; i++)
            {
                times.Add(markers[i].Time);
            }
            ffmpeg.cutByTimeSpans(times, audio);
        }

        private void audioWaveImage_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            seeker.Location = new Point(coordinates.X, seeker.Location.Y);
            if (wavePlayer != null && wavePlayer.PlaybackState == PlaybackState.Playing)
            {
                audio.File.CurrentTime.Add(new TimeSpan (0,0,0,0, (int)seekerCalc()));
            }
        }

        private double seekerCalc()
        {
            return (audio.File.CurrentTime.TotalMilliseconds - locationTime());
        }

        private void markerCurrent_Click(object sender, EventArgs e)
        {
            if (audio != null)
            {
                PictureBox marker = new PictureBox();
                marker.Size = new Size(2, seeker.Size.Height);
                marker.Location = seeker.Location;
                marker.BackColor = Color.Blue;
                this.Controls.Add(marker);
                marker.BringToFront();

                Marker mmarker = new Marker(TimeSpan.FromMilliseconds(audio.File.TotalTime.TotalMilliseconds * (marker.Location.X / (double)audioWaveImage.Width)));

                pmarkers.Add(marker);
                markers.Add(mmarker);

                lb_Markers.DataSource = null;
                lb_Markers.DataSource = markers;
            }
        }

        private void markerOther_Click(object sender, EventArgs e)
        {
            if (audio != null)
            {
                TimeSpan ts = new TimeSpan(0, int.Parse(markerHour.Text), int.Parse(markerMinute.Text), int.Parse(markerSeconds.Text), int.Parse(markerMiliseconds.Text));
                if (ts <= audio.File.TotalTime)
                {
                    Marker mmarker = new Marker(ts);
                    PictureBox marker = new PictureBox();
                    marker.Size = new Size(2, seeker.Size.Height);
                    marker.Location = new Point(mmarker.calculateX(this.Width-16, audio.File.TotalTime), seeker.Location.Y);
                    marker.BackColor = Color.Blue;
                    this.Controls.Add(marker);
                    marker.BringToFront();

                    pmarkers.Add(marker);
                    markers.Add(mmarker);

                    lb_Markers.DataSource = null;
                    lb_Markers.DataSource = markers;
                }
                else
                {
                    MessageBox.Show("A marker ideje nem lehet nagyobb mint a megnyitott audiofájl ideje!");
                }
            }
        }

        private void lb_Markers_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < pmarkers.Count; i++)
            {
                if (lb_Markers.SelectedValue == markers[i])
                {
                    pmarkers[i].BackColor = Color.Green;
                    pmarkers[i].Width = 3;
                }
                else
                {
                    pmarkers[i].BackColor = Color.Blue;
                    pmarkers[i].Width = 2;
                }
            }
        }
    }
}
