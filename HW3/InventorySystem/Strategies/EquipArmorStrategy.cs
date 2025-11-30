using InventorySystem.Interfaces;
using InventorySystem.Models;
using InventorySystem.States;
using System;

namespace InventorySystem.Strategies
{
    public class EquipArmorStrategy : IItemUsageStrategy
    {
        public void Use(Item item, PlayerContext context)
        {
            if (item is Armor armor)
            {
                // При экипировке брони увеличиваем защиту игрока
                context.Defense += armor.Defense;
                item.IsEquipped = true;
                
                // Использование брони уменьшает ее прочность
                armor.Durability -= 1;
                CheckDurability(armor);
            }
        }
        
        private void CheckDurability(Armor armor)
        {
            if (armor.Durability <= 0)
            {
                armor.State = new BrokenState();
            }
            else if (armor.Durability < armor.MaxDurability / 2)
            {
                armor.State = new WornState();
            }
        }
    }
}