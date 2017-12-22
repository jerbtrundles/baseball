using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baseball {
    class PlayerLifetimeStats {
        public List<PlayerSingleGameStats> GameStats = new List<PlayerSingleGameStats>();

        public int TotalAtBats { get => GameStats.Select(x => x.AtBats).Sum(); }
        public int TotalHits { get => GameStats.Select(x => x.Hits).Sum(); }
        public string LifetimeBattingAverage { get => ((double)TotalHits / TotalAtBats).ToString("F2"); }

        public int TotalDoubles { get => GameStats.Select(x => x.Hits).Sum(); }
        public int TotalTriples { get => GameStats.Select(x => x.Hits).Sum(); }
        public int TotalHomeruns { get => GameStats.Select(x => x.Hits).Sum(); }
        public int TotalWalks { get => GameStats.Select(x => x.Hits).Sum(); }

        public int TotalOuts { get => GameStats.Select(x => x.TotalOuts).Sum(); }
        public int TotalStrikeouts { get => GameStats.Select(x => x.Strikeouts).Sum(); }
        public int TotalPopOuts { get => GameStats.Select(x => x.PopOuts).Sum(); }
        public int TotalGroundOuts { get => GameStats.Select(x => x.GroundOuts).Sum(); }
        public int TotalLineOuts { get => GameStats.Select(x => x.LineOuts).Sum(); }
        public int TotalFlyOuts { get => GameStats.Select(x => x.FlyOuts).Sum(); }

        //////////////////////////

        public string AverageAtBats { get => GameStats.Select(x => x.AtBats).Average().ToString("F2"); }
        public string AverageHits { get => GameStats.Select(x => x.Hits).Average().ToString("F2"); }

        public string AverageDoubles { get => GameStats.Select(x => x.Hits).Average().ToString("F2"); }
        public string AverageTriples { get => GameStats.Select(x => x.Hits).Average().ToString("F2"); }
        public string AverageHomeruns { get => GameStats.Select(x => x.Hits).Average().ToString("F2"); }
        public string AverageWalks { get => GameStats.Select(x => x.Hits).Average().ToString("F2"); }

        public string AverageOuts { get => GameStats.Select(x => x.TotalOuts).Average().ToString("F2"); }
        public string AverageStrikeouts { get => GameStats.Select(x => x.Strikeouts).Average().ToString("F2"); }
        public string AveragePopOuts { get => GameStats.Select(x => x.PopOuts).Average().ToString("F2"); }
        public string AverageGroundOuts { get => GameStats.Select(x => x.GroundOuts).Average().ToString("F2"); }
        public string AverageLineOuts { get => GameStats.Select(x => x.LineOuts).Average().ToString("F2"); }
        public string AverageFlyOuts { get => GameStats.Select(x => x.FlyOuts).Average().ToString("F2"); }
    }
}
