using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DungeonExplorer
{
    /// <summary>
    ///  A collection to manage items
    /// </summary>
    public class Inventory
    {
        private List<Item> _items;

        public Inventory()
        {
            _items = new List<Item>();
        }

        /// <summary>
        /// Adds items to the inventory
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Removes items from the inventory
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(Item item)
        {
          _items.Remove(item);
        }

        /// <summary>
        /// Tracks which items are in the inventory.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool ContainsItem(Item item)
        {
           return _items.Contains(item);
        }

        /// <summary>
        /// Gives the player a choice to pick up the knife
        /// </summary>
        /// <param name="player"></param>
        /// <param name="itemName"></param>
        public void PickUpItem(Player player, string itemName)
        {
            // Checks if player has the item
            if (player.InventoryContents().Contains(itemName))
            {
                return;
            }

            while (true)
            {
                // Asks the player if they want to pick up the item
                Console.WriteLine($"\nThere is a {itemName} in front of you.\nDo you want to pick up the {itemName}? y/n ");
                string choice = Console.ReadLine().ToLower();

                // Adds the item to the player's inventory if they input 'y'
                if (choice == "y")
                {
                    Item item = ItemList.Weapons.Find(i => i.Name == itemName);  // Finds weapons
                    if (item == null)
                    {
                        item = ItemList.Potions.Find(i => i.Name == itemName);  // Finds potions
                    }
                    if (item == null && itemName == "Apothecary Key")
                    {
                        item = ItemList.Keys.Find(i => i.Name == "Apothecary Key");  // Finds apothecary key
                    }
                    if (item != null)
                    {
                        player.Inventory.AddItem(item);
                        item.Collect();
                    } 
                    return; 
                }

                // Displays to the player that they have chosen not to pick up the item
                else if (choice == "n")
                {
                    Console.WriteLine($"You have chosen not to pick up the {itemName}.");
                    return;
                }

                // Prompts player to enter a valid input
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'y' or 'n': ");
                }
            }
        }

        /// <summary>
        /// Gets the list of items
        /// </summary>
        /// <returns></returns>
        public List<Item> GetItems()
        {
            return _items;
        }
        
        /// <summary>
        /// Displays the weapons in the player's inventory
        /// Also displays the damage of each weapon
        /// </summary>
        public void ShowWeapons()
        {
            var weapons = _items.OfType<Weapon>().ToList();

            Console.WriteLine("Weapons in your inventory: ");
            foreach (var weapon in weapons)
            {
                Console.WriteLine($"{weapon.Name}: Damage - {weapon.Damage}");
            }
        }

        /// <summary>
        /// Displays the potions in the player's inventory
        /// Also displays the heal amount of each potion
        /// </summary>
        public void ShowPotions()
        {
            var potions = _items.OfType<Potion>().ToList();

            Console.WriteLine("Potions in your inventory: ");
            foreach (var potion in potions)
            {
                Console.WriteLine($"{potion.Name}: Heals: {potion.HealAmount}");
            }
        }
    }
}
