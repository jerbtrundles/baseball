using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baseball {
    class TeamSingleGameStats {
        public Team Team { get; private set; }

        public TeamSingleGameStats(Team team) {
            Team = team;
        }
    }
}
