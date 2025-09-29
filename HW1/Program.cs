using System;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ДОБРО ПОЖАЛОВАТЬ В ВЕНДИНГОВЫЙ АВТОМАТ ===");
            Console.WriteLine();

            var vendingMachine = new VendingMachineCore();

            while (true)
            {
                ShowMainMenu();
                string? choice = Console.ReadLine();

                bool isRunning = true;
                bool shouldRun = isRunning;

                if (shouldRun == true)
                {
                    switch (choice)
                    {
                        case "1":
                            vendingMachine.DisplayProducts();
                            Console.WriteLine("Нажмите Enter для продолжения...");
                            Console.ReadLine();
                            break;
                        case "2":
                            InsertCoinsMenu(vendingMachine);
                            break;
                        case "3":
                            vendingMachine.DisplayInsertedCoins();
                            Console.WriteLine("Нажмите Enter для продолжения...");
                            Console.ReadLine();
                            break;
                        case "4":
                            PurchaseProductMenu(vendingMachine);
                            break;
                        case "5":
                            vendingMachine.ReturnCoins();
                            Console.WriteLine("Нажмите Enter для продолжения...");
                            Console.ReadLine();
                            break;
                        case "6":
                            if (vendingMachine.AdminLogin())
                            {
                                vendingMachine.AdminMenu();
                            }
                            break;
                        case "0":
                            Console.WriteLine("Спасибо за использование вендингового автомата!");
                            return;
                        default:
                            Console.WriteLine("Некорректный выбор!");
                            break;
                    }
                }
            }
        }

        static void ShowMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=== ГЛАВНОЕ МЕНЮ ===");
            Console.WriteLine("1. Посмотреть товары");
            Console.WriteLine("2. Вставить монеты");
            Console.WriteLine("3. Проверить внесенную сумму");
            Console.WriteLine("4. Купить товар");
            Console.WriteLine("5. Вернуть деньги");
            Console.WriteLine("6. Администраторский режим");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");
        }

        static void InsertCoinsMenu(VendingMachineCore vendingMachine)
        {
            Console.WriteLine("\n=== ВСТАВКА МОНЕТ ===");
            Console.WriteLine("Принимаются монеты: 1, 2, 5, 10, 50 рублей");
            Console.WriteLine("Введите номинал монеты (0 - выход):");

            while (true)
            {
                string? input = Console.ReadLine();

                if (input == "0")
                {
                    break;
                }

                if (decimal.TryParse(input, out decimal coinValue))
                {
                    vendingMachine.InsertCoin(coinValue);
                    vendingMachine.DisplayInsertedCoins();
                    Console.WriteLine("\nВведите следующую монету или 0 для выхода:");
                }
                else
                {
                    Console.WriteLine("Некорректный ввод! Попробуйте еще раз:");
                }
            }
        }

        static void PurchaseProductMenu(VendingMachineCore vendingMachine)
        {
            Console.WriteLine("\n=== ПОКУПКА ТОВАРА ===");
            vendingMachine.DisplayProducts();
            vendingMachine.DisplayInsertedCoins();

            Console.Write("\nВведите код товара (0 - отмена): ");
            string? input = Console.ReadLine();

            if (input == "0")
            {
                return;
            }

            if (int.TryParse(input, out int productCode))
            {
                bool success = vendingMachine.PurchaseProduct(productCode);
                if (success)
                {
                    Console.WriteLine("Покупка завершена успешно!");
                }
            }
            else
            {
                Console.WriteLine("Некорректный код товара!");
            }

            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
        }
    }
}
