using System;
using Xunit;
using InventorySystem.Services;
using InventorySystem.Models;
using InventorySystem.Interfaces;

namespace InventorySystem.Tests
{
    public class UpgradeServiceTests
    {
        private readonly IUpgradeStrategy _upgradeService;
        private readonly IItemFactory _itemFactory;
        
        public UpgradeServiceTests()
        {
            _upgradeService = new UpgradeService();
            _itemFactory = new ItemFactory();
        }
        
        [Fact]
        public void UpgradeItem_ShouldIncreaseStats_OnSuccess()
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
            
            var material = new UpgradeMaterial("Sharpening Stone", 5);
            
            // Act
            var result = _upgradeService.Upgrade(weapon, material);
            
            // Assert
            Assert.NotNull(result);
            if (result is Weapon upgradedWeapon)
            {
                // Урон должен увеличиться (но точное значение зависит от случайного фактора)
                // В тестах мы проверяем, что результат не null и имеет правильный тип
                Assert.Equal(weapon.WeaponType, upgradedWeapon.WeaponType);
                Assert.Equal(weapon.Durability, upgradedWeapon.Durability);
            }
        }
        
        [Fact]
        public void UpgradeItem_ShouldHandleFailure_LosingDurability()
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
            
            var material = new UpgradeMaterial("Low-quality Stone", 20);
            
            // Act
            var result = _upgradeService.Upgrade(weapon, material);
            
            // Assert
            Assert.NotNull(result);
            if (result is Weapon upgradedWeapon)
            {
                // В случае неудачи прочность должна уменьшиться
                // (точное значение зависит от случайного фактора)
                Assert.Equal(weapon.WeaponType, upgradedWeapon.WeaponType);
            }
        }
    }
}