namespace CollectionOfProducts
{
    using System;

    class PriceRange
    {
        private decimal startRange;
        private decimal endRange;

        public PriceRange(decimal startRange, decimal endRange)
        {
            this.AddValues(startRange, endRange);
        }

        public decimal StartRange
        {
            get
            {
                return this.startRange;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Start price in range cannot be less than from zero!");
                }
                this.startRange = value;
            }
        }

        public decimal EndRange
        {
            get
            {
                return this.endRange;
            }
            set
            {
                if (value > uint.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("EndRange cannot be more than uint max value!");
                }
                this.endRange = value;
            }
        }

        private void AddValues(decimal start, decimal end)
        {
            this.StartRange = Math.Min(start, end);
            this.EndRange = Math.Max(start, end);
        }
    }
}
