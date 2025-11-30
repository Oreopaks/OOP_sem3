using InventorySystem.Interfaces;
using InventorySystem.Models;
using System;

namespace InventorySystem.Strategies
{
    public class QuestItemUsageStrategy : IItemUsageStrategy
    {
        public void Use(Item item, PlayerContext context)
        {
            if (item is QuestItem questItem && questItem.IsQuestItem)
            {
                // Квестовые предметы обычно нельзя использовать обычным способом
                throw new InvalidOperationException("Quest items cannot be used this way");
            }
        }
    }
}