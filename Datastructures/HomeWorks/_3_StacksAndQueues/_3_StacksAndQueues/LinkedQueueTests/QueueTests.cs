namespace LinkedQueueTests
{
    using LinkedQueue;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class QueueTests
    {
        [TestMethod]
        public void CreateNewStack_CountOfElementsShouldBe0()
        {
            var queue = new LinkedQueue<int>();
            Assert.AreEqual(0, queue.Count);
        }

        [TestMethod]
        public void CreateNewStackAddOneElement_CountOfElementsShouldBe1()
        {
            var queue = new LinkedQueue<int>();
            queue.Enqueue(3);
            Assert.AreEqual(1, queue.Count);
            Assert.AreEqual(3, queue.FirstNode.Value);
        }

        [TestMethod]
        public void CreateNewStackAddTwoElements_CountOfElementsShouldBe2()
        {
            var queue = new LinkedQueue<int>();
            queue.Enqueue(3);
            queue.Enqueue(4);
            Assert.AreEqual(2, queue.Count);
            Assert.AreEqual(3, queue.FirstNode.Value);
        }

        [TestMethod]
        public void AddTwoElementsAndPopLastAdded_ReturnedElementShoulBeEqualToLastPushed()
        {
            var queue = new LinkedQueue<int>();
            queue.Enqueue(3);
            queue.Enqueue(4);
            var element = queue.Dequeue();
            Assert.AreEqual(1, queue.Count);
            Assert.AreEqual(4, queue.FirstNode.Value);
            Assert.AreEqual(3, element);
        }

        [TestMethod]
        public void CheckToArrayMethod()
        {
            var queue = new LinkedQueue<int>();
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);
            var arr = queue.ToArray();
            CollectionAssert.AreEqual(new int[] { 3, 4, 5 }, arr);
        }
    }
}
