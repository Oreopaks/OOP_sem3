using System;
using System.Collections.Generic;
using InventorySystem.Models;
using InventorySystem.Interfaces;

namespace InventorySystem.Interfaces
{
    public interface IInventoryService
    {
        void AddItem(Guid playerId, Item item, int quantity = 1);
        void RemoveItem(Guid playerId, Guid itemId, int quantity = 1);
        IEnumerable<ItemStack> GetItems(Guid playerId);
        void EquipItem(Guid playerId, Guid itemId);
        void UnequipItem(Guid playerId, Guid itemId);
        void UseItem(Guid playerId, Guid itemId);
        void CombineItems(Guid playerId, Guid firstItemId, Guid secondItemId);
        Item UpgradeItem(Guid playerId, Guid itemId, IUpgradeMaterial material);
    }
}