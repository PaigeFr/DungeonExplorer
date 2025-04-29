using System;
using System.Diagnostics;

namespace DungeonExplorer
{
    /// <summary>
    /// Manages the puzzles in the game
    /// </summary>
    public class Puzzle
    {
        /// <summary>
        /// An anagram puzzle
        /// </summary>
        public void Anagram(Player player)
        {
            string scrambledWord = "OPPYERHC";
            string correctAnswer = "prophecy";

            // Displays to the player what they have to do
            Console.WriteLine("\nYou find an ancienty note on the floor that you can only just read..." +
            "\nTo stop the monster from spawning, rearrange the letters:" + 
            "\n" + scrambledWord);

            // Prompts player for their answer
            Console.WriteLine("Enter the correct word: ");
            string playerInput = Console.ReadLine().ToUpper();

            // Player does not go into battle if the get the puzzle correct
            if (playerInput.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\nCorrect! The monster stays hidden.");
            }

            // If player answers incorrectly, then they will be in a battle with an erkling
            else
            {
                Console.WriteLine("\nThat is not correct...");
                Console.WriteLine("You have encountered an erkling!");
                Erkling erkling = new Erkling();
                Battle battle = new Battle(player, erkling);
                battle.Start();
            }
        }
    }

    /// <summary>
    /// Tests for the puzzle class
    /// </summary>
    public class PuzzleTests
    {
        public void RunTests()
        {
            // Checks if the Anagram method works with the correct input
            var player1 = new Player("Test Player", 100);
            var puzzle = new Puzzle();

            // Simulates the correct input by passing the correct answer
            string correctAnswer = "prophecy";
            string playerInput = correctAnswer.ToUpper();  

            // Checks if the method behaves as it should with the correct answer
            Debug.Assert(CheckAnswer(playerInput, correctAnswer), "Test failed: Correct answer should not trigger a battle.");

            // Checks if the anagram method works with the incorrect input
            playerInput = "incorrectanswer"; 
            Debug.Assert(!CheckAnswer(playerInput, correctAnswer), "Test failed: Incorrect answer should trigger a battle.");
        }

        // Simulates the behavior of the Anagram method with a given input
        private bool CheckAnswer(string playerInput, string correctAnswer)
        {
            // Simulates the anagram check logic from Puzzle.Anagram()
            return playerInput.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase);
        }
    }
}
