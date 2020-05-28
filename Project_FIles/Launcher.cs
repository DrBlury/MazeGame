using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MazeGame;

namespace mazegame
{
    public partial class Launcher : Form
    {
        Maze maze;
        private String filePath;
        [STAThread]
        static void Main() {
            Application.Run(new Launcher());
        }

        public Launcher() {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
        }

        private void button1_Click(object sender, EventArgs e) {
            String pathOfExecutable = System.Environment.CurrentDirectory + "/";

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
                    tickImage.Visible = true;
                    crossImage.Visible = false;
                    button2.Enabled = true;
                }
                catch (MazeReadException) {
                    // TODO really throw the exception in the maze class and catch it here. (Show cross)
                    crossImage.Visible = true;
                    tickImage.Visible = false;
                    button2.Enabled = false;
                }
                //Get the path of specified file
                
                
            }
        }
        void Form_Closed(object sender, FormClosedEventArgs e) {
            this.Show();
        }

        void button2_Click(object sender, EventArgs e) {
            Hide();
            Form gameForm = new MazeGame.MazeGame(maze);
            gameForm.FormClosed += new FormClosedEventHandler(Form_Closed);

            try {
                gameForm.ShowDialog();
            } catch {

            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e) {

        }

        private void MazeRunner_Load(object sender, EventArgs e) {

        }
    }
}
