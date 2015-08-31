namespace AATree
{
    using System;
    public class AaTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        public class Node
        {
            private int level;
            internal Node left;
            internal Node right;
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

            public int Level
            {
                get
                {
                    return this.level;
                }
                set
                {
                    this.level = value;
                }
            }

            public Node Left
            {
                get
                {
                    return this.left;
                }
                set
                {
                    this.left = value;
                }
            }

            public Node Right
            {
                get
                {
                    return this.right;
                }
                set
                {
                    this.right = value;
                }
            }

            public TKey Key
            {
                get
                {
                    return this.key;
                }
                set
                {
                    this.key = value;
                }
            }

            public TValue Value
            {
                get
                {
                    return this.value;
                }
                set
                {
                    this.value = value;
                }
            }
        }

        private Node root;
        private Node sentinel;
        private Node deleted;

        public Node Root => this.root;

        public AaTree()
        {
            this.root = new Node();
            this.sentinel = this.Root;
            this.deleted = null;
        }

        public void Skew(ref Node currentNode)
        {
            if (currentNode.Level == currentNode.Left.Level)
            {
                Node left = currentNode.Left;
                currentNode.Left = left.Right;
                left.Right = currentNode;
                currentNode = left;
            }
        }

        public void Split(ref Node currentNode)
        {
            if (currentNode.Right.Right.Level == currentNode.Level)
            {
                Node right = currentNode.Right;
                currentNode.Right = right.Left;
                right.Left = currentNode;
                currentNode = right;
                currentNode.Level++;
            }
        }

        public bool Add(TKey key, TValue value)
        {
            return this.Insert(ref this.root, key, value);
        }

        public bool Remove(TKey key)
        {
            return this.Delete(ref this.root, key);
        }

        public TValue this[TKey key]
        {
            get
            {
                Node node = this.Search(this.Root, key);
                return node == null ? default(TValue) : node.Value;
            }
            set
            {
                Node node = this.Search(this.root, key);
                if (node != null)
                {
                    node.Value = value;
                }
                else
                {
                    this.Add(key, value);
                }
            }
        }

        private Node Search(Node node, TKey key)
        {
            if (node == this.sentinel)
            {
                return null;
            }
            int diff = key.CompareTo(node.Key);
            if (diff > 0)
            {
                return this.Search(node.Right, key);
            }
            else if (diff < 0)
            {
                return this.Search(node.Left, key);
            }
            else
            {
                return node;    
            }
        }

        private bool Insert(ref Node node, TKey key, TValue value)
        {
            if (node == this.sentinel)
            {
                node = new Node(key, value, this.sentinel);
                return true;
            }

            int diff = key.CompareTo(node.Key);
            if (diff > 0)
            {
                var left = node.Left;
                if (!this.Insert(ref left, key, value))
                {
                    return false;
                }
            }
            else if (diff < 0)
            {
                var right = node.Right;
                if (!this.Insert(ref right, key, value))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            this.Skew(ref node);
            this.Split(ref node);
            return true;
        }

        private bool Delete(ref Node node, TKey key)
        {
            if (node == this.sentinel)
            {
                return (this.deleted != null);
            }

            int compare = key.CompareTo(node.Key);
            if (compare < 0)
            {
                if (!this.Delete(ref node.left, key))
                {
                    return false;
                }
            }
            else
            {
                if (compare == 0)
                {
                    this.deleted = node;
                }
                if (!this.Delete(ref node.right, key))
                {
                    return false;
                }
            }

            if (this.deleted != null)
            {
                this.deleted.Key = node.Key;
                this.deleted.Value = node.Value;
                this.deleted = null;
                node = node.Right;
            }
            else if (node.Left.Level < node.Level - 1
                    || node.Right.Level < node.Level - 1)
            {
                --node.Level;
                if (node.Right.Level > node.Level)
                {
                    node.Right.Level = node.Level;
                }
                this.Skew(ref node);
                this.Skew(ref node.right);
                this.Skew(ref node.right.right);
                this.Split(ref node);
                this.Split(ref node.right);
            }

            return true;
        }
    }
}
