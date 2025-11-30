using System;
using System.Collections.Generic;
using System.Linq;
using InventorySystem.Models;

namespace InventorySystem.Repositories
{
    public interface IInventoryRepository
    {
        void AddItem(Guid playerId, ItemStack itemStack);
        void RemoveItem(Guid playerId, Guid itemId, int quantity);
        IEnumerable<ItemStack> GetItems(Guid playerId);
        ItemStack FindItem(Guid playerId, Guid itemId);
        void UpdateItem(Guid playerId, ItemStack itemStack);
    }
    
    public class InMemoryInventoryRepository : IInventoryRepository
    {
        private readonly Dictionary<Guid, List<ItemStack>> _inventories;
        
        public InMemoryInventoryRepository()
        {
            _inventories = new Dictionary<Guid, List<ItemStack>>();
        }
        
        public void AddItem(Guid playerId, ItemStack itemStack)
        {
            if (!_inventories.ContainsKey(playerId))
            {
                _inventories[playerId] = new List<ItemStack>();
            }
            
            // Проверяем, есть ли уже такой предмет в инвентаре (для стекуемых предметов)
            var existingStack = _inventories[playerId]
                .FirstOrDefault(stack => stack.Item.Id == itemStack.Item.Id && stack.IsStackable());
                
            if (existingStack != null && existingStack.IsStackable())
            {
                existingStack.Quantity += itemStack.Quantity;
            }
            else
            {
                _inventories[playerId].Add(itemStack);
            }
        }
        
        public void RemoveItem(Guid playerId, Guid itemId, int quantity)
        {
            if (!_inventories.ContainsKey(playerId))
                return;
                
            var itemStack = _inventories[playerId]
                .FirstOrDefault(stack => stack.Item.Id == itemId);
                
            if (itemStack == null)
                return;
                
            if (itemStack.Quantity <= quantity)
            {
                _inventories[playerId].Remove(itemStack);
            }
            else
            {
                itemStack.Quantity -= quantity;
            }
        }
        
        public IEnumerable<ItemStack> GetItems(Guid playerId)
        {
            if (!_inventories.ContainsKey(playerId))
                return new List<ItemStack>();
                
            return _inventories[playerId];
        }
        
        public ItemStack FindItem(Guid playerId, Guid itemId)
        {
            if (!_inventories.ContainsKey(playerId))
                return null!;
                
            return _inventories[playerId]
                .FirstOrDefault(stack => stack.Item.Id == itemId) ?? null!;
        }
        
        public void UpdateItem(Guid playerId, ItemStack itemStack)
        {
            if (!_inventories.ContainsKey(playerId))
                return;
                
            var existingStack = _inventories[playerId]
                .FirstOrDefault(stack => stack.Item.Id == itemStack.Item.Id);
                
            if (existingStack != null)
            {
                // Заменяем существующий стек на обновленный
                var index = _inventories[playerId].IndexOf(existingStack);
                _inventories[playerId][index] = itemStack;
            }
        }
    }
}