using System;
using System.Dynamic;
using System.Diagnostics;

namespace DungeonExplorer
{
    /// <summary>
    /// Manages the statstics of the game
    /// </summary>
    public class Statistics
    {
        public static int MonstersDefeated { get; private set; } = 0;

        public static void MonsterDefeated()
        {
            MonstersDefeated++;
        }

        public static void DisplayStatistics()
        {
            Console.WriteLine($"Monsters defeated: {MonstersDefeated}");
        }

        // The statistics are reset after tests are completed
        public static void ResetStatistics()
        {
            MonstersDefeated = 0;
        }
    }

    /// <summary>
    /// Tests for the statistics class
    /// </summary>
    public class StatisticsTests
    {
        public void RunTests()
        {
            // Checks if the initial value of MonstersDefeated is 0
            Debug.Assert(Statistics.MonstersDefeated == 0, "Test failed: MonstersDefeated should be 0 at the start.");

            // Simulates defeating a monster and checks if the amount increases
            Statistics.MonsterDefeated();
            Debug.Assert(Statistics.MonstersDefeated == 1, "Test failed: MonstersDefeated should be 1 after defeating one monster.");

            // Simulates defeating two more monsters and checks the amount again
            Statistics.MonsterDefeated();
            Statistics.MonsterDefeated();
            Statistics.MonsterDefeated();
            Debug.Assert(Statistics.MonstersDefeated == 4, "Test failed: MonstersDefeated should be 4 after defeating three monsters.");

            DisplayStatisticsTest();
            Statistics.ResetStatistics();
            Debug.Assert(Statistics.MonstersDefeated == 0, "Test failed: MonstersDefeated should be reset to 0 after tests.");
        }

        /// <summary>
        /// Tests the DisplayStatistics method
        /// </summary>
        private void DisplayStatisticsTest()
        {
            try
            {
                Console.Write("Statistics test:");
                Statistics.DisplayStatistics();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test failed: {ex.Message}");
            }
        }
    }
}
