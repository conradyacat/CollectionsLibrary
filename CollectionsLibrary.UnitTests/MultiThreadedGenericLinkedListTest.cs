using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CollectionsLibrary.UnitTests
{
    [TestClass]
    public class MultiThreadedGenericLinkedListTest
    {
        [TestMethod]
        public void TestHeadOperation()
        {
            var linkedList = new GenericLinkedList<string>();

            var t1 = Task.Run(() =>
                {
                    for(var i = 0; i < 100; i++)
                    {
                        Thread.Sleep(100);
                        linkedList.AddFirst("Task1-" + i.ToString());
                    }
                });

            var t2 = Task.Run(() =>
            {
                for (var i = 0; i < 100; i++)
                {
                    Thread.Sleep(100);
                    linkedList.AddFirst("Task2-" + i.ToString());
                }
            });

            var t4 = Task.Run(() =>
            {
                for (var i = 0; i < 100; i++)
                {
                    Thread.Sleep(100);
                    linkedList.AddFirst("Task3-" + i.ToString());
                }
            });

            var t3 = Task.Run(() =>
            {
                for (var i = 0; i < 10; i++)
                {
                    Thread.Sleep(300);
                    linkedList.RemoveFirst();
                }
            });

            var t5 = Task.Run(() =>
            {
                for (var i = 0; i < 100; i++)
                {
                    Thread.Sleep(200);
                    linkedList.RemoveFirst();
                }
            });

            Task.WaitAll(new[] { t1, t2, t3, t4, t5 });

            var iterator = linkedList.GetIterator();
            while (iterator.MoveNext())
            {
                Debug.WriteLine(iterator.Current);
            }
        }
    }
}
