namespace TowerOfHanoi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Towers
    {
        private Stack<int> source;
        private Stack<int> destination;
        private Stack<int> spare;

        private int stepsTaken = 0;
        private int size = 0;

        public Towers(int end)
        {
            this.size = end;
            this.Source = new Stack<int>(Enumerable.Range(1, end).Reverse());
            this.Destination = new Stack<int>();
            this.Spare = new Stack<int>();
        }

        public Stack<int> Source
        {
            get
            {
                return this.source;
            }
            set
            {
                this.source = value;
            }
        }

        public Stack<int> Destination
        {
            get
            {
                return this.destination;
            }
            set
            {
                this.destination = value;
            }
        }

        public Stack<int> Spare
        {
            get
            {
                return this.spare;
            }
            set
            {
                this.spare = value;
            }
        }

        public void MoveDisks(int disk, Stack<int> from = null, Stack<int> to = null, Stack<int> other = null)
        {
            if (from == null)
            {
                from = this.Source;
            }
            if (to == null)
            {
                to = this.Destination;
            }
            if (other == null)
            {
                other = this.Spare;
            }

            if (disk == 1)
            {  
                var popedElement = from.Pop();
                to.Push(popedElement);
                this.PrintCurrentStep(disk);   
            }
            else
            {
                this.MoveDisks(disk-1, from, other, to);
                var poped = from.Pop();
                to.Push(poped);
                this.PrintCurrentStep(poped);   
                this.MoveDisks(disk-1, other, to, from);
            } 
        }

        private void PrintCurrentStep(int disk)
        {
            this.stepsTaken++;
            Console.WriteLine($"Step #{this.stepsTaken}: Moved disk {disk}");
            this.PrintResult();
        }

        public void PrintResult()
        {
            Console.WriteLine($"Source: {string.Join(", ", this.Source.Reverse())}");
            Console.WriteLine($"Destination: {string.Join(", ", this.Destination.Reverse())}");
            Console.WriteLine($"Spare: {string.Join(", ", this.Spare.Reverse())}");
            Console.WriteLine();
        }
    }
}
