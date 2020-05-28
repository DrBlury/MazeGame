using System;
using System.Drawing;
using System.Windows.Forms;
using MazeGame;

namespace mazegame
{
    public class Launcher : Form
    {
        String pathOfExecutable = System.Environment.CurrentDirectory + "/";
        PictureBox confirmationImage;
        Button startBtn;
        Maze maze;
        private String filePath;

        [STAThread]
        static void Main() {
            Application.Run(new Launcher());
        }

        public Launcher() {
            Icon = new Icon(pathOfExecutable + "/Ressources/Images/icon.ico");
            Width = 600;
            Height = 400;
            BackColor = Color.FromArgb(30, 30, 30);
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Text = String.Empty;

            DrawExitButton();
            DrawLogo();
            DrawStartButton();
            DrawfileLoaderButton();
            DrawConfirmationImage();
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

        private void DrawConfirmationImage() {
            confirmationImage = new PictureBox {
                Name = "confirmationImage",
                Size = new Size(100, 100),
                Location = new Point(400, 250),
                Image = Image.FromFile(pathOfExecutable + "/Ressources/Images/tick.png"),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
            };
            Controls.Add(confirmationImage);
            confirmationImage.Hide();
        }

        private void ChangeConfirmationImage(bool accepted) {
            switch (accepted) {
                case true:
                    confirmationImage.Image = Image.FromFile(pathOfExecutable + "/Ressources/Images/tick.png");
                    break;
                case false:
                    confirmationImage.Image = Image.FromFile(pathOfExecutable + "/Ressources/Images/cross.png");
                    break;
            }
            
        }

        private void DrawfileLoaderButton() {
            Button fileLoaderBtn = new Button();
            fileLoaderBtn.Location = new Point(100, 250);
            fileLoaderBtn.Height = 40;
            fileLoaderBtn.Width = 300;
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
            startBtn.Location = new Point(100, 310);
            startBtn.Height = 40;
            startBtn.Width = 300;
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
            exitBtn.Location = new Point(520, 20);
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

        void StartBtn_Click(object sender, EventArgs e) {
            Hide();
            Form gameForm = new MazeGame.MazeGame(maze);
            gameForm.FormClosed += new FormClosedEventHandler(Form_Closed);

            try {
                gameForm.ShowDialog();
            } catch {

            }
        }
    }
}
