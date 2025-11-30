using InventorySystem.Interfaces;
using InventorySystem.Models;
using System;

namespace InventorySystem.States
{
    public class BrokenState : IItemState
    {
        public string Name => "Broken";
        
        public void Use(Item item, PlayerContext context)
        {
            // Сломанные предметы нельзя использовать
            throw new InvalidOperationException("Cannot use broken item");
        }
        
        public void Repair(Item item, RepairMaterial material)
        {
            // Ремонт сломанного предмета восстанавливает его до изношенного состояния
            // Проверяем, что предмет имеет свойство Durability
            if (item is Weapon weapon)
            {
                weapon.Durability += material.RepairValue;
                if (weapon.Durability > 0)
                {
                    weapon.State = new WornState();
                }
            }
            else if (item is Armor armor)
            {
                armor.Durability += material.RepairValue;
                if (armor.Durability > 0)
                {
                    armor.State = new WornState();
                }
            }
        }
    }
}