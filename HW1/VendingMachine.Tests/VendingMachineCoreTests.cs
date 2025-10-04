using Xunit;
using VendingMachine;
using System.IO;
using System;

namespace VendingMachine.Tests
{
    public class VendingMachineCoreTests
    {
        [Fact]
        public void CreateVendingMachine_Test()
        {
            VendingMachineCore machine = new VendingMachineCore();

            Assert.Equal(0, machine.GetTotalInserted());
            Assert.Equal(5, machine.GetProducts().Count);
        }

        [Fact]
        public void InsertMoney_Test()
        {
            VendingMachineCore machine = new VendingMachineCore();
            
            Console.SetOut(new StringWriter());

            machine.InsertCoin(10m);

            Assert.Equal(10m, machine.GetTotalInserted());
        }

        [Fact]
        public void BuyProduct_WithEnoughMoney_Test()
        {
            VendingMachineCore machine = new VendingMachineCore();
            
            Console.SetOut(new StringWriter());

            machine.InsertCoin(50m);

            bool success = machine.PurchaseProduct(1);

            Assert.True(success);
            
            Product? product = machine.GetProductByCode(1);
            Assert.NotNull(product);
            Assert.Equal(9, product.Quantity);
        }

        [Fact]
        public void BuyProduct_WithoutMoney_Test()
        {
            VendingMachineCore machine = new VendingMachineCore();
            
            Console.SetOut(new StringWriter());

            bool success = machine.PurchaseProduct(1);

            Assert.False(success);
            
            Product? product = machine.GetProductByCode(1);
            Assert.NotNull(product);
            Assert.Equal(10, product.Quantity);
        }
    }
}
