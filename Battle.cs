using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace DungeonExplorer
{
    /// <summary>
    /// Manages the battle between the player and monster
    /// </summary>
    public class Battle
    {
        private Player _player;
        private Monster _monster;
        private Weapon _equippedWeapon;

        public Battle(Player player, Monster monster)
        {
            _player = player;
            _monster = monster;
        }

        /// <summary>
        /// Starts the battle between the monster and player
        /// </summary>
        public void Start()
        {
            // If player has no weapons equipped, the game will be over
            if (!HasWeapons())
            {
                Console.WriteLine("You have no weapons to attack with! Game over.");
                Environment.Exit(0);
            }

            bool battle = true;
            while (battle)
            {
                // Asks the player if they want to equip a weapon or a potion
                Console.WriteLine("\nWhat would you like to do?" +
                "\n1. Equip a weapon" +
                "\n2. Equip a potion" +
                "\nEnter '1' or '2':");

                string battleChoice = Console.ReadLine();
                if (battleChoice == "1")
                {
                    // Prompts player to equip weapon
                    EquipWeapon();
                    break;
                }
                else if (battleChoice == "2")
                {
                    // Prompts player to equip a potion
                    EquipPotion();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please select again.");
                }
            }
        }

        /// <summary>
        /// Checks if the player has weapons in their inventory
        /// </summary>
        /// <returns></returns>
        public bool HasWeapons()
        {
            var weapons = _player.Inventory.GetItems().OfType<Weapon>().ToList();
             return weapons.Any();
        }

        /// <summary>
        /// Allows the player to equip a weapon of choice to battle with
        /// </summary>
        private void EquipWeapon()
        {
            var weapons = _player.Inventory.GetItems().OfType<Weapon>().ToList();
            
            // If player has no weapons in their inventory, they won't be able to equip a weapon
            if (weapons.Count == 0)
            {
                Console.WriteLine("You have no weapons in your inventory.");
                ContinueBattle();
                return;
            }

            _player.Inventory.ShowWeapons();  // Displays the weapons in the player's inventory
            // Prompts the player to enter the weapon they want
            Console.WriteLine("Enter the name of the weapon you want to equip: ");
            string weaponChoice = Console.ReadLine();
            Weapon weapon = ItemList.Weapons.FirstOrDefault(w => w.Name.ToLower() == weaponChoice.ToLower());

            // Player equips the weapon they choose
            if (weapon != null)
            {
                _equippedWeapon = weapon;
                Console.WriteLine($"You have equipped the {weapon.Name}.");
                ChoiceAfterEquipWeapon();
            }
            // If invalid input, the player will be asked again.
            else
            {
                Console.WriteLine("Invalid weapon choice.");
                EquipWeapon();
            }
        }

        /// <summary>
        /// After the player has equipped a weapon, they can choose to attack or switch to a potion.
        /// </summary>
        private void ChoiceAfterEquipWeapon()
        {
            Console.WriteLine("\nWhat would you like to do now?" +
            "\n1. Attack" +
            "\n2. Switch to potion" +
            "\nEnter '1' or '2':");
            string actionChoice = Console.ReadLine();

            // Player attacks the monster
            if (actionChoice == "1")
            {
                Attack();
            }
            // Player can equip a potion
            else if (actionChoice == "2")
            {
                EquipPotion();
            }
            // If invalid input, player is asked what they want to do again
            else
            {
                Console.WriteLine("Invalid input.");
                ChoiceAfterEquipWeapon();
            }
        }
        /// <summary>
        /// Allows the player to equip a potion of choice to use in the battle
        /// </summary>
        private void EquipPotion()
        {
            var potions = _player.Inventory.GetItems().OfType<Potion>().ToList();
            
            // If the player has no potions in their inventory, they won't be able to equip a potion
            if (potions.Count == 0)
            {
                Console.WriteLine("You have no potions in your inventory.");
                ContinueBattle();
                return;
            }

            // Player is prompted to select a potion they want to equip
            _player.Inventory.ShowPotions();
            Console.WriteLine("Enter the name of the potion you want to equip: ");
            string potionChoice = Console.ReadLine();
            Potion potion = ItemList.Potions.FirstOrDefault(p => p.Name.ToLower() == potionChoice.ToLower());

            // Player equips poition
            if (potion != null)
            {
                Console.WriteLine($"You have equipped the {potion.Name}.");
                ChoiceAfterEquipPoition(potion);
            }
            // If invalid choice, player will be asked to equip potion again
            else
            {
                Console.WriteLine("Invalid potion choice.");
                EquipPotion();
            }
        }

        /// <summary>
        /// Player is asked if they want to use the potion or switch to a weapon.
        /// </summary>
        /// <param name="potion"></param>
        private void ChoiceAfterEquipPoition(Potion potion)
        {
            string actionChoice = "";
            while (actionChoice != "1" && actionChoice != "2")
            {
                Console.WriteLine("\nWhat would you like to do now?" +
                "\n1. Use the potion" +
                "\n2. Switch to weapon" +
                "\nEnter '1' or '2':");

                actionChoice = Console.ReadLine();
                // Player uses the potion
                if (actionChoice == "1")
                {
                    UsePotion(potion);
                }
                // Player is asked to equip a weapon
                else if (actionChoice == "2")
                {
                    EquipWeapon();
                }
                // If invalid input, player will be asked again
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
        }

        /// <summary>
        /// Player uses the potion they equipped.
        /// </summary>
        /// <param name="potion"></param>
        private void UsePotion(Potion potion)
        {
            potion.Use();  // Displays to the player the effect of using the potion 
            _player.Health += potion.HealAmount; // Adds health to player
            _player.Inventory.RemoveItem(potion);  // Removes the potion from inventory after use
            Console.WriteLine($"Your health is now {_player.Health}.");  // Displays the health to the player
            ContinueBattle();
        }

        /// <summary>
        /// The player attacks the monster they are battling with
        /// </summary>
        private void Attack()
        {
            // Asks the user to equip a weapon if they do not have one equipped
            if (_equippedWeapon == null)
            {
                Console.WriteLine("You don't have a weapon equipped.");
                EquipWeapon();
                return;
            }

            _equippedWeapon.Use();  // Displays to the user how they attacked the monster

            int damage = _equippedWeapon.Damage;
            _player.DealDamage(_monster, damage);  // Takes away health from the monster

            // Displays to the user that they have defeated the monster
            if (_monster.Health <= 0)
            {
                Console.WriteLine($"You have defeated the {_monster.Name}!");

                // The basilisk drops the apothecary key for the player
                if (_monster.Name == "Basilisk")
                {
                    Console.WriteLine("The Basilisk drops the Apothecary Key!");
                    _player.Inventory.PickUpItem(_player, "Apothecary Key");  // Player choose to pick up the key
                }

                // Displays the statistics of how many monsters the player has defeated
                Statistics.MonsterDefeated();
                Statistics.DisplayStatistics();
            }
            // Displays to the user the player and monster's health
            else
            {
                Console.WriteLine($"{_monster.Name}'s health is now {_monster.Health}.");
                MonsterAttack();
                Console.WriteLine($"{_player.Name}'s health is now {_player.Health}.");
                ContinueBattle();
            }
        }

        /// <summary>
        /// Monster attacks the player and takes away the player's health
        /// </summary>
        private void MonsterAttack()
        {
            // Monster attacks the player based on the monster's damage
            int damage = _monster.Damage;
            Console.WriteLine($"\n{_monster.Name} attacks you, dealing {damage} damage!");

            _player.TakeDamage(damage); // Player takes the damage

            // Checks if the player is still alive 
            if (_player.Health <= 0)
            {
                Console.WriteLine("You have been defeated!");
                Environment.Exit(0);  // Game will end
            }
        }

        /// <summary>
        /// Asks the user if they want to attack or use a potion
        /// </summary>
        private void ContinueBattle()
        {
            string actionChoice = "";
            while (actionChoice != "1" && actionChoice != "2")
            {
                Console.WriteLine("\nWhat would you like to do next?" +
                "\n1. Attack" +
                "\n2. Equip a potion" +
                "\nEnter '1' or '2':");

                actionChoice = Console.ReadLine();
                if (actionChoice == "1")
                {
                    Attack();
                }
                 else if (actionChoice == "2")
                 {
                    EquipPotion();
                 }
                 else
                 {
                    Console.WriteLine("Invalid input.");
                 }
            }
        }
    }
}
