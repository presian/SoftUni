namespace IntervalTree
{

    public class IntervalTree
    {
        private AATree tree; 
        public IntervalTree()
            :this(null)
        {
        }

        public IntervalTree(Node rootNode)
        {
            this.tree = new AATree();
        }

        public Node RootNode => this.tree.Root;

        public bool Add(Interval interval)
        {
            return this.Insert(interval);
        }

        public bool Remove(Interval interval)
        {
            return this.Delete(interval);
        }

        public Node Find(int start, int end)
        {
            var interval = new Interval(start, end);
            return this.tree.Find(this.tree.Root, interval);
        }

        private bool Delete(Interval interval)
        {
            var deleted = this.tree.Remove(interval);
            return deleted != null;
        }

        private bool Insert(Interval interval)
        {
            var currentNode = this.tree.Add(interval, interval.Max);
            return currentNode != null;
        }

    }
}
