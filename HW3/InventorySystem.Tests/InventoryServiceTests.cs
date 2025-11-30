using System;
using System.Linq;
using Xunit;
using InventorySystem.Services;
using InventorySystem.Models;
using InventorySystem.Repositories;
using InventorySystem.Interfaces;
using InventorySystem.States;
using InventorySystem.Strategies;

namespace InventorySystem.Tests
{
    public class InventoryServiceTests
    {
        private readonly IInventoryService _inventoryService;
        private readonly IItemFactory _itemFactory;
        private readonly IInventoryRepository _repository;
        private readonly ICombiner _combiner;
        private readonly IUpgradeStrategy _upgradeStrategy;
        private readonly Guid _playerId;
        
        public InventoryServiceTests()
        {
            _playerId = Guid.NewGuid();
            _repository = new InMemoryInventoryRepository();
            _itemFactory = new ItemFactory();
            _combiner = new CombinerService(_itemFactory);
            _upgradeStrategy = new UpgradeService();
            _inventoryService = new InventoryService(_repository, _itemFactory, _combiner, _upgradeStrategy);
        }
        
        [Fact]
        public void AddItem_ShouldIncreaseCount_WhenItemValid()
        {
            // Arrange
            var weapon = _itemFactory.CreateWeapon(new WeaponSpecification
            {
                Name = "Iron Sword",
                Description = "A sturdy iron sword",
                Weight = 5,
                Damage = 10,
                WeaponType = WeaponType.Sword,
                Durability = 100
            });
            
            // Act
            _inventoryService.AddItem(_playerId, weapon);
            
            // Assert
            var items = _inventoryService.GetItems(_playerId);
            Assert.Single(items);
            Assert.Equal(weapon.Id, items.First().Item.Id);
        }
        
        [Fact]
        public void RemoveItem_ShouldDecreaseCount_WhenItemExists()
        {
            // Arrange
            var potion = _itemFactory.CreatePotion(new PotionSpecification
            {
                Name = "Health Potion",
                Description = "Restores 50 health points",
                Weight = 1,
                Effect = EffectType.Heal,
                Potency = 50,
                Duration = TimeSpan.FromSeconds(0),
                IsConsumable = true
            });
            
            _inventoryService.AddItem(_playerId, potion, 3);
            
            // Act
            _inventoryService.RemoveItem(_playerId, potion.Id, 1);
            
            // Assert
            var items = _inventoryService.GetItems(_playerId);
            Assert.Single(items);
            Assert.Equal(2, items.First().Quantity);
        }
        
        
        [Fact]
        public void EquipItem_ShouldMarkItemAsEquipped_AndApplyEffectsToPlayer()
        {
            // Arrange
            var weapon = _itemFactory.CreateWeapon(new WeaponSpecification
            {
                Name = "Iron Sword",
                Description = "A sturdy iron sword",
                Weight = 5,
                Damage = 10,
                WeaponType = WeaponType.Sword,
                Durability = 100
            });
            
            // Назначаем стратегию использования
            weapon.UsageStrategy = new EquipWeaponStrategy();
            
            _inventoryService.AddItem(_playerId, weapon);
            
            // Act
            _inventoryService.EquipItem(_playerId, weapon.Id);
            
            // Assert
            var items = _inventoryService.GetItems(_playerId);
            var equippedWeapon = items.First().Item as Weapon;
            Assert.NotNull(equippedWeapon);
            Assert.True(equippedWeapon.IsEquipped);
            Assert.Equal(99, equippedWeapon.Durability); // Прочность уменьшилась на 1
        }
        
        [Fact]
        public void UnequipItem_ShouldRemoveEquipEffects()
        {
            // Arrange
            var weapon = _itemFactory.CreateWeapon(new WeaponSpecification
            {
                Name = "Iron Sword",
                Description = "A sturdy iron sword",
                Weight = 5,
                Damage = 10,
                WeaponType = WeaponType.Sword,
                Durability = 100
            });
            
            // Назначаем стратегию использования
            weapon.UsageStrategy = new EquipWeaponStrategy();
            
            _inventoryService.AddItem(_playerId, weapon);
            _inventoryService.EquipItem(_playerId, weapon.Id);
            
            // Act
            _inventoryService.UnequipItem(_playerId, weapon.Id);
            
            // Assert
            var items = _inventoryService.GetItems(_playerId);
            var unequippedWeapon = items.First().Item as Weapon;
            Assert.NotNull(unequippedWeapon);
            Assert.False(unequippedWeapon.IsEquipped);
        }
        
        [Fact]
        public void UseItem_Potion_ShouldApplyEffectAndDecreaseStack()
        {
            // Arrange
            var potion = _itemFactory.CreatePotion(new PotionSpecification
            {
                Name = "Health Potion",
                Description = "Restores 50 health points",
                Weight = 1,
                Effect = EffectType.Heal,
                Potency = 50,
                Duration = TimeSpan.FromSeconds(0),
                IsConsumable = true
            });
            
            // Назначаем стратегию использования
            potion.UsageStrategy = new ConsumePotionStrategy();
            
            _inventoryService.AddItem(_playerId, potion, 2);
            
            // Act
            _inventoryService.UseItem(_playerId, potion.Id);
            
            // Assert
            var items = _inventoryService.GetItems(_playerId);
            Assert.Single(items);
            Assert.Equal(1, items.First().Quantity);
        }
        
        [Fact]
        public void UseItem_BrokenItem_ShouldThrow()
        {
            // Arrange
            var weapon = _itemFactory.CreateWeapon(new WeaponSpecification
            {
                Name = "Iron Sword",
                Description = "A sturdy iron sword",
                Weight = 5,
                Damage = 10,
                WeaponType = WeaponType.Sword,
                Durability = 100
            });
            
            // Устанавливаем состояние предмета как сломанное
            weapon.State = new BrokenState();
            weapon.UsageStrategy = new EquipWeaponStrategy();
            
            _inventoryService.AddItem(_playerId, weapon);
            
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => 
                _inventoryService.UseItem(_playerId, weapon.Id));
        }
    }
}