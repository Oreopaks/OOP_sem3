using System;
using InventorySystem.Interfaces;

namespace InventorySystem.Models
{
    public class Potion : Item
    {
        public EffectType Effect { get; set; }
        public int Potency { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsConsumable { get; set; }
        
        public Potion()
        {
            Type = ItemType.Potion;
            IsConsumable = true;
        }
        
        public override string GetInfo()
        {
            return base.GetInfo() + $" | Effect: {Effect} | Potency: {Potency} | Duration: {Duration}";
        }
    }
}