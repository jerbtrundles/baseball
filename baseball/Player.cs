using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baseball {
    class Player {
        public string Name { get; set; }
        public PlayerSingleGameStats CurrentGameStats { get; set; }
        public PlayerLifetimeStats LifetimeStats = new PlayerLifetimeStats();

        public void NewGame() {
            CurrentGameStats = new PlayerSingleGameStats();
        }
    }
}
