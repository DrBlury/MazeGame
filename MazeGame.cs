using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace MazeGame
{
    class MazeGame : Form
    {
        Maze maze;
        float tileWidth;
        float tileHeight;
        RectangleF bounds;
        float playerSizeMultiplicator = 0.6f;
        SizeF playerSize;

        bool updateMaze = true;

        Bitmap grass;
        Brush grassbrush;

        Bitmap wall;
        Brush wallbrush;

        public MazeGame()
        {

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            grass = (Bitmap)Image.FromFile(@"C:\Users\DrBlury\Desktop\test\grassTexture.bmp", true);
            wall = (Bitmap)Image.FromFile(@"C:\Users\DrBlury\Desktop\test\woodTexture.bmp", true);

            grassbrush = new TextureBrush(this.grass, System.Drawing.Drawing2D.WrapMode.Tile);
            wallbrush = new TextureBrush(this.wall, System.Drawing.Drawing2D.WrapMode.Tile);

            Width = 600;
            Height = 600;
            Text = "MazeGame - Press SPACE to start automatic mode...";
            maze = new Maze();
            maze.readMap("test.maze");
            
        }
        static void Main()
        {
            Application.Run(new MazeGame());
        }

        override
        protected void OnResize(EventArgs e)
        {
            updateMaze = true;
            Refresh();
        }

        override
        protected void OnPaint(PaintEventArgs e)
        {
            if(updateMaze)
            {
                bounds = e.Graphics.VisibleClipBounds;
                tileWidth = bounds.Width / maze.width;
                tileHeight = bounds.Height / maze.height;
                playerSize = new SizeF(
                                        tileWidth * playerSizeMultiplicator, 
                                        tileHeight * playerSizeMultiplicator
                                      );

                Point tilePoint = new Point();

                for (int j = 0; j < maze.height; j++)
                {
                    tilePoint.Y = j;
                    for (int i = 0; i < maze.width; i++)
                    {
                        tilePoint.X = i;
                        drawTile(e, tilePoint);
                    }
                }
                updateMaze = false;
            }else
            {
                drawInvalidatedTiles(e, maze.invalidatedTiles);
            }
        }

        private void drawInvalidatedTiles(PaintEventArgs e, List<Point> invalidatedTiles)
        {
            while (invalidatedTiles.Count > 0)
            {
                drawTile(e, invalidatedTiles[0]);
                invalidatedTiles.RemoveAt(0);
            }  
        }

        private void drawTile(PaintEventArgs e, Point point)
        {
            RectangleF rect = new RectangleF(
                new PointF(point.X * tileWidth, point.Y * tileHeight),
                new SizeF(tileWidth, tileHeight));

            switch(maze.map[point.Y, point.X])
            {
                case 2:
                    e.Graphics.FillRectangle(grassbrush, rect);
                    drawPlayerTile(e, point, rect);
                    break;
                case 3:
                    e.Graphics.FillRectangle(grassbrush, rect);
                    break;
                case 0:
                    drawItemTile(e, point, rect);
                    break;
                case 1:
                    e.Graphics.FillRectangle(wallbrush, rect);
                    break;
            }
        }

        private void drawPlayerTile(PaintEventArgs e, Point point, RectangleF rect)
        {
            // Now draw player on top
            RectangleF player = new RectangleF(
                new PointF(
                    (point.X * tileWidth) + tileWidth * ((1f - playerSizeMultiplicator) / 2), 
                    (point.Y * tileHeight) + tileHeight * ((1f - playerSizeMultiplicator) / 2)), 
                    playerSize);
                
            e.Graphics.FillRectangle(Brushes.Red, player);
        }

        private void drawItemTile(PaintEventArgs e, Point point, RectangleF rect)
        {
            Brush brush = new TextureBrush(this.grass, System.Drawing.Drawing2D.WrapMode.Tile);
            e.Graphics.FillRectangle(brush, rect);

            //Now draw the item on top
            brush = Brushes.Yellow;
            RectangleF itemRect = new RectangleF(
                new PointF((point.X * tileWidth) + tileWidth * 0.3f, (point.Y * tileHeight) + tileHeight * 0.3f),
                new SizeF(tileWidth * 0.4f, tileHeight * 0.4f));
            e.Graphics.FillRectangle(Brushes.Yellow, itemRect);
        }

        override
        protected void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    movePlayer(new Point(maze.playerposition.X, maze.playerposition.Y - 1));
                    break;                
                case Keys.Down:
                    movePlayer(new Point(maze.playerposition.X, maze.playerposition.Y + 1));
                    break;
                case Keys.Left:
                    movePlayer(new Point(maze.playerposition.X - 1, maze.playerposition.Y));
                    break;
                case Keys.Right:
                    movePlayer(new Point(maze.playerposition.X + 1, maze.playerposition.Y));
                    break;
            }
            Update();
        }

        private void movePlayer(Point futurePosition)
        {
            // Check if the player is trying to go inside a wall
            if (maze.map[futurePosition.Y, futurePosition.X] != 1)
            {
                // Move the player to the future position and replace the tile
                // that the player stood on with a grass tile. (3)
                maze.map[maze.playerposition.Y, maze.playerposition.X] = 3;
                invalidatePlayerTile();

                // Move the playerposition to a different position
                maze.playerposition = futurePosition;

                // Set the new tile to be the player tile
                maze.map[maze.playerposition.Y, maze.playerposition.X] = 2;
                invalidatePlayerTile();
            }
        }

        private void invalidatePlayerTile()
        {
            maze.invalidatedTiles.Add(
                new Point(maze.playerposition.X, maze.playerposition.Y));

            Invalidate(new Region(
                new RectangleF(
                    maze.playerposition.X * tileWidth,
                    maze.playerposition.Y * tileHeight,
                    tileWidth,
                    tileHeight
                )
            ));
        }
    }
}
