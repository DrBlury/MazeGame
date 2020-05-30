using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MazeAI
{
    class Maze
    {
        public int[,] map;
        public int width;
        public int height;
        public int itemsLeft;
        public List<Point> invalidatedTiles = new List<Point>();
        public Point playerposition = new Point();

        public void readMap(String filePath)
        {
            String[] mapfile = File.ReadAllLines(filePath);
            itemsLeft = 0;

            if (!int.TryParse(mapfile[0], out width) ||
                !int.TryParse(mapfile[1], out height)) {
                invalidMazeError(1);
            };
            
            map = new int[width, height];
            // For each line (i is the row | Y AXIS)
            for (int row = 2; row < height + 2; row++)
            {
                // Read the map row
                Char[] maprow = mapfile[row].ToCharArray();
                // Read each character of that row
                // (c is the column | X Axis)
                for (int column = 0; column < width; column++)
                {
                    switch (maprow[column])
                    {
                        case '#':
                            map[column, row - 2] = 1;
                            break;
                        case '.':
                            map[column, row - 2] = 0;
                            itemsLeft++;
                            break;
                        case '@':
                            playerposition = new Point(column, row-2);
                            map[column, row - 2] = 2;
                            break;
                        default:
                            invalidMazeError(2);
                            break;
                    }
                }
            }
            
        }

        private static void invalidMazeError(int errorNumber = 0) {
            String message;
            switch (errorNumber) {
                case 1:
                    message = "Unable to read the maze dimensions. \nCheck readme for example format of the .maze file!";
                    break;
                case 2:
                    message = "Unknown symbols in the maze. \nCheck readme for example format of the .maze file!";
                    break;
                default:
                    message = "An error occured in your .maze file.";
                    break;
            }
            
            string caption = "Error Detected in .maze file";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, buttons);

        }
    }
}
