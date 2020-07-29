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
        private enum PlaybackState
        {
            Playing, Stopped, Paused
        }

        private PlaybackState _playbackState;
        private AudioPlayer player;
        private Audio audio = null;
        private Command ffmpeg;
        private List<Marker> markers;
        private List<PictureBox> pmarkers;
        private CUEManager manager;

        public MainWindow()
        {
            InitializeComponent();
            audioWaveImage.Width = this.Width-16;
            markers = new List<Marker>();
            pmarkers = new List<PictureBox>();
            label1.SendToBack();
            label2.SendToBack();
            label3.SendToBack();
            lbScale.DataSource = new List<string>()
            {
                 "MS", "SS", "MM", "HH"
            };
            this.KeyPreview = true;

        }

        //HOTKEYS
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.O)
            {
                openAudio.PerformClick();
                e.Handled = true;
            }
            if (e.Control && e.KeyCode == Keys.M)
            {
                if (markerOther.Enabled)
                {
                    markerOther.PerformClick();
                }
                e.Handled = true;
            }
            if (e.Control && e.KeyCode == Keys.D)
            {
                if (btnDeleteMarker.Enabled)
                {
                    btnDeleteMarker.PerformClick();
                }
                e.Handled = true;
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (cut.Enabled)
                {
                    cut.PerformClick();
                }
                e.Handled = true;
            }
            if (e.Control && e.KeyCode == Keys.F)
            {
                if (lb_Markers.Enabled)
                {
                    lb_Markers.Focus();
                }
                e.Handled = true;
            }
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
                t.IsBackground = true;
                t.Start();
                timeLocation();
                updateMarkers();
            }
        }

        private void openAudio_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "mp3 fájlok|*.mp3";
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileNames.Length > 1)
                {
                    ffmpeg = new Command();
                    string result = ffmpeg.mergeFiles(openFileDialog1.FileNames);
                    audio = new Audio(result, openFileDialog1.FileNames[0]);
                }
                else
                {
                    audio = new Audio(openFileDialog1.FileNames[0], openFileDialog1.FileNames[0]);
                }
                Thread t = new Thread(() => audioWave());
                t.IsBackground = true;
                t.Start();
                buttonChange(false);
                enableOtherControls();
                player = new AudioPlayer(audio.aPath);
                trackLength.Text = FormatTimeSpan(player.GetLength());
                openMarker.Enabled = true;
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
            return string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D2}", (int)ts.TotalHours, (int)ts.TotalMinutes, ts.Seconds, ts.Milliseconds);
        }

        private void timeLocation()
        {
            if (timer1.Enabled == true)
            {
                seeker.Location = new Point((int)((player.GetPosition() / (player.GetLengthInMSeconds())) * audioWaveImage.Width), seeker.Location.Y);
            }
            else
            {
                seeker.Location = new Point(0, seeker.Location.Y);
            }
        }

        private void updateMarkers()
        {
            for (int i = 0; i < pmarkers.Count; i++)
            {
                pmarkers[i].Location = new Point(markers[i].calculateX(this.Width - 16, player.GetLength()), pmarkers[i].Location.Y);
            }
        }
        private double locationTime()
        {
            if (audio != null)
            {
                return player.GetLength().TotalMilliseconds * (seeker.Location.X / (double)audioWaveImage.Width);
            }
            return 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeLocation();
            now.Text = FormatTimeSpan(TimeSpan.FromMilliseconds(player.GetPosition()));
        }

        private void start_Click(object sender, EventArgs e)
        {
            if (audio != null)
            {
                if (_playbackState == PlaybackState.Stopped)
                {
                    player = new AudioPlayer(audio.aPath);
                    player.PlaybackStopType = AudioPlayer.PlaybackStopTypes.PlaybackStoppedReachingEndOfFile;
                    player.PlaybackPaused += _audioPlayer_PlaybackPaused;
                    player.PlaybackResumed += _audioPlayer_PlaybackResumed;
                    player.PlaybackStopped += _audioPlayer_PlaybackStopped;
                }
                timer1.Enabled = true;
                buttonChange(true);
                player.TogglePlayPause();

            }
        }
        private void _audioPlayer_PlaybackStopped()
        {
            _playbackState = PlaybackState.Stopped;
            timer1.Enabled = false;
            buttonChange(false);
            timeLocation();
            if (player.PlaybackStopType == AudioPlayer.PlaybackStopTypes.PlaybackStoppedReachingEndOfFile)
            {
                now.Text = "00:00.00";
            }
        }

        private void _audioPlayer_PlaybackResumed()
        {
            _playbackState = PlaybackState.Playing;
            buttonChange(true);
        }

        private void _audioPlayer_PlaybackPaused()
        {
            _playbackState = PlaybackState.Paused;
            buttonChange(false);
        }

        private void pause_Click(object sender, EventArgs e)
        {
            player.Pause();
            buttonChange(false);
        }

        private void stop_Click(object sender, EventArgs e)
        {
            player.PlaybackStopType = AudioPlayer.PlaybackStopTypes.PlaybackStoppedByUser;
            player.Stop();
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
        private IWavePlayer CreateWavePlayer()
        {
            return new WaveOutEvent();
        }

        private void cut_Click(object sender, EventArgs e)
        {
            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = audio.aPath;
            saveFileDialog1.Title = "Add meg a vágott fájloknak a helyét és nevét!";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ffmpeg = new Command();
                List<TimeSpan> times = new List<TimeSpan>();
                for (int i = 0; i < markers.Count; i++)
                {
                    times.Add(markers[i].Time);
                }
                ffmpeg.cutByTimeSpans(times, player.GetLength(), audio, saveFileDialog1.FileName);
            }
        }

        private void audioWaveImage_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            seeker.Location = new Point(coordinates.X, seeker.Location.Y);
            if (player != null)
            {
                player.SetPosition(locationTime());
            }
            timer1.Enabled = true;
        }

        /*private double seekerCalc()
        {
            return (audio.File.CurrentTime.TotalMilliseconds - locationTime());
        }*/

        private void resetDataSource()
        {
            lb_Markers.DataSource = null;
            lb_Markers.DataSource = markers.OrderBy(m => m.Time).ToList();
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

                Marker mmarker = new Marker(TimeSpan.FromMilliseconds(player.GetLength().TotalMilliseconds * (marker.Location.X / (double)audioWaveImage.Width)));

                pmarkers.Add(marker);
                markers.Add(mmarker);

                resetDataSource();
            }
        }

        private void markerOther_Click(object sender, EventArgs e)
        {
            if (audio != null)
            {
                TimeSpan ts = new TimeSpan(0, int.Parse(markerHour.Text), int.Parse(markerMinute.Text), int.Parse(markerSeconds.Text), int.Parse(markerMiliseconds.Text));
                if (ts <= player.GetLength())
                {
                    Marker mmarker = new Marker(ts);
                    PictureBox marker = new PictureBox();
                    marker.Size = new Size(2, seeker.Size.Height);
                    marker.Location = new Point(mmarker.calculateX(this.Width-16, player.GetLength()), seeker.Location.Y);
                    marker.BackColor = Color.Blue;
                    this.Controls.Add(marker);
                    marker.BringToFront();

                    pmarkers.Add(marker);
                    markers.Add(mmarker);

                    resetDataSource();
                }
                else
                {
                    MessageBox.Show("A marker ideje nem lehet nagyobb mint a megnyitott audiofájl ideje!");
                }
            }
        }

        private void opButtons(bool enable)
        {
            if (enable)
            {
                btnDeleteMarker.Enabled = true;
                lbScale.Enabled = true;
                numericUpDown1.Enabled = true;
                btnAdd.Enabled = true;
                btnSubtract.Enabled = true;
                cut.Enabled = true;
                saveMarker.Enabled = true;
            }
            else
            {
                btnDeleteMarker.Enabled = false;
                lbScale.Enabled = false;
                numericUpDown1.Enabled = false;
                btnAdd.Enabled = false;
                btnSubtract.Enabled = false;
                cut.Enabled = false;
                saveMarker.Enabled = false;
            }
        }
        private void lb_Markers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pmarkers.Count > 0)
            {
                opButtons(true);
            }
            else
            {
                opButtons(false);
            }


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

        private void btnDeleteMarker_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pmarkers.Count; i++)
            {
                if (lb_Markers.SelectedValue == markers[i])
                {
                    this.Controls.Remove(pmarkers[i]);
                    pmarkers.RemoveAt(i);
                    markers.RemoveAt(i);
                    resetDataSource();
                    return;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pmarkers.Count; i++)
            {
                if (lb_Markers.SelectedValue == markers[i])
                {
                    if (lbScale.SelectedValue.ToString() == "MS")
                    {
                        markers[i].Time = markers[i].Time.Add(new TimeSpan(0, 0, 0, 0, (int)numericUpDown1.Value));
                        pmarkers[i].Location = new Point(markers[i].calculateX(this.Width - 16, player.GetLength()), seeker.Location.Y);
                        resetDataSource();
                        return;
                    }
                    else if (lbScale.SelectedValue.ToString() == "SS")
                    {
                        markers[i].Time = markers[i].Time.Add(new TimeSpan(0,0,(int)numericUpDown1.Value));
                        pmarkers[i].Location = new Point(markers[i].calculateX(this.Width - 16, player.GetLength()), seeker.Location.Y);
                        resetDataSource();
                        return;
                    }
                    else if (lbScale.SelectedValue.ToString() == "MM")
                    {
                        markers[i].Time = markers[i].Time.Add(new TimeSpan(0, (int)numericUpDown1.Value, 0));
                        pmarkers[i].Location = new Point(markers[i].calculateX(this.Width - 16, player.GetLength()), seeker.Location.Y);
                        resetDataSource();
                        return;
                    }
                    else if (lbScale.SelectedValue.ToString() == "HH")
                    {
                        markers[i].Time = markers[i].Time.Add(new TimeSpan((int)numericUpDown1.Value, 0, 0));
                        pmarkers[i].Location = new Point(markers[i].calculateX(this.Width - 16, player.GetLength()), seeker.Location.Y);
                        resetDataSource();
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pmarkers.Count; i++)
            {
                if (lb_Markers.SelectedValue == markers[i])
                {
                    if (lbScale.SelectedValue.ToString() == "MS")
                    {
                        markers[i].Time = markers[i].Time.Subtract(new TimeSpan(0, 0, 0, 0, (int)numericUpDown1.Value));
                        pmarkers[i].Location = new Point(markers[i].calculateX(this.Width - 16, player.GetLength()), seeker.Location.Y);
                        resetDataSource();
                        return;
                    }
                    else if (lbScale.SelectedValue.ToString() == "SS")
                    {
                        markers[i].Time = markers[i].Time.Subtract(new TimeSpan(0, 0, (int)numericUpDown1.Value));
                        pmarkers[i].Location = new Point(markers[i].calculateX(this.Width - 16, player.GetLength()), seeker.Location.Y);
                        resetDataSource();
                        return;
                    }
                    else if (lbScale.SelectedValue.ToString() == "MM")
                    {
                        markers[i].Time = markers[i].Time.Subtract(new TimeSpan(0, (int)numericUpDown1.Value, 0));
                        pmarkers[i].Location = new Point(markers[i].calculateX(this.Width - 16, player.GetLength()), seeker.Location.Y);
                        resetDataSource();
                        return;
                    }
                    else if (lbScale.SelectedValue.ToString() == "HH")
                    {
                        markers[i].Time = markers[i].Time.Subtract(new TimeSpan((int)numericUpDown1.Value, 0, 0));
                        pmarkers[i].Location = new Point(markers[i].calculateX(this.Width - 16, player.GetLength()), seeker.Location.Y);
                        resetDataSource();
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (player != null)
            {
                player.Stop();
                player.Dispose();
            }
            Environment.Exit(Environment.ExitCode);
        }

        private void saveMarker_Click(object sender, EventArgs e)
        {
            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = audio.aPath;
            saveFileDialog1.Title = "Add meg a menteni kívánt markerek gyűjtőnevét!";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                manager = new CUEManager();
                List<TimeSpan> times = new List<TimeSpan>();
                for (int i = 0; i < markers.Count; i++)
                {
                    times.Add(markers[i].Time);
                }
                manager.saveMarkers(times, saveFileDialog1.FileName+".cue", audio);
            }
        }

        private void openMarker_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "cue fájlok|*.cue";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                manager = new CUEManager();
                if (markers == null ||  markers.Count == 0)
                {
                    List<Marker> omarkers = manager.openMarkers(openFileDialog1.FileName);
                    for (int i = 0; i < omarkers.Count; i++)
                    {

                        PictureBox marker = new PictureBox();
                        marker.Size = new Size(2, seeker.Size.Height);
                        marker.Location = new Point(omarkers[i].calculateX(this.Width - 16, player.GetLength()), seeker.Location.Y);
                        marker.BackColor = Color.Blue;
                        this.Controls.Add(marker);
                        marker.BringToFront();

                        markers.Add(omarkers[i]);
                        pmarkers.Add(marker);

                    }
                    resetDataSource();
                }
                else
                {
                    //notify user if they want to replace current markers
                }
            }
        }
    }
    
}
