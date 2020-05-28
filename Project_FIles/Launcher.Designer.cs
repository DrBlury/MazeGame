namespace mazegame
{
    partial class Launcher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
            this.mainLogo = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tickImage = new System.Windows.Forms.PictureBox();
            this.filename_label = new System.Windows.Forms.Label();
            this.crossImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tickImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.crossImage)).BeginInit();
            this.SuspendLayout();
            // 
            // mainLogo
            // 
            this.mainLogo.Image = ((System.Drawing.Image)(resources.GetObject("mainLogo.Image")));
            this.mainLogo.Location = new System.Drawing.Point(12, 12);
            this.mainLogo.Name = "mainLogo";
            this.mainLogo.Size = new System.Drawing.Size(776, 297);
            this.mainLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mainLogo.TabIndex = 0;
            this.mainLogo.TabStop = false;
            this.mainLogo.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(12, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(389, 52);
            this.button1.TabIndex = 1;
            this.button1.Text = "Choose .maze file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(536, 338);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(252, 100);
            this.button2.TabIndex = 2;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tickImage
            // 
            this.tickImage.Image = ((System.Drawing.Image)(resources.GetObject("tickImage.Image")));
            this.tickImage.Location = new System.Drawing.Point(407, 338);
            this.tickImage.Name = "tickImage";
            this.tickImage.Size = new System.Drawing.Size(123, 100);
            this.tickImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.tickImage.TabIndex = 3;
            this.tickImage.TabStop = false;
            this.tickImage.Visible = false;
            // 
            // filename_label
            // 
            this.filename_label.AutoSize = true;
            this.filename_label.Location = new System.Drawing.Point(13, 338);
            this.filename_label.Name = "filename_label";
            this.filename_label.Size = new System.Drawing.Size(329, 25);
            this.filename_label.TabIndex = 4;
            this.filename_label.Text = "Click \"Choose .maze file\" to load a maze";
            // 
            // crossImage
            // 
            this.crossImage.Image = ((System.Drawing.Image)(resources.GetObject("crossImage.Image")));
            this.crossImage.Location = new System.Drawing.Point(407, 338);
            this.crossImage.Name = "crossImage";
            this.crossImage.Size = new System.Drawing.Size(123, 100);
            this.crossImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.crossImage.TabIndex = 3;
            this.crossImage.TabStop = false;
            this.crossImage.Visible = false;
            this.crossImage.Click += new System.EventHandler(this.pictureBox1_Click_1);
            // 
            // MazeRunner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 471);
            this.Controls.Add(this.crossImage);
            this.Controls.Add(this.filename_label);
            this.Controls.Add(this.tickImage);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mainLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MazeRunner";
            this.Text = "MazeRunner";
            this.Load += new System.EventHandler(this.MazeRunner_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tickImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.crossImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mainLogo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox tickImage;
        private System.Windows.Forms.Label filename_label;
        private System.Windows.Forms.PictureBox crossImage;
    }
}