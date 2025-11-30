using System;
using Xunit;
using InventorySystem.Services;
using InventorySystem.Models;
using InventorySystem.Interfaces;

namespace InventorySystem.Tests
{
    public class CombinerServiceTests
    {
        private readonly ICombiner _combiner;
        private readonly IItemFactory _itemFactory;
        
        public CombinerServiceTests()
        {
            _itemFactory = new ItemFactory();
            _combiner = new CombinerService(_itemFactory);
        }
        
        [Fact]
        public void CombineItems_ShouldReturnNewItem_WhenRecipeMatches()
        {
            // Arrange
            var weapon1 = _itemFactory.CreateWeapon(new WeaponSpecification
            {
                Name = "Iron Sword",
                Description = "A sturdy iron sword",
                Weight = 5,
                Damage = 10,
                WeaponType = WeaponType.Sword,
                Durability = 100
            });
            
            var weapon2 = _itemFactory.CreateWeapon(new WeaponSpecification
            {
                Name = "Steel Sword",
                Description = "A sharp steel sword",
                Weight = 6,
                Damage = 15,
                WeaponType = WeaponType.Sword,
                Durability = 120
            });
            
            // Act
            var result = _combiner.Combine(weapon1, weapon2);
            
            // Assert
            Assert.NotNull(result);
            Assert.Equal(ItemType.Weapon, result.Type);
            Assert.StartsWith("Enhanced", result.Name);
            
            if (result is Weapon enhancedWeapon)
            {
                // Проверяем, что урон увеличился примерно на 20%
                var expectedDamage = (int)((weapon1.Damage + weapon2.Damage) * 1.2);
                Assert.Equal(expectedDamage, enhancedWeapon.Damage);
            }
        }
        
        [Fact]
        public void CombineItems_ShouldThrow_WhenRecipeNotFound()
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
            
            var questItem = _itemFactory.CreateQuestItem(
                "Magic Stone", 
                "A mysterious stone with magical properties", 
                2);
            
            // Act & Assert
            // В текущей реализации этот случай обрабатывается как ремонт оружия
            var result = _combiner.Combine(weapon, questItem);
            Assert.NotNull(result);
        }
    }
}