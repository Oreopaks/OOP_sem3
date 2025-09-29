using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine
{
    public class VendingMachineCore
    {
        private List<Product> products;
        private Dictionary<decimal, int> coinInventory; 
        private Dictionary<decimal, int> insertedCoins; 
        private decimal totalInserted;

        public VendingMachineCore()
        {
            products = new List<Product>();
            coinInventory = new Dictionary<decimal, int>();
            insertedCoins = new Dictionary<decimal, int>();
            totalInserted = 0;

            InitializeProducts();
            InitializeCoins();
        }

        private void InitializeProducts()
        {
            products.Add(new Product("Кока-кола", 50.00m, 10, 1));
            products.Add(new Product("Чипсы", 45.00m, 8, 2));
            products.Add(new Product("Шоколад", 60.00m, 12, 3));
            products.Add(new Product("Сок", 40.00m, 15, 4));
            products.Add(new Product("Печенье", 35.00m, 20, 5));
        }

        private void InitializeCoins()
        {
            var coinValues = new decimal[] { 1, 2, 5, 10, 50 };
            foreach (var value in coinValues)
            {
                coinInventory[value] = 20;
                insertedCoins[value] = 0;
            }
        }

        public void DisplayProducts()
        {
            Console.WriteLine("\n=== ДОСТУПНЫЕ ТОВАРЫ ===");
            Console.WriteLine("Код | Товар                | Цена      | Количество");
            Console.WriteLine("----+----------------------+-----------+-----------");
            
            foreach (var product in products)
            {
                if (product.Quantity > 0)
                {
                    Console.WriteLine(product.ToString());
                }
                else
                {
                    Console.WriteLine($"{product.Code:D2} | {product.Name,-20} | {product.Price} руб. | НЕТ В НАЛИЧИИ");
                }
            }
        }

        public void DisplayInsertedCoins()
        {
            Console.WriteLine($"\nВнесено денег: {totalInserted} руб.");
            
            if (insertedCoins.Any(c => c.Value > 0))
            {
                Console.WriteLine("Внесеные монеты:");
                foreach (var coin in insertedCoins.Where(c => c.Value > 0))
                {
                    Console.WriteLine($"  {coin.Key} руб. x {coin.Value}");
                }
            }
        }

        public void InsertCoin(decimal coinValue)
        {
            if (insertedCoins.ContainsKey(coinValue))
            {
                insertedCoins[coinValue]++;
                totalInserted += coinValue;
                coinInventory[coinValue]++;
                Console.WriteLine($"Принята монета {coinValue} руб. Общая сумма: {totalInserted} руб.");
            }
            else
            {
                Console.WriteLine("Неверный номинал монеты! Принимаются: 1, 2, 5, 10, 50 рублей");
            }
        }

        public bool PurchaseProduct(int productCode)
        {
            var product = products.FirstOrDefault(p => p.Code == productCode);
            
            if (product == null)
            {
                Console.WriteLine("Товар с таким кодом не найден!");
                return false;
            }
            
            if (product.Quantity <= 0)
            {
                Console.WriteLine("Товар закончился!");
                return false;
            }
            
            if (totalInserted < product.Price)
            {
                Console.WriteLine($"Недостаточно средств! Нужно еще {product.Price - totalInserted} руб.");
                return false;
            }
            
            decimal change = totalInserted - product.Price;
            totalInserted -= product.Price;
            product.Quantity--;
            
            Console.WriteLine($"Товар '{product.Name}' выдан!");
            
            if (change > 0)
            {
                if (GiveChange(change))
                {
                    Console.WriteLine($"Выдана сдача: {change} руб.");
                }
                else
                {
                    totalInserted += change;
                    Console.WriteLine($"Сдача {change} руб. остается на вашем балансе для дальнейших покупок");
                }
            }
            Console.WriteLine($"Остаток на вашем балансе: {totalInserted} руб.");
            
            return true;
        }

        private bool GiveChange(decimal amount)
        {
            var changeCoins = new Dictionary<decimal, int>();
            var availableCoins = coinInventory.Where(c => c.Value > 0).OrderByDescending(c => c.Key);
            
            decimal remainingAmount = amount;
            
            foreach (var coin in availableCoins)
            {
                if (remainingAmount >= coin.Key && coin.Value > 0)
                {
                    int coinsNeeded = (int)(remainingAmount / coin.Key);
                    int coinsAvailable = coin.Value;
                    int coinsToGive = Math.Min(coinsNeeded, coinsAvailable);
                    
                    if (coinsToGive > 0)
                    {
                        changeCoins[coin.Key] = coinsToGive;
                        remainingAmount -= coin.Key * coinsToGive;
                        coinInventory[coin.Key] -= coinsToGive;
                    }
                }
            }
            
            if (remainingAmount > 0.01m)
            {
                foreach (var coin in changeCoins)
                {
                    coinInventory[coin.Key] += coin.Value;
                }
                return false;
            }
            
            foreach (var coin in changeCoins)
            {
                Console.WriteLine($"Сдача: {coin.Key} руб. x {coin.Value}");
            }
            
            return true;
        }

        public void ReturnCoins()
        {
            if (totalInserted > 0)
            {
                Console.WriteLine($"\nВозвращаем ваши деньги: {totalInserted} руб.");
                foreach (var coin in insertedCoins.Where(c => c.Value > 0))
                {
                    coinInventory[coin.Key] -= coin.Value;
                    Console.WriteLine($"Выданы деньги на сумму: {totalInserted} руб.");
                }
                
                foreach (var key in insertedCoins.Keys.ToList())
                {
                    insertedCoins[key] = 0;
                }
                totalInserted = 0;
            }
            else
            {
                Console.WriteLine("Нет денег для возврата");
            }
        }

        public bool AdminLogin()
        {
            Console.WriteLine("Переход в администраторский режим");
            return true;
        }

        public void AdminMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=== АДМИН ПАНЕЛЬ ===");
                Console.WriteLine("1. Пополнить товары");
                Console.WriteLine("2. Собрать деньги");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                
                bool isContinue = true;
                bool shouldContinue = isContinue;

                if (shouldContinue == true)
                {
                    switch (choice)
                    {
                        case "1":
                            RestockProducts();
                            break;
                        case "2":
                            CollectMoney();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Некорректный выбор!");
                            break;
                    }
                }
            }
        }

        private void RestockProducts()
        {
            Console.WriteLine("\nСписок товаров:");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Code}. {product.Name} - Текущее количество: {product.Quantity}");
            }
            
            Console.Write("Введите код товара для пополнения: ");
            if (int.TryParse(Console.ReadLine(), out int code))
            {
                var product = products.FirstOrDefault(p => p.Code == code);
                if (product != null)
                {
                    Console.Write("Введите количество для добавления: ");
                    if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                    {
                        product.Quantity += quantity;
                        Console.WriteLine($"Товар '{product.Name}' пополнен на {quantity} единиц");
                    }
                    else
                    {
                        Console.WriteLine("Некорректное количество!");
                    }
                }
                else
                {
                    Console.WriteLine("Товар не найден!");
                }
            }
            else
            {
                Console.WriteLine("Некорректный код товара!");
            }
        }

        private void CollectMoney()
        {
            decimal totalMoney = 0;
            Console.WriteLine("Собранные деньги:");
            
            bool hasCollectedCoins = false;
            foreach (var coin in insertedCoins)
            {
                if (coin.Value > 0)
                {
                    decimal coinTotal = coin.Key * coin.Value;
                    totalMoney += coinTotal;
                    Console.WriteLine($"{coin.Key} руб. x {coin.Value} = {coinTotal} руб.");
                    hasCollectedCoins = true;
                }
            }

            if (!hasCollectedCoins)
            {
                Console.WriteLine("Нет собранных денег.");
                return;
            }

            Console.WriteLine($"Общая сумма: {totalMoney} руб.");
            Console.Write("Собрать все деньги? (y/n): ");
            
            if (Console.ReadLine()?.ToLower() == "y")
            {
                foreach (var key in insertedCoins.Keys.ToList())
                {
                    insertedCoins[key] = 0;
                }
                Console.WriteLine($"Собрано {totalMoney} руб.");
            }
        }
    }
}
