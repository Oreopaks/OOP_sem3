namespace VendingMachine
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Code { get; set; }

        public Product(string name, decimal price, int quantity, int code)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            Code = code;
        }

        public override string ToString()
        {
            return $"{Code:D2} | {Name,-20} | {Price} руб.     | {Quantity}";
        }
    }
}
