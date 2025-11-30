using InventorySystem.Models;

namespace InventorySystem.Interfaces
{
    // Спецификации для создания предметов
    public class WeaponSpecification
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Weight { get; set; }
        public int Damage { get; set; }
        public WeaponType WeaponType { get; set; }
        public int Durability { get; set; }
    }
    
    public class ArmorSpecification
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Weight { get; set; }
        public int Defense { get; set; }
        public ArmorType ArmorType { get; set; }
        public int Durability { get; set; }
    }
    
    public class PotionSpecification
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Weight { get; set; }
        public EffectType Effect { get; set; }
        public int Potency { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsConsumable { get; set; }
    }
    
    public interface IItemFactory
    {
        Weapon CreateWeapon(WeaponSpecification spec);
        Armor CreateArmor(ArmorSpecification spec);
        Potion CreatePotion(PotionSpecification spec);
        QuestItem CreateQuestItem(string name, string description, int weight);
    }
}