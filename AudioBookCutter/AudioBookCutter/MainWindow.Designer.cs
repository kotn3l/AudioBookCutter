﻿namespace AudioBookCutter
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
            ((System.ComponentModel.ISupportInitialize)(this.audioWaveImage)).BeginInit();
            this.SuspendLayout();
            // 
            // audioWaveImage
            // 
            this.audioWaveImage.Location = new System.Drawing.Point(12, 31);
            this.audioWaveImage.Name = "audioWaveImage";
            this.audioWaveImage.Size = new System.Drawing.Size(775, 170);
            this.audioWaveImage.TabIndex = 0;
            this.audioWaveImage.TabStop = false;
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
            this.trackLength.Location = new System.Drawing.Point(723, 204);
            this.trackLength.Name = "trackLength";
            this.trackLength.Size = new System.Drawing.Size(70, 13);
            this.trackLength.TabIndex = 1;
            this.trackLength.Text = "00:00:00.000";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.trackLength);
            this.Controls.Add(this.audioWaveImage);
            this.Menu = this.mainMenu1;
            this.Name = "MainWindow";
            this.Text = "Main";
            this.ResizeEnd += new System.EventHandler(this.MainWindow_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.audioWaveImage)).EndInit();
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
    }
}

