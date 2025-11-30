using InventorySystem.Interfaces;

namespace InventorySystem.Models
{
    public class UpgradeMaterial : IUpgradeMaterial
    {
        public string Name { get; set; }
        public int UpgradeValue { get; set; }
        
        public UpgradeMaterial(string name, int upgradeValue)
        {
            Name = name;
            UpgradeValue = upgradeValue;
        }
    }
}