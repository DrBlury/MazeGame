using System;
using System.Windows.Forms;
using System.Drawing;

namespace MazeGame
{
    class MazeGame : Form
    {
        Maze maze;
        public MazeGame()
        {
            Width = 600;
            Height = 600;
            Text = "MazeGame";
            maze = new Maze();
            maze.readMap("test.maze");
        }
        static void Main()
        {
            
            Application.Run(new MazeGame());
        }

        override
        protected void OnPaint(PaintEventArgs e)
        {
            RectangleF bounds = e.Graphics.VisibleClipBounds;
            

            float tileWidth = bounds.Width / maze.width;
            float tileHeight = bounds.Height / maze.height;

            for (int j = 0; j < maze.height; j++)
            {
                for (int i = 0; i < maze.width; i++)
                {
                    Brush brush = Brushes.Black;

                    RectangleF rect = new RectangleF(
                        new PointF(i * tileWidth, j * tileHeight),
                        new SizeF(tileWidth, tileHeight));

                    switch (maze.map[j, i])
                    {
                        case 0:
                            brush = Brushes.Yellow;
                            break;
                        case 1:
                            brush = Brushes.SandyBrown;
                            break;
                        case 2:
                            brush = Brushes.Red;
                            break;
                        case 3:
                            brush = Brushes.LightGreen;
                            break;
                    }
                    e.Graphics.FillRectangle(brush, rect);
                }
            }
        }
        
        override
        protected void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    movePlayer(new int[2] { maze.playerposition[0] - 1, maze.playerposition[1] });
                    Refresh();
                    break;                
                case Keys.Down:
                    movePlayer(new int[2] { maze.playerposition[0] + 1, maze.playerposition[1] });
                    Refresh();
                    break;
                case Keys.Left:
                    movePlayer(new int[2] { maze.playerposition[0], maze.playerposition[1] - 1});
                    Refresh();
                    break;
                case Keys.Right:
                    movePlayer(new int[2] { maze.playerposition[0], maze.playerposition[1] + 1});
                    Refresh();
                    break;
            }
        }

        private void movePlayer(int[] futurePosition)
        {
            // Check if the player is trying to go inside a wall
            if (maze.map[futurePosition[0], futurePosition[1]] != 1)
            {
                // Move the player to the future position and replace the tile
                // that the player stood on with a grass tile. (3)
                maze.map[maze.playerposition[0], maze.playerposition[1]] = 3;
                // Move the playerposition to a different position
                maze.playerposition = futurePosition;
                // Set the new tile to be the player tile
                maze.map[maze.playerposition[0], maze.playerposition[1]] = 2;
            }
        }
    }
}
