using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using mazegame;

namespace MazeGame
{
    class Maze
    {
        public int[,] map;
        public int width;
        public int height;
        public List<Point> invalidatedTiles = new List<Point>();
        public Point playerposition = new Point();

        public void readMap(String filePath)
        {
            String[] mapfile = File.ReadAllLines(filePath);


            if (!int.TryParse(mapfile[0], out width) ||
                !int.TryParse(mapfile[1], out height)) 
            {
                string message = "Maze file invalid. Please check for proper formatting.";
                string caption = "Error Detected in file";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.OK) {
                    // Closes the parent form
                    throw new MazeReadException("Maze Invalid.");
                }
            };
            
            map = new int[width, height];

            Console.WriteLine("Width of the maze: " + width);
            Console.WriteLine("Height of the maze: " + height);

            // For each line (i is the row | Y AXIS)
            for (int i = 2; i < height + 2; i++)
            {
                // Read the map row
                Char[] maprow = mapfile[i].ToCharArray();
                // Read each character of that row
                // (c is the column | X Axis)
                for (int c = 0; c < width; c++)
                {
                    switch (maprow[c])
                    {
                        case '#':
                            map[c, i - 2] = 1;
                            break;
                        case '.':
                            map[c, i - 2] = 0;
                            break;
                        case '@':
                            playerposition = new Point(c, i-2);
                            map[c, i - 2] = 2;
                            break;
                    }
                }
            }
            
        }
    }
}
