using System;
using Xunit;
using InventorySystem.States;
using InventorySystem.Models;
using InventorySystem.Strategies;
using InventorySystem.Interfaces;

namespace InventorySystem.Tests
{
    public class StateTests
    {
        [Fact]
        public void BrokenState_Use_ShouldThrow()
        {
            // Arrange
            var brokenState = new BrokenState();
            var weapon = new Weapon
            {
                Name = "Broken Sword",
                Damage = 5,
                Durability = 0,
                MaxDurability = 100
            };
            var context = new PlayerContext();
            
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => 
                brokenState.Use(weapon, context));
        }
        
        [Fact]
        public void WornState_Use_ShouldReduceDurability()
        {
            // Arrange
            var wornState = new WornState();
            var weapon = new Weapon
            {
                Name = "Worn Sword",
                Damage = 10,
                Durability = 50,
                MaxDurability = 100
            };
            weapon.UsageStrategy = new EquipWeaponStrategy();
            var context = new PlayerContext();
            
            // Act
            wornState.Use(weapon, context);
            
            // Assert
            // Прочность уменьшится на 1 при использовании
            Assert.Equal(49, weapon.Durability);
        }
        
        [Fact]
        public void NewState_Use_ShouldAllowUsage()
        {
            // Arrange
            var newState = new NewState();
            var weapon = new Weapon
            {
                Name = "Iron Sword",
                Damage = 10,
                Durability = 100,
                MaxDurability = 100
            };
            weapon.UsageStrategy = new EquipWeaponStrategy();
            var context = new PlayerContext();
            
            // Act
            newState.Use(weapon, context);
            
            // Assert
            // Прочность уменьшится на 1 при использовании
            Assert.Equal(99, weapon.Durability);
        }
    }
}