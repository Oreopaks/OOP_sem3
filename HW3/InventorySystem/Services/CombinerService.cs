using InventorySystem.Interfaces;
using InventorySystem.Models;
using InventorySystem.States;
using System;

namespace InventorySystem.Services
{
    public class CombinerService : ICombiner
    {
        private readonly IItemFactory _itemFactory;
        
        public CombinerService(IItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }
        
        public Item Combine(Item a, Item b)
        {
            // Простая логика комбинирования:
            // 1. Если комбинируем два одинаковых оружия, создаем улучшенное оружие
            // 2. Если комбинируем сломанное оружие с материалом, чиним его
            // 3. Если комбинируем два зелья одного типа, создаем более мощное зелье
            
            if (a.Type == ItemType.Weapon && b.Type == ItemType.Weapon)
            {
                return CombineWeapons((Weapon)a, (Weapon)b);
            }
            else if (a.Type == ItemType.Potion && b.Type == ItemType.Potion)
            {
                return CombinePotions((Potion)a, (Potion)b);
            }
            else if (a.Type == ItemType.Weapon && b.Type == ItemType.QuestItem)
            {
                return RepairWeaponWithMaterial((Weapon)a, b);
            }
            
            throw new InvalidOperationException("Cannot combine these items");
        }
        
        private Weapon CombineWeapons(Weapon weapon1, Weapon weapon2)
        {
            // Комбинирование двух оружий создает новое, более мощное оружие
            var newDamage = (int)((weapon1.Damage + weapon2.Damage) * 1.2); // 20% бонус
            var newDurability = (weapon1.Durability + weapon2.Durability) / 2;
            
            return _itemFactory.CreateWeapon(new WeaponSpecification
            {
                Name = $"Enhanced {weapon1.Name}",
                Description = $"Combined from {weapon1.Name} and {weapon2.Name}",
                Weight = (weapon1.Weight + weapon2.Weight) / 2,
                Damage = newDamage,
                WeaponType = weapon1.WeaponType,
                Durability = newDurability
            });
        }
        
        private Potion CombinePotions(Potion potion1, Potion potion2)
        {
            // Комбинирование двух зелий одного типа создает более мощное зелье
            if (potion1.Effect != potion2.Effect)
                throw new InvalidOperationException("Cannot combine potions of different types");
                
            var newPotency = (int)((potion1.Potency + potion2.Potency) * 1.5); // 50% бонус
            
            return _itemFactory.CreatePotion(new PotionSpecification
            {
                Name = $"Super {potion1.Name}",
                Description = $"Combined from two {potion1.Name}",
                Weight = potion1.Weight,
                Effect = potion1.Effect,
                Potency = newPotency,
                Duration = TimeSpan.FromTicks(Math.Max(potion1.Duration.Ticks, potion2.Duration.Ticks)),
                IsConsumable = true
            });
        }
        
        private Weapon RepairWeaponWithMaterial(Weapon weapon, Item material)
        {
            // Ремонт оружия с помощью квестового предмета (материала)
            var repairedWeapon = new Weapon
            {
                Name = weapon.Name,
                Description = weapon.Description,
                Weight = weapon.Weight,
                Damage = weapon.Damage,
                WeaponType = weapon.WeaponType,
                Durability = weapon.MaxDurability, // Восстанавливаем до максимальной прочности
                MaxDurability = weapon.MaxDurability,
                State = new NewState()
            };
            
            return repairedWeapon;
        }
    }
}