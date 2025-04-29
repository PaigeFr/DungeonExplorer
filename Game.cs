using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DungeonExplorer
{
    /// <summary>
    /// Handles the game flow and initializes the player as well as rooms
    /// </summary>
    public class Game
{
    private Player _player;
    private GameMap _gameMap;
    private Room _currentRoom;
    private Room _cell;
    private Room _doom;
    private Room _chamber;
    private Room _apothecary;
    private Room _forest;
    private Room _archives;
    private Room _vault;

    public Game()
    {
        // Initialises the rooms
        _cell = new Room("Cell", "An empty cell with just a bed.");
        _doom = new Room("Room of Doom", "A huge room with harmless bats.");
        _chamber = new Room("A brightly lit stone underground chamber. The air is cold with the scent of damp.");
        _apothecary = new Room("The air is full of scents of herbs and potions. Wooden shelves line the crumbling walls, crammed with empty bottles. The room feels forgotten.");
        _forest = new Room("A dark, foggy forest. The air carries the scent of death. The trees are twisted and each step leads deeper into the unknown.");
        _archives = new Room("The stale air carrie heavy dust. Books line the crumbling shelves. their titles faded byond reading. Somewhere deep within, knowledge and danger still linger.");
        _vault = new Room("A room made up of ancient stone and polished obsidian. The walls shimmer colours of silver and amethyst.");

        _currentRoom = _cell;  // Sets initial room
        _player = new Player("DefaultName", 100);  // Initialises player with default name
        _gameMap = new GameMap(_player);  // Initialises game map
    }

    /// <summary>
    /// Handles the game flow for the Room of Doom
    /// </summary>
    /// <param name="newRoom"></param>
    public void RoomOfDoom(Room newRoom)
    {
        _currentRoom = newRoom;  // Sets the room to the Room of Doom
        
        if (_gameMap.xPosition == 0 && _gameMap.yPosition == 3)  // Position (0, 3)
        {
            _player.Inventory.PickUpItem(_player, "Knife");  // Prompts the player to pick up the knife
        }

        List<Monster> monsters = _currentRoom.GetMonsters();

        if (_gameMap.xPosition == 1 &&  _gameMap.yPosition == 3) // Position (1, 3)
        {
            Console.WriteLine("You have encountered a dragon!");
           Monster dragon = monsters.FirstOrDefault(m => m.Name == "Dragon"); 

           // Player starts battling the dragon
           if (dragon != null)
           {
            Battle battle = new Battle(_player, dragon);
            battle.Start();
           }
        }
    }

    /// <summary>
    /// Handles the game flow for the Chamber
    /// </summary>
    /// <param name="newRoom"></param>
    public void Chamber(Room newRoom)
    {
        _currentRoom = newRoom;  // Sets the room to the Chamber

        if (_gameMap.xPosition == -5 && _gameMap.yPosition == -1)  // Position (-5, -1)
        {
            _player.Inventory.PickUpItem(_player, "Sword");  // Prompts the player to pick up the sword
            
        }

        if (_gameMap.xPosition == 7 && _gameMap.yPosition == -2)  // Position (7, -2)
        {

            _player.Inventory.PickUpItem(_player, "Doomfang Sword");  // Prompts the player to pick up the doomfang sword
        }

        if (_gameMap.xPosition == 0 && _gameMap.yPosition == -2)  // Position (0, -2)
        {
            _player.Inventory.PickUpItem(_player, "Small Healing Potion");   // Prompts the player to pick up the small healing potion
        }

        List<Monster> monsters = _currentRoom.GetMonsters();
        if (_gameMap.xPosition == 3 && _gameMap.yPosition == -3) // Position (3, -3)
        {
            Console.WriteLine("You have encountered a basilisk!");
            Monster basilisk = monsters.FirstOrDefault(m => m.Name == "Basilisk");

            // Player starts battling the basilisk
            if (basilisk != null)
            {
                Battle battle = new Battle(_player, basilisk);
                battle.Start();
            }
        }
    }

    /// <summary>
    /// Handles the game flow for the Apothecary
    /// </summary>
    /// <param name="newRoom"></param>
    private void Apothecary(Room newRoom)
    {
        _currentRoom = newRoom;  // Sets the room to the Apothecary

        if (_gameMap.xPosition == 3 && _gameMap.yPosition == 4)  // Position (3, 4)
        {
            _player.Inventory.PickUpItem(_player, "Small Healing Potion");  // Prompts the player to pick up the small healing potion
        }
        if (_gameMap.xPosition == 4 && _gameMap.yPosition == 2)  // Position (4, 2)
        {
            _player.Inventory.PickUpItem(_player, "Medium Healing Potion");  // Prompts the player to pick up the medium healing potion
        }
        if (_gameMap.xPosition == 5 && _gameMap.yPosition == 3)  // Position (5, 3)
        {
            _player.Inventory.PickUpItem(_player, "Large Healing Potion");  // Prompts the player to pick up the large healing potion
        }
    }

    /// <summary>
    /// Handles the game flow for the Forbidden Forest
    /// </summary>
    /// <param name="newRoom"></param>
    private void ForbiddenForest(Room newRoom)
    {
        _currentRoom = newRoom;  // Sets the room to the Forbidden Forest

        List<Monster> monsters = _currentRoom.GetMonsters();
        Random random = new Random();
        int chance = random.Next(1, 101);
        bool acromantulaSpawned = false;
        bool axeSpawned = false;

        if (chance <= 10)  // 10% chance for an acromantula to appear every time the player moves in the forest
        {
            Console.WriteLine("You have encountered an acromantula!");
            Monster acromantula = monsters.FirstOrDefault(m => m.Name == "Acromantula");

            // Player starts battling the basilisk
            if (acromantula != null)
            {
                acromantulaSpawned = true;
                Battle battle = new Battle(_player, acromantula);
                battle.Start();
            }
        }

        // 20% chance for an axe to appear every time the player moves in the forest
        if (!acromantulaSpawned && random.Next(1, 101) <= 20)
        {
            _player.Inventory.PickUpItem(_player, "Axe");
            axeSpawned = true;
        }

        // 5% chance for a mace to appear every time the player moves in the forest
        if (!acromantulaSpawned && !axeSpawned && random.Next(1, 101) <= 5)
        {
            _player.Inventory.PickUpItem(_player, "Mace");
        }
    }

    
    /// <summary>
    /// Handles the game flow for the Abondened Archives
    /// </summary>
    /// <param name="newRoom"></param>
    private void AbandonedArchives(Room newRoom)
    {
        _currentRoom = newRoom;  // Sets the room to the Abandoned Archives

        if(_gameMap.xPosition == 1 && _gameMap.yPosition == 6)  // Position (1, 6)
        {
            _player.Inventory.PickUpItem(_player, "Small Healing Potion");  // Prompts the player to pick up the small healing potion
        }
    
        if(_gameMap.xPosition == 1 && _gameMap.yPosition == 7)  // Position (1, 7)
        {
            // Player has to solve an anagram
            Puzzle puzzle = new Puzzle();
            puzzle.Anagram(_player);
        }
    }

    /// <summary>
    /// Handles the game flow for the Vault
    /// </summary>
    /// <param name="newRoom"></param>
    private void Vault(Room newRoom)
    {
        _currentRoom = newRoom;

        if(_gameMap.xPosition == 1 && _gameMap.yPosition == 11)  // Position (1, 11)
        {
            _player.Inventory.PickUpItem(_player, "Blade");  // Prompts the player to pick up the blade
        }

        if (_gameMap.xPosition == -3 && _gameMap.yPosition == 14)  // Position (-3, 14)
        {
            _player.Inventory.PickUpItem(_player, "Obsidian Blade");  // Prompts the player to pick up the obsidian blade
        }
    }
    
        /// <summary>
        /// Starts the game and handles the game flow
        /// </summary>
        public void Start()
        {
            Console.WriteLine("\nWelcome to Dungeon Explorer!");

            // Prompts user for their name
            string playerName = null;
            while (string.IsNullOrEmpty(playerName))
            {
                Console.Write("Please enter your name: ");
                playerName = Console.ReadLine();

                // If name is empty or null, the program will prompt the user again
                if (string.IsNullOrEmpty(playerName))
                {
                    Console.WriteLine("Your name cannot be empty.");
                }
            }

            // Initialises game with one player & sets health to 100
            _player = new Player(playerName, 100);  

            Note();  // Displays the note

            bool playing = true;
            while (playing)
            {
                // Prompts user to choose an option - to view room's description, health, inventory or to continue playing
                Console.WriteLine("\nPlease choose an option:" +
                "\n1. View the room's description" +
                "\n2. View your health" +
                "\n3. View your inventory" +
                "\n4. Explore" +
                "\n5. Stop playing");
                Console.WriteLine("Enter '1', '2', '3' '4' or '5': ");
                string options = Console.ReadLine();

                if (options == "1")
                {
                    Console.WriteLine("\n" + _currentRoom.GetDescription());  // Displays the room's description
                }
                else if (options == "2")
                {
                    Console.WriteLine("\nHealth: " + _player.Health);  // Displays the player's health
                }
                else if (options == "3")
                {
                    Console.WriteLine("\nInventory: " + _player.InventoryContents());  // Display's the player's inventory
                }
                else if (options == "4")
                {
                    // Asks the player which direction they want to move in
                    _gameMap.MoveDirection();
                    _currentRoom = _gameMap.RoomPosition();

                    if (_currentRoom != null && _currentRoom.RoomName == "Room of Doom")
                    {
                        RoomOfDoom(_currentRoom);
                    }
                    else if (_currentRoom != null && _currentRoom.RoomName == "Chamber")
                    {
                        Chamber(_currentRoom);
                    }
                    else if (_currentRoom != null && _currentRoom.RoomName == "Apothecary")
                    {
                        Apothecary(_currentRoom);
                    }
                    else if (_currentRoom.RoomName == "Forbidden Forest")
                    {
                        ForbiddenForest(_currentRoom);
                    }
                    else if (_currentRoom.RoomName == "Abandoned Archives")
                    {
                        AbandonedArchives(_currentRoom);
                    }
                    else if (_currentRoom.RoomName == "Vault")
                    {
                        Vault(_currentRoom);
                    }
                }
                else if (options == "5")
                {
                    Console.WriteLine("\nYou have chosen to stop playing.");
                    playing = false;  // The game will now end
                }
                else
                {
                    // User will be prompted to select one of the 5 options again
                    Console.WriteLine("You have selected an invalid input. Please choose again.");  
                }
            }
        }

        /// <summary>
        /// Displays a note to the player to introduce them to the objective of the game
        /// </summary>
        private void Note()
        {
            Console.WriteLine($"\nYou notice that there's a note on the floor. It reads...\n" + 
            $"\nDear {_player.Name},\n\nYou have been locked inside the dungeon. You must navigate through the rooms to escape." +
            "\nBeware, you will need to battle creatures throughout your journey!");
        }
    }
}
