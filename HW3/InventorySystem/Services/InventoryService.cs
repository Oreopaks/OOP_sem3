using System;
using System.Collections.Generic;
using System.Linq;
using InventorySystem.Interfaces;
using InventorySystem.Models;
using InventorySystem.Repositories;

namespace InventorySystem.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;
        private readonly IItemFactory _itemFactory;
        private readonly ICombiner _combiner;
        private readonly IUpgradeStrategy _upgradeStrategy;
        
        public InventoryService(
            IInventoryRepository repository, 
            IItemFactory itemFactory,
            ICombiner combiner,
            IUpgradeStrategy upgradeStrategy)
        {
            _repository = repository;
            _itemFactory = itemFactory;
            _combiner = combiner;
            _upgradeStrategy = upgradeStrategy;
        }
        
        public void AddItem(Guid playerId, Item item, int quantity = 1)
        {
            var itemStack = new ItemStack(item, quantity);
            _repository.AddItem(playerId, itemStack);
        }
        
        public void RemoveItem(Guid playerId, Guid itemId, int quantity = 1)
        {
            _repository.RemoveItem(playerId, itemId, quantity);
        }
        
        public IEnumerable<ItemStack> GetItems(Guid playerId)
        {
            return _repository.GetItems(playerId);
        }
        
        public void EquipItem(Guid playerId, Guid itemId)
        {
            var itemStack = _repository.FindItem(playerId, itemId);
            if (itemStack == null)
                throw new ArgumentException("Item not found in inventory");
                
            // Проверяем, что предмет еще не экипирован
            if (itemStack.Item.IsEquipped)
                throw new InvalidOperationException("Item is already equipped");
                
            // Используем предмет (экипируем)
            var context = new PlayerContext();
            itemStack.Item.Use(context);
            
            // Обновляем предмет в репозитории
            _repository.UpdateItem(playerId, itemStack);
        }
        
        public void UnequipItem(Guid playerId, Guid itemId)
        {
            var itemStack = _repository.FindItem(playerId, itemId);
            if (itemStack == null)
                throw new ArgumentException("Item not found in inventory");
                
            // Проверяем, что предмет экипирован
            if (!itemStack.Item.IsEquipped)
                throw new InvalidOperationException("Item is not equipped");
                
            // Снимаем экипировку
            itemStack.Item.IsEquipped = false;
            
            // Обновляем предмет в репозитории
            _repository.UpdateItem(playerId, itemStack);
        }
        
        public void UseItem(Guid playerId, Guid itemId)
        {
            var itemStack = _repository.FindItem(playerId, itemId);
            if (itemStack == null)
                throw new ArgumentException("Item not found in inventory");
                
            // Используем предмет
            var context = new PlayerContext();
            itemStack.Item.Use(context);
            
            // Если предмет стекуемый и расходуемый, уменьшаем количество
            if (itemStack.IsStackable() && itemStack.Quantity > 1)
            {
                itemStack.Quantity -= 1;
                _repository.UpdateItem(playerId, itemStack);
            }
            else if (itemStack.IsStackable() && itemStack.Quantity == 1)
            {
                // Если это был последний предмет в стеке, удаляем стек
                _repository.RemoveItem(playerId, itemId, 1);
            }
            
            // Обновляем предмет в репозитории (если он не был удален)
            if (itemStack.Quantity > 0)
            {
                _repository.UpdateItem(playerId, itemStack);
            }
        }
        
        public void CombineItems(Guid playerId, Guid firstItemId, Guid secondItemId)
        {
            var firstItemStack = _repository.FindItem(playerId, firstItemId);
            var secondItemStack = _repository.FindItem(playerId, secondItemId);
            
            if (firstItemStack == null || secondItemStack == null)
                throw new ArgumentException("One or both items not found in inventory");
                
            // Комбинируем предметы
            var newItem = _combiner.Combine(firstItemStack.Item, secondItemStack.Item);
            
            // Удаляем старые предметы
            _repository.RemoveItem(playerId, firstItemId, firstItemStack.Quantity);
            _repository.RemoveItem(playerId, secondItemId, secondItemStack.Quantity);
            
            // Добавляем новый предмет
            AddItem(playerId, newItem);
        }
        
        public Item UpgradeItem(Guid playerId, Guid itemId, IUpgradeMaterial material)
        {
            var itemStack = _repository.FindItem(playerId, itemId);
            if (itemStack == null)
                throw new ArgumentException("Item not found in inventory");
                
            // Улучшаем предмет
            var upgradedItem = _upgradeStrategy.Upgrade(itemStack.Item, material);
            
            // Заменяем старый предмет на улучшенный
            itemStack.Item = upgradedItem;
            _repository.UpdateItem(playerId, itemStack);
            
            return upgradedItem;
        }
    }
}