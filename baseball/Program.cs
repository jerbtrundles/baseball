using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baseball {
    class Program {

        static int nTeam1Wins = 0;
        static int nTeam2Wins = 0;
        static int nTies = 0;

        static int nLogSize = 25;
        static List<string> GameLog = new List<string>(nLogSize);
        static int nLogIndex = 1;

        static string strClearString = new string(' ', 25);
        static string strLastAction = string.Empty;

        static int[] nTeam1Scores = new int[9];
        static int[] nTeam2Scores = new int[9];

        static int nInning = 0;
        static int nTeam = 0;
        static int nBalls = 0;
        static int nStrikes = 0;
        static int nOuts = 0;

        static int nFirst = 0;
        static int nSecond = 0;
        static int nThird = 0;

        static int nTeam1Score { get => nTeam1Scores.Sum(); }
        static int nTeam2Score { get => nTeam2Scores.Sum(); }

        static int nTeam1Hits = 0;
        static int nTeam2Hits = 0;

        static int nTotalSingles = 0;
        static int nTotalDoubles = 0;
        static int nTotalTriples = 0;
        static int nTotalHomeruns = 0;
        static int nTotalBalls = 0;
        static int nTotalWalks = 0;
        static int nTotalStrikeouts = 0;
        static int nTotalStrikes = 0;
        static int nTotalFlyOuts = 0;
        static int nTotalLineOuts = 0;
        static int nTotalPopOuts = 0;

        static int nTotalGames { get => nTeam1Wins + nTeam2Wins + nTies; }
        static double dAverageSingles = 0;
        static int nAverageSingles { get => (int)(Math.Round(dAverageSingles)); }
        static double dAverageDoubles = 0;
        static int nAverageDoubles { get => (int)(Math.Round(dAverageDoubles)); }
        static double dAverageTriples = 0;
        static int nAverageTriples { get => (int)(Math.Round(dAverageTriples)); }
        static double dAverageHomeruns = 0;
        static int nAverageHomeruns { get => (int)(Math.Round(dAverageHomeruns)); }
        static double dAverageBalls = 0;
        static int nAverageBalls { get => (int)(Math.Round(dAverageBalls)); }
        static double dAverageWalks = 0;
        static int nAverageWalks { get => (int)(Math.Round(dAverageWalks)); }
        static double dAverageStrikes = 0;
        static int nAverageStrikes { get => (int)(Math.Round(dAverageStrikes)); }
        static double dAverageStrikeouts = 0;
        static int nAverageStrikeouts { get => (int)(Math.Round(dAverageStrikeouts)); }
        static double dAverageFlyOuts = 0;
        static int nAverageFlyOuts { get => (int)(Math.Round(dAverageFlyOuts)); }
        static double dAverageLineOuts = 0;
        static int nAverageLineOuts { get => (int)(Math.Round(dAverageLineOuts)); }
        static double dAveragePopOuts = 0;
        static int nAveragePopOuts { get => (int)(Math.Round(dAveragePopOuts)); }

        static Random r = new Random(DateTime.Now.Millisecond);

        static void Main(string[] args) {
            Console.CursorVisible = false;
            NewGame();

            while (true) {
                while (!GameFinished()) {
                    MainLoop();
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

        private static void MainLoop(bool bDoAction = true) {
            DrawLog();
            DrawTotalStats(62, 3);
            if (bDoAction) { RandomAction(); }
            GameStatus();
            DrawField(60, Console.WindowHeight - 8);
            DrawScoreboard();
        }

        private static void GameStatus() {
            Console.SetCursorPosition(0, 0);

            if (strLastAction != string.Empty) {
                Console.WriteLine(strClearString);
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                Console.WriteLine("Previous play: " + strLastAction);
            }

            Console.WriteLine(strClearString);
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            Console.WriteLine("Team 1: " + nTeam1Score.ToString());

            Console.WriteLine(strClearString);
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            Console.WriteLine("Team 2: " + nTeam2Score.ToString());

            Console.WriteLine(strClearString);
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            if (nInning == 9) {
                Console.WriteLine("Inning: bottom 9");
            }
            else {
                Console.WriteLine("Inning: " + ((nTeam == 0) ? "top " : "bottom ") + (nInning + 1).ToString());
            }

            Console.WriteLine(strClearString);
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            Console.WriteLine("Batting: Team " + (nTeam + 1).ToString());
            Console.WriteLine(strClearString);
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            Console.WriteLine("Count: " + nBalls.ToString() + "-" + nStrikes.ToString());
            Console.WriteLine(strClearString);
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            Console.WriteLine("Outs: " + nOuts.ToString());

            Console.WriteLine();
        }

        private static void DrawHistory() {
            Console.SetCursorPosition(0, 8);
            Console.WriteLine("Team 1 record: " + nTeam1Wins.ToString() + "-" + nTeam2Wins.ToString() + (nTies > 0 ? "-" + nTies.ToString() : string.Empty));
            Console.WriteLine("Team 2 record: " + nTeam2Wins.ToString() + "-" + nTeam1Wins.ToString() + (nTies > 0 ? "-" + nTies.ToString() : string.Empty));
        }

        private static void DrawAveragesShell() {
            Console.SetCursorPosition(30, 0);
            Console.WriteLine("|-------------------------|");
            Console.SetCursorPosition(30, 1);
            Console.WriteLine("| Game Stats              |");
            Console.SetCursorPosition(30, 2);
            Console.WriteLine("|-------------------------|");
            for (int i = 0; i < 11; i++) {
                Console.SetCursorPosition(30, i + 3);
                Console.WriteLine("|                         |");
            }
            Console.SetCursorPosition(30, 14);
            Console.WriteLine("|-------------------------|");
        }

        private static void DrawAverages() {
            Console.SetCursorPosition(32, 3);
            Console.WriteLine("Average singles: " + nAverageSingles.ToString());
            Console.SetCursorPosition(32, 4);
            Console.WriteLine("Average doubles: " + nAverageDoubles.ToString());
            Console.SetCursorPosition(32, 5);
            Console.WriteLine("Average triples: " + nAverageTriples.ToString());
            Console.SetCursorPosition(32, 6);
            Console.WriteLine("Average homeruns: " + nAverageHomeruns.ToString());
            Console.SetCursorPosition(32, 7);
            Console.WriteLine("Average balls: " + nAverageBalls.ToString());
            Console.SetCursorPosition(32, 8);
            Console.WriteLine("Average walks: " + nAverageWalks.ToString());
            Console.SetCursorPosition(32, 9);
            Console.WriteLine("Average strikes: " + nAverageStrikes.ToString());
            Console.SetCursorPosition(32, 10);
            Console.WriteLine("Average strikeouts: " + nAverageStrikeouts.ToString());
            Console.SetCursorPosition(32, 11);
            Console.WriteLine("Average fly outs: " + nAverageFlyOuts.ToString());
            Console.SetCursorPosition(32, 12);
            Console.WriteLine("Average line outs: " + nAverageLineOuts.ToString());
            Console.SetCursorPosition(32, 13);
            Console.WriteLine("Average pop outs: " + nAveragePopOuts.ToString());
        }

        private static void DrawTotalStatsShell() {
            Console.SetCursorPosition(60, 0);
            Console.WriteLine("|-------------------------|");
            Console.SetCursorPosition(60, 1);
            Console.WriteLine("| Game Stats              |");
            Console.SetCursorPosition(60, 2);
            Console.WriteLine("|-------------------------|");
            for (int i = 0; i < 11; i++) {
                Console.SetCursorPosition(60, i + 3);
                Console.WriteLine("|                         |");
            }
            Console.SetCursorPosition(60, 14);
            Console.WriteLine("|-------------------------|");
        }
        private static void DrawTotalStats(int x, int y) {
            Console.SetCursorPosition(x, y);
            Console.WriteLine("Total singles: " + nTotalSingles.ToString());
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("Total doubles: " + nTotalDoubles.ToString());
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("Total triples: " + nTotalTriples.ToString());
            Console.SetCursorPosition(x, y + 3);
            Console.WriteLine("Total homeruns: " + nTotalHomeruns.ToString());
            Console.SetCursorPosition(x, y + 4);
            Console.WriteLine("Total balls: " + nTotalBalls.ToString());
            Console.SetCursorPosition(x, y + 5);
            Console.WriteLine("Total strikes: " + nTotalStrikes.ToString());
            Console.SetCursorPosition(x, y + 6);
            Console.WriteLine("Total walks: " + nTotalWalks.ToString());
            Console.SetCursorPosition(x, y + 7);
            Console.WriteLine("Total strikeouts: " + nTotalStrikeouts.ToString());
            Console.SetCursorPosition(x, y + 8);
            Console.WriteLine("Total fly outs: " + nTotalFlyOuts.ToString());
            Console.SetCursorPosition(x, y + 9);
            Console.WriteLine("Total line outs: " + nTotalLineOuts.ToString());
            Console.SetCursorPosition(x, y + 10);
            Console.WriteLine("Total pop outs: " + nTotalPopOuts.ToString());
        }

        #region Log
        private static void DrawLogShell() {
            Console.SetCursorPosition(90, 0);
            Console.WriteLine("|-------------------------|");
            Console.SetCursorPosition(90, 1);
            Console.WriteLine("| Game Log                |");
            Console.SetCursorPosition(90, 2);
            Console.WriteLine("|-------------------------|");
            for (int i = 0; i < nLogSize; i++) {
                Console.SetCursorPosition(90, i + 3);
                Console.WriteLine("|                         |");
            }
            Console.SetCursorPosition(90, 3 + nLogSize);
            Console.WriteLine("|-------------------------|");
        }
        private static void DrawLog() {
            for (int i = 0; i < GameLog.Count; i++) {
                Console.SetCursorPosition(92, i + 3);
                Console.WriteLine(new string(' ', 20));
                Console.SetCursorPosition(92, i + 3);
                Console.WriteLine(GameLog[i]);
            }

            Console.WriteLine();
        }
        private static void Log(string s) {
            if (GameLog.Count >= nLogSize) {
                GameLog.RemoveAt(0);
            }

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write(new string(' ', 30));
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("Last play: " + s);

            GameLog.Add(nLogIndex.ToString() + ". " + s);
            nLogIndex++;
        }
        #endregion



        #region Miscellaneous
        private static void NewGame() {
            Console.Clear();

            DrawTotalStatsShell();
            DrawLogShell();
            DrawAveragesShell();

            DrawHistory();
            DrawAverages();

            nLogIndex = 1;
            GameLog = new List<string>(nLogSize);

            nTotalSingles = 0;
            nTotalDoubles = 0;
            nTotalTriples = 0;
            nTotalHomeruns = 0;
            nTotalBalls = 0;
            nTotalWalks = 0;
            nTotalStrikeouts = 0;
            nTotalStrikes = 0;
            nTotalFlyOuts = 0;
            nTotalLineOuts = 0;
            nTotalPopOuts = 0;

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
            if (nTeam1Score > nTeam2Score) {
                nTeam1Wins++;
            }
            else if (nTeam2Score > nTeam1Score) {
                nTeam2Wins++;
            }
            else {
                nTies++;
            }

            dAverageSingles = (dAverageSingles * (nTotalGames - 1) + nTotalSingles) / nTotalGames;
            dAverageDoubles = (dAverageDoubles * (nTotalGames - 1) + nTotalDoubles) / nTotalGames;
            dAverageTriples = (dAverageTriples * (nTotalGames - 1) + nTotalTriples) / nTotalGames;
            dAverageHomeruns = (dAverageHomeruns * (nTotalGames - 1) + nTotalHomeruns) / nTotalGames;
            dAverageBalls = (dAverageBalls * (nTotalGames - 1) + nTotalBalls) / nTotalGames;
            dAverageWalks = (dAverageWalks * (nTotalGames - 1) + nTotalWalks) / nTotalGames;
            dAverageStrikes = (dAverageStrikes * (nTotalGames - 1) + nTotalStrikes) / nTotalGames;
            dAverageStrikeouts = (dAverageStrikeouts * (nTotalGames - 1) + nTotalStrikeouts) / nTotalGames;
            dAverageFlyOuts = (dAverageFlyOuts * (nTotalGames - 1) + nTotalFlyOuts) / nTotalGames;
            dAverageLineOuts = (dAverageLineOuts * (nTotalGames - 1) + nTotalLineOuts) / nTotalGames;
            dAveragePopOuts = (dAveragePopOuts * (nTotalGames - 1) + nTotalPopOuts) / nTotalGames;

            DrawHistory();
            DrawAverages();

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("Starting new game in 5 seconds.");
            System.Threading.Thread.Sleep(5000);
        }

        private static void ScoreRuns(int nRuns) {
            Log(nRuns.ToString() + (nRuns > 1 ? " runs scored." : " run scored."));

            if (nTeam == 0) {
                nTeam1Scores[nInning] += nRuns;
            }
            else {
                nTeam2Scores[nInning] += nRuns;
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
            int nRandom = r.Next(200);
            if (nRandom > 198) {
                // homerun
                ProcessHomerun();
            }
            else if (nRandom > 194) {
                // triple
                ProcessTriple();
            }
            else if (nRandom > 190) {
                // double
                ProcessDouble();
            }
            else if (nRandom > 180) {
                // single
                ProcessSingle();
            }
            else if (nRandom > 160) {
                // random out
                ProcessRandomOut();
            }
            else if (nRandom > 80) {
                // strike
                ProcessStrike();
            }
            else {
                // ball
                ProcessBall();
            }
        }
        private static void ProcessHomerun() {
            Log("Homerun!");
            strLastAction = "Homerun!";
            nTotalHomeruns++;

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
            Log("Triple!");
            strLastAction = "Triple!";
            nTotalTriples++;

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
            Log("Double!");
            strLastAction = "Double!";
            nTotalDoubles++;

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
            Log("Single!");
            strLastAction = "Single!";
            nTotalSingles++;

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
                    Log("Pop out!");
                    nTotalPopOuts++;
                    strLastAction = "Pop out!";
                    break;
                case 1:
                    Log("Fly out!");
                    nTotalFlyOuts++;
                    strLastAction = "Fly out!";
                    break;
                case 2:
                    Log("Line out!");
                    nTotalLineOuts++;
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
            Log("Strikeout!");
            strLastAction = "Strikeout!";
            nTotalStrikeouts++;

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
            nTotalStrikes++;

            Log("Strike " + nStrikes.ToString() + "!");

            if (nStrikes == 3) {
                ProcessStrikeout();
            }
        }
        private static void ProcessBall() {
            nBalls++;
            nTotalBalls++;

            Log("Ball " + nBalls.ToString() + ".");

            if (nBalls == 4) {
                ProcessWalk();
            }
        }
        private static void ProcessWalk() {
            Log("Walk!");
            strLastAction = "Walk!";
            nTotalWalks++;

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
        private static void DrawField(int x, int y) {
            // |-------|
            // |...o...|
            // |../.\..|
            // |.o...o.|
            // |..\./..|
            // |...o...|
            // |-------|

            Console.SetCursorPosition(x, y);
            Console.WriteLine("|-------|");

            Console.SetCursorPosition(x, y + 1);
            if (nSecond > 0) {
                Console.WriteLine("|...x...|");
            }
            else {
                Console.WriteLine("|...o...|");
            }

            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("|../.\\..|");

            Console.SetCursorPosition(x, y + 3);
            if (nThird > 0 && nFirst > 0) {
                Console.WriteLine("|.x...x.|");
            }
            else if (nThird > 0) {
                Console.WriteLine("|.x...o.|");
            }
            else if (nFirst > 0) {
                Console.WriteLine("|.o...x.|");
            }
            else {
                Console.WriteLine("|.o...o.|");
            }

            Console.SetCursorPosition(x, y + 4);
            Console.WriteLine("|..\\./..|");

            Console.SetCursorPosition(x, y + 5);
            Console.WriteLine("|...o...|");
            Console.SetCursorPosition(x, y + 6);
            Console.WriteLine("|-------|");
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

            Console.SetCursorPosition(0, Console.WindowHeight - 8);
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
                            Console.Write(nTeam1Scores[i].ToString() + '!');
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
                            Console.Write(nTeam2Scores[i].ToString() + '!');
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
