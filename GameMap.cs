using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using DungeonExplorer;
using System.Linq;

namespace DungeonExplorer
{
    /// <summary>
    /// Manages multiple interconnected rooms
    /// </summary>
    public class GameMap
    {
        public int xPosition = 0;
        public int yPosition = 0;

        private Player _player;
        private Room _cell;
        private Room _doom;
        private Room _chamber;
        private Room _apothecary;
        private Room _forest;
        private Room _archives;
        private Room _vault;
        public Room CurrentRoom { get; private set; }

        public GameMap(Player player)
        {
            // Initialises the rooms
            _player = player;
            _cell = new Room("Cell", "A dark cell with a bed.");
            _doom = new Room("Room of Doom", "A huge dimly rit room with harmless bats.");
            _chamber = new Room("Chamber", "A brightly lit stone underground chamber. The air is cold with the scent of damp.");
            _apothecary = new Room("Apothecary", "The air is full of scents of herbs and potions. Wooden shelves line the crumbling walls, crammed with empty bottles. The room feels forgotten.");
            _forest = new Room("Forbidden Forest", "A dark, foggy forest. The air is damp which carries the scent of death. The trees are twisted and each step leads deeper into the unknown.");
            _archives = new Room("Abandoned Archives", "The stale air carrie heavy dust. Books line the crumbling shelves. their titles faded byond reading. Somewhere deep within, knowledge and danger still linger.");
            _vault = new Room("Vault", "A room made up of ancient stone and polished obsidian. The walls shimmer colours of silver and amethyst.");
            CurrentRoom = _cell;
        }

        /// <summary>
        /// Allows player to move in a direction which they choose
        /// </summary>
        public void MoveDirection()
        {
            string direction = "";

            while (true)
            {
                // Stores previous position
                int xPrevious = xPosition;
                int yPrevious = yPosition;

                // Prompts the player to pick where they want to move to
                Console.WriteLine("Would you like to move:" +
                "Left(L), Right(R), Forwards (F), or Backwards (B)?");
                direction = Console.ReadLine()?.ToUpper();

                // Updates the position based on the direction
                if (direction == "L")
                {
                    xPosition -= 1;
                }
                else if (direction == "R")
                {
                    xPosition += 1;
                }
                else if (direction == "F")
                {
                    yPosition += 1;
                }
                else if (direction == "B")
                {
                    yPosition -= 1;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter L, R, F or B.");
                    continue; // Continues asking for input if the input is invalid
                }

                // Updates the current room based on the player's position
                if (xPosition >= -1 && xPosition <= 1 && yPosition >= 0 && yPosition <= 1)  // x: -1 to 1, y: 0 to 1
                {
                    CurrentRoom = _cell;
                    Console.WriteLine("\nYou are in the Cell.");
                }

                else if (xPosition >= -3 && xPosition <= 2 && yPosition >= 2 && yPosition <= 5)  // x: -3 to 2, y: 2 to 5
                {
                    CurrentRoom = _doom;
                    Console.WriteLine("\nYou are in the Room of Doom.");
                }

                else if (xPosition >= 3 && xPosition <= 6 && yPosition >= 2 && yPosition <= 5)  // x: 3 to 6, y: 2 to 5
                {
                    // Player has to have the apothecary key in their inventory to enter the apothecary
                    Item apothecaryKey = ItemList.Keys.FirstOrDefault(k => k.Name == "Apothecary Key");

                    // If the player has the key, they can enter the apothecary
                    if (_player.Inventory.ContainsItem(apothecaryKey))
                    {
                        CurrentRoom = _apothecary;
                        Console.WriteLine("\nYou are in the Apothecary.");
                    }

                    // If the player does not have the key, they can't enter and stay in the same position as before
                    else
                    {
                        Console.WriteLine("\nYou need the key for the apothecary room." +
                        "You can collect the key by defeating the basilisk in the chamber.");
                        xPosition = xPrevious;
                        yPosition = yPrevious;
                    }
                }

                else if (xPosition >= -10 && xPosition <= 10 && yPosition <= -1 && yPosition >= -3) // x: -10 to 10, y: -1 to -3
                {
                    
                    CurrentRoom = _chamber;
                    Console.WriteLine("\nYou are in the Chamber.");
                }

                else if (xPosition >= -3 && xPosition <= 2 && yPosition >= 6 && yPosition <= 9)  // x: -3 to 2, y: 6 to 9
                {
                    CurrentRoom = _archives;
                    Console.WriteLine("\nYou are in the Abandoned Archives.");
                }

                else if (xPosition >= -4 && xPosition <= 3 && yPosition >= 10 && yPosition <= 14)  // x: -4 to 3, y: 10 to 14
                {
                    CurrentRoom = _vault;
                    Console.WriteLine("\nYou are in the vault.");
                }

                else
                {
                    // If the player is not in any other room, they will be in the forbidden forest
                    CurrentRoom = _forest;
                    Console.WriteLine("\nYou are in the Forbidden Forest.");
                }

                // The position is always being displayed
                Console.WriteLine($"Current position: ({xPosition}, {yPosition})");
                break;
            }
        }

        /// <summary>
        /// Room positions for each room.
        /// </summary>
        /// <returns></returns>
        public Room RoomPosition()
        {
            if (xPosition >= -1 && xPosition <= 1 && yPosition >= 0 && yPosition <= 1)
            {
                return _cell;
            }
            else if (xPosition >= -3 && xPosition <= 2 && yPosition >= 2 && yPosition <= 5)
            {
                return _doom;
            }
            else if (xPosition >= -10 && xPosition <= 10 && yPosition <= -1 && yPosition >= -3)
            {
                return _chamber;
            }
            else if (xPosition >= 3 && xPosition <= 6 && yPosition >= 2 && yPosition <= 5)
            {
                return _apothecary;
            }
            else if (xPosition >= -3 && xPosition <= 2 && yPosition >= 6 && yPosition <= 9)
            {
                return _archives;
            }
            else if (xPosition >= -4 && xPosition <= 3 && yPosition >= 10 && yPosition <= 14)
            {
                return _vault;
            }
            else
            {
                return _forest;
            }
        }
    }
}
