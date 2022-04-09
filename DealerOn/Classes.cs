using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasketTest
{
    internal class Item
    {
        public int Quantity { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public bool IsExempt { get; set; }
        public bool IsImport { get; set; }

        string[] taxExempt = { "Chocolate bar", "Book", "Packet of headache pills", "Imported box of chocolates" };

        public Item(int quantity, string type, decimal price)
        {
            Quantity = quantity;
            Type = type;
            Price = price;
            IsExempt = taxExempt.Contains(type) ? true : false;
            IsImport = type.Contains("Imported") ? true : false;
        }
    }

    internal class Basket
    {
        public List<Item> Items { get; set; }

        public Basket()
        {
            Items = new List<Item>();
        }
    }
}
