namespace Shopping_Center
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using Wintellect.PowerCollections;

    public class Product : IComparable<Product>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Producer { get; set; }

        public Product(string name, decimal price, string producer)
        {
            this.Name = name;
            this.Price = price;
            this.Producer = producer;
        }


        public int CompareTo(Product other)
        {
            var comparer = this.Name.CompareTo(other.Name);
            if (comparer == 0)
            {
                comparer = this.Producer.CompareTo(other.Producer);
                if (comparer == 0)
                {
                    comparer = this.Price.CompareTo(other.Price);
                }
            }

            return comparer;
        }
    }

    public class MyShoppingCenter
    {
        public Dictionary<string, OrderedBag<Product>> ProductsByName { get; set; }
        public Dictionary<string, OrderedBag<Product>> ProductsByProducer { get; set; }
        public OrderedDictionary<decimal, Product> ProductsByPrice { get; set; }
        public Dictionary<string, SortedSet<Product>> ProductsByNameAndProducer { get; set; }

        public string AddProduct(string name, decimal price, string producer)
        {
            var p = new Product(name, price, producer);

            if (this.ProductsByName.ContainsKey(name))
            {
                this
            }

        }

        private string PrintResult(IEnumerable<Product> products)
        {
            return null;
        }
    }
}
