using System;

class Product : IComparable<Product>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Producer { get; set; }
    public bool IsDeleted { get; set; }

    public override string ToString()
    {
        return $"{{{this.Name};{this.Producer};{this.Price:0.00}}}";
    }

    public int CompareTo(Product other)
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