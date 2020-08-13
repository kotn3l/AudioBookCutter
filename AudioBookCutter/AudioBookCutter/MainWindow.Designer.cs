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
            this.saveMarkerFrames = new System.Windows.Forms.MenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.trackLength = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.now = new System.Windows.Forms.Label();
            this.start = new System.Windows.Forms.Button();
            this.seeker = new System.Windows.Forms.PictureBox();
            this.pause = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.cut = new System.Windows.Forms.Button();
            this.markerCurrent = new System.Windows.Forms.Button();
            this.markerOther = new System.Windows.Forms.Button();
            this.markerHour = new System.Windows.Forms.TextBox();
            this.markerMinute = new System.Windows.Forms.TextBox();
            this.markerSeconds = new System.Windows.Forms.TextBox();
            this.markerMiliseconds = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_Time = new System.Windows.Forms.Label();
            this.lb_Markers = new System.Windows.Forms.ListBox();
            this.btnDeleteMarker = new System.Windows.Forms.Button();
            this.lbScale = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSubtract = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lb_rendering = new System.Windows.Forms.Label();
            this.btnSkip = new System.Windows.Forms.Button();
            this.tb_Edit = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.audioWaveImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seeker)).BeginInit();
            this.SuspendLayout();
            // 
            // audioWaveImage
            // 
            this.audioWaveImage.AccessibleDescription = "A megnyitott hangfájl hangullám reprezentációja képben.";
            this.audioWaveImage.AccessibleName = "Audio wave";
            this.audioWaveImage.AccessibleRole = System.Windows.Forms.AccessibleRole.Graphic;
            this.audioWaveImage.BackColor = System.Drawing.Color.LightGray;
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
            this.saveMarker,
            this.saveMarkerFrames});
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
            this.openMarker.Click += new System.EventHandler(this.openMarker_Click);
            // 
            // saveMarker
            // 
            this.saveMarker.Enabled = false;
            this.saveMarker.Index = 2;
            this.saveMarker.Text = "Marker mentése... (ms)";
            this.saveMarker.Click += new System.EventHandler(this.saveMarker_Click);
            // 
            // saveMarkerFrames
            // 
            this.saveMarkerFrames.Enabled = false;
            this.saveMarkerFrames.Index = 3;
            this.saveMarkerFrames.Text = "Marker mentése... (frames)";
            this.saveMarkerFrames.Click += new System.EventHandler(this.saveMarkerFrames_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.Title = "Válassza ki a kezelni kívánt hanganyagokat";
            // 
            // trackLength
            // 
            this.trackLength.AccessibleDescription = "A megnyitott hangfájl teljes hossza.";
            this.trackLength.AccessibleName = "Audio hossza";
            this.trackLength.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
            this.trackLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackLength.AutoSize = true;
            this.trackLength.Location = new System.Drawing.Point(718, 9);
            this.trackLength.Name = "trackLength";
            this.trackLength.Size = new System.Drawing.Size(70, 13);
            this.trackLength.TabIndex = 1;
            this.trackLength.Text = "00:00:00.000";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // now
            // 
            this.now.AccessibleDescription = "Az audiofájlban a seeker jelenlegi helyzete.";
            this.now.AccessibleName = "Jelenlegi idő";
            this.now.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
            this.now.AutoSize = true;
            this.now.Location = new System.Drawing.Point(9, 9);
            this.now.Name = "now";
            this.now.Size = new System.Drawing.Size(70, 13);
            this.now.TabIndex = 2;
            this.now.Text = "00:00:00.000";
            // 
            // start
            // 
            this.start.AccessibleDescription = "A lejátszás elindításához.";
            this.start.AccessibleName = "Lejátszás";
            this.start.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.start.Enabled = false;
            this.start.Location = new System.Drawing.Point(12, 191);
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
            this.seeker.Location = new System.Drawing.Point(0, 17);
            this.seeker.Name = "seeker";
            this.seeker.Size = new System.Drawing.Size(1, 170);
            this.seeker.TabIndex = 4;
            this.seeker.TabStop = false;
            // 
            // pause
            // 
            this.pause.AccessibleDescription = "A lejátszás szüneteltetéséhez.";
            this.pause.AccessibleName = "Szünet";
            this.pause.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.pause.Enabled = false;
            this.pause.Location = new System.Drawing.Point(53, 191);
            this.pause.Name = "pause";
            this.pause.Size = new System.Drawing.Size(47, 23);
            this.pause.TabIndex = 5;
            this.pause.Text = "Pause";
            this.pause.UseVisualStyleBackColor = true;
            this.pause.Click += new System.EventHandler(this.pause_Click);
            // 
            // stop
            // 
            this.stop.AccessibleDescription = "A lejátszás leállításához.";
            this.stop.AccessibleName = "Megállítás";
            this.stop.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.stop.Enabled = false;
            this.stop.Location = new System.Drawing.Point(106, 191);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(39, 23);
            this.stop.TabIndex = 6;
            this.stop.Text = "Stop";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // cut
            // 
            this.cut.AccessibleDescription = "A gomb elindítja a vágást a markerek mentén.";
            this.cut.AccessibleName = "Vágás gomb";
            this.cut.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cut.Enabled = false;
            this.cut.Location = new System.Drawing.Point(393, 192);
            this.cut.Name = "cut";
            this.cut.Size = new System.Drawing.Size(75, 23);
            this.cut.TabIndex = 7;
            this.cut.Text = "Vágás";
            this.cut.UseVisualStyleBackColor = true;
            this.cut.Click += new System.EventHandler(this.cut_Click);
            // 
            // markerCurrent
            // 
            this.markerCurrent.AccessibleDescription = "Markert helyez a seeker jelenlegi pozíciójába.";
            this.markerCurrent.AccessibleName = "Jelenlegi marker lehelyezés.";
            this.markerCurrent.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.markerCurrent.Enabled = false;
            this.markerCurrent.Location = new System.Drawing.Point(151, 191);
            this.markerCurrent.Name = "markerCurrent";
            this.markerCurrent.Size = new System.Drawing.Size(138, 34);
            this.markerCurrent.TabIndex = 8;
            this.markerCurrent.Text = "Marker a jelenlegi pozícióba";
            this.markerCurrent.UseVisualStyleBackColor = true;
            this.markerCurrent.Click += new System.EventHandler(this.markerCurrent_Click);
            // 
            // markerOther
            // 
            this.markerOther.AccessibleDescription = "Markert helyez a lentebb megadott időhöz.";
            this.markerOther.AccessibleName = "Marker megadott időhöz.";
            this.markerOther.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.markerOther.Enabled = false;
            this.markerOther.Location = new System.Drawing.Point(151, 231);
            this.markerOther.Name = "markerOther";
            this.markerOther.Size = new System.Drawing.Size(138, 23);
            this.markerOther.TabIndex = 9;
            this.markerOther.Text = "Marker ehhez az időhöz";
            this.markerOther.UseVisualStyleBackColor = true;
            this.markerOther.Click += new System.EventHandler(this.markerOther_Click);
            // 
            // markerHour
            // 
            this.markerHour.AccessibleDescription = "A manuális marker óra mezője.";
            this.markerHour.AccessibleName = "Óra";
            this.markerHour.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.markerHour.Enabled = false;
            this.markerHour.Location = new System.Drawing.Point(151, 260);
            this.markerHour.Name = "markerHour";
            this.markerHour.Size = new System.Drawing.Size(21, 20);
            this.markerHour.TabIndex = 10;
            this.markerHour.Text = "0";
            this.markerHour.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.markerHour_KeyPress);
            // 
            // markerMinute
            // 
            this.markerMinute.AccessibleDescription = "A manuális marker perc mezője.";
            this.markerMinute.AccessibleName = "Perc";
            this.markerMinute.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.markerMinute.Enabled = false;
            this.markerMinute.Location = new System.Drawing.Point(178, 260);
            this.markerMinute.Name = "markerMinute";
            this.markerMinute.Size = new System.Drawing.Size(21, 20);
            this.markerMinute.TabIndex = 11;
            this.markerMinute.Text = "0";
            this.markerMinute.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.markerMinute_KeyPress);
            // 
            // markerSeconds
            // 
            this.markerSeconds.AccessibleDescription = "A manuális marker másodperc mezője.";
            this.markerSeconds.AccessibleName = "Másodperc";
            this.markerSeconds.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.markerSeconds.Enabled = false;
            this.markerSeconds.Location = new System.Drawing.Point(205, 260);
            this.markerSeconds.Name = "markerSeconds";
            this.markerSeconds.Size = new System.Drawing.Size(21, 20);
            this.markerSeconds.TabIndex = 12;
            this.markerSeconds.Text = "0";
            this.markerSeconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.markerSeconds_KeyPress);
            // 
            // markerMiliseconds
            // 
            this.markerMiliseconds.AccessibleDescription = "A manuális marker milliszekundum mezője.";
            this.markerMiliseconds.AccessibleName = "Milliszekundum";
            this.markerMiliseconds.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.markerMiliseconds.Enabled = false;
            this.markerMiliseconds.Location = new System.Drawing.Point(232, 260);
            this.markerMiliseconds.Name = "markerMiliseconds";
            this.markerMiliseconds.Size = new System.Drawing.Size(57, 20);
            this.markerMiliseconds.TabIndex = 13;
            this.markerMiliseconds.Text = "0";
            this.markerMiliseconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.markerMiliseconds_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(170, 263);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = ":";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(197, 263);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = ":";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(224, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = ".";
            // 
            // lbl_Time
            // 
            this.lbl_Time.AccessibleDescription = "Címkék a manuális marker adatainak bevitelére.";
            this.lbl_Time.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
            this.lbl_Time.AutoSize = true;
            this.lbl_Time.Location = new System.Drawing.Point(150, 282);
            this.lbl_Time.Name = "lbl_Time";
            this.lbl_Time.Size = new System.Drawing.Size(122, 13);
            this.lbl_Time.TabIndex = 17;
            this.lbl_Time.Text = "HH   MM    SS           ms";
            // 
            // lb_Markers
            // 
            this.lb_Markers.AccessibleDescription = "Markerek listája időrendben.";
            this.lb_Markers.AccessibleName = "Markerek listája";
            this.lb_Markers.AccessibleRole = System.Windows.Forms.AccessibleRole.List;
            this.lb_Markers.FormattingEnabled = true;
            this.lb_Markers.Location = new System.Drawing.Point(295, 192);
            this.lb_Markers.Name = "lb_Markers";
            this.lb_Markers.Size = new System.Drawing.Size(92, 264);
            this.lb_Markers.TabIndex = 18;
            this.lb_Markers.SelectedIndexChanged += new System.EventHandler(this.lb_Markers_SelectedIndexChanged);
            // 
            // btnDeleteMarker
            // 
            this.btnDeleteMarker.AccessibleDescription = "Törli a kiválasztott markert.";
            this.btnDeleteMarker.AccessibleName = "Marker törlése";
            this.btnDeleteMarker.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDeleteMarker.Enabled = false;
            this.btnDeleteMarker.Location = new System.Drawing.Point(153, 399);
            this.btnDeleteMarker.Name = "btnDeleteMarker";
            this.btnDeleteMarker.Size = new System.Drawing.Size(136, 23);
            this.btnDeleteMarker.TabIndex = 19;
            this.btnDeleteMarker.Text = "Marker törlése";
            this.btnDeleteMarker.UseVisualStyleBackColor = true;
            this.btnDeleteMarker.Click += new System.EventHandler(this.btnDeleteMarker_Click);
            // 
            // lbScale
            // 
            this.lbScale.AccessibleDescription = "Mértékegységek felsorolva a markerek szerekesztéséhez.";
            this.lbScale.AccessibleName = "Mértékegység szerkesztéshez";
            this.lbScale.AccessibleRole = System.Windows.Forms.AccessibleRole.List;
            this.lbScale.Enabled = false;
            this.lbScale.FormattingEnabled = true;
            this.lbScale.Location = new System.Drawing.Point(153, 324);
            this.lbScale.Name = "lbScale";
            this.lbScale.Size = new System.Drawing.Size(40, 56);
            this.lbScale.TabIndex = 21;
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleDescription = "Hozzáadja a kiválasztott markerhez a beírt értéket a kiválasztott mértékkel.";
            this.btnAdd.AccessibleName = "Hozzáadás";
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(199, 357);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(42, 23);
            this.btnAdd.TabIndex = 22;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSubtract
            // 
            this.btnSubtract.AccessibleDescription = "Kivonja a kiválasztott markerból a beírt értéket a kiválasztott mértékkel.";
            this.btnSubtract.AccessibleName = "Kivonás";
            this.btnSubtract.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSubtract.Enabled = false;
            this.btnSubtract.Location = new System.Drawing.Point(248, 357);
            this.btnSubtract.Name = "btnSubtract";
            this.btnSubtract.Size = new System.Drawing.Size(41, 23);
            this.btnSubtract.TabIndex = 23;
            this.btnSubtract.Text = "-";
            this.btnSubtract.UseVisualStyleBackColor = true;
            this.btnSubtract.Click += new System.EventHandler(this.btnSubtract_Click);
            // 
            // label4
            // 
            this.label4.AccessibleDescription = "Marker szerkesztésének címke.";
            this.label4.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(150, 308);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Marker szerkesztése";
            // 
            // lb_rendering
            // 
            this.lb_rendering.AccessibleDescription = "Akkor jelenik meg, ha még az audiofájl hullámja nem jelent meg.";
            this.lb_rendering.AccessibleName = "Rendering címke.";
            this.lb_rendering.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
            this.lb_rendering.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lb_rendering.AutoSize = true;
            this.lb_rendering.BackColor = System.Drawing.Color.Transparent;
            this.lb_rendering.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_rendering.Location = new System.Drawing.Point(325, 91);
            this.lb_rendering.Name = "lb_rendering";
            this.lb_rendering.Size = new System.Drawing.Size(144, 29);
            this.lb_rendering.TabIndex = 25;
            this.lb_rendering.Text = "Rendering...";
            this.lb_rendering.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_rendering.Visible = false;
            // 
            // btnSkip
            // 
            this.btnSkip.AccessibleDescription = "A lejátszás a következő markertől folytatódik.";
            this.btnSkip.AccessibleName = "Következő marker";
            this.btnSkip.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSkip.Enabled = false;
            this.btnSkip.Location = new System.Drawing.Point(12, 220);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(133, 23);
            this.btnSkip.TabIndex = 26;
            this.btnSkip.Text = "To next marker";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // tb_Edit
            // 
            this.tb_Edit.AccessibleDescription = "A marker szerkesztéséhez ide kell beirni az értéket.";
            this.tb_Edit.AccessibleName = "Marker szerkesztés érték";
            this.tb_Edit.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.tb_Edit.Enabled = false;
            this.tb_Edit.Location = new System.Drawing.Point(200, 324);
            this.tb_Edit.Name = "tb_Edit";
            this.tb_Edit.Size = new System.Drawing.Size(89, 20);
            this.tb_Edit.TabIndex = 27;
            this.tb_Edit.Text = "0";
            this.tb_Edit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_Edit_KeyPress);
            // 
            // MainWindow
            // 
            this.AccessibleDescription = "Az applikáció ablaka";
            this.AccessibleName = "Fő ablak";
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 462);
            this.Controls.Add(this.tb_Edit);
            this.Controls.Add(this.btnSkip);
            this.Controls.Add(this.lb_rendering);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSubtract);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lbScale);
            this.Controls.Add(this.btnDeleteMarker);
            this.Controls.Add(this.lb_Markers);
            this.Controls.Add(this.lbl_Time);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.markerMiliseconds);
            this.Controls.Add(this.markerSeconds);
            this.Controls.Add(this.markerMinute);
            this.Controls.Add(this.markerHour);
            this.Controls.Add(this.markerOther);
            this.Controls.Add(this.markerCurrent);
            this.Controls.Add(this.cut);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.pause);
            this.Controls.Add(this.seeker);
            this.Controls.Add(this.start);
            this.Controls.Add(this.now);
            this.Controls.Add(this.trackLength);
            this.Controls.Add(this.audioWaveImage);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "MainWindow";
            this.Text = "Audio Book Cutter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.MainWindow_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
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
        private System.Windows.Forms.Button markerCurrent;
        private System.Windows.Forms.Button markerOther;
        private System.Windows.Forms.TextBox markerHour;
        private System.Windows.Forms.TextBox markerMinute;
        private System.Windows.Forms.TextBox markerSeconds;
        private System.Windows.Forms.TextBox markerMiliseconds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_Time;
        private System.Windows.Forms.ListBox lb_Markers;
        private System.Windows.Forms.Button btnDeleteMarker;
        private System.Windows.Forms.ListBox lbScale;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSubtract;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label lb_rendering;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.MenuItem saveMarkerFrames;
        private System.Windows.Forms.TextBox tb_Edit;
    }
}

