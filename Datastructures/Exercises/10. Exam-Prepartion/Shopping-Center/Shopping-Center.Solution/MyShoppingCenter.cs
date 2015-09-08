namespace Shopping_Center
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Wintellect.PowerCollections;

    class MyShoppingCenter
    {
        private const string PRODUCT_ADDED = "Product added";
        private const string X_PRODUCTS_DELETED = " products deleted";
        private const string NO_PRODUCTS_FOUND = "No products found";
        private const string INCORRECT_COMMAND = "Incorrect command";

        public OrderedMultiDictionary<string, Product> ProductsByName { get; set; }
        public OrderedMultiDictionary<string, Product> ProductsByProducer { get; set; } 
        public OrderedDictionary<decimal, Bag<Product>> ProductsByPrice { get; set; } 
        

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
                this.ProductsByPrice.Add(newProduct.Price, new Bag<Product>
                {
                    newProduct
                } );
            }
            else
            {
                this.ProductsByPrice[newProduct.Price].Add(newProduct);
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
            var sb = new StringBuilder();
            foreach (var product in products)
            {
                sb.AppendFormat($"{{product.Name}};{{product.Producer}};{{product.Price}}\n");
            }
            return sb.ToString();
        }

        private string DeleteProductsByNameAndProducer(string name, string producer)
        {
            
        }

        private string DeleteProductsByProducer(string producer)
        {
            
        }

        public string ProcessCommand(string command)
        {
            
        }
    }
}
