namespace VendingMachine
{
    public class Coin
    {
        public decimal Value { get; set; }
        public int Count { get; set; }

        public Coin(decimal value, int count = 0)
        {
            Value = value;
            Count = count;
        }

        public override string ToString()
        {
            return $"{Value:C} x {Count}";
        }
    }
}
