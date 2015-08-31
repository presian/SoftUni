namespace IntervalTree
{
    using System;

    public class Interval : IComparable<Interval>
    {
        private int min;
        private int max;

        public Interval(int min, int max)
        {
            this.Min = min;
            this.Max = max;
        }

        public int Min
        {
            get
            {
                return this.min;
            }
            set
            {
                this.min = value;
            }
        }

        public int Max
        {
            get
            {
                return this.max;
            }
            set
            {
                this.max = value;
            }
        }

        public int CompareTo(Interval other)
        {
            if (this.Min > other.Min)
            {
                return 1;
            }
            else if(this.Min < other.Min)
            {
                return -1;
            }

            return 0;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == this.GetType())
            {
                var interval = obj as Interval;
                if (interval != null 
                    && interval.Min == this.Min 
                    && interval.Max == this.Max)
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return $"Min: {this.Min}, Max: {this.Max}";
        }
    }
}
