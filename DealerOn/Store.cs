using System;
using System.IO;

namespace ShoppingBasketTest
{
    class Store
    {
        Basket customerOrder = new Basket();

        public void ReadFile()
        {
            Console.WriteLine("Enter test #:");
            string testNumber = Console.ReadLine();
            string textFile = @$"C:\Users\Username\Desktop\test{testNumber}.txt";
            using (StreamReader file = new StreamReader(textFile))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    string[] orderLine = ln.Split(" at ");
                    string[] quantityAndType = orderLine[0].Split(new[] { ' ' }, 2);
                    Item i = new Item(Int32.Parse(quantityAndType[0]), quantityAndType[1], Decimal.Parse(orderLine[1]));
                    customerOrder.Items.Add(i);
                }
                file.Close();
            }
        }

        public void CombineItems()
        {
            List<int> toDelete = new List<int>();
            for (int i = 0; i < customerOrder.Items.Count; i++)
            {
                if (toDelete.Contains(i))
                {
                    continue;
                }
                else
                {
                    for (int j = i + 1; j < customerOrder.Items.Count; j++)
                    {
                        if (customerOrder.Items[i].Type == customerOrder.Items[j].Type)
                        {
                            customerOrder.Items[i].Quantity += customerOrder.Items[j].Quantity;
                            toDelete.Add(j);
                        }
                    }
                }
                
            }
            for (int k = 0; k < toDelete.Count; k++)
            {
                customerOrder.Items.RemoveAt(toDelete[k]);
            }
        }

        public void TotalOrder()
        {
            decimal total = 0;
            decimal taxes = 0;
            decimal regTax = (decimal).10;
            decimal importTax = (decimal).05;

            foreach (Item i in customerOrder.Items)
            {
                decimal itemTax = 0;
                if (i.IsImport)
                {
                    itemTax += i.Price * importTax;
                }
                itemTax += i.IsExempt ? 0 : i.Price * regTax;
                itemTax = Math.Ceiling(itemTax * 20) / 20;
                decimal newPrice = (i.Price + itemTax) * i.Quantity;
                taxes += itemTax;
                total += newPrice;
                if (i.Quantity > 1)
                {
                    Console.WriteLine($"{i.Type}: {newPrice} ({i.Quantity} @ {(i.Price+itemTax)})");
                }
                else
                {
                    Console.WriteLine($"{i.Type}: {newPrice}");
                }
            }
            
            Console.WriteLine($"Sales Taxes: {String.Format("{0:0.00}", taxes)}");
            Console.WriteLine($"Total: {total}");
        }

        static void Main(string[] args)
        {
            Store s = new();
            s.ReadFile();
            s.CombineItems();
            s.TotalOrder();  
        }
    }
}
