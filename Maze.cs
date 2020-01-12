using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MazeGame
{
    class Maze
    {
        public int[,] map;
        public int width;
        public int height;
        public int[] playerposition = new int[2];

        public void readMap(String filename)
        {
            String[] mapfile = File.ReadAllLines(@"C:\Users\DrBlury\Desktop\test\" + filename);
            
            int.TryParse(mapfile[0], out width);
            int.TryParse(mapfile[1], out height);

            map = new int[height, width];

            Console.WriteLine("Width of the maze: " + width);
            Console.WriteLine("Height of the maze: " + height);

            for (int i = 2; i < height + 2; i++)
            {
                Char[] maprow = mapfile[i].ToCharArray();
                for (int c = 0; c < width; c++)
                {
                    switch (maprow[c])
                    {
                        case '#':
                            map[i-2, c] = 1;
                            break;
                        case '.':
                            map[i-2, c] = 0;
                            break;
                        case '@':
                            playerposition = new int[]{ i - 2, c};
                            map[i-2, c] = 2;
                            break;
                    }
                }
            }
            
        }
    }
}
