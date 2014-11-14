using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using bt05;

namespace UnitTests
{
    [TestClass]
    public class BinarySearchTreeTest
    {
        [TestMethod]
        public void SearchTest()
        {
            BinarySearchTree tree = createTree();

            int expectedResult = 29;
            int actualResult = tree.Search(29);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetInOrderTest()
        {
            BinarySearchTree tree = createTree();

            int[] expectedResult = new int[12] { 5, 9, 17, 22, 29, 33, 46, 47, 48, 53, 68, 88 };
            int[] actualResult = tree.GetInOrder();

            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetPreOrderTest()
        {
            BinarySearchTree tree = createTree();

            int[] expectedResult = new int[12] { 47, 22, 9, 5, 17, 33, 29, 46, 68, 53, 48, 88 };
            int[] actualResult = tree.GetPreOrder();

            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetPostOrderTest()
        {
            BinarySearchTree tree = createTree();

            int[] expectedResult = new int[12] { 5, 17, 9, 29, 46, 33, 22, 48, 53, 88, 68, 47};
            int[] actualResult = tree.GetPostOrder();
        }

        [TestMethod]
        public void DeleteLeafNodeTest()
        {
            BinarySearchTree tree = createTree();

            tree.Delete(5);

            int[] expectedResult = new int[11] { 9, 17, 22, 29, 33, 46, 47, 48, 53, 68, 88 };
            int[] actualResult = tree.GetInOrder();

            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void DeleteOneChildNode()
        {
            BinarySearchTree tree = createTree();

            tree.Delete(53);

            int[] expectedResult = new int[11] { 5, 9, 17, 22, 29, 33, 46, 47, 48, 68, 88 };
            int[] actualResult = tree.GetInOrder();

            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void DeleteTwoChildrenNode()
        {
            BinarySearchTree tree = createTree();

            tree.Delete(33);

            int[] expectedResult = new int[11] { 5, 9, 17, 22, 29, 46, 47, 48, 53, 68, 88 };
            int[] actualResult = tree.GetInOrder();

            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void DeleteNonExistantNode()
        {
            BinarySearchTree tree = createTree();

            tree.Delete(100);

            int[] expectedResult = new int[12] { 5, 9, 17, 22, 29, 33, 46, 47, 48, 53, 68, 88 };
            int[] actualResult = tree.GetInOrder();

            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        private BinarySearchTree createTree()
        {
            int[] insertions = new int[12] { 47, 22, 68, 9, 33, 53, 88, 5, 17, 29, 46, 48 };
            BinarySearchTree tree = new BinarySearchTree();

            foreach (int value in insertions)
            {
                tree.Insert(value);
            }

            return tree;
        }

    }
}
