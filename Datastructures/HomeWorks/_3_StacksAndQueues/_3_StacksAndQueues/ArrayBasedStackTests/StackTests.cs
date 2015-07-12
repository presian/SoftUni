namespace ArrayBasedStackTests
{
    using System;
    using ArrayBasedStack;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StackTests
    {
        [TestMethod]
        public void CreateNewStack_TheCountSouldBeZero()
        {
            var stack = new ArrayStack<int>();

            Assert.AreEqual(0, stack.Count);
        }

        [TestMethod]
        public void CreateNewStack_PushElement_TheCountSouldBeOne()
        {
            var stack = new ArrayStack<int>();
            stack.Push(3);

            Assert.AreEqual(1, stack.Count);
        }

        [TestMethod]
        public void CreateNewStack_PushElementThanPopIt_TheCountSouldBeZero()
        {
            var stack = new ArrayStack<int>();
            stack.Push(3);
            var element = stack.Pop();

            Assert.AreEqual(3, element);
            Assert.AreEqual(0, stack.Count);
        }

        [TestMethod]
        public void CreateNewStack_Push1000ElementThanPopsIt()
        {
            var stack = new ArrayStack<string>();
            for (int i = 0; i < 1000; i++)
            {
                stack.Push(i.ToString());
                Assert.AreEqual(i + 1, stack.Count);
            }

            for (int i = 0; i < 1000; i++)
            {
                var element = stack.Pop();
                Assert.AreEqual(1000 - i - 1, stack.Count);
                Assert.AreEqual((1000 - i - 1).ToString(), element);
            }
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void CreateNewStack_PopElement_SouldRiseException()
        {
            var stack = new ArrayStack<int>();
            stack.Pop();
        }

        [TestMethod]
        public void CreateNewStackWithInitialSize1_PushElementThanPopIt_TheCountSouldBeZero()
        {
            var stack = new ArrayStack<int>(1);
            Assert.AreEqual(0, stack.Count);
            
            stack.Push(3);
            Assert.AreEqual(1, stack.Count);
            stack.Push(2);
            Assert.AreEqual(2, stack.Count);
            var element = stack.Pop();
            Assert.AreEqual(1, stack.Count);
            Assert.AreEqual(2, element);

            var element2 = stack.Pop();
            Assert.AreEqual(3, element2);
            Assert.AreEqual(0, stack.Count);
        }

        [TestMethod]
        public void CreateNewStack_PushElementsThanGetLikeArray()
        {
            var stack = new ArrayStack<int>();
            stack.Push(3);
            stack.Push(5);
            stack.Push(-2);
            stack.Push(7);
            var elements = stack.ToArray();

            CollectionAssert.AreEqual(new int[] { 7, -2, 5, 3}, elements);
        }

        [TestMethod]
        public void CreateNewStack_ThanGetLikeArray()
        {
            var stack = new ArrayStack<DateTime>();
            var elements = stack.ToArray();

            CollectionAssert.AreEqual(new DateTime[]{}, elements);
        }
    }
}
