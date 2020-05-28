using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;

namespace MazeGame
{
    class Maze
    {
        public int[,] map;
        public int width;
        public int height;
        public List<Point> invalidatedTiles = new List<Point>();
        public Point playerposition = new Point();

        public void readMap(String filename)
        {
            String pathOfExecutable = System.Environment.CurrentDirectory + "/";
            Console.WriteLine(pathOfExecutable);
            String[] mapfile = File.ReadAllLines(pathOfExecutable + filename);
            
            int.TryParse(mapfile[0], out width);
            int.TryParse(mapfile[1], out height);

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
