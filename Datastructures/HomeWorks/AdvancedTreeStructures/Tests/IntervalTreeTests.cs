namespace Tests
{
    using IntervalTree;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class IntervalTreeTests
    {
        [TestMethod]
        public void CreateTreeAddOneNode()
        {
            var tree = new IntervalTree();
            var interval =new Interval(1,2);
            tree.Add(interval);

            Assert.AreEqual(tree.RootNode.Key, new Interval(1, 2));
        }

        [TestMethod]
        public void CreateTreeAddTwoNodes()
        {
            var tree = new IntervalTree();
            var interval = new Interval(10, 20);
            var secondInterval = new Interval(1, 2);
            tree.Add(interval);
            tree.Add(secondInterval);

            Assert.AreEqual(tree.RootNode.Key, secondInterval);
            Assert.AreEqual(tree.RootNode.Right.Key, interval);
        }

        [TestMethod]
        public void CreateTreeAddTwoReversedNodes()
        {
            var tree = new IntervalTree();
            var interval = new Interval(10, 20);
            var secondInterval = new Interval(1, 2);
            tree.Add(secondInterval);
            tree.Add(interval);
            

            Assert.AreEqual(tree.RootNode.Key, secondInterval);
            Assert.AreEqual(tree.RootNode.Right.Key, interval);
        }

        [TestMethod]
        public void CreateTreeAddThtreeNodes()
        {
            var tree = new IntervalTree();
            var interval = new Interval(10, 20);
            var secondInterval = new Interval(1, 2);
            var thirdInterval = new Interval(5, 10);
            tree.Add(secondInterval);
            tree.Add(interval);
            tree.Add(thirdInterval);


            Assert.AreEqual(tree.RootNode.Key, thirdInterval);
            Assert.AreEqual(tree.RootNode.Right.Key, interval);
            Assert.AreEqual(tree.RootNode.Left.Key, secondInterval);
        }

        [TestMethod]
        public void CreateTreeAddFiveNodes()
        {
            var tree = new IntervalTree();
            var one = new Interval(1, 5);
            var two = new Interval(25, 88);
            var three = new Interval(3, 18);
            var four = new Interval(23, 63);
            var five = new Interval(17, 99);
            tree.Add(one);
            tree.Add(two);
            tree.Add(three);
            tree.Add(four);
            tree.Add(five);


            Assert.AreEqual(tree.RootNode.Key, three);
            Assert.AreEqual(tree.RootNode.Right.Key, four);
            Assert.AreEqual(tree.RootNode.Right.Left.Key, five);
            Assert.AreEqual(tree.RootNode.Right.Right.Key, two);
            Assert.AreEqual(tree.RootNode.Left.Key, one);
        }

        [TestMethod]
        public void CreateTreeAddThenNodesAndSearchForClosestInterval()
        {
            var tree = new IntervalTree();
            tree.Add(new Interval(1, 5));
            tree.Add(new Interval(25, 88));
            tree.Add(new Interval(3, 18));
            tree.Add(new Interval(23, 63));
            tree.Add(new Interval(17, 99));
            tree.Add(new Interval(44, 63));
            tree.Add(new Interval(94, 95));
            tree.Add(new Interval(38, 55));
            tree.Add(new Interval(11, 110));
            tree.Add(new Interval(111, 225));
            tree.Add(new Interval(2, 358));
            tree.Add(new Interval(115, 486));

            var finded = tree.Find(17, 18);

            Assert.AreEqual(finded.Key, new Interval(17,99));
            
        }

        [TestMethod]
        public void CreateTreeAddThtreeNodesAndDeleteOne()
        {
            var tree = new IntervalTree();
            var interval = new Interval(10, 20);
            var secondInterval = new Interval(1, 2);
            var thirdInterval = new Interval(5, 10);
            tree.Add(secondInterval);
            tree.Add(interval);
            tree.Add(thirdInterval);

            tree.Remove(secondInterval);

            Assert.AreEqual(tree.RootNode.Key, thirdInterval);
            Assert.AreEqual(tree.RootNode.Right.Key, interval);
            Assert.AreEqual(tree.RootNode.Left.Key, null);
        }
    } 
}
