using InventorySystem.Interfaces;
using InventorySystem.Models;
using InventorySystem.States;
using System;

namespace InventorySystem.Services
{
    public class UpgradeService : IUpgradeStrategy
    {
        public Item Upgrade(Item item, IUpgradeMaterial material)
        {
            // Создаем копию предмета с улучшенными характеристиками
            switch (item.Type)
            {
                case ItemType.Weapon when item is Weapon weapon:
                    return UpgradeWeapon(weapon, material);
                case ItemType.Armor when item is Armor armor:
                    return UpgradeArmor(armor, material);
                default:
                    throw new InvalidOperationException("Cannot upgrade this item type");
            }
        }
        
        private Weapon UpgradeWeapon(Weapon weapon, IUpgradeMaterial material)
        {
            // Логика улучшения оружия
            var random = new Random();
            var successChance = 0.7; // 70% шанс успеха
            
            var upgradedWeapon = new Weapon
            {
                Name = weapon.Name,
                Description = weapon.Description,
                Weight = weapon.Weight,
                WeaponType = weapon.WeaponType,
                MaxDurability = weapon.MaxDurability,
                State = weapon.State
            };
            
            if (random.NextDouble() < successChance)
            {
                // Успешное улучшение
                upgradedWeapon.Damage = (int)(weapon.Damage * 1.2); // +20% урона
                upgradedWeapon.Durability = weapon.Durability; // Сохраняем текущую прочность
                upgradedWeapon.Name = $"Upgraded {weapon.Name}";
            }
            else
            {
                // Неудачное улучшение - предмет теряет прочность
                upgradedWeapon.Damage = weapon.Damage; // Урон не изменяется
                upgradedWeapon.Durability = Math.Max(1, weapon.Durability - material.UpgradeValue); // Теряем прочность
                
                // Если прочность упала до нуля, предмет ломается
                if (upgradedWeapon.Durability <= 0)
                {
                    upgradedWeapon.Durability = 0;
                    upgradedWeapon.State = new BrokenState();
                }
                else if (upgradedWeapon.Durability < upgradedWeapon.MaxDurability / 2)
                {
                    upgradedWeapon.State = new WornState();
                }
            }
            
            return upgradedWeapon;
        }
        
        private Armor UpgradeArmor(Armor armor, IUpgradeMaterial material)
        {
            // Логика улучшения брони
            var random = new Random();
            var successChance = 0.6; // 60% шанс успеха
            
            var upgradedArmor = new Armor
            {
                Name = armor.Name,
                Description = armor.Description,
                Weight = armor.Weight,
                ArmorType = armor.ArmorType,
                MaxDurability = armor.MaxDurability,
                State = armor.State
            };
            
            if (random.NextDouble() < successChance)
            {
                // Успешное улучшение
                upgradedArmor.Defense = (int)(armor.Defense * 1.2); // +20% защиты
                upgradedArmor.Durability = armor.Durability; // Сохраняем текущую прочность
                upgradedArmor.Name = $"Upgraded {armor.Name}";
            }
            else
            {
                // Неудачное улучшение - предмет теряет прочность
                upgradedArmor.Defense = armor.Defense; // Защита не изменяется
                upgradedArmor.Durability = Math.Max(1, armor.Durability - material.UpgradeValue); // Теряем прочность
                
                // Если прочность упала до нуля, предмет ломается
                if (upgradedArmor.Durability <= 0)
                {
                    upgradedArmor.Durability = 0;
                    upgradedArmor.State = new BrokenState();
                }
                else if (upgradedArmor.Durability < upgradedArmor.MaxDurability / 2)
                {
                    upgradedArmor.State = new WornState();
                }
            }
            
            return upgradedArmor;
        }
    }
}