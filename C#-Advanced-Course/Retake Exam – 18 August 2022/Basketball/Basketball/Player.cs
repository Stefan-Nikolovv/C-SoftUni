using System;

namespace Basketball
{
    public class Player
    {
        public string Name { get; private set; }

        public string Position { get; private set; }

        public double Rating { get; private set; }
        public int Games { get; private set; }
        public bool Retired { get; private set { false; } }
    }

}
