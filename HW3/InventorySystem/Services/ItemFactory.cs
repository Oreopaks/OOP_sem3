using InventorySystem.Interfaces;
using InventorySystem.Models;
using InventorySystem.States;

namespace InventorySystem.Services
{
    public class ItemFactory : IItemFactory
    {
        public Weapon CreateWeapon(WeaponSpecification spec)
        {
            return new Weapon
            {
                Name = spec.Name,
                Description = spec.Description,
                Weight = spec.Weight,
                Damage = spec.Damage,
                WeaponType = spec.WeaponType,
                Durability = spec.Durability,
                MaxDurability = spec.Durability,
                State = new NewState(),
                UpgradeStrategy = null!
            };
        }
        
        public Armor CreateArmor(ArmorSpecification spec)
        {
            return new Armor
            {
                Name = spec.Name,
                Description = spec.Description,
                Weight = spec.Weight,
                Defense = spec.Defense,
                ArmorType = spec.ArmorType,
                Durability = spec.Durability,
                MaxDurability = spec.Durability,
                State = new NewState()
            };
        }
        
        public Potion CreatePotion(PotionSpecification spec)
        {
            return new Potion
            {
                Name = spec.Name,
                Description = spec.Description,
                Weight = spec.Weight,
                Effect = spec.Effect,
                Potency = spec.Potency,
                Duration = spec.Duration,
                IsConsumable = spec.IsConsumable,
                State = new NewState()
            };
        }
        
        public QuestItem CreateQuestItem(string name, string description, int weight)
        {
            return new QuestItem
            {
                Name = name,
                Description = description,
                Weight = weight,
                State = new NewState()
            };
        }
    }
}