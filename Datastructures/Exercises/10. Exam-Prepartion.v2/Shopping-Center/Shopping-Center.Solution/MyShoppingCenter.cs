namespace Shopping_Center
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        private const string ProductAdded = "Product added";
        private const string X_ProductsDeleted = " products deleted";

        private const string NoProductsFound = "No products found";

        public Dictionary<string, OrderedBag<Product>> ProductsByName { get; set; }
        public Dictionary<string, OrderedBag<Product>> ProductsByProducer { get; set; }
        public OrderedDictionary<decimal, OrderedBag<Product>> ProductsByPrice { get; set; }
        public Dictionary<string, OrderedBag<Product>> ProductsByNameAndProducer { get; set; }

        public string AddProduct(string name, string price, string producer)
        {
            var p = new Product(name, decimal.Parse(price), producer);

            if (this.ProductsByName.ContainsKey(name))
            {
                this.ProductsByName[name].Add(p);
            }
            else
            {
                this.ProductsByName.Add(name, new OrderedBag<Product>() { {p} });
            }

            if (this.ProductsByProducer.ContainsKey(producer))
            {
                this.ProductsByProducer[producer].Add(p);
            }
            else
            {
                this.ProductsByProducer.Add(producer, new OrderedBag<Product>() { {p} });
            }

            if (this.ProductsByPrice.ContainsKey(p.Price))
            {
                this.ProductsByPrice[p.Price].Add(p);
            }
            else
            {
                this.ProductsByPrice.Add(p.Price, new OrderedBag<Product>() { {p} });
            }

            var nameProducerKey = $"{name}{producer}";
            if (this.ProductsByNameAndProducer.ContainsKey(nameProducerKey))
            {
                this.ProductsByNameAndProducer[nameProducerKey].Add(p);
            }
            else
            {
                this.ProductsByNameAndProducer.Add(nameProducerKey, new OrderedBag<Product>() { {p} });
            }

            return ProductAdded;
        }

        public string DeleteProduct(string name, string producer)
        {
            var key = $"{name}{producer}";
            if (this.ProductsByNameAndProducer.ContainsKey(key))
            {
                var products = this.ProductsByNameAndProducer[key];
                var deletedCount = products.Count;
                
                if (deletedCount > 0)
                {
                    foreach (var product in products)
                    {
                        this.ProductsByName[name].Remove(product);
                        this.ProductsByPrice[product.Price].Remove(product);
                        this.ProductsByProducer[product.Producer].Remove(product);
                    }

                    this.ProductsByNameAndProducer.Remove(key);
                    return $"{deletedCount}{X_ProductsDeleted}";
                }

                return NoProductsFound;
            }

            return NoProductsFound;
        }

        public string DeleteProduct(string producer)
        {
            if (this.ProductsByProducer.ContainsKey(producer))
            {
                var products = this.ProductsByProducer[producer];
                var deletedCount = products.Count;

                if (deletedCount > 0)
                {
                    foreach (var product in products)
                    {
                        this.ProductsByName[product.Name].Remove(product);
                        this.ProductsByPrice[product.Price].Remove(product);
                        this.ProductsByNameAndProducer[$"{product.Name}{product.Producer}"].Remove(product);
                    }

                    this.ProductsByProducer.Remove(producer);
                    return $"{deletedCount}{X_ProductsDeleted}";
                }

                return NoProductsFound;
            }

            return NoProductsFound;
        }

        public string FindProductsByName(string name)
        {
            if (this.ProductsByName.ContainsKey(name))
            {
                return this.PrintResult(this.ProductsByName[name]);
            }

            return NoProductsFound;
        }

        public string FindProductsByProducer(string producer)
        {
            if (this.ProductsByProducer.ContainsKey(producer))
            {
                return this.PrintResult(this.ProductsByProducer[producer]);
            }

            return NoProductsFound;
        }

        public string FindProductsByPriceRange(string startPrice, string endPrice)
        {
            return this.PrintResult(
                this.ProductsByPrice
                .Range(decimal.Parse(startPrice), true, decimal.Parse(endPrice), true)
                .SelectMany(p=>p.Value));
        }

        private string PrintResult(IEnumerable<Product> products)
        {
            var result = string.Join("\n\r", products);
            if (result.Length > 0)
            {
                return result;
            }

            return NoProductsFound;
        }

        public string ProcessCommand(string userInput)
        {
            var command = userInput.Substring(0, userInput.IndexOf(" "));
            var commandParts = userInput.Substring(userInput.IndexOf(" ") + 1)
                .Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);
            switch (command)
            {
                case "AddProduct":
                    return this.AddProduct(commandParts[0], commandParts[1], commandParts[2]);
                case "DeleteProducts":
                    if (commandParts.Length == 2)
                    {
                        return this.DeleteProduct(commandParts[0], commandParts[1]);
                    }

                    return this.DeleteProduct(commandParts[0]);
                case "FindProductsByName":
                    return this.FindProductsByName(commandParts[0]);
                case "FindProductsByProducer":
                    return this.FindProductsByProducer(commandParts[0]);
                case "FindProductsByPriceRange":
                    return this.FindProductsByPriceRange(commandParts[0], commandParts[1]);
                default:
                    return "Invalid command!";

            }
        }
    }
}
