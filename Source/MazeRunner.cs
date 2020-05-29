using System.Collections;
using System.Drawing;

namespace MazeAI
{
    class MazeRunner
    {

        private MazeGame mazegame;

        public MazeRunner (MazeGame mazegame)
        {
            this.mazegame = mazegame;
        }

        public void search()
        {
            // Phase 1 - 1
            Queue queue = new Queue();
            queue.Enqueue(mazegame.maze.playerposition);

            Hashtable hashtable = new Hashtable();
            Stack stack = new Stack();
            // Phase 1 - 2
            while (queue.Count > 0) {
                // Phase 1 - 2.1
                Point coords = (Point)queue.Dequeue();

                // Phase 1 - 2.2
                if (mazegame.maze.map[coords.X, coords.Y] == 0) {
                    // Phase 2
                    // Found an Item
                    stack.Push(coords);
                    Point from = (Point)hashtable[coords];

                    while (from != mazegame.maze.playerposition) {
                        stack.Push(from);
                        from = (Point)hashtable[from];
                    }
                    queue.Clear();
                }
                else {
                    Point[] neighbors = new Point[4];
                    // First neighbor / Below
                    neighbors[0] = new Point(coords.X, coords.Y + 1);

                    // Second neighbor / Above
                    neighbors[1] = new Point(coords.X, coords.Y - 1);

                    // third neighbor / Right
                    neighbors[2] = new Point(coords.X + 1, coords.Y);

                    // Second neighbor / Left
                    neighbors[3] = new Point(coords.X - 1, coords.Y);

                    foreach (Point neighbor in neighbors) {
                        bool isContained = hashtable.ContainsKey(neighbor);
                        bool isWall = mazegame.maze.map[neighbor.X, neighbor.Y] == 1;
                        if (!isWall && !isContained) {
                            queue.Enqueue(neighbor);
                            hashtable.Add(neighbor, coords);
                        }
                    }
                }
            }
            mazegame.walkPath(stack);
        }
    }

}
