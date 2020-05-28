using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Timers;
using System.Collections;
using Timer = System.Timers.Timer;

namespace MazeGame
{
    class MazeGame : Form
    {
        public Maze maze;
        public bool canWalk;
        public Timer aTimer;

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

        Icon icon;

        MazeRunner runner;
        String pathOfExecutable = System.Environment.CurrentDirectory + "/";

        public MazeGame(Maze maze) {
            this.maze = maze;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            grass = (Bitmap)Image.FromFile(pathOfExecutable + "/Ressources/Images/grassTexture.bmp", true);
            wall = (Bitmap)Image.FromFile(pathOfExecutable + "/Ressources/Images/stoneTexture.bmp", true);

            grassbrush = new TextureBrush(this.grass, System.Drawing.Drawing2D.WrapMode.Tile);
            wallbrush = new TextureBrush(this.wall, System.Drawing.Drawing2D.WrapMode.Tile);

            icon = new Icon(pathOfExecutable + "/images/icon.ico");
            Icon = icon;
            Width = 600;
            Height = 600;
            Text = "MazeGame - Press SPACE to start automatic mode...";
            runner = new MazeRunner(this);
            SetTimer();
        }

        private void SetTimer() {
            aTimer = new Timer();
            aTimer.Interval = 100;
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = false;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e) {
            this.canWalk = true;
            aTimer.Enabled = false;
            aTimer.Stop();
        }

        override
        protected void OnResize(EventArgs e) {
            updateMaze = true;
            Refresh();
        }

        override
        protected void OnPaint(PaintEventArgs e) {
            if (updateMaze) {
                bounds = e.Graphics.VisibleClipBounds;
                tileWidth = bounds.Width / maze.width;
                tileHeight = bounds.Height / maze.height;
                playerSize = new SizeF(
                                        tileWidth * playerSizeMultiplicator,
                                        tileHeight * playerSizeMultiplicator
                                      );

                Point tilePoint = new Point();

                for (int j = 0; j < maze.height; j++) {
                    tilePoint.Y = j;
                    for (int i = 0; i < maze.width; i++) {
                        tilePoint.X = i;
                        drawTile(e, tilePoint);
                    }
                }
                updateMaze = false;
            }
            else {
                drawInvalidatedTiles(e, maze.invalidatedTiles);
            }
        }

        private void drawInvalidatedTiles(PaintEventArgs e, List<Point> invalidatedTiles) {
            while (invalidatedTiles.Count > 0) {
                drawTile(e, invalidatedTiles[0]);
                invalidatedTiles.RemoveAt(0);
            }
        }

        private void drawTile(PaintEventArgs e, Point point) {
            RectangleF rect = new RectangleF(
                new PointF(point.X * tileWidth, point.Y * tileHeight),
                new SizeF(tileWidth, tileHeight));

            switch (maze.map[point.X, point.Y]) {
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

        private void drawPlayerTile(PaintEventArgs e, Point point, RectangleF rect) {
            // Now draw player on top
            RectangleF player = new RectangleF(
                new PointF(
                    (point.X * tileWidth) + tileWidth * ((1f - playerSizeMultiplicator) / 2),
                    (point.Y * tileHeight) + tileHeight * ((1f - playerSizeMultiplicator) / 2)),
                    playerSize);

            e.Graphics.FillRectangle(Brushes.Red, player);
        }

        private void drawItemTile(PaintEventArgs e, Point point, RectangleF rect) {
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
        protected void OnKeyDown(KeyEventArgs e) {
            switch (e.KeyCode) {
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
                case Keys.Space:
                    runner.search();
                    break;
            }

        }

        public void walkPath(Stack waypoints) {
            while (waypoints.Count > 0) {
                if (this.canWalk) {
                    movePlayer((Point)waypoints.Pop());
                }
            }
        }

        public void movePlayer(Point futurePosition) {
            // Check if the player is trying to go inside a wall
            if (maze.map[futurePosition.X, futurePosition.Y] != 1 && this.canWalk) {
                this.canWalk = false;
                aTimer.Start();
                // Move the player to the future position and replace the tile
                // that the player stood on with a grass tile. (3)
                maze.map[maze.playerposition.X, maze.playerposition.Y] = 3;
                invalidatePlayerTile();

                // Move the playerposition to a different position
                maze.playerposition = futurePosition;

                // Set the new tile to be the player tile
                maze.map[maze.playerposition.X, maze.playerposition.Y] = 2;
                invalidatePlayerTile();
            }
            Update();
        }

        private void invalidatePlayerTile() {
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
