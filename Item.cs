using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DungeonExplorer
{
    /// <summary>
    ///  Represents multiple types of items
    /// </summary>
    public abstract class Item : ICollectible
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Displays the name and description of each item
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name} - {Description}";
        }

        public abstract void Use();

        /// <summary>
        /// Displays to the user that they have collected an item.
        /// </summary>
        public void Collect()
        {
            Console.WriteLine($"You have collected the {Name}!");
        }

        public override bool Equals(object obj)
        {
            return obj is Item item && item.Name == this.Name;
        }

        // Checks for object equality
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    public class Weapon : Item
    {
        /// <summary>
        /// Represents the weapons
        /// </summary>
        public int Range { get; set; }
        public int Damage { get; set; }

        // When a player uses a weapon, it will deal damage to the monster
        public Weapon(string name, int damage) : base(name, $"Damage: {damage}")
        {
            Damage = damage;
        }

       // Checks if the weapon is working & displays to the user what weapon the player is using
        public override void Use()
        {
            Debug.Assert(!string.IsNullOrEmpty(Name), "Item name must not be null or empty.");
            Console.WriteLine($"\nYou use the {Name}!");
        }

        // Checks if the weapon is working & displays the damage of the weapon the player is using
        public new void Collect()
        {
            Debug.Assert(Name != null, "Item name should not be null.");
            base.Collect();
            Console.WriteLine($"The {Name} has {Damage} damage.");
        }
    }

    /// <summary>
    /// When a player uses a potion, it will give them health
    /// </summary>
    public class Potion : Item
    {
        public int HealAmount { get; set; }
        public Potion(string name, int healAmount) : base(name, $"Adds {healAmount} health.")
        {
            HealAmount = healAmount;
        }

        // Prompts to the user which potion they drink
        public override void Use()
        {
            Console.WriteLine($"You drink the {Name}.");
        }

        // Displays to the user the potion's health amount
        public new void Collect()
        {
            base.Collect();
            Console.WriteLine($"The {Name} heals {HealAmount} health.");
        }
    }

    // Displays to the user what happens when they use a key
    public class Key : Item
    {
        public Key(string name) : base (name, "")
        {
        }
        public override void Use()
        {
            Console.WriteLine($"You hold the {Name} in your hand.");
        }
    }

    /// <summary>
    /// List of items
    /// </summary>
    public static class ItemList
    {
        // List of weapons
        public static List<Weapon> Weapons = new List<Weapon>
        {
            new Weapon("Knife", 10),
            new Weapon("Axe", 20),
            new Weapon("Sword", 50),
            new Weapon("Mace", 75),
            new Weapon("Doomfang Sword", 100),
            new Weapon("Blade", 30),
            new Weapon("Obsidian Blade", 80),
        };

        // List of potions
        public static List<Potion> Potions = new List<Potion>
        {
            new Potion("Small Healing Potion", 25),
            new Potion("Medium Healing Potion", 50),
            new Potion("Large Healing Potion", 75)
        };

        // List of keys
        public static List<Item> Keys = new List<Item>
        {
            new Key("Apothecary Key")
        };
    }
}
