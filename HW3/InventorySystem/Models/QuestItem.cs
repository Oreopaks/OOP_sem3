using System;
using InventorySystem.Interfaces;

namespace InventorySystem.Models
{
    public class QuestItem : Item
    {
        public bool IsQuestItem { get; set; }
        
        public QuestItem()
        {
            Type = ItemType.QuestItem;
            IsQuestItem = true;
        }
        
        public override string GetInfo()
        {
            return base.GetInfo() + " | Quest Item";
        }
    }
}