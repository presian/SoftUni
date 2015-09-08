namespace Shopping_Center
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Wintellect.PowerCollections;

    class ShoppingCenterMain
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var center = new MyShoppingCenter();

            int commands = int.Parse(Console.ReadLine());
            for (int i = 1; i <= commands; i++)
            {
                string command = Console.ReadLine();
                string commandResult = center.ProcessCommand(command);
                Console.WriteLine(commandResult);
            }
        }
    }


    class Product : IComparable<global::Product>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Producer { get; set; }
        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            return $"{{{this.Name};{this.Producer};{this.Price:0.00}}}";
        }

        public int CompareTo(global::Product other)
        {
            var nameComareResult = this.Name.CompareTo(other.Name);
            if (nameComareResult == 0)
            {
                var producerCompareResult = this.Producer.CompareTo(other.Producer);
                if (producerCompareResult == 0)
                {
                    return this.Price.CompareTo(other.Price);
                }
                else
                {
                    return producerCompareResult;
                }
            }
            else
            {
                return nameComareResult;
            }
        }
    }

    class MyShoppingCenter
    {
        private const string PRODUCT_ADDED = "Product added";
        private const string X_PRODUCTS_DELETED = " products deleted";
        private const string NO_PRODUCTS_FOUND = "No products found";
        private const string INCORRECT_COMMAND = "Incorrect command";

        public MyShoppingCenter()
        {
            this.ProductsByName = new OrderedMultiDictionary<string, Product>(true);
            this.ProductsByProducer = new OrderedMultiDictionary<string, Product>(true);
            this.ProductsByPrice = new OrderedDictionary<decimal, Bag<Product>>();
            this.ProductsByNameAndProducer = new Dictionary<string, OrderedBag<Product>>();
        }

        public OrderedMultiDictionary<string, Product> ProductsByName { get; set; }
        public OrderedMultiDictionary<string, Product> ProductsByProducer { get; set; } 
        public OrderedDictionary<decimal, Bag<Product>> ProductsByPrice { get; set; } 
        public Dictionary<string, OrderedBag<Product>> ProductsByNameAndProducer { get; set; } 
        

        private string AddProduct(string name, string price, string producer)
        {
            var newProduct = new Product
            {
                IsDeleted = false,
                Name = name,
                Price = decimal.Parse(price),
                Producer = producer
            };

            this.ProductsByName.Add(newProduct.Name, newProduct);
            this.ProductsByProducer.Add(newProduct.Producer, newProduct);
            if (this.ProductsByPrice.ContainsKey(newProduct.Price))
            {
                this.ProductsByPrice[newProduct.Price].Add(newProduct);
            }
            else
            {
                this.ProductsByPrice.Add(newProduct.Price, new Bag<Product>
                {
                    newProduct
                }); 
            }

            var namePriceKey = $"{newProduct.Name}{newProduct.Producer}";

            if (this.ProductsByNameAndProducer.ContainsKey(namePriceKey))
            {
                this.ProductsByNameAndProducer[namePriceKey].Add(newProduct);
            }
            else
            {
                this.ProductsByNameAndProducer.Add(namePriceKey, new OrderedBag<Product>() { {newProduct} });
            }
            

            return PRODUCT_ADDED;
        }

        private string FindProductsByName(string name)
        {

            return this.PrintProducts(this.ProductsByName[name]);
        }

        private string FindProductsByProducer(string producer)
        {
            return this.PrintProducts(this.ProductsByProducer[producer]);
        }

        private string FindProductsByPriceRange(string from, string to)
        {
            return this.PrintProducts(this.ProductsByPrice.Range(int.Parse(from), true, int.Parse(to), true)
                .Values
                .SelectMany(p => p)
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Producer)
                .ThenBy(p => p.Price));
        }

        private string PrintProducts(IEnumerable<Product> products)
        {
            var result = string.Join("\n", products);
            if (result.Length == 0)
            {
                return NO_PRODUCTS_FOUND;
            }

            return result;
        }

        private string DeleteProductsByNameAndProducer(string name, string producer)
        {
            var count = 0;
            var key = $"{name}{producer}";
            if (this.ProductsByNameAndProducer.ContainsKey(key))
            {
                var products = this.ProductsByNameAndProducer[key];
                count = products.Count;
                foreach (var product in products)
                {
                    this.ProductsByName.Remove(product.Name, product);
                    this.ProductsByPrice[product.Price].Remove(product);
                    this.ProductsByProducer[product.Producer].Remove(product);
                }    
            }

            if (count == 0)
            {
                return NO_PRODUCTS_FOUND;
            }

            return $"{count}{X_PRODUCTS_DELETED}";
        }

        private string DeleteProductsByProducer(string producer)
        {
            var deleted = this.ProductsByProducer[producer];
            var count = deleted.Count;
            foreach (var product in deleted)
            {
                this.ProductsByName.Remove(product.Name, product);
                this.ProductsByPrice[product.Price].Remove(product);
            }
            
            this.ProductsByProducer.Remove(producer);
            return $"{count}{X_PRODUCTS_DELETED}";
        }

        public string ProcessCommand(string command)
        {
            var endCommandIndex = command.IndexOf(" ");
            var currentCommand = command.Substring(0, endCommandIndex);

            var commandParts = command.Substring(endCommandIndex + 1).Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);
            switch (currentCommand)
            {
                case "AddProduct":
                    return this.AddProduct(commandParts[0], commandParts[1], commandParts[2]);
                case "FindProductsByName":
                    return this.FindProductsByName(commandParts[0]);
                case "FindProductsByProducer":
                    return this.FindProductsByProducer(commandParts[0]);
                case "DeleteProducts":
                    if (commandParts.Length == 2)
                    {
                        return this.DeleteProductsByNameAndProducer(commandParts[0], commandParts[1]);
                    }
                    return this.DeleteProductsByProducer(commandParts[0]);
                case "FindProductsByPriceRange":
                    return this.FindProductsByPriceRange(commandParts[0], commandParts[1]);
                default:
                    return INCORRECT_COMMAND;
            }
        }
    }
}
