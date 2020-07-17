namespace AudioBookCutter
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.audioWaveImage = new System.Windows.Forms.PictureBox();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.openAudio = new System.Windows.Forms.MenuItem();
            this.openMarker = new System.Windows.Forms.MenuItem();
            this.saveMarker = new System.Windows.Forms.MenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.trackLength = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.now = new System.Windows.Forms.Label();
            this.start = new System.Windows.Forms.Button();
            this.seeker = new System.Windows.Forms.PictureBox();
            this.pause = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.cut = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.audioWaveImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seeker)).BeginInit();
            this.SuspendLayout();
            // 
            // audioWaveImage
            // 
            this.audioWaveImage.Location = new System.Drawing.Point(0, 31);
            this.audioWaveImage.Name = "audioWaveImage";
            this.audioWaveImage.Size = new System.Drawing.Size(800, 145);
            this.audioWaveImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.audioWaveImage.TabIndex = 0;
            this.audioWaveImage.TabStop = false;
            this.audioWaveImage.Click += new System.EventHandler(this.audioWaveImage_Click);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.openAudio,
            this.openMarker,
            this.saveMarker});
            this.menuItem1.Text = "Fájl";
            // 
            // openAudio
            // 
            this.openAudio.Index = 0;
            this.openAudio.Text = "Hanganyag(ok) megnyitása...";
            this.openAudio.Click += new System.EventHandler(this.openAudio_Click);
            // 
            // openMarker
            // 
            this.openMarker.Enabled = false;
            this.openMarker.Index = 1;
            this.openMarker.Text = "Marker megnyitása...";
            // 
            // saveMarker
            // 
            this.saveMarker.Enabled = false;
            this.saveMarker.Index = 2;
            this.saveMarker.Text = "Marker mentése...";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.Title = "Válassza ki a kezelni kívánt hanganyagokat";
            // 
            // trackLength
            // 
            this.trackLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackLength.AutoSize = true;
            this.trackLength.Location = new System.Drawing.Point(733, 15);
            this.trackLength.Name = "trackLength";
            this.trackLength.Size = new System.Drawing.Size(55, 13);
            this.trackLength.TabIndex = 1;
            this.trackLength.Text = "00:00.000";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // now
            // 
            this.now.AutoSize = true;
            this.now.Location = new System.Drawing.Point(12, 15);
            this.now.Name = "now";
            this.now.Size = new System.Drawing.Size(55, 13);
            this.now.TabIndex = 2;
            this.now.Text = "00:00.000";
            // 
            // start
            // 
            this.start.Enabled = false;
            this.start.Location = new System.Drawing.Point(12, 182);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(35, 23);
            this.start.TabIndex = 3;
            this.start.Text = "Play";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // seeker
            // 
            this.seeker.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.seeker.Location = new System.Drawing.Point(0, 12);
            this.seeker.Name = "seeker";
            this.seeker.Size = new System.Drawing.Size(1, 170);
            this.seeker.TabIndex = 4;
            this.seeker.TabStop = false;
            // 
            // pause
            // 
            this.pause.Enabled = false;
            this.pause.Location = new System.Drawing.Point(53, 182);
            this.pause.Name = "pause";
            this.pause.Size = new System.Drawing.Size(47, 23);
            this.pause.TabIndex = 5;
            this.pause.Text = "Pause";
            this.pause.UseVisualStyleBackColor = true;
            this.pause.Click += new System.EventHandler(this.pause_Click);
            // 
            // stop
            // 
            this.stop.Enabled = false;
            this.stop.Location = new System.Drawing.Point(106, 182);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(39, 23);
            this.stop.TabIndex = 6;
            this.stop.Text = "Stop";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // cut
            // 
            this.cut.Location = new System.Drawing.Point(12, 222);
            this.cut.Name = "cut";
            this.cut.Size = new System.Drawing.Size(75, 23);
            this.cut.TabIndex = 7;
            this.cut.Text = "button1";
            this.cut.UseVisualStyleBackColor = true;
            this.cut.Click += new System.EventHandler(this.cut_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cut);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.pause);
            this.Controls.Add(this.seeker);
            this.Controls.Add(this.start);
            this.Controls.Add(this.now);
            this.Controls.Add(this.trackLength);
            this.Controls.Add(this.audioWaveImage);
            this.Menu = this.mainMenu1;
            this.Name = "MainWindow";
            this.Text = "Main";
            this.ResizeEnd += new System.EventHandler(this.MainWindow_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.audioWaveImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seeker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox audioWaveImage;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem openAudio;
        private System.Windows.Forms.MenuItem openMarker;
        private System.Windows.Forms.MenuItem saveMarker;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label trackLength;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label now;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.PictureBox seeker;
        private System.Windows.Forms.Button pause;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.Button cut;
    }
}

