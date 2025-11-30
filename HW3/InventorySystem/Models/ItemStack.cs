using System;
using System.Collections.Generic;

namespace InventorySystem.Models
{
    public class ItemStack
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }
        
        public ItemStack(Item item, int quantity = 1)
        {
            Item = item;
            Quantity = quantity;
        }
        
        public bool IsStackable()
        {
            // Зелья и другие расходники могут быть стекуемыми
            return Item.Type == ItemType.Potion || 
                   (Item is Potion potion && potion.IsConsumable);
        }
        
        public string GetInfo()
        {
            if (Quantity > 1 && IsStackable())
            {
                return $"{Item.GetInfo()} x{Quantity}";
            }
            return Item.GetInfo();
        }
    }
}