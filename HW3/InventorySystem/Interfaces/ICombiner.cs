using InventorySystem.Models;

namespace InventorySystem.Interfaces
{
    public interface ICombiner
    {
        Item Combine(Item a, Item b);
    }
}