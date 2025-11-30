using System;

namespace InventorySystem.Models
{
    public class PlayerContext
    {
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Damage { get; set; }
        
        public PlayerContext()
        {
            Health = 100;
            Mana = 100;
            Strength = 10;
            Defense = 0;
            Damage = 0;
        }
    }
}