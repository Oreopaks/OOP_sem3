using System;
using InventorySystem.Interfaces;

namespace InventorySystem.Models
{
    public class Weapon : Item
    {
        public int Damage { get; set; }
        public WeaponType WeaponType { get; set; }
        public int Durability { get; set; }
        public int MaxDurability { get; set; }
        public IUpgradeStrategy UpgradeStrategy { get; set; } = null!;
        
        public Weapon()
        {
            Type = ItemType.Weapon;
        }
        
        public override string GetInfo()
        {
            return base.GetInfo() + $" | Damage: {Damage} | Durability: {Durability}/{MaxDurability}";
        }
    }
}