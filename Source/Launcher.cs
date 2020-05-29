using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using MazeGame;

namespace mazegame
{
    
    public class Launcher : Form
    {
        String pathOfExecutable = System.Environment.CurrentDirectory + "/";
        PictureBox confirmationImage;
        Button startBtn;
        CheckBox autoPlayerCbox;
        Maze maze;
        private String filePath;

        [STAThread]
        static void Main() {
            Application.Run(new Launcher());
        }

        

        public Launcher() {
            Icon = new Icon(pathOfExecutable + "/Ressources/Images/icon.ico");
            Width = 600;
            Height = 370;
            BackColor = Color.FromArgb(30, 30, 30);
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Text = String.Empty;

            DrawExitButton();
            DrawLogo();
            DrawStartButton();
            DrawfileLoaderButton();
            DrawConfirmationImage();
            DrawAutoPlayerCheckbox();
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case WM_NCHITTEST:
                    base.WndProc(ref m);

                    if ((int)m.Result == HTCLIENT)
                        m.Result = (IntPtr)HTCAPTION;
                    return;
            }

            base.WndProc(ref m);
        }

        private void DrawLogo() {
            PictureBox logo = new PictureBox {
                Name = "logo",
                Size = new Size(400, 200),
                Location = new Point(100, 20),
                Image = Image.FromFile(pathOfExecutable + "/Ressources/Images/logo_small.png"),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Controls.Add(logo);
        }

        private void DrawAutoPlayerCheckbox() {
            PictureBox autoPlayerImage = new PictureBox {
                Name = "autoplayer",
                Size = new Size(60, 40),
                Location = new Point(100, 310),
                Image = Image.FromFile(pathOfExecutable + "/Ressources/Images/auto.png"),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };
            Controls.Add(autoPlayerImage);

            autoPlayerCbox = new CheckBox();
            autoPlayerCbox.Location = new Point(100, 310);
            autoPlayerCbox.Height = 40;
            autoPlayerCbox.Width = 100;
            //autoPlayerCbox.FlatStyle = FlatStyle.Flat;
            //autoPlayerCbox.FlatAppearance.BorderSize = 0;

            // Set background and foreground
            autoPlayerCbox.BackColor = Color.FromArgb(180, 70, 70);
            autoPlayerCbox.ForeColor = Color.White;

            autoPlayerCbox.Name = "autoPlayerCbox";
            autoPlayerCbox.Appearance = Appearance.Button;

            Controls.Add(autoPlayerCbox);
        }

        private void DrawConfirmationImage() {
            confirmationImage = new PictureBox {
                Name = "confirmationImage",
                Size = new Size(40, 40),
                Location = new Point(460, 250),
                Image = Image.FromFile(pathOfExecutable + "/Ressources/Images/tick.png"),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.FromArgb(40, 200, 40),
            };
            Controls.Add(confirmationImage);
            confirmationImage.Hide();
        }

        private void ChangeConfirmationImage(bool accepted) {
            switch (accepted) {
                case true:
                    confirmationImage.Image = Image.FromFile(pathOfExecutable + "/Ressources/Images/tick.png");
                    confirmationImage.BackColor = Color.FromArgb(40, 200, 40); // Green
                    break;
                case false:
                    confirmationImage.Image = Image.FromFile(pathOfExecutable + "/Ressources/Images/cross.png");
                    confirmationImage.BackColor = Color.FromArgb(120, 30, 30); // Red
                    break;
            }
            confirmationImage.BringToFront();
            
        }

        private void DrawfileLoaderButton() {
            Button fileLoaderBtn = new Button();
            fileLoaderBtn.Location = new Point(100, 250);
            fileLoaderBtn.Height = 40;
            fileLoaderBtn.Width = 400;
            fileLoaderBtn.FlatStyle = FlatStyle.Flat;
            fileLoaderBtn.FlatAppearance.BorderSize = 0;

            // Set background and foreground
            fileLoaderBtn.BackColor = Color.FromArgb(180, 70, 70);
            fileLoaderBtn.ForeColor = Color.White;

            fileLoaderBtn.Text = "Click to load a \".maze\" file";
            fileLoaderBtn.Name = "fileLoaderBtn";
            fileLoaderBtn.Font = new Font("Georgia", 16);

            // Add a Button Click Event handler
            fileLoaderBtn.Click += new EventHandler(FileLoaderBtn_Click);

            Controls.Add(fileLoaderBtn);
        }

        private void DrawStartButton() {
            startBtn = new Button();
            startBtn.Location = new Point(220, 310);
            startBtn.Height = 40;
            startBtn.Width = 280;
            startBtn.FlatStyle = FlatStyle.Flat;
            startBtn.FlatAppearance.BorderSize = 0;

            // Set background and foreground
            startBtn.BackColor = Color.FromArgb(180, 70, 70);
            startBtn.ForeColor = Color.White;

            startBtn.Text = "Start";
            startBtn.Name = "startBtn";
            startBtn.Font = new Font("Georgia", 16);

            // Add a Button Click Event handler
            startBtn.Click += new EventHandler(StartBtn_Click);

            Controls.Add(startBtn);
        }

        private void DrawExitButton() {
            Button exitBtn = new Button();
            exitBtn.Location = new Point(540, 15);
            exitBtn.Height = 40;
            exitBtn.Width = 40;
            exitBtn.FlatStyle = FlatStyle.Flat;
            exitBtn.FlatAppearance.BorderSize = 0;

            // Set background and foreground
            exitBtn.BackColor = Color.FromArgb(180, 70, 70);
            exitBtn.ForeColor = Color.White;

            exitBtn.Text = "X";
            exitBtn.Name = "exitBtn";
            exitBtn.Font = new Font("Georgia", 16);

            // Add a Button Click Event handler
            exitBtn.Click += new EventHandler(ExitBtn_Click);

            Controls.Add(exitBtn);
        }

        private void ExitBtn_Click(object sender, EventArgs e) {
            Close();
        }

        private void FileLoaderBtn_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = pathOfExecutable;
            openFileDialog.Filter = "maze files (*.maze)|*.maze";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                filePath = openFileDialog.FileName;
                maze = new Maze();
                try {
                    maze.readMap(filePath);
                    ChangeConfirmationImage(true);
                    startBtn.Enabled = true;
                }
                catch (MazeReadException) {
                    // TODO really throw the exception in the maze class and catch it here. (Show cross)
                    ChangeConfirmationImage(false);
                    startBtn.Enabled = false;
                }                
            }
            confirmationImage.Show();
        }
        void Form_Closed(object sender, FormClosedEventArgs e) {
            this.Show();
        }
        
        private void ThreadProc() {
            Application.Run(new MazeGame.MazeGame(this, maze, autoPlayerCbox.Checked));
        }

        void StartBtn_Click(object sender, EventArgs e) {
            Hide();
            Thread t = new Thread(ThreadProc);
            t.Start();
        }

    }
}
