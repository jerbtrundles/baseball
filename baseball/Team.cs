using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baseball {
    class Team {
        public List<Player> Players = new List<Player>();

        private int _index = 0;
        public Player CurrentPlayer { get => Players[_index]; }

        public void NextPlayer() {
            _index = (_index + 1) % Players.Count;
        }
    }
}
