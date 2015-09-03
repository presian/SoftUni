namespace CollectionOfProducts
{
    using System;
    using System.Collections.Generic;

    class ProductsDatastructue
    {
        public ProductsDatastructue()
        {
            this.Products = new Dictionary<uint, Product>();
            this.ProductsByPrice = new Dictionary<decimal, SortedSet<Product>>();
            this.ProductsByTitle = new Dictionary<string, SortedSet<Product>>();
            this.ProductsByTitleAndPrice = new Dictionary<Tuple<string, decimal>, SortedSet<Product>>();
//            this.ProductsByTitleAndPriceRange = new Dictionary<Tuple<string, PriceRange>, SortedSet<Product>>();
//            this.ProductsBySupplierAndPriceRange = new Dictionary<Tuple<string, PriceRange>, SortedSet<Product>>();
            this.CurrnetId = 1;
        }

        private Dictionary<uint, Product> Products{ get; set; }
        private Dictionary<decimal, SortedSet<Product>> ProductsByPrice { get; set; }
        private Dictionary<string, SortedSet<Product>> ProductsByTitle { get; set; }
        private Dictionary<Tuple<string, decimal>, SortedSet<Product>> ProductsByTitleAndPrice { get; set; }
//        private Dictionary<Tuple<string, PriceRange>, SortedSet<Product>> ProductsByTitleAndPriceRange { get; set; }
//        private Dictionary<Tuple<string, PriceRange>, SortedSet<Product>> ProductsBySupplierAndPriceRange { get; set; }
        private uint CurrnetId { get; set; }

        private bool Insert(string title, decimal price, string supplier)
        {
            var newProduct = new Product(this.CurrnetId, title, price, supplier);
            // add in main dict
            if (!this.Products.ContainsValue(newProduct))
            {
                this.Products.Add(this.CurrnetId, newProduct);
            }

            // add in byTitle dict
            if (this.ProductsByTitle.ContainsKey(title))
            {
                this.ProductsByTitle[title].Add(newProduct);
            }
            else
            {
                this.ProductsByTitle.Add(title, new SortedSet<Product> { newProduct});
            }

            // add in byPrice dict
            if (this.ProductsByPrice.ContainsKey(price))
            {
                this.ProductsByPrice[price].Add(newProduct);
            }
            else
            {
                this.ProductsByPrice.Add(price, new SortedSet<Product> { newProduct });
            }

            // add in by title and price
            var titleAndPriceKey = new Tuple<string, decimal>( title, price );
            if (this.ProductsByTitleAndPrice.ContainsKey(titleAndPriceKey))
            {
                this.ProductsByTitleAndPrice[titleAndPriceKey].Add(newProduct);
            }
            else
            {
                this.ProductsByTitleAndPrice.Add(titleAndPriceKey, new SortedSet<Product> {newProduct});
            }

            
            this.CurrnetId++;

            return true;
        }

        public bool Add(string title, decimal price, string supplier)
        {
            return this.Insert(title, price, supplier);
        }
    }
}
