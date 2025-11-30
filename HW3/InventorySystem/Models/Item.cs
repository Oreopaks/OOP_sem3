using System;
using InventorySystem.Interfaces;

namespace InventorySystem.Models
{
    public abstract class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ItemType Type { get; set; }
        public IItemState State { get; set; } = null!;
        public IItemUsageStrategy UsageStrategy { get; set; } = null!;
        public int Weight { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsEquipped { get; set; }
        
        protected Item()
        {
            Id = Guid.NewGuid();
            IsEquipped = false;
        }
        
        public virtual string GetInfo()
        {
            return $"[{Type}] {Name} ({State.Name}) - {Description}";
        }
        
        public void Use(PlayerContext context)
        {
            State.Use(this, context);
        }
    }
}