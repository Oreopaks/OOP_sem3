using InventorySystem.Interfaces;
using InventorySystem.Models;
using InventorySystem.States;
using System;

namespace InventorySystem.Strategies
{
    public class EquipWeaponStrategy : IItemUsageStrategy
    {
        public void Use(Item item, PlayerContext context)
        {
            if (item is Weapon weapon)
            {
                // При экипировке оружия увеличиваем урон игрока
                context.Damage += weapon.Damage;
                item.IsEquipped = true;
                
                // Использование оружия уменьшает его прочность
                weapon.Durability -= 1;
                CheckDurability(weapon);
            }
        }
        
        private void CheckDurability(Weapon weapon)
        {
            if (weapon.Durability <= 0)
            {
                weapon.State = new BrokenState();
            }
            else if (weapon.Durability < weapon.MaxDurability / 2)
            {
                weapon.State = new WornState();
            }
        }
    }
}