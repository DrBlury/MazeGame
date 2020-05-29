using System;
using System.Windows.Forms;

using System.Drawing;
using System.Collections.Generic;
using System.Timers;
using System.Collections;
using Timer = System.Timers.Timer;

namespace MazeAI
{
    class MazeGame : Form
    {
        public Maze maze;
        public bool canWalk = true;
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

        MazeRunner runner;
        String pathOfExecutable = AppDomain.CurrentDomain.BaseDirectory;

        Form mainForm;

        public MazeGame(Form mainForm, Maze maze) {
            this.mainForm = mainForm;
            this.maze = maze;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            //THIS IS NOT COMPATIBLE WITH MONO. It will throw an exception.Only for windows use. (Makes this whole application look a lot nicer)
            this.wall = new Bitmap(pathOfExecutable + "Resources/Images/stoneTexture.bmp");
            this.grass = new Bitmap(pathOfExecutable + "Resources/Images/grassTexture.bmp");

            //TextureBrush brush = new TextureBrush();

            grassbrush = new TextureBrush(this.grass, System.Drawing.Drawing2D.WrapMode.Tile);
            wallbrush = new TextureBrush(this.wall, System.Drawing.Drawing2D.WrapMode.Tile);

            Width = 600;
            Height = 600;
            Text = "MazeRunner! - have fun. PRESS SPACE TO AUTO WALK!";
            SetTimer();

            runner = new MazeRunner(this);

            Refresh();
            Update();
        }

        private void SetTimer() {
            aTimer = new Timer();
            aTimer.Interval = 100;
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e) {
            this.canWalk = true;
            aTimer.Enabled = false;
            aTimer.Stop();
        }

        private void MazeGame_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            mainForm.Show();
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
                Refresh();
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
            maze.itemsLeft--;
        }

        public void movePlayer(Point futurePosition) {
            // Check if the player is trying to go inside a wall
            if (maze.map[futurePosition.X, futurePosition.Y] != 1 && this.canWalk) {
                this.canWalk = false;
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
            aTimer.Start();
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
