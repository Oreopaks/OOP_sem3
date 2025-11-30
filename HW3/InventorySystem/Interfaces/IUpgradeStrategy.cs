using InventorySystem.Models;

namespace InventorySystem.Interfaces
{
    public interface IUpgradeMaterial
    {
        string Name { get; }
        int UpgradeValue { get; }
    }
    
    public interface IUpgradeStrategy
    {
        Item Upgrade(Item item, IUpgradeMaterial material);
    }
}