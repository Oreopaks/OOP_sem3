using System;
using InventorySystem.Interfaces;

namespace InventorySystem.Models
{
    public class Armor : Item
    {
        public int Defense { get; set; }
        public ArmorType ArmorType { get; set; }
        public int Durability { get; set; }
        public int MaxDurability { get; set; }
        public IUpgradeStrategy UpgradeStrategy { get; set; } = null!;
        
        public Armor()
        {
            Type = ItemType.Armor;
        }
        
        public override string GetInfo()
        {
            return base.GetInfo() + $" | Defense: {Defense} | Durability: {Durability}/{MaxDurability}";
        }
    }
}