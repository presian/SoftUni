namespace _7_LinkedList
{
    public class ListNode<T>
    {
        private T nodeValue;
        private ListNode<T> nexNode;

        public ListNode(T nodeValue)
        {
            this.nodeValue = nodeValue;
            this.nexNode = null;
        }

        public T NodeValue
        {
            get
            {
                return this.nodeValue;
            }
            set
            {
                this.nodeValue = value;
            }
        }

        public ListNode<T> NextNode
        {
            get
            {
                return this.nexNode;
            }
            set
            {
                this.nexNode = value;
            }
        }

        public override string ToString()
        {
            return this.NodeValue.ToString();
        }
    }
}
