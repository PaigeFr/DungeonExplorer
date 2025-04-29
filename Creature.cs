using System;

namespace DungeonExplorer
{
    public abstract class Creature : IDamageable
    {
        public string Name { get; protected set; }
        public int Health { get; set; }
        public int Damage { get; set; }

        public Creature(string name, int damage, int health)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }

        /// <summary>
        /// Takes health away from the monster
        /// </summary>
        /// <param name="damage"></param>
        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
        }

       /// <summary>
       /// Deals damage to the monster the player is battling with
       /// </summary>
       /// <param name="target"></param>
       /// <param name="damage"></param>
       public virtual void DealDamage(Creature target, int damage)
       {
        Console.WriteLine($"{Name} deals {damage} damage to {target.Name}!");
        target.TakeDamage(damage);
       }
    }
}
