using Xunit;
using VendingMachine;

namespace VendingMachine.Tests
{
    public class CoinTests
    {
        [Fact]
        public void CreateCoin_Test()
        {
            Coin coin = new Coin(10m, 5);

            Assert.Equal(10m, coin.Value);
            Assert.Equal(5, coin.Count);
        }

        [Fact]
        public void CreateCoinWithoutCount_Test()
        {
            Coin coin = new Coin(5m);

            Assert.Equal(5m, coin.Value);
            Assert.Equal(0, coin.Count);
        }

        [Fact]
        public void CoinToString_Test()
        {
            Coin coin = new Coin(10m, 3);

            string text = coin.ToString();

            Assert.Contains("10", text);
            Assert.Contains("3", text);
        }
    }
}
