using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Serilog;
using Microsoft.VisualBasic.FileIO;

namespace AudioBookCutter
{
    public partial class MainWindow : Form
    {
        private enum PlaybackState
        {
            Playing, Stopped, Paused
        }

        private PlaybackState playbackState;
        private AudioPlayer player;
        private Audio audio = null;
        private Command ffmpeg;

        private List<Marker> markers;
        private List<PictureBox> pmarkers;

        private List<string> filenames;
        private List<Audio> audioMultiple;
        private List<Marker> multiple;
        private List<PictureBox> pmultiple;

        private CUEManager manager;
        private bool resized;
        private string workingDir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        private string main = "[MAIN] ";
        private string errorMsg = "Egy hiba lépett fel az alkalmazásban. Kérlek zárd be a programot, és csatold az exe mellett lévő log fájlt GitHubon egy issue létrehozásával, vagy küldd erre az e-mail címre: kotn3l@gmail.com";
        private int selectedMarkerIndex = -1;
        private int selectedFileIndex = -1;

        public MainWindow()
        {
            InitializeComponent();
            audioWaveImage.Width = this.Width - 16;
            markers = new List<Marker>();
            pmarkers = new List<PictureBox>();
            label1.SendToBack();
            label2.SendToBack();
            label3.SendToBack();
            resized = false;
            lb_rendering.BringToFront();
            emptyLogs();
            var log = new LoggerConfiguration()
            .WriteTo.File(workingDir+@"/log.log", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
            .CreateLogger();
            Log.Logger = log;
            Log.Information(main + "Application started successfully");
        }

        //HOTKEYS
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V)
            {
                if (playbackState == PlaybackState.Playing || playbackState == PlaybackState.Paused)
                {
                    stop.PerformClick();
                }
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.X)
            {
                if (player != null)
                {
                    if (playbackState == PlaybackState.Paused || playbackState == PlaybackState.Stopped)
                    {
                        start.PerformClick();
                    }
                }
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.C)
            {
                if (player != null)
                {
                    if (playbackState == PlaybackState.Playing)
                    {
                        pause.PerformClick();
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
            if (e.Control && e.KeyCode == Keys.N)
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
            if (e.KeyCode == Keys.Delete)
            {
                if (markers != null && markers.Count > 0)
                {
                    deleteMarker();
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
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (saveMarker.Enabled)
                {
                    saveMarker.PerformClick();
                }
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.F)
            {
                if (lbFiles.Enabled)
                {
                    lbFiles.Focus();
                }
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.Left)
            {
                if (player != null)
                {
                    skipOne(false);
                }
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.Right)
            {
                if (player != null)
                {
                    skipOne(true);
                }
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.U)
            {
                if (btnManualSkip.Enabled)
                {
                    btnManualSkip.PerformClick();
                }
                e.Handled = true;
                return;
            }
        }
        private void MainWindow_ResizeEnd(object sender, EventArgs e)
        {
            if (resized)
            {
                resized = false;
                audioWaveImage.Width = this.Width - 16;
                if (audio != null)
                {
                    Thread t = new Thread(() => audioWave());
                    t.IsBackground = true;
                    t.Start();
                    timeLocation();
                    updateMarkers();
                }
            }
        }
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (player != null)
            {
                player.Stop();
                abcDispose();
            }
            Log.Information(main + "Application is closing");
            Log.CloseAndFlush();
        }
        private void MainWindow_Resize(object sender, EventArgs e)
        {
            resized = true;
        }
        private void emptyLogs()
        {
            if (File.Exists(workingDir + @"/log.log"))
            {
                File.Delete(workingDir + @"/log.log");
            }
        }

        private void audioWave()
        {
            try
            {
                renderText(true);
                audioWaveImage.Image = audio.audioWave(this.Width - 16, Log.Logger);
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
            filenames = new List<string>();
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
                        removeAllDivs();
                        resetDataSource();
                        Log.Information(main + "Reopening");
                    }
                    if (openFileDialog1.FileNames.Length > 1)
                    {
                        lbFiles.Enabled = true;
                        audioMultiple = new List<Audio>();
                        multiple = new List<Marker>();
                        pmultiple = new List<PictureBox>();
                        Log.Information(main + "Multiple files opened: {0}", openFileDialog1.FileNames);
                        ffmpeg = new Command(Log.Logger);
                        string result = ffmpeg.mergeFiles(openFileDialog1.FileNames);
                        audio = new Audio(result, openFileDialog1.FileNames[0]);
                        for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
                        {
                            filenames.Add(Path.GetFileName(openFileDialog1.FileNames[i]));
                        }
                        filenames.Add(Path.GetFileName(result));
                    }
                    else
                    {
                        audio = new Audio(openFileDialog1.FileNames[0], openFileDialog1.FileNames[0]);
                        filenames.Add(Path.GetFileName(openFileDialog1.FileNames[0]));
                        Log.Information(main + "One file opened: {0}", Path.GetFileName(audio.aPath));
                    }
                    lbFiles.DataSource = filenames;
                    lbFiles.SelectedIndex = filenames.Count - 1;
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
                    trackLength.Text = FormatTimeSpan(player.GetLength());
                    Log.Information(main + "Track length: {0}", trackLength.Text);
                    playbackState = PlaybackState.Stopped;
                    timeLocation();
                    updateMarkers();
                    if (multiple != null)
                    {
                        placeMultiple(openFileDialog1.FileNames);
                    }
                    Thread t = new Thread(() => audioWave());
                    t.IsBackground = true;
                    t.Start();
                    resized = false;
                    this.Text = "Audio Book Cutter - " + audio.OriginalName + @"/" + Path.GetFileName(audio.aPath);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error occured while creating player");
                    MessageBox.Show(errorMsg);
                }
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
            if (player != null)
            {
                for (int i = 0; i < pmarkers.Count; i++)
                {
                    pmarkers[i].Location = new Point(markers[i].calculateX(this.Width - 16, player.GetLength()), pmarkers[i].Location.Y);
                }
                if (pmultiple != null)
                {
                    for (int i = 0; i < pmultiple.Count; i++)
                    {
                        pmultiple[i].Location = new Point(multiple[i].calculateX(this.Width - 16, player.GetLength()), pmultiple[i].Location.Y);
                    }
                }
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
        private void placeMultiple(string[] files)
        {
            for (int i = 0; i < files.Length; i++)
            {
                audioMultiple.Add(new Audio(files[i], files[i]));
            }
            AudioPlayer p = new AudioPlayer(audioMultiple[0].aPath);
            Marker ddiv = new Marker(p.GetLength());
            p.Dispose();
            addDiv(ddiv);
            for (int i = 1; i < audioMultiple.Count; i++)
            {
                AudioPlayer ap = new AudioPlayer(audioMultiple[i].aPath);
                Marker mdiv = new Marker(ap.GetLength()+multiple[i-1].Time);
                ap.Dispose();
                addDiv(mdiv);
            }
            Log.Information(main + "File lengths: {0}", multiple);
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
                    if (playbackState == PlaybackState.Stopped)
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
        private void skipOne(bool add)
        {
            if (add)
            {
                player.SetPosition(player.GetPosition() + new TimeSpan(0,0,1).TotalMilliseconds);
            }
            else
            {
                player.SetPosition(player.GetPosition() - new TimeSpan(0, 0, 1).TotalMilliseconds);
            }
        }
        private void btnManualSkip_Click(object sender, EventArgs e)
        {
            try
            {
                if (player != null)
                {
                    TimeSpan ts = new TimeSpan(0, int.Parse(markerHour.Text), int.Parse(markerMinute.Text), int.Parse(markerSeconds.Text), int.Parse(markerMiliseconds.Text));
                    if (multiple != null)
                    {
                        if (lbFiles.SelectedIndex == 0)
                        {
                            if (ts <= multiple[0].Time)
                            {
                                player.SetPosition(ts.TotalMilliseconds);
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Ebben a fájlban nem tudsz ehhez az időhöz ugrani!");
                                return;
                            }
                        }
                        else
                        {
                            if (lbFiles.SelectedIndex == filenames.Count - 1)
                            {
                                if (ts <= player.GetLength())
                                {
                                    player.SetPosition(ts.TotalMilliseconds);
                                    return;
                                }
                                else
                                {
                                    MessageBox.Show("Nem ugorhatsz fájlon kívüli időhöz!");
                                    return;
                                }
                            }
                            else
                            {
                                for (int i = 1; i < filenames.Count - 1; i++)
                                {
                                    if (lbFiles.SelectedIndex == i)
                                    {
                                        if (ts <= multiple[i].Time - multiple[i - 1].Time)
                                        {
                                            player.SetPosition((multiple[i - 1].Time + ts).TotalMilliseconds);
                                            return;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ebben a fájlban nem tudsz ehhez az időhöz ugrani!");
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (ts <= player.GetLength())
                        {
                            player.SetPosition(ts.TotalMilliseconds);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Nem ugorhatsz fájlon kívüli időhöz!");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while trying to seek by manual marker");
                MessageBox.Show(errorMsg);
            }
            
        }

        private void _audioPlayer_PlaybackStopped()
        {
            playbackState = PlaybackState.Stopped;
            timer1.Enabled = false;
            buttonChange(false);
            timeLocation();
            now.Text = "00:00:00.00";
            Log.Information(main + "Playback stopped");
        }
        private void _audioPlayer_PlaybackResumed()
        {
            playbackState = PlaybackState.Playing;
            buttonChange(true);
            timer1.Enabled = true;
            Log.Information(main + "Playback started");
        }
        private void _audioPlayer_PlaybackPaused()
        {
            playbackState = PlaybackState.Paused;
            buttonChange(false);
            timer1.Enabled = false;
            Log.Information(main + "Playback paused");
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
            btnManualSkip.Enabled = true;
            openCSV.Enabled = true;
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
                cut.Enabled = true;
                saveMarker.Enabled = true;
                saveMarkerFrames.Enabled = true;
            }
            else
            {
                cut.Enabled = false;
                saveMarker.Enabled = false;
                saveMarkerFrames.Enabled = false;
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
                    ffmpeg = new Command(Log.Logger);
                    List<TimeSpan> times = new List<TimeSpan>();
                    for (int i = 0; i < markers.Count; i++)
                    {
                        times.Add(markers[i].Time);
                    }
                    Log.Information(main + "Cut initiated with {0} markers", times.Count);
                    ffmpeg.cutByTimeSpans(times, player.GetLength(), audio, saveFileDialog1.FileName);
                    MessageBox.Show("Vágás befejezve!");
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
                Marker test = new Marker(TimeSpan.FromMilliseconds(player.GetPosition()));
                //Marker mmarker = new Marker(TimeSpan.FromMilliseconds(player.GetLength().TotalMilliseconds * (seeker.Location.X / (double)audioWaveImage.Width)));
                addMarker(test);
                Log.Information(main + "Marker added at {0}", FormatTimeSpan(test.Time));
                resetDataSource();
            }
        }
        private void markerOther_Click(object sender, EventArgs e)
        {
            if (audio != null)
            {
                try
                {
                    Log.Information(main + "User entered manual timespan values: {0}:{1}:{2}.{3}", markerHour.Text, markerMinute.Text, markerSeconds.Text, markerMiliseconds.Text);
                    TimeSpan ts = new TimeSpan(0, int.Parse(markerHour.Text), int.Parse(markerMinute.Text), int.Parse(markerSeconds.Text), int.Parse(markerMiliseconds.Text));
                    if (multiple != null)
                    {
                        if (lbFiles.SelectedIndex == 0)
                        {
                            if (ts <= multiple[0].Time)
                            {
                                Log.Information(main + "User choose {0}", filenames[0]);
                                Marker mmarker = new Marker(ts);
                                addMarker(mmarker);
                                resetDataSource();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("A marker ideje nem lehet nagyobb mint a megnyitott audiofájl ideje!");
                                return;
                            }
                        }
                        else
                        {
                            if (lbFiles.SelectedIndex == filenames.Count - 1)
                            {
                                if (ts <= player.GetLength())
                                {
                                    Log.Information(main + "User choose to add in the merged file: {0}", filenames[filenames.Count - 1]);
                                    Marker mmarker = new Marker(ts);
                                    addMarker(mmarker);
                                    resetDataSource();
                                    return;
                                }
                                else
                                {
                                    MessageBox.Show("A marker ideje nem lehet nagyobb mint a megnyitott audiofájl ideje!");
                                    return;
                                }
                            }
                            else
                            {
                                for (int i = 1; i < filenames.Count - 1; i++)
                                {
                                    if (lbFiles.SelectedIndex == i)
                                    {
                                        if (ts <= multiple[i].Time - multiple[i - 1].Time)
                                        {
                                            Log.Information(main + "User choose {0}", filenames[i]);
                                            Marker mmarker = new Marker(multiple[i - 1].Time + ts);
                                            addMarker(mmarker);
                                            resetDataSource();
                                            return;
                                        }
                                        else
                                        {
                                            MessageBox.Show("A marker ideje nem lehet nagyobb mint a megnyitott audiofájl ideje!");
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (ts <= player.GetLength())
                        {
                            Marker mmarker = new Marker(ts);
                            addMarker(mmarker);
                            resetDataSource();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("A marker ideje nem lehet nagyobb mint a megnyitott audiofájl ideje!");
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error occured while trying to add manual markers");
                    MessageBox.Show(errorMsg);
                }
            }
        }
        private void deleteMarker()
        {
            if (selectedMarkerIndex < markers.Count && selectedMarkerIndex >= 0)
            {
                int temp = selectedMarkerIndex;
                Log.Information(main + "Marker deleted at {0}", FormatTimeSpan(markers[selectedMarkerIndex].Time));
                removeMarker(selectedMarkerIndex);
                resetDataSource();
                temp--;
                lb_Markers.SelectedIndex = temp;
                return;
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
                    Log.Information(main + "Marker (ms) saved at: {0}", saveFileDialog1.FileName + ".cue");
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
                    Log.Information(main + "Marker (frames) saved at: {0}", saveFileDialog1.FileName + ".cue");
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
                            Log.Information(main + "Markers loaded from {0}", openFileDialog1.FileName);
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
                                Log.Information(main + "Markers loaded from {0}, deleted old markers", openFileDialog1.FileName);
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
            if (!markerAlreadyExists(marker))
            {
                PictureBox pmarker = new PictureBox();
                pmarker.Size = new Size(2, seeker.Size.Height);
                pmarker.Location = new Point(marker.calculateX(this.Width - 16, player.GetLength()), seeker.Location.Y);
                pmarker.BackColor = Color.Blue;
                this.Controls.Add(pmarker);
                pmarker.BringToFront();
                pmarkers.Add(pmarker);
                markers.Add(marker);
                lb_Markers.Focus();
                Log.Information(main + "Marker added at {0}", FormatTimeSpan(marker.Time));
            }
            else
            {
                Log.Information(main + "Marker already exists at {0}", FormatTimeSpan(marker.Time));
                MessageBox.Show("A hozzáadni kívánt marker már létezik!");
            }
        }
        private bool markerAlreadyExists(Marker marker)
        {
            for (int i = 0; i < markers.Count; i++)
            {
                if (marker.Time == markers[i].Time)
                {
                    return true;
                }
            }
            return false;
        }
        private void addDiv(Marker marker)
        {
            PictureBox div = new PictureBox();
            div.Size = new Size(1, seeker.Size.Height);
            div.Location = new Point(marker.calculateX(this.Width - 16, player.GetLength()), seeker.Location.Y);
            div.BackColor = Color.YellowGreen;
            this.Controls.Add(div);
            div.BringToFront();
            multiple.Add(marker);
            pmultiple.Add(div);
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
            Log.Information(main + "Removed all markers");
        }
        private void removeAllDivs()
        {
            if (pmultiple != null)
            {
                for (int i = 0; i < pmultiple.Count; i++)
                {
                    this.Controls.Remove(pmultiple[i]);
                }
                pmultiple.Clear();
                multiple.Clear();
                audioMultiple.Clear();
                filenames.Clear();
                lbFiles.DataSource = null;
                Log.Information(main + "Removed all dividers");
            }
        }
        private void markerHour_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (numericOnly(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void markerMinute_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (numericOnly(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void markerSeconds_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (numericOnly(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void markerMiliseconds_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (numericOnly(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private bool numericOnly(char character)
        {
            if (!char.IsControl(character) && !char.IsDigit(character))
            {
                return true;
            }
            return false;
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
                    selectedMarkerIndex = i;
                }
                else
                {
                    pmarkers[i].BackColor = Color.Blue;
                    pmarkers[i].Width = 2;
                }
            }
        }
        private void lb_Markers_DoubleClick(object sender, EventArgs e)
        {
            markerJump();
        }
        private void lb_Markers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                markerJump();
                e.Handled = true;
                return;
            }
            int i = lb_Markers.SelectedIndex;
            if (e.KeyCode == Keys.Up)
            {
                i--;
                indexCheck(ref i, lb_Markers.Items.Count);
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.Down)
            {
                i++;
                indexCheck(ref i, lb_Markers.Items.Count);
                e.Handled = true;
                return;
            }
        }
        private void lbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedFileIndex = lbFiles.SelectedIndex;
        }
        private void lbFiles_DoubleClick(object sender, EventArgs e)
        {
            fileJump();
        }
        private void lbFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                fileJump();
                e.Handled = true;
                return;
            }
            int i = lbFiles.SelectedIndex;
            if (e.KeyCode == Keys.Up)
            {
                i--;
                indexCheckFile(ref i, lbFiles.Items.Count);
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.Down)
            {
                i++;
                indexCheckFile(ref i, lbFiles.Items.Count);
                e.Handled = true;
                return;
            }
        }
        private void indexCheck(ref int index, int maxIndex)
        {
            if (index < maxIndex && index >= 0)
            {
                lb_Markers.SelectedIndex = index;
            }
        }
        private void indexCheckFile(ref int index, int maxIndex)
        {
            if (index < maxIndex && index >= 0)
            {
                lbFiles.SelectedIndex = index;
            }
        }

        private void markerJump()
        {
            if (player != null && (selectedMarkerIndex < markers.Count && selectedMarkerIndex >= 0))
            {
                player.SetPosition(markers[selectedMarkerIndex].Time.TotalMilliseconds+500);
            }
        }
        private void fileJump()
        {
            if (player != null && (multiple.Count > 0 && multiple != null) && selectedFileIndex < multiple.Count)
            {
                player.SetPosition(multiple[selectedFileIndex].Time.TotalMilliseconds);
            }
        }

        private void abcDispose()
        {
            player.Dispose();
            audio.Dispose();
            player = null;
            audio = null;
        }

        private void openCSV_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "csv fájlok|*.csv";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                csvImport(openFileDialog1.FileName);
            }
        }
        private void csvImport(string FilePath)
        {
            try
            {
                using (TextFieldParser parser = new TextFieldParser(FilePath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        if (fields.Length < 2 || fields[1] == null)
                        {
                            MessageBox.Show("Kevés oszlop van a CSV fájlban! Az első oszlop a fájlnevet, a másodiknak az időt kell tartalmaznia.");
                            return;
                        }
                        if (fields[0] == filenames[0].Split('.')[0])
                        {
                            TimeSpan ts = parseTime(fields);
                            if (ts <= multiple[0].Time)
                            {
                                Log.Information(main + "User choose {0}", filenames[0]);
                                Marker mmarker = new Marker(ts);
                                addMarker(mmarker);
                                resetDataSource();
                                continue;
                            }
                            else
                            {
                                MessageBox.Show("A marker ideje nem lehet nagyobb mint a megnyitott audiofájl ideje!");
                                return;
                            }
                        }
                        for (int i = 1; i < filenames.Count - 1; i++)
                        {
                            if (fields[0] == filenames[i].Split('.')[0])
                            {
                                TimeSpan ts = parseTime(fields);
                                if (ts <= multiple[i].Time - multiple[i - 1].Time)
                                {
                                    Log.Information(main + "User choose {0}", filenames[i]);
                                    Marker mmarker = new Marker(multiple[i - 1].Time + ts);
                                    addMarker(mmarker);
                                    resetDataSource();
                                    break;
                                }
                                else
                                {
                                    MessageBox.Show("A marker ideje nem lehet nagyobb mint a megnyitott audiofájl ideje!");
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occured while trying to add manual markers");
                MessageBox.Show(errorMsg);
            }
        }
        private TimeSpan parseTime(string[] fields)
        {
            string[] kettos = fields[1].Split(':');
            string[] pont = null;
            TimeSpan ts = new TimeSpan();
            if (fields[1].All(char.IsDigit))
            {
                MessageBox.Show("Az időben nem szerepelhet betű!");
                return ts;
            }
            if (kettos[kettos.Length - 1].Contains('.'))
            {
                pont = kettos[kettos.Length - 1].Split('.');
            }
            if (pont == null)
            {
                if (kettos.Length == 3)
                {
                    ts = new TimeSpan(int.Parse(kettos[0]), int.Parse(kettos[1]), int.Parse(kettos[2]));
                }
                else if (kettos.Length == 2)
                {
                    ts = new TimeSpan(0, int.Parse(kettos[0]), int.Parse(kettos[1]));
                }
            }
            else
            {
                if (kettos.Length == 3)
                {
                    ts = new TimeSpan(0, int.Parse(kettos[0]), int.Parse(kettos[1]), int.Parse(pont[0]), int.Parse(pont[1]));
                }
                else if (kettos.Length == 2)
                {
                    ts = new TimeSpan(0, 0, int.Parse(kettos[0]), int.Parse(pont[0]), int.Parse(pont[1]));
                }
            }
            return ts;
        }
    }

}
