namespace PlayWithTrees
{
    using System.Collections.Generic;
    using System.Linq;

    class Tree<T>
    {
        private T value;
        private Tree<T> parent;
        private ICollection<Tree<T>> children;

        public Tree(T value, params Tree<T>[] children)
        {
            this.Value = value;
            this.Children = new List<Tree<T>>();
            foreach (var child in children)
            {
                child.parent = this;
                this.children.Add(child);
            }
        }

        private static Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

        public static Tree<int> GetTreeNodeByValue(int value)
        {
            if (!nodeByValue.ContainsKey(value))
            {
                nodeByValue[value] = new Tree<int>(value);
            }

            return nodeByValue[value];
        }

        public T Value
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

        public Tree<T> Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                this.parent = value;
            }
        }

        public ICollection<Tree<T>> Children
        {
            get
            {
                return this.children;
            }
            set
            {
                this.children = value;
            }
        }

        static Tree<int> FindRootNode()
        {
            return nodeByValue.Values.FirstOrDefault(n => n.Parent == null);
        } 

        static IEnumerable<Tree<int>> FindMiddleNodes()
        {
            return nodeByValue.Values.Where(n => n.Parent != null && n.Children.Count > 0);
        } 
    }
}
