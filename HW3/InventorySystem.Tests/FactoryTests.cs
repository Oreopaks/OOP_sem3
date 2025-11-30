using System;
using Xunit;
using InventorySystem.Services;
using InventorySystem.Models;
using InventorySystem.Interfaces;

namespace InventorySystem.Tests
{
    public class FactoryTests
    {
        private readonly IItemFactory _itemFactory;
        
        public FactoryTests()
        {
            _itemFactory = new ItemFactory();
        }
        
        [Fact]
        public void ItemFactory_ShouldCreateWeapon_WithCorrectProperties()
        {
            // Arrange
            var spec = new WeaponSpecification
            {
                Name = "Iron Sword",
                Description = "A sturdy iron sword",
                Weight = 5,
                Damage = 10,
                WeaponType = WeaponType.Sword,
                Durability = 100
            };
            
            // Act
            var weapon = _itemFactory.CreateWeapon(spec);
            
            // Assert
            Assert.NotNull(weapon);
            Assert.Equal(spec.Name, weapon.Name);
            Assert.Equal(spec.Description, weapon.Description);
            Assert.Equal(spec.Weight, weapon.Weight);
            Assert.Equal(spec.Damage, weapon.Damage);
            Assert.Equal(spec.WeaponType, weapon.WeaponType);
            Assert.Equal(spec.Durability, weapon.Durability);
            Assert.Equal(spec.Durability, weapon.MaxDurability);
            Assert.Equal(ItemType.Weapon, weapon.Type);
        }
        
        [Fact]
        public void ItemFactory_ShouldCreateArmor_WithCorrectProperties()
        {
            // Arrange
            var spec = new ArmorSpecification
            {
                Name = "Leather Armor",
                Description = "Light armor made of leather",
                Weight = 10,
                Defense = 5,
                ArmorType = ArmorType.Chest,
                Durability = 80
            };
            
            // Act
            var armor = _itemFactory.CreateArmor(spec);
            
            // Assert
            Assert.NotNull(armor);
            Assert.Equal(spec.Name, armor.Name);
            Assert.Equal(spec.Description, armor.Description);
            Assert.Equal(spec.Weight, armor.Weight);
            Assert.Equal(spec.Defense, armor.Defense);
            Assert.Equal(spec.ArmorType, armor.ArmorType);
            Assert.Equal(spec.Durability, armor.Durability);
            Assert.Equal(spec.Durability, armor.MaxDurability);
            Assert.Equal(ItemType.Armor, armor.Type);
        }
        
        [Fact]
        public void ItemFactory_ShouldCreatePotion_WithCorrectProperties()
        {
            // Arrange
            var spec = new PotionSpecification
            {
                Name = "Health Potion",
                Description = "Restores 50 health points",
                Weight = 1,
                Effect = EffectType.Heal,
                Potency = 50,
                Duration = TimeSpan.FromSeconds(10),
                IsConsumable = true
            };
            
            // Act
            var potion = _itemFactory.CreatePotion(spec);
            
            // Assert
            Assert.NotNull(potion);
            Assert.Equal(spec.Name, potion.Name);
            Assert.Equal(spec.Description, potion.Description);
            Assert.Equal(spec.Weight, potion.Weight);
            Assert.Equal(spec.Effect, potion.Effect);
            Assert.Equal(spec.Potency, potion.Potency);
            Assert.Equal(spec.Duration, potion.Duration);
            Assert.Equal(spec.IsConsumable, potion.IsConsumable);
            Assert.Equal(ItemType.Potion, potion.Type);
        }
        
        [Fact]
        public void ItemFactory_ShouldCreateQuestItem_WithCorrectProperties()
        {
            // Arrange
            var name = "Ancient Artifact";
            var description = "A mysterious artifact from ancient times";
            var weight = 3;
            
            // Act
            var questItem = _itemFactory.CreateQuestItem(name, description, weight);
            
            // Assert
            Assert.NotNull(questItem);
            Assert.Equal(name, questItem.Name);
            Assert.Equal(description, questItem.Description);
            Assert.Equal(weight, questItem.Weight);
            Assert.True(questItem.IsQuestItem);
            Assert.Equal(ItemType.QuestItem, questItem.Type);
        }
    }
}