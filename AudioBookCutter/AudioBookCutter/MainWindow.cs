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
using System.Diagnostics;
using Serilog;
using Serilog.Core;

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
        private bool resized;
        private string workingDir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        private string errorMsg = "Egy hiba lépett fel az alkalmazásban. Kérlek zárd be a programot, és csatold az exe mellett lévő log fájlokat GitHubon egy issue létrehozásával, vagy küldd erre az e-mail címre: kotn3l@gmail.com";

        public MainWindow()
        {
            InitializeComponent();
            audioWaveImage.Width = this.Width - 16;
            markers = new List<Marker>();
            pmarkers = new List<PictureBox>();
            label1.SendToBack();
            label2.SendToBack();
            label3.SendToBack();
            lbScale.DataSource = new List<string>()
            {
                 "MS", "SS", "MM", "HH"
            };
            resized = false;
            lb_rendering.BringToFront();
            emptyLogs();
            var log =
            new LoggerConfiguration()
            .WriteTo.File(workingDir+@"/main.log")
            .CreateLogger();
            Log.Logger = log;
        }

        //HOTKEYS
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                if (_playbackState == PlaybackState.Playing || _playbackState == PlaybackState.Paused)
                {
                    stop.PerformClick();
                }
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.Space)
            {
                if (player != null)
                {
                    if (_playbackState == PlaybackState.Playing)
                    {
                        pause.PerformClick();
                    }
                    else if (_playbackState == PlaybackState.Paused || _playbackState == PlaybackState.Stopped)
                    {
                        start.PerformClick();
                    }
                }
                e.Handled = true;
                return;
            }
            if (e.Control && e.KeyCode == Keys.O)
            {
                openAudio.PerformClick();
                e.Handled = true;
                return;
            }
            if (e.Control && e.KeyCode == Keys.M)
            {
                if (markerCurrent.Enabled)
                {
                    markerCurrent.PerformClick();
                }
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.M)
            {
                if (markerOther.Enabled)
                {
                    markerOther.PerformClick();
                }
                e.Handled = true;
                return;
            }
            if (e.Control && e.KeyCode == Keys.D)
            {
                if (btnDeleteMarker.Enabled)
                {
                    btnDeleteMarker.PerformClick();
                }
                e.Handled = true;
                return;
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (cut.Enabled)
                {
                    cut.PerformClick();
                }
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.L)
            {
                if (lb_Markers.Enabled)
                {
                    lb_Markers.Focus();
                }
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.H)
            {
                if (markerHour.Enabled)
                {
                    markerHour.Focus();
                }
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.K)
            {
                if (btnSkip.Enabled)
                {
                    btnSkip.PerformClick();
                }
                e.Handled = true;
                return;
            }
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (saveMarker.Enabled)
                {
                    saveMarker.PerformClick();
                }
                e.Handled = true;
                return;
            }
        }
        private void MainWindow_ResizeEnd(object sender, EventArgs e)
        {
            if (audio != null && resized)
            {
                resized = false;
                audioWaveImage.Width = this.Width - 16;
                Thread t = new Thread(() => audioWave());
                t.IsBackground = true;
                t.Start();
                timeLocation();
                updateMarkers();
            }
        }
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (player != null)
            {
                player.Stop();
                abcDispose();
            }
            Log.CloseAndFlush();
            base.OnClosing(e);
            Environment.Exit(Environment.ExitCode);
        }
        private void MainWindow_Resize(object sender, EventArgs e)
        {
            resized = true;
        }
        private void emptyLogs()
        {
            if (File.Exists(workingDir + @"/main.log"))
            {
                File.Delete(workingDir + @"/main.log");
            }
            if (File.Exists(workingDir + @"/command.log"))
            {
                File.Delete(workingDir + @"/command.log");
            }
        }

        private void audioWave()
        {
            try
            {
                renderText(true);
                audioWaveImage.Image = audio.audioWave(this.Width - 16);
                renderText(false);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error occured in audio wave rendering");
                MessageBox.Show(errorMsg);
            }
            
        }
        public void renderText(bool render)
        {
            if (!InvokeRequired)
            {
                lb_rendering.Visible = render;
            }
            else
            {
                Invoke(new Action<bool>(renderText), render);
            }
        }
        private void openAudio_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "mp3 fájlok|*.mp3";
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (audio != null)
                    {
                        abcDispose();
                        removeAllMarkers();
                        resetDataSource();
                        Log.Information("Reopening");
                    }
                    if (openFileDialog1.FileNames.Length > 1)
                    {
                        Log.Information("Multiple files opened: {0}", openFileDialog1.FileNames);
                        ffmpeg = new Command();
                        string result = ffmpeg.mergeFiles(openFileDialog1.FileNames);
                        audio = new Audio(result, openFileDialog1.FileNames[0]);
                        Log.Information("Multiple files merged, result: {0}", Path.GetFileName(audio.aPath));
                    }
                    else
                    {
                        audio = new Audio(openFileDialog1.FileNames[0], openFileDialog1.FileNames[0]);
                        Log.Information("One file opened: {0}", Path.GetFileName(audio.aPath));
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error occured while opening files");
                    MessageBox.Show(errorMsg);
                }
                buttonChange(false);
                enableOtherControls();
                try
                {
                    player = new AudioPlayer(audio.aPath);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error occured while creating player");
                    MessageBox.Show(errorMsg);
                }
                trackLength.Text = FormatTimeSpan(player.GetLength());
                Log.Information("Track length: {0}", trackLength.Text);
                _playbackState = PlaybackState.Stopped;
                timeLocation();
                updateMarkers();
                Thread t = new Thread(() => audioWave());
                t.IsBackground = true;
                t.Start();
                resized = false;
                this.Text = "Audio Book Cutter - "+ audio.OriginalName + @"/" + Path.GetFileName(audio.aPath);
            }
        }

        private static string FormatTimeSpan(TimeSpan ts)
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D2}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
        }
        private void timeLocation()
        {
            seeker.Location = new Point((int)((player.GetPosition() / (player.GetLengthInMSeconds())) * audioWaveImage.Width), seeker.Location.Y);
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
            try
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
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while trying to play audio");
                MessageBox.Show(errorMsg);
            }
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
        private void audioWaveImage_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            seeker.Location = new Point(coordinates.X, seeker.Location.Y);
            try
            {
                if (player != null)
                {
                    player.SetPosition(locationTime());
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while trying to seek into track");
                MessageBox.Show(errorMsg);
            }
            
        }
        private void btnSkip_Click(object sender, EventArgs e)
        {
            if (player != null && (markers.Count > 0 && markers != null))
            {
                List<Marker> omarkers = new List<Marker>(markers.OrderBy(marker => marker.Time.TotalMilliseconds));
                if (omarkers[0].Time.TotalMilliseconds >= player.GetPosition())
                {
                    player.SetPosition(omarkers[0].Time.TotalMilliseconds);
                    timeLocation();
                    return;
                }
                for (int i = 0; i < omarkers.Count - 1; i++)
                {
                    if (player.GetPosition() >= omarkers[i].Time.TotalMilliseconds && player.GetPosition() <= omarkers[i + 1].Time.TotalMilliseconds)
                    {
                        player.SetPosition(omarkers[i + 1].Time.TotalMilliseconds);
                        timeLocation();
                        return;
                    }
                }
            }
        }

        private void _audioPlayer_PlaybackStopped()
        {
            _playbackState = PlaybackState.Stopped;
            timer1.Enabled = false;
            buttonChange(false);
            timeLocation();
            now.Text = "00:00:00.00";
            Log.Information("Playback stopped");
        }
        private void _audioPlayer_PlaybackResumed()
        {
            _playbackState = PlaybackState.Playing;
            buttonChange(true);
            timer1.Enabled = true;
            Log.Information("Playback started");
        }
        private void _audioPlayer_PlaybackPaused()
        {
            _playbackState = PlaybackState.Paused;
            buttonChange(false);
            timer1.Enabled = false;
            Log.Information("Playback paused");
        }

        private void enableOtherControls()
        {
            markerCurrent.Enabled = true;
            markerOther.Enabled = true;
            markerHour.Enabled = true;
            markerMinute.Enabled = true;
            markerSeconds.Enabled = true;
            markerMiliseconds.Enabled = true;
            openMarker.Enabled = true;
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
                saveMarkerFrames.Enabled = true;
                btnSkip.Enabled = true;
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
                saveMarkerFrames.Enabled = false;
                btnSkip.Enabled = false;
            }
        }

        private void cut_Click(object sender, EventArgs e)
        {
            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = audio.aPath;
            saveFileDialog1.Title = "Add meg a vágott fájloknak a helyét és nevét!";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ffmpeg = new Command();
                    List<TimeSpan> times = new List<TimeSpan>();
                    for (int i = 0; i < markers.Count; i++)
                    {
                        times.Add(markers[i].Time);
                    }
                    ffmpeg.cutByTimeSpans(times, player.GetLength(), audio, saveFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error occured while trying to cut audio");
                    MessageBox.Show(errorMsg);
                }
                
            }
        }
        private void markerCurrent_Click(object sender, EventArgs e)
        {
            if (audio != null)
            {
                Marker mmarker = new Marker(TimeSpan.FromMilliseconds(player.GetLength().TotalMilliseconds * (seeker.Location.X / (double)audioWaveImage.Width)));
                addMarker(mmarker);
                Log.Information("Marker added at {0}", FormatTimeSpan(mmarker.Time));
                resetDataSource();
            }
        }
        private void markerOther_Click(object sender, EventArgs e)
        {
            if (audio != null)
            {
                try
                {
                    Log.Information("User entered manual timespan values: {0}:{1}:{2}.{3}", markerHour.Text, markerMinute.Text, markerSeconds.Text, markerMiliseconds.Text);
                    TimeSpan ts = new TimeSpan(0, int.Parse(markerHour.Text), int.Parse(markerMinute.Text), int.Parse(markerSeconds.Text), int.Parse(markerMiliseconds.Text));
                    if (ts <= player.GetLength())
                    {
                        Marker mmarker = new Marker(ts);
                        addMarker(mmarker);
                        Log.Information("Marker added at {0}", FormatTimeSpan(mmarker.Time));
                        resetDataSource();
                    }
                    else
                    {
                        MessageBox.Show("A marker ideje nem lehet nagyobb mint a megnyitott audiofájl ideje!");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error occured while trying to add manual markers");
                    MessageBox.Show(errorMsg);
                }
            }
        }
        private void btnDeleteMarker_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pmarkers.Count; i++)
            {
                if (lb_Markers.SelectedValue == markers[i])
                {
                    Log.Information("Marker deleted at {0}", FormatTimeSpan(markers[i].Time));
                    removeMarker(i);
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
                        markers[i].Time = markers[i].Time.Add(new TimeSpan(0, 0, (int)numericUpDown1.Value));
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
        private void saveMarker_Click(object sender, EventArgs e)
        {
            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = audio.aPath;
            saveFileDialog1.Title = "Add meg a menteni kívánt markerek gyűjtőnevét!";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    manager = new CUEManager();
                    List<TimeSpan> times = new List<TimeSpan>();
                    for (int i = 0; i < markers.Count; i++)
                    {
                        times.Add(markers[i].Time);
                    }
                    manager.saveMarkersMS(times, saveFileDialog1.FileName + ".cue", audio);
                    Log.Information("Marker (ms) saved at: {0}", saveFileDialog1.FileName + ".cue");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error occured while trying to save markers (ms)");
                    MessageBox.Show(errorMsg);
                }
            }
        }
        private void saveMarkerFrames_Click(object sender, EventArgs e)
        {
            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = audio.aPath;
            saveFileDialog1.Title = "Add meg a menteni kívánt markerek gyűjtőnevét!";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    manager = new CUEManager();
                    List<TimeSpan> times = new List<TimeSpan>();
                    for (int i = 0; i < markers.Count; i++)
                    {
                        times.Add(markers[i].Time);
                    }
                    manager.saveMarkersOG(times, saveFileDialog1.FileName + ".cue", audio);
                    Log.Information("Marker (frames) saved at: {0}", saveFileDialog1.FileName + ".cue");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error occured while trying to save markers (frames)");
                    MessageBox.Show(errorMsg);
                }
                
            }
        }
        private void openMarker_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "cue fájlok|*.cue";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    manager = new CUEManager();
                    if (manager.max(openFileDialog1.FileName) <= player.GetLength())
                    {
                        List<Marker> omarkers = manager.openMarkers(openFileDialog1.FileName);
                        if (markers == null || markers.Count == 0)
                        {
                            for (int i = 0; i < omarkers.Count; i++)
                            {
                                addMarker(omarkers[i]);
                            }
                            resetDataSource();
                            Log.Information("Markers loaded");
                        }
                        else
                        {
                            if (MessageBox.Show("Ki akarod cserélni az eddigi markereket a megnyitottakra?", "Marker csere?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                removeAllMarkers();
                                for (int i = 0; i < omarkers.Count; i++)
                                {
                                    addMarker(omarkers[i]);
                                }
                                resetDataSource();
                                Log.Information("Markers loaded, deleted old markers");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("A megnyitott marker az audio fájlon kívűlre mutat!");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error occured while trying to open markers");
                    MessageBox.Show(errorMsg);
                }
                
            }
        }
        private void addMarker(Marker marker)
        {
            PictureBox pmarker = new PictureBox();
            pmarker.Size = new Size(2, seeker.Size.Height);
            pmarker.Location = new Point(marker.calculateX(this.Width - 16, player.GetLength()), seeker.Location.Y);
            pmarker.BackColor = Color.Blue;
            this.Controls.Add(pmarker);
            pmarker.BringToFront();
            pmarkers.Add(pmarker);
            markers.Add(marker);
        }
        private void removeMarker(int i)
        {
            this.Controls.Remove(pmarkers[i]);
            pmarkers.RemoveAt(i);
            markers.RemoveAt(i);
        }
        private void removeAllMarkers()
        {
            for (int i = 0; i < pmarkers.Count; i++)
            {
                this.Controls.Remove(pmarkers[i]);
            }
            pmarkers.Clear();
            markers.Clear();
            Log.Information("Removed all markers");
        }

        private void resetDataSource()
        {
            lb_Markers.DataSource = null;
            lb_Markers.DataSource = markers.OrderBy(m => m.Time).ToList();
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

        private void abcDispose()
        {
            player.Dispose();
            audio.Dispose();
            player = null;
            audio = null;
        }
    }

}
