using InventorySystem.Models;

namespace InventorySystem.Interfaces
{
    public interface IItemUsageStrategy
    {
        void Use(Item item, PlayerContext context);
    }
}