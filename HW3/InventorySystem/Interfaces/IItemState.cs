using InventorySystem.Models;

namespace InventorySystem.Interfaces
{
    public interface IItemState
    {
        string Name { get; }
        void Use(Item item, PlayerContext context);
        void Repair(Item item, RepairMaterial material);
    }
}