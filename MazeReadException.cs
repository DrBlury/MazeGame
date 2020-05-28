using System;
using System.Collections.Generic;
using System.Text;

namespace mazegame
{
    class MazeReadException : Exception
    {
        public MazeReadException() {
        }

        public MazeReadException(string message)
            : base(message) {
        }

        public MazeReadException(string message, Exception inner)
            : base(message, inner) {
        }
    }
}
