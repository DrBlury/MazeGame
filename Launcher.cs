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
    public partial class MazeRunner : Form
    {
        private String filePath;
        [STAThread]
        static void Main() {
            Application.Run(new MazeRunner());
        }

        public MazeRunner() {
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
                //Get the path of specified file
                tickImage.Visible = true;
                filePath = openFileDialog.FileName;
                button2.Enabled = true;
            }
        }
        void button2_Click(object sender, EventArgs e) {
            Hide();
            Form gameForm = new MazeGame.MazeGame(this, filePath);
            try {
                gameForm.ShowDialog();
            } catch {

            }
        }
    }
}
