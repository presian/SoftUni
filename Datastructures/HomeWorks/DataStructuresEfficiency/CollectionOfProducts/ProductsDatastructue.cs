namespace CollectionOfProducts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    class ProductsDatastructue
    {
        public ProductsDatastructue()
        {
            this.Products = new Dictionary<uint, Product>();
            this.ProductsByPrice = new OrderedDictionary<decimal, OrderedMultiDictionary<uint, Product>>();
            this.ProductsByTitle = new Dictionary<string, OrderedMultiDictionary<uint, Product>>();
            this.ProductsBySupplier = new Dictionary<string, OrderedMultiDictionary<decimal, Product>>();
            this.ProductsByTitleAndPrice = new Dictionary<Tuple<string, decimal>, OrderedMultiDictionary<uint, Product>>();
            this.CurrnetId = 1;
        }

        private Dictionary<uint, Product> Products{ get; set; }
        private OrderedDictionary<decimal, OrderedMultiDictionary<uint,Product>> ProductsByPrice { get; set; }
        private Dictionary<string, OrderedMultiDictionary<uint, Product>> ProductsByTitle { get; set; }
        private Dictionary<string, OrderedMultiDictionary<decimal, Product>> ProductsBySupplier { get; set; }
        private Dictionary<Tuple<string, decimal>, OrderedMultiDictionary<uint, Product>> ProductsByTitleAndPrice { get; set; }
        private uint CurrnetId { get; set; }
        public uint Count { get; set; }

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
                this.ProductsByTitle[title].Add(newProduct.Id, newProduct);
            }
            else
            {
                this.ProductsByTitle.Add(title, new OrderedMultiDictionary<uint, Product>(true) { { newProduct.Id, newProduct } });
            }

            // add in byPrice dict
            if (this.ProductsByPrice.ContainsKey(price))
            {
                this.ProductsByPrice[price].Add(newProduct.Id, newProduct);
            }
            else
            {
                this.ProductsByPrice.Add(price, new OrderedMultiDictionary<uint, Product>(true) { { newProduct.Id, newProduct } });
            }

            // add in by title and price
            var titleAndPriceKey = new Tuple<string, decimal>( title, price );
            if (this.ProductsByTitleAndPrice.ContainsKey(titleAndPriceKey))
            {
                this.ProductsByTitleAndPrice[titleAndPriceKey].Add(newProduct.Id, newProduct);
            }
            else
            {
                this.ProductsByTitleAndPrice.Add(titleAndPriceKey, new OrderedMultiDictionary<uint, Product>(true) { {newProduct.Id, newProduct} });
            }

            // add in byTitle dict
            if (this.ProductsBySupplier.ContainsKey(supplier))
            {
                this.ProductsBySupplier[supplier].Add(newProduct.Price, newProduct);
            }
            else
            {
                this.ProductsBySupplier.Add(supplier, new OrderedMultiDictionary<decimal, Product>(true) { { newProduct.Price, newProduct } });
            }

            this.CurrnetId++;
            this.Count++;

            return true;
        }

        private bool Delete(uint id)
        {
            if (this.Products.ContainsKey(id))
            {
                var product = this.Products[id];
                this.Products.Remove(product.Id);
                this.ProductsByPrice[product.Price].Remove(product.Id);
                this.ProductsByTitle[product.Title].Remove(product.Id);
                this.ProductsBySupplier[product.Supplier][product.Price].Remove(product);
                this.ProductsByTitleAndPrice[new Tuple<string, decimal>(product.Title, product.Price)].Remove(product.Id);
                this.Count++;
            }

            return false;
        }

        private IEnumerable<Product> SearchByPriceRange(decimal startPrice, decimal endPrice)
        {
            var productsInRange = this.ProductsByPrice.Range(startPrice, true, endPrice, true)
                .SelectMany(p=>p.Value.Values)
                .OrderBy(p=>p.Id)
                .ToList();
            return productsInRange;
        }

        private IEnumerable<Product> SearchByTitle(string title)
        {
            OrderedMultiDictionary<uint, Product> produtsByTitle; 
            this.ProductsByTitle.TryGetValue(title, out produtsByTitle);

            if (produtsByTitle != null)
            {
                return produtsByTitle.Values;
            }
            else
            {
                return new List<Product>();
            }
        }

        private IEnumerable<Product> SearchByTitleAndPrice(string title, decimal price)
        {
            OrderedMultiDictionary<uint, Product> productsByTitleAndPrice;
            this.ProductsByTitleAndPrice.TryGetValue(new Tuple<string, decimal>(title, price), out productsByTitleAndPrice);

            if (productsByTitleAndPrice != null)
            {
                return productsByTitleAndPrice.Values;
            }
            else
            {
                return new List<Product>();
            }
        }

        private IEnumerable<Product> SearchByTitleAndPriceRange(string title, decimal startPrice, decimal endPrice)
        {
            var pricerangeKeys = this.ProductsByPrice.Range(startPrice, true, endPrice, true).Keys;
            var result = pricerangeKeys
                .SelectMany(pricerangeKey => this.SearchByTitleAndPrice(title, pricerangeKey))
                .OrderBy(p => p.Id);
            return result;
        }

        private IEnumerable<Product> SearchBySupplierAndPrice(string supplier, decimal price)
        {
            OrderedMultiDictionary<decimal, Product> productsBySupplier;
            this.ProductsBySupplier.TryGetValue(supplier, out productsBySupplier);

            if (productsBySupplier != null)
            {
                return productsBySupplier.Values.OrderBy(p => p.Id);
            }
            else
            {
                return new List<Product>();
            }
        }

        private IEnumerable<Product> SearchBySupplierAndPriceRange(string supplier, decimal startPrice, decimal endPrice)
        {
            OrderedMultiDictionary<decimal, Product> productsBySupplier;
            this.ProductsBySupplier.TryGetValue(supplier, out productsBySupplier);
            if (productsBySupplier != null)
            {
                return productsBySupplier
                    .Range(startPrice, true, endPrice, true)
                    .SelectMany(p => p.Value)
                    .OrderBy(p => p.Id);
            }
            else
            {
                return new List<Product>();
            }
        }


        public bool Add(string title, decimal price, string supplier)
        {
            return this.Insert(title, price, supplier);
        }

        public bool Remove(uint id)
        {
            return this.Delete(id);
        }

        public IEnumerable<Product> FindByPriceRane(decimal startPrice, decimal endPrice)
        {
            return this.SearchByPriceRange(startPrice, endPrice);
        }

        public IEnumerable<Product> FindByTitle(string title)
        {
            return this.SearchByTitle(title);
        }

        public IEnumerable<Product> FindByTitleAndPrice(string title, decimal price)
        {
            return this.SearchByTitleAndPrice(title, price);
        }

        public IEnumerable<Product> FindByTitleAndPriceRange(string title, decimal startPrice, decimal endPrice)
        {
            return this.SearchByTitleAndPriceRange(title, startPrice, endPrice);
        }

        public IEnumerable<Product> FindBySupplierAndPrice(string supplier, decimal price)
        {
            return this.SearchBySupplierAndPrice(supplier, price);
        }

        public IEnumerable<Product> FindBySupplierAndPriceRange(string supplier, decimal startPrice, decimal endPrice)
        {
            return this.SearchBySupplierAndPriceRange(supplier, startPrice, endPrice);
        } 


    }
}
