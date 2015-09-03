namespace CollectionOfProducts
{
    using System;

    class Product : IComparable<Product>
    {
        private uint id;
        private string title;
        private decimal price;
        private string supplier;


        public Product(uint id, string title, decimal price, string supplier)
        {
            this.Id = id;
            this.Title = title;
            this.Price = price;
            this.Supplier = supplier;
        }

        public uint Id { get; set; }

        public string Title { get; set; }

        public decimal Price
        {
            get
            {
                return this.price;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Price cannot be zero or less!");
                }

                this.price = value;
            }
        }

        public string Supplier { get; set; }

        public int CompareTo(Product other)
        {
            if (this.Id > other.Id)
            {
                return 1;
            }
            else if (this.Id < other.Id)
            {
                return -1;
            }

            return 0;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (typeof(object) == typeof(Product))
            {
                var product = obj as Product;
                if (this.Price == product.Price 
                    && this.Supplier == product.Supplier 
                    && this.Title == product.Title)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return $"{this.Price}-{this.Supplier}-{this.Title}".GetHashCode();
        }
    }
}
