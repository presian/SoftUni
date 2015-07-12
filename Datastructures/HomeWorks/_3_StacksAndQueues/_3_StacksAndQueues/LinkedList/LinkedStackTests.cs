namespace LinkedStack
{
    using LinkedStack;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LinkedStackTests
    {
        [TestMethod]
        public void CreateNewStack_CountOfElementsShouldBe0()
        {
            var stack = new LinkedStack<int>();
            Assert.AreEqual(0, stack.Count);
        }

        [TestMethod]
        public void CreateNewStackAddOneElement_CountOfElementsShouldBe1()
        {
            var stack = new LinkedStack<int>();
            stack.Push(3);
            Assert.AreEqual(1, stack.Count);
            Assert.AreEqual(3, stack.FirstNode.Value);
        }

        [TestMethod]
        public void CreateNewStackAddTwoElements_CountOfElementsShouldBe2()
        {
            var stack = new LinkedStack<int>();
            stack.Push(3);
            stack.Push(4);
            Assert.AreEqual(2, stack.Count);
            Assert.AreEqual(4, stack.FirstNode.Value);
        }

        [TestMethod]
        public void AddTwoElementsAndPopLastAdded_ReturnedElementShoulBeEqualToLastPushed()
        {
            var stack = new LinkedStack<int>();
            stack.Push(3);
            stack.Push(4);
            var element = stack.Pop();
            Assert.AreEqual(1, stack.Count);
            Assert.AreEqual(3, stack.FirstNode.Value);
            Assert.AreEqual(4, element);
        }

        [TestMethod]
        public void CheckToArrayMethod()
        {
            var stack = new LinkedStack<int>();
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            var arr = stack.ToArray();
            CollectionAssert.AreEqual(new int[]{5, 4, 3}, arr);
        }
    }
}
