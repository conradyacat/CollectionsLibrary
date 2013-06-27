using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CollectionsLibrary.UnitTests
{
    [TestClass]
    public class SingleThreadedGenericLinkedListTest
    {
        [TestMethod]
        public void TestHeadOperation()
        {
            var linkedList = new GenericLinkedList<string>();
            linkedList.AddFirst("A");
            linkedList.AddFirst("B");
            linkedList.AddFirst("C");
            linkedList.AddFirst("D");

            Assert.AreEqual(4, linkedList.Count);

            linkedList.RemoveFirst();

            Assert.AreEqual(3, linkedList.Count);

            var iterator = linkedList.GetIterator();
            var expectedValues = new[] { "A", "B", "C" };
            var counter = 2;
            while (iterator.MoveNext())
            {
                Assert.AreEqual<string>(expectedValues[counter], iterator.Current);
                counter--;
            }

            Assert.AreEqual<int>(-1, counter);
        }

        [TestMethod]
        public void TestTailOperation()
        {
            var linkedList = new GenericLinkedList<string>();
            linkedList.AddLast("A");
            linkedList.AddLast("B");
            linkedList.AddLast("C");
            linkedList.AddLast("D");

            Assert.AreEqual(4, linkedList.Count);

            linkedList.RemoveLast();

            Assert.AreEqual(3, linkedList.Count);

            var iterator = linkedList.GetIterator();
            var expectedValues = new[] { "A", "B", "C" };
            var counter = 0;
            while (iterator.MoveNext())
            {
                Assert.AreEqual<string>(expectedValues[counter], iterator.Current);
                counter++;
            }

            Assert.AreEqual<int>(3, counter);
        }

        [TestMethod]
        public void TestHeadAndTailOperation()
        {
            var linkedList = new GenericLinkedList<string>();
            linkedList.AddFirst("A");
            linkedList.AddLast("B");
            linkedList.AddFirst("C");
            linkedList.AddLast("D");

            Assert.AreEqual(4, linkedList.Count);

            var iterator = linkedList.GetIterator();
            var expectedValues = new[] { "C", "A", "B", "D" };
            var counter = 0;
            while (iterator.MoveNext())
            {
                Assert.AreEqual<string>(expectedValues[counter], iterator.Current);
                counter++;
            }

            linkedList.RemoveLast();

            Assert.AreEqual(3, linkedList.Count);

            linkedList.RemoveFirst();

            Assert.AreEqual(2, linkedList.Count);
            
            iterator = linkedList.GetIterator();
            expectedValues = new[] { "A", "B" };
            counter = 0;
            while (iterator.MoveNext())
            {
                Assert.AreEqual<string>(expectedValues[counter], iterator.Current);
                counter++;
            }

            Assert.AreEqual<int>(2, counter);
        }

        [TestMethod]
        public void TestFindFirst()
        {
            var linkedList = new GenericLinkedList<string>();
            var findNode = new Node<string>("A");
            linkedList.AddFirst(findNode);
            linkedList.AddLast("B");
            linkedList.AddFirst("C");
            linkedList.AddLast("D");

            var actualNode = linkedList.FindFirst("A");

            Assert.IsNotNull(actualNode);
            Assert.IsTrue(object.ReferenceEquals(findNode, actualNode));
        }

        [TestMethod]
        public void TestFindLast()
        {
            var linkedList = new GenericLinkedList<string>();
            linkedList.AddFirst("A");
            linkedList.AddFirst("B");
            linkedList.AddFirst("C");
            var findNode = new Node<string>("A");
            linkedList.AddLast(findNode);

            var actualNode = linkedList.FindLast("A");

            Assert.IsNotNull(actualNode);
            Assert.IsTrue(object.ReferenceEquals(findNode, actualNode));
        }

        [TestMethod]
        public void TestClear()
        {
            var linkedList = new GenericLinkedList<string>();
            linkedList.AddFirst("A");
            linkedList.AddFirst("B");
            linkedList.AddFirst("C");
            linkedList.AddFirst("D");

            Assert.AreEqual(4, linkedList.Count);

            linkedList.Clear();

            Assert.AreEqual(0, linkedList.Count);
        }
    }
}
