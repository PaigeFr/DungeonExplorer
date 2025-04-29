using System.Linq;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace DungeonExplorer
{
    
    /// <summary>
    /// Tracks the player’s name and a single attribute, such as health or a basic inventory
    /// </summary>
    public class Player : Creature
    {
        public Inventory Inventory { get; private set; }

        public Player(string name, int health) : base(name, 10, health)
        {
            Inventory = new Inventory();
        }

        /// <summary>
        /// Used to display the inventory contents.
        /// </summary>
        /// <returns></returns>
        public string InventoryContents()
        {
            return string.Join(", ", Inventory.GetItems().Select(item => item.Name));
        }
    }
    
    /// <summary>
    /// Testing class for the InventoryContents method
    /// </summary>
    public class PlayerTests
    {
         public void RunTests()
        {
            // Checks if Player is initialized with a positive health
            var playerTest = new Player("Test Player", 100);
            Debug.Assert(playerTest.Health > 0, "Player's health should be greater than zero.");

            // Checks if the Inventory is initialized correctly
            Debug.Assert(playerTest.Inventory != null, "Player's inventory should not be null.");

            // Checks if InventoryContents returns correct data when the inventory is empty
            Debug.Assert(string.IsNullOrEmpty(playerTest.InventoryContents()), "Inventory should be empty when no items are added.");
        }
    }
}
