using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MazeGame
{
    class MazeRunner
    {
        private MazeGame mazegame;
        private Maze maze;

        public MazeRunner (MazeGame mazegame)
        {
            this.mazegame = mazegame;
            this.maze = mazegame.maze;
        }

        public void search()
        {
            Queue queue = new Queue();
            Point coord = new Point(maze.playerposition.Y, maze.playerposition.X);
            queue.Enqueue(coord);
            Hashtable hashtable = new Hashtable();


            while (queue.Count > 0)
            {
                Point coords = (Point)queue.Dequeue();

                if (maze.map[coords.X, coords.Y] == 0)
                {
                    // Found an Item
                    Stack stack = new Stack();
                    stack.Push(coords);
                    Point from = (Point)hashtable[coords];
                    stack.Push(from);
                    mazegame.movePlayer((Point)stack.Pop());
                    
                } else
                {
                    Point[] neighbors = new Point[4];
                    // First neighbor / Below
                    neighbors[0] = new Point(coords.X, coords.Y + 1);

                    // Second neighbor / Above
                    neighbors[1] = new Point(coords.X, coords.Y - 1);

                    // third neighbor / Right
                    neighbors[2] = new Point(coords.X + 1, coords.Y);

                    // Second neighbor / Left
                    neighbors[3] = new Point(coords.X - 1, coords.Y);

                    foreach (Point neighbor in neighbors)
                    {
                        if (maze.map[neighbor.X, neighbor.Y] != 1)
                        {
                            queue.Enqueue(neighbor);
                            hashtable.Add(neighbor, new int[] {maze.playerposition.Y, maze.playerposition.X});
                        }
                    }
                }
            }
        }
    }
}
