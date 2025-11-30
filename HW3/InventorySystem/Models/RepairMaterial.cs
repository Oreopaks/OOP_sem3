using System;

namespace InventorySystem.Models
{
    public class RepairMaterial
    {
        public string Name { get; set; }
        public int RepairValue { get; set; }
        
        public RepairMaterial(string name, int repairValue)
        {
            Name = name;
            RepairValue = repairValue;
        }
    }
}