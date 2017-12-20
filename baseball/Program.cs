using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baseball {
    class Program {
        static int nFirst = 0;
        static int nSecond = 0;
        static int nThird = 0;

        static int nTeam = 0;
        static int nInning = 0;
        static int nStrikes = 0;
        static int nBalls = 0;
        static int nOuts = 0;

        static List<string> GameLog = new List<string>();

        static string strClearString = new string(' ', 50);
        static string strLastAction = string.Empty;

        static int[] nTeam1Scores = new int[9];
        static int[] nTeam2Scores = new int[9];

        static int nTeam1Score { get => nTeam1Scores.Sum(); }
        static int nTeam2Score { get => nTeam2Scores.Sum(); }

        static int nTeam1Hits = 0;
        static int nTeam2Hits = 0;

        static Random r = new Random(DateTime.Now.Millisecond);

        static void Main(string[] args) {
            Console.CursorVisible = false;

            while (true) {
                while (!GameFinished()) {
                    ClearScreen();
                    DrawLog();
                    RandomAction();
                    GameStatus();
                    DrawField();
                    DrawScoreboard();

                    System.Threading.Thread.Sleep(100);

                    // debugging
                    if (Console.KeyAvailable) {
                        Console.ReadKey();
                        Console.ReadKey();
                    }
                }

                EndOfGame();
                NewGame();
            }
        }

        private static void GameStatus() {
            Console.SetCursorPosition(0, 0);

            if (strLastAction != string.Empty) {
                Console.WriteLine("Previous play: " + strLastAction);
            }

            Console.WriteLine("Team 1: " + nTeam1Score.ToString());
            Console.WriteLine("Team 2: " + nTeam2Score.ToString());

            if (nInning == 9) {
                Console.WriteLine("Inning: bottom 9");
            }
            else {
                Console.WriteLine("Inning: " + ((nTeam == 0) ? "top " : "bottom ") + (nInning + 1).ToString());
            }

            Console.WriteLine("Batting: Team " + (nTeam + 1).ToString());
            Console.WriteLine("Count: " + nBalls.ToString() + "-" + nStrikes.ToString());
            Console.WriteLine("Outs: " + nOuts.ToString());

            Console.WriteLine();
        }

        #region Log
        private static void DrawLog() {
            for (int i = 0; i < GameLog.Count; i++) {
                Console.SetCursorPosition(60, i);
                Console.WriteLine(new string(' ', 20));
                Console.SetCursorPosition(60, i);
                Console.WriteLine(GameLog[i]);
            }

            Console.WriteLine();
        }
        private static void Log(string s) {
            if (GameLog.Count >= 10) {
                GameLog.RemoveAt(0);
            }

            GameLog.Add(s);
        }
        #endregion

        #region Miscellaneous
        private static void NewGame() {
            Console.Clear();

            nInning = 0;
            nTeam = 0;
            nBalls = 0;
            nStrikes = 0;
            nOuts = 0;

            nTeam1Scores = new int[9];
            nTeam2Scores = new int[9];

            nTeam1Hits = 0;
            nTeam2Hits = 0;
        }
        private static void EndOfGame() {
            Console.Clear();
            Console.WriteLine("End of game.");
            Console.WriteLine();
            GameStatus();

            Console.WriteLine();
            DrawScoreboard();

            Console.WriteLine();
            Console.WriteLine("Starting new game in 5 seconds.");
            System.Threading.Thread.Sleep(5000);
        }
        private static void ClearScreen() {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < 25; i++) {
                Console.WriteLine(strClearString);
            }
            Console.SetCursorPosition(0, 0);
        }
        private static void ScoreRuns(int nRuns) {
            if (nTeam == 0) {
                nTeam1Scores[nInning] += nRuns;
                Console.WriteLine("Team 1 scores " + nRuns.ToString() + (nRuns > 1 ? " runs." : " run."));
            }
            else {
                nTeam2Scores[nInning] += nRuns;
                Console.WriteLine("Team 2 scores " + nRuns.ToString() + (nRuns > 1 ? " runs." : " run."));
            }
        }
        private static void ClearBases() {
            Log("Clearing bases.");

            nFirst = 0;
            nSecond = 0;
            nThird = 0;
        }
        private static void NextBatter() {
            Log("Next batter.");

            nBalls = 0;
            nStrikes = 0;
        }
        private static void NextTeam() {
            Log("Changing teams.");

            nFirst = 0;
            nSecond = 0;
            nThird = 0;
            nStrikes = 0;
            nBalls = 0;
            nOuts = 0;

            nTeam = (nTeam + 1) % 2;
            if (nTeam == 0) {
                nInning++;
            }
        }
        private static bool GameFinished() {
            if (nInning >= 8 && nTeam == 1 && nTeam2Score > nTeam1Score) {
                return true;
            }
            else if (nInning >= 9) {
                return true;
            }
            else {
                return false;
            }
        }
        #endregion

        #region Process Plays
        private static void RandomAction() {
            int nRandom = r.Next(100);
            if (nRandom > 99) {
                // homerun
                ProcessHomerun();
            }
            else if (nRandom > 97) {
                // triple
                ProcessTriple();
            }
            else if (nRandom > 95) {
                // double
                ProcessDouble();
            }
            else if (nRandom > 90) {
                // single
                ProcessSingle();
            }
            else if (nRandom > 80) {
                // random out
                ProcessRandomOut();
            }
            else if (nRandom > 40) {
                // strike
                ProcessStrike();
            }
            else {
                // ball
                ProcessBall();
            }
        }
        private static void ProcessHomerun() {
            strLastAction = "Homerun!";

            if (nTeam == 0) {
                nTeam1Hits++;
            }
            else {
                nTeam2Hits++;
            }

            int nRuns = 1;

            if (nFirst != 0) { nRuns++; }
            if (nSecond != 0) { nRuns++; }
            if (nThird != 0) { nRuns++; }

            ScoreRuns(nRuns);
            ClearBases();
            NextBatter();
        }
        private static void ProcessTriple() {
            strLastAction = "Triple!";

            if (nTeam == 0) {
                nTeam1Hits++;
            }
            else {
                nTeam2Hits++;
            }

            int nRuns = 0;

            if (nThird != 0) {
                nRuns++;
                nThird = 0;
            }

            if (nSecond != 0) {
                nRuns++;
                nSecond = 0;
            }

            if (nFirst != 0) {
                nRuns++;
                nFirst = 0;
            }

            if (nRuns > 0) {
                ScoreRuns(nRuns);
            }

            nThird = 1;

            NextBatter();
        }
        private static void ProcessDouble() {
            strLastAction = "Double!";

            if (nTeam == 0) {
                nTeam1Hits++;
            }
            else {
                nTeam2Hits++;
            }

            int nRuns = 0;

            if (nThird != 0) {
                nRuns++;
                nThird = 0;
            }

            if (nSecond != 0) {
                nRuns++;
                nSecond = 0;
            }

            if (nFirst != 0) {
                nThird = 1;
                nFirst = 0;
            }

            if (nRuns > 0) {
                ScoreRuns(nRuns);
            }

            nSecond = 1;

            NextBatter();
        }
        private static void ProcessSingle() {
            strLastAction = "Single!";

            if (nTeam == 0) {
                nTeam1Hits++;
            }
            else {
                nTeam2Hits++;
            }

            int nRuns = 0;

            if (nThird != 0) {
                nRuns++;
                nThird = 0;
            }

            if (nSecond != 0) {
                nThird = 1;
                nSecond = 0;
            }

            if (nFirst != 0) {
                nSecond = 1;
                nFirst = 0;
            }

            if (nRuns > 0) {
                ScoreRuns(nRuns);
            }

            nFirst = 1;

            NextBatter();
        }
        private static void ProcessRandomOut() {
            switch (r.Next(3)) {
                case 0:
                    strLastAction = "Pop out!";
                    break;
                case 1:
                    strLastAction = "Fly out!";
                    break;
                case 2:
                    strLastAction = "Line out!";
                    break;
            }

            nOuts++;
            if (nOuts == 3) {
                NextTeam();
            }
            else {
                NextBatter();
            }
        }
        private static void ProcessStrikeout() {
            strLastAction = "Strikeout!";

            nOuts++;
            if (nOuts == 3) {
                NextTeam();
            }
            else {
                NextBatter();
            }
        }
        private static void ProcessStrike() {
            nStrikes++;

            Log("Strike " + nStrikes.ToString() + "!");

            if (nStrikes == 3) {
                ProcessStrikeout();
            }
        }
        private static void ProcessBall() {
            nBalls++;

            Log("Ball " + nBalls.ToString() + ".");

            if (nBalls == 4) {
                ProcessWalk();
            }
        }
        private static void ProcessWalk() {
            strLastAction = "Walk!";

            if (nFirst != 0) {
                if (nSecond != 0) {
                    if (nThird != 0) {
                        ScoreRuns(1);
                    }

                    nThird = 1;
                }

                nSecond = 1;
            }

            nFirst = 1;
            NextBatter();
        }
        #endregion

        #region Draw Field
        private static void DrawField() {
            if (nSecond > 0) {
                Console.WriteLine("..x..");
            }
            else {
                Console.WriteLine("..o..");
            }

            if (nThird > 0 && nFirst > 0) {
                Console.WriteLine("x...x");
            }
            else if (nThird > 0) {
                Console.WriteLine("x...o");
            }
            else if (nFirst > 0) {
                Console.WriteLine("o...x");
            }
            else {
                Console.WriteLine("o...o");
            }

            Console.WriteLine("..o..");
            Console.WriteLine();
        }
        #endregion

        #region Draw Scoreboard
        private static void DrawScoreboard() {
            // |--------------------------------|
            // |        |1|2|3|4|5|6|7|8|9|R|H|E|
            // |--------------------------------|
            // | Team 1 |0|0|0|0|0|0|0|0|0|0|0|0|
            // | Team 2 |0|0|0|0|0|0|0|0|0|0|0|0|
            // |--------------------------------|

            // new string('-', Team1ScoresString().Length() - )

            Console.SetCursorPosition(0, 20);
            Console.WriteLine(ScoreboardLineString());
            ScoreboardInningsString();
            Console.WriteLine(ScoreboardLineString());
            Team1ScoresString();
            Team2ScoresString();
            Console.WriteLine(ScoreboardLineString());
        }
        private static void ScoreboardInningsString() {
            Console.Write("|        |");

            for (int i = 0; i < 9; i++) {
                Console.Write(' ');
                if (nTeam1Scores[i] >= 10 || nTeam2Scores[i] >= 10) {
                    Console.Write(' ');
                }

                if (nInning == i) {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write((i + 1).ToString());
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" |");
            }

            if (nTeam1Score >= 10 || nTeam2Score >= 10) {
                Console.Write(' ');
            }

            Console.Write(" R |");

            if (nTeam1Hits >= 10 || nTeam2Hits >= 10) {
                Console.Write(' ');
            }

            Console.WriteLine(" H | E |");
        }
        private static string ScoreboardLineString() {
            return "|" + new string('-', LineLength() - 2) + "|";
        }
        private static int LineLength() {
            StringBuilder sb = new StringBuilder();

            sb.Append("| Team 2 |");

            for (int i = 0; i < 9; i++) {
                sb.Append(' ');
                if (nTeam2Scores[i] < 10 && nTeam1Scores[i] >= 10) {
                    sb.Append(' ');
                }

                sb.Append(nTeam2Scores[i] + " |");
            }

            sb.Append(' ');

            if (nTeam2Score < 10 && nTeam1Score >= 10) {
                sb.Append(' ');
            }

            sb.Append(nTeam2Score + " | ");

            if (nTeam1Hits >= 10 && nTeam2Hits < 10) {
                sb.Append(' ');
            }

            sb.Append(nTeam2Hits + " | 0 |");

            return sb.ToString().Length;
        }
        private static void Team1ScoresString() {
            Console.Write("| Team 1 |");

            for (int i = 0; i < 9; i++) {
                // initial buffer space
                Console.Write(' ');
                // extra buffer space if the other team scored double-digit runs this inning (but we haven't)
                if (nTeam1Scores[i] < 10 && nTeam2Scores[i] >= 10) {
                    Console.Write(' ');
                }

                // special coloring if current inning
                if (nInning == i) {
                    if (nTeam == 0) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (nTeam1Scores[i] > 0) {
                            // red number followed by ! if runs scored
                            Console.Write(nTeam1Scores[i] + '!');
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write('|');
                        }
                        else {
                            // red X if current inning and no runs scored
                            Console.Write('X');
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(" |");
                        }
                    }
                    else {
                        // green number if current inning but other team is batting
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(nTeam1Scores[i]);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(" |");
                    }
                }
                else {
                    // gray X for future innings
                    // gray number for past innings
                    Console.Write(((nTeam1Scores[i] > 0) || nInning > i || (nInning == i && nTeam == 1) ? nTeam1Scores[i].ToString() : "X") + " |");
                }
            }

            // initial buffer space for total runs
            Console.Write(' ');
            if (nTeam1Score < 10 && nTeam2Score >= 10) {
                // extra buffer space if other team has a double-digit total score (but we don't)
                Console.Write(' ');
            }

            Console.Write(nTeam1Score + " | ");
            if (nTeam1Hits < 10 && nTeam2Hits >= 10) {
                // buffer space for total hits if other team has double-digits (as with runs)
                Console.Write(' ');
            }

            // static text for errors for now
            Console.WriteLine(nTeam1Hits + " | 0 |");
        }
        private static void Team2ScoresString() {
            Console.Write("| Team 2 |");

            for (int i = 0; i < 9; i++) {
                Console.Write(' ');
                if (nTeam2Scores[i] < 10 && nTeam1Scores[i] >= 10) {
                    Console.Write(' ');
                }

                if (nInning == i) {
                    if (nTeam == 1) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (nTeam2Scores[i] > 0) {
                            Console.Write(nTeam2Scores[i]);
                            Console.Write('!');
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write('|');
                        }
                        else {
                            Console.Write('X');
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(" |");
                        }
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(nTeam2Scores[i]);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(" |");
                    }
                }
                else {
                    Console.Write(((nTeam2Scores[i] > 0) || nInning > i ? nTeam2Scores[i].ToString() : "X") + " |");
                }
            }

            Console.Write(' ');

            if (nTeam2Score < 10 && nTeam1Score >= 10) {
                Console.Write(' ');
            }

            Console.Write(nTeam2Score + " | ");

            if (nTeam1Hits >= 10 && nTeam2Hits < 10) {
                Console.Write(' ');
            }

            Console.WriteLine(nTeam2Hits + " | 0 |");
        }
        #endregion
    }
}
