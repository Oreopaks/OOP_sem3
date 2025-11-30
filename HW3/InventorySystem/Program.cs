using System;
using InventorySystem.Services;
using InventorySystem.Models;
using InventorySystem.Repositories;
using InventorySystem.States;
using InventorySystem.Strategies;
using InventorySystem.Interfaces;

namespace InventorySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем необходимые сервисы
            var repository = new InMemoryInventoryRepository();
            var itemFactory = new ItemFactory();
            var combiner = new CombinerService(itemFactory);
            var upgradeStrategy = new UpgradeService();
            
            // Создаем сервис инвентаря
            var inventoryService = new InventoryService(repository, itemFactory, combiner, upgradeStrategy);
            
            // Создаем игрока
            var playerId = Guid.NewGuid();
            Console.WriteLine($"Player ID: {playerId}");
            
            // Создаем предметы
            var sword = itemFactory.CreateWeapon(new WeaponSpecification
            {
                Name = "Iron Sword",
                Description = "A sturdy iron sword",
                Weight = 5,
                Damage = 10,
                WeaponType = WeaponType.Sword,
                Durability = 100
            });
            
            // Назначаем стратегию использования для оружия
            sword.UsageStrategy = new EquipWeaponStrategy();
            
            var healthPotion = itemFactory.CreatePotion(new PotionSpecification
            {
                Name = "Health Potion",
                Description = "Restores 50 health points",
                Weight = 1,
                Effect = EffectType.Heal,
                Potency = 50,
                Duration = TimeSpan.FromSeconds(0),
                IsConsumable = true
            });
            
            // Назначаем стратегию использования для зелья
            healthPotion.UsageStrategy = new ConsumePotionStrategy();
            
            var armor = itemFactory.CreateArmor(new ArmorSpecification
            {
                Name = "Leather Armor",
                Description = "Light armor made of leather",
                Weight = 10,
                Defense = 5,
                ArmorType = ArmorType.Chest,
                Durability = 80
            });
            
            // Назначаем стратегию использования для брони
            armor.UsageStrategy = new EquipArmorStrategy();
            
            // Добавляем предметы в инвентарь
            inventoryService.AddItem(playerId, sword);
            inventoryService.AddItem(playerId, healthPotion, 3); // Добавляем 3 зелья
            inventoryService.AddItem(playerId, armor);
            
            Console.WriteLine("Items added to inventory:");
            var items = inventoryService.GetItems(playerId);
            foreach (var itemStack in items)
            {
                Console.WriteLine($"  {itemStack.GetInfo()}");
            }
            
            // Экипируем оружие
            Console.WriteLine("\nEquipping sword...");
            inventoryService.EquipItem(playerId, sword.Id);
            
            // Используем зелье
            Console.WriteLine("\nUsing health potion...");
            inventoryService.UseItem(playerId, healthPotion.Id);
            
            // Проверяем инвентарь после использования
            Console.WriteLine("\nInventory after using potion:");
            items = inventoryService.GetItems(playerId);
            foreach (var itemStack in items)
            {
                Console.WriteLine($"  {itemStack.GetInfo()}");
            }
            
            Console.WriteLine("\nDemo completed.");
        }
    }
}