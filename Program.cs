using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DungeonExplorer
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Runs tests
            var testingPlayer = new PlayerTests();
            testingPlayer.RunTests();
            Console.WriteLine("Player testing completed.");

            var testingPuzzle = new PuzzleTests();
            testingPuzzle.RunTests();
            Console.WriteLine("Puzzle testing completed.");

            var testingStatistics = new StatisticsTests();
            testingStatistics.RunTests();
            Console.WriteLine("Statistics testing completed.");

            // Starts the game
            Game game = new Game();
            game.Start();
            Console.WriteLine("Waiting for your Implementation");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
