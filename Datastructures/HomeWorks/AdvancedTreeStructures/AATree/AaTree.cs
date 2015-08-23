namespace AATree
{
    using System;

    public class AaTree<TKey, TValue> where TValue : IComparable
    {
        public class Node
        {
            private int level;
            private Node left;
            private Node right;
            private TKey key;
            private TValue value;

            internal Node()
            {
                this.level = 0;
                this.left = this;
                this.right = this;

            }

            internal Node(TKey key, TValue value, Node sentinel)
            {
                this.level = 1; 
                this.key = key;
                this.value = value;
                this.left = sentinel;
                this.right = sentinel;
            }
        }

        private Node root;
        private Node sentinel;
        private Node deleted;

        public Node Root => this.root;

        public AaTree()
        {
            this.root = new Node();
            this.sentinel = new Node();
            this.deleted = null;
        }

        public void Skew()
        {
            
        }

    }
}
