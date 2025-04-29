using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    /// <summary>
    /// Represents a single room in the game with a description and possibly an item & monster
    /// </summary>
    public class Room
    {
        private string _description;
        public string RoomName { get; private set; }
        public Room Left { get; set; }  
        public Room Right { get; set; }
        private List<string> _items;
        private List<Monster> _monsters;

        public Room(string roomName)
        {
            this.RoomName = roomName;
            this._items = new List<string>();
            this._monsters = new List<Monster>();

            if (roomName == "Cell")
            {
                 this._description = "An empty cell with just a bed.";  // Cell's room description
            }
            else if (roomName == "Room of Doom")
            {
                this._description = "A huge dimly rit room with harmless bats."; // Room of doom's description
                // Adds weapons & monsters to the room of doom
                _items.Add("Knife");  
                _monsters.Add(new Dragon());
            }
            else if (roomName == "Chamber")
            {
                this._description = "A brightly lit stone underground chamber. The air is cold with the scent of damp.";
                 // Adds weapons & monsters to the chambers
                 _items.Add("Sword");
                 _items.Add("Doomfang Sword");
                 _items.Add("Small Healing Potion");
                 _monsters.Add(new Basilisk());
            }
            else if (roomName == "Apothecary")
            {
                this._description = "The air is full of scents of herbs and potions. Wooden shelves line the crumbling walls, crammed with empty bottles. The room feels forgotten.";
                // Adds potions to the apothecary
                _items.Add("Small Healing Potion");
            }
            else if (roomName == "Forbidden Forest")
            {
                this._description = "A dark, foggy forest. The air carries the scent of death. The trees are twisted and each step leads deeper into the unknown.";
                // Adds monsters to the forbidden forest
                _monsters.Add(new Acromantula());
            }
            else if (roomName == "Abandoned Archives")
            {
                this._description = "The stale air carrie heavy dust. Books line the crumbling shelves. their titles faded byond reading. Somewhere deep within, knowledge and danger still linger.";
                // Adds monsters and items to the Abondened Archives
                _items.Add("Small Healing Potion");
                _monsters.Add(new Erkling());
            }
            else if (roomName == "Vault")
            {
                this._description = "A room made up of ancient stone and polished obsidian. The walls shimmer colours of silver and amethyst.";
                // Adds items to the Vault
                _items.Add("Blade");
                _items.Add("Obsidian Blade");
            }
        }

        /// <summary>
        /// Returns the room's monsters
        /// </summary>
        /// <returns></returns>
        public List<Monster> GetMonsters()
            {
                return _monsters;
            }

        public Room(string roomName, string v) : this(roomName)
        {
        }

        /// <summary>
        /// Returns the room's description
        /// </summary>
        /// <returns></returns>
        public string GetDescription()
        {
            return $"{RoomName}: {_description}";
        }

        /// <summary>
        /// Returns the items
        /// </summary>
        /// <returns></returns>
        public List<string> GetItems()
        {
            return _items;
        }
    }
}
