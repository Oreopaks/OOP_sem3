using InventorySystem.Interfaces;
using InventorySystem.Models;
using System;

namespace InventorySystem.States
{
    public class NewState : IItemState
    {
        public string Name => "New";
        
        public void Use(Item item, PlayerContext context)
        {
            // Новые предметы можно использовать без ограничений
            item.UsageStrategy?.Use(item, context);
        }
        
        public void Repair(Item item, RepairMaterial material)
        {
            // Новые предметы не нуждаются в ремонте
            throw new InvalidOperationException("New items don't need repair");
        }
    }
}