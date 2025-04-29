using System;
using System.Formats.Asn1;
using System.Runtime;

namespace DungeonExplorer
{
    /// <summary>
    ///  Represents monsters in rooms with unique behaviours
    /// </summary>
    public class Monster : Creature
    {
        public Monster(string name, int damage, int health) : base(name, damage, health) { }

        // Displays the amount of damage the monster has taken when the player attacks
        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Console.WriteLine($"{Name} takes {damage} damage!");
        }
    }

    /// <summary>
    /// Dragon's behaviours
    /// </summary>
    public class Dragon : Monster 
    {
        public Dragon() : base("Dragon", 20, 300) { }  // Dragon deals 20 health, has 300 health
        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Console.WriteLine("The Dragon roars!");
        }
    }

    /// <summary>
    /// Basilisk's behaviours
    /// </summary>
    public class Basilisk : Monster
    {
        public Basilisk() : base("Basilisk", 25, 300) { }  // Basilisk deals 25 health, has 300 health
        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Console.WriteLine("The Basilisk hisses!");
        }
    }

    /// <summary>
    /// Acromantula's behaviours
    /// </summary>
    public class Acromantula : Monster
    {
        public Acromantula() : base("Acromantula", 10, 300) { }  // Acromantula deals 10 health, has 300 health
        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Console.WriteLine("The Acromantula chirps!");
        }
    }

    /// <summary>
    /// Erkling's behaviours
    /// </summary>
    public class Erkling : Monster
    {
        public Erkling() : base("Erkling", 5, 70) { }  // Erkling deals 5 health, has 70 health

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Console.WriteLine("The erkling squeaks!");
        }
    }
}
