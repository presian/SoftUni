namespace IntervalTree
{
    using System;

    public class Node
    {
        // Node internal data
        internal int level;
        internal Node left;
        internal Node right;
        internal Node parent;

        // User data
        internal Interval key;
        internal int? value;

        // Constuctor for the sentinel node
        internal Node()
        {
            this.level = 0;
            this.left = this;
            this.right = this;
            this.parent = null;
            this.Value = null;
        }

        // Constuctor for regular nodes (that all start life as leaf nodes)
        internal Node( Interval key, int? value, Node sentinel)
        {
            this.level = 1;
            this.left = sentinel;
            this.right = sentinel;
            this.key = key;
            this.value = value;
        }

        public Node Left
        {
            get { return this.left; }
            set { this.left = value; }
        }

        public Node Right
        {
            get { return this.right; }
            set { this.right = value; }
        }

        public Interval Key
        {
            get { return this.key; }
            set { this.key = value; }
        }

        public int? Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }

    public class AATree
    {
        

        private Node root;
        private Node sentinel;
        private Node deleted;
        private int? deletedValue;

        public Node Root
        {
            get { return this.root; }
        }

        public AATree()
        {
            root = sentinel = new Node();
            deleted = null;
        }

        private void Skew(ref Node node)
        {
            if (node.level == node.left.level)
            {
                // Rotate parents
                node.left.parent = node.parent;
                node.left.right.parent = node;
                node.parent = node.left;

                // Rotate right
                Node left = node.left;
                node.left = left.right;
                left.right = node;
                node = left;
            }

            this.GetValue(node);
        }

        private void Split(ref Node node)
        {
            if (node.right.right.level == node.level)
            {
                // Rotate parents
                node.right.parent = node.parent;
                node.right.left.parent = node;
                node.parent = node.right;

                // Rotate left
                Node right = node.right;
                node.right = right.left;
            
                right.left = node;
                node = right;
                node.level++;
            }

            this.GetValue(node);
        }

        private void GetValue(Node node)
        {
            if (node == this.sentinel)
            {
                return;
            }

            if (node.Right.Key != null)
            {
                node.Right.value = this.getMaxChildValue(node.Right);
            }
            if (node.Left.Key != null)
            {
                node.Left.value = this.getMaxChildValue(node.Left);
            }

            node.Value = this.getMaxChildValue(node);

            var isSwapNeeded = true;
            var currentNode = node;
            while (currentNode.parent != null && isSwapNeeded)
            {
                var newValue = this.getMaxChildValue(currentNode.parent);
                if (newValue > currentNode.parent.Value 
                    || (this.deletedValue.HasValue 
                        && this.deletedValue.Value == currentNode.parent.Value)
                    )
                {
                    currentNode.parent.Value = newValue;
                }
                else
                {
                    isSwapNeeded = false;
                }

                currentNode = currentNode.parent;
            }
        }

        private int? getMaxChildValue(Node node)
        {
            if (node.left.Value == null && node.right.Value == null)
            {
                return node.Key.Max;
            }
            else if (node.left.Value == null)
            {
                return Math.Max(node.right.value.Value, node.Value.Value);
            }
            else if (node.right.Value == null)
            {
                return Math.Max(node.left.Value.Value, node.Value.Value);
            }
            else
            {
                return Math.Max(node.left.Value.Value, node.right.Value.Value);
            }
        }

        private Node Insert(ref Node node, Interval key, int? value, Node parent = null)
        {
            if (node == this.sentinel)
            {
                node = new Node(key, value, this.sentinel);
                node.parent = parent;
                return node;
            }

            int compare = key.CompareTo(node.key);
            if (compare < 0)
            {
                if (this.Insert(ref node.left, key, value, node) == null)
                {
                    return null;
                }
            }
            else if (compare > 0)
            {
                if (this.Insert(ref node.right, key, value, node) == null)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

            this.Skew(ref node);
            this.Split(ref node);

            return node;
        }

        private Node Delete(ref Node node, Interval key, Node parent = null)
        {
            if (node == this.sentinel)
            {
                if (this.deleted != null)
                {
                    return node;
                }

                return null;
            }

            int compare = key.CompareTo(node.key);
            if (compare < 0)
            {
                if (this.Delete(ref node.left, key, node) == null)
                {
                    return null;
                }
            }
            else
            {
                if (compare == 0)
                {
                    this.deleted = node;
                    this.deletedValue = this.deleted.Value;
                }
                if (this.Delete(ref node.right, key, node) == null)
                {
                    return null;
                }
            }

            if (this.deleted != null)
            {
                this.deleted.key = node.key;
                this.deleted.value = node.value;
                this.deleted = null;
                // 
//                var maxFromChilds = this.getMaxChildValue(node);
//                node.Right.Value = maxFromChilds;
//                node.Right.parent = node.parent;
                //

                node = node.right;

                //
//                this.GetValue(node);
            }
            else if (node.left.level < node.level - 1
                     || node.right.level < node.level - 1)
            {
                --node.level;
                if (node.right.level > node.level)
                {
                    node.right.level = node.level;
                }

                this.Skew(ref node);
                this.Skew(ref node.right);
                this.Skew(ref node.right.right);
                this.Split(ref node);
                this.Split(ref node.right);
            }

            return node;
        }

        private Node Search(Node node, Interval key)
        {
            if (node == this.sentinel)
            {
                return null;
            }

            int compare = key.CompareTo(node.key);
            if (compare < 0)
            {
                return this.Search(node.left, key);
            }
            else if (compare > 0)
            {
                return this.Search(node.right, key);
            }
            else
            {
                return node;
            }
        }

        public Node Add(Interval key, int? value)
        {
            return this.Insert(ref this.root, key, value);
        }

        public Node Remove(Interval key)
        {
            this.deletedValue = null;
            return this.Delete(ref this.root, key);
        }

        public int? this[Interval key]
        {
            get
            {
                Node node = this.Search(this.root, key);
                return node == null ? null : node.value;
            }
            set
            {
                Node node = this.Search(this.root, key);
                if (node == null)
                {
                    this.Add(key, value);
                }
                else
                {
                    node.value = value;
                }
            }
        }

        public Node Find(Node node, Interval key)
        {
            if (node == this.sentinel)
            {
                return null;
            }

            int compare = key.CompareTo(node.key);
            if (compare < 0)
            {
                return this.Find(node.left, key);
            }
            else if (compare > 0)
            {
                if (node.Key.Max >= key.Max)
                {
                    if (node.Right != this.sentinel 
                        && node.Right.Key.Min <= key.Min 
                        && node.Right.Key.Max >= key.Min)
                    {
                        return this.Find(node.right, key);
                    }

                    return node;
                }

                return this.Find(node.right, key);
            }
            else
            {
                if (node.Key.Max >= key.Max)
                {
                    return node;
                }

                return null;
            }
        }
    }

}
