using InventorySystem.Interfaces;
using InventorySystem.Models;
using System;

namespace InventorySystem.States
{
    public class WornState : IItemState
    {
        public string Name => "Worn";
        
        public void Use(Item item, PlayerContext context)
        {
            // Используем предмет, но с возможностью ухудшения состояния
            item.UsageStrategy?.Use(item, context);
            
            // При использовании изношенного предмета есть шанс, что он сломается
            var random = new Random();
            if (random.NextDouble() < 0.3) // 30% шанс сломаться
            {
                if (item is Weapon weapon)
                {
                    weapon.State = new BrokenState();
                }
                else if (item is Armor armor)
                {
                    armor.State = new BrokenState();
                }
            }
        }
        
        public void Repair(Item item, RepairMaterial material)
        {
            // Ремонт изношенного предмета может восстановить его до нового состояния
            if (item is Weapon weapon)
            {
                weapon.Durability += material.RepairValue;
                if (weapon.Durability >= weapon.MaxDurability)
                {
                    weapon.State = new NewState();
                    weapon.Durability = weapon.MaxDurability;
                }
            }
            else if (item is Armor armor)
            {
                armor.Durability += material.RepairValue;
                if (armor.Durability >= armor.MaxDurability)
                {
                    armor.State = new NewState();
                    armor.Durability = armor.MaxDurability;
                }
            }
        }
    }
}