using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baseball {
    class Game {
        private Team[] Teams;

        private int _hittingTeam = 0;
        private int _pitchingTeam { get => (_hittingTeam + 1) % 2; }
        public Team HittingTeam { get => Teams[_hittingTeam]; }
        public Team PitchingTeam { get => Teams[_pitchingTeam]; }

        public bool SomeoneOnFirst { get; private set; }
        public bool SomeoneOnSecond { get; private set; }
        public bool SomeoneOnThird { get; private set; }

        public int Inning { get; private set; }
        public int Outs { get; private set; }

        public int Strikes { get; private set; }
        public int Balls { get; private set; }

        public TeamSingleGameStats Team1Stats { get; private set; }
        public TeamSingleGameStats Team2Stats { get; private set; }

        public Game(Team team1, Team team2) {
            Teams = new Team[] { team1, team2 };
            Inning = 0;
            Outs = 0;
            Strikes = 0;
            Balls = 0;

            Team1Stats = new TeamSingleGameStats(team1);
            Team2Stats = new TeamSingleGameStats(team2);
        }

        private void ChangeTeams() {
            _hittingTeam = _pitchingTeam;
        }
    }
}
