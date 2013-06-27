using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CollectionsLibrary
{
    public class GenericLinkedList<T>
    {
        private object _syncRoot = new object();
        private volatile Node<T> _head;
        // reference to the last node to make it easier if the operation will start from the end
        private volatile Node<T> _tail;
        private volatile int _count;

        public GenericLinkedList()
        {
        }

        public bool IsEmpty
        {
            get { return Count == 0; }
        }

        public int Count
        {
            get
            {
                lock (_syncRoot)
                {
                    return _count;
                }
            }
        }

        internal Node<T> First
        {
            get
            {
                lock (_syncRoot)
                {
                    return _head;
                }
            }
        }

        internal Node<T> Last
        {
            get
            {
                lock (_syncRoot)
                {
                    return _tail;
                }
            }
        }

        public void AddFirst(Node<T> newNode)
        {
            lock (_syncRoot)
            {
                if (IsEmpty)
                {
                    _head = newNode;
                    _tail = newNode;
                }
                else
                {
                    // reference the new node's next item to the current head reference
                    newNode.Next = _head;
                    // reference the current head's previous item to the new node since 
                    // we're pushing the current head down
                    _head.Previous = newNode;
                    // set the new node to the head
                    _head = newNode;
                }

                _count++;
            }
        }

        public void AddFirst(T value)
        {
            AddFirst(new Node<T>(value));
        }

        public void AddLast(Node<T> newNode)
        {
            lock (_syncRoot)
            {
                if (IsEmpty)
                {
                    _head = newNode;
                    _tail = newNode;
                }
                else
                {
                    // reference the current tail as the previous item of the new node
                    newNode.Previous = _tail;
                    // reference the new node as the current tail's next item
                    _tail.Next = newNode;
                    // set the new node to the tail
                    _tail = newNode;
                }

                _count++;
            }
        }

        public void AddLast(T value)
        {
            AddLast(new Node<T>(value));
        }

        public void RemoveFirst()
        {
            lock (_syncRoot)
            {
                if (IsEmpty)
                    throw new Exception("There are no item(s) remove.");

                if (object.ReferenceEquals(_head, _tail))
                {
                    _head = null;
                    _tail = null;
                }
                else
                {
                    _head = _head.Next;
                }

                _count--;
            }
        }

        public void RemoveLast()
        {
            lock (_syncRoot)
            {
                if (IsEmpty)
                    throw new Exception("There are no item(s) remove.");

                if (object.ReferenceEquals(_head, _tail))
                {
                    _head = null;
                    _tail = null;
                }
                else
                {
                    _tail = _tail.Previous;
                    _tail.Next = null;
                }

                _count--;
            }
        }

        public void Clear()
        {
            lock (_syncRoot)
            {
                _head = null;
                _tail = null;
                _count = 0;
            }
        }

        /// <summary>
        /// Find the first occurence
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Node<T> FindFirst(T value)
        {
            var iterator = new GenericLinkedListIterator<T>(this);

            while (iterator.MoveNext())
            {
                if (iterator.Current.Equals(value))
                    return iterator.CurrentNode;
            }

            return null;
        }

        /// <summary>
        /// Find the last occurrence
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Node<T> FindLast(T value)
        {
            var iterator = new GenericLinkedListIterator<T>(this);
            Node<T> matchingNode = null;

            while (iterator.MoveNext())
            {
                if (iterator.Current.Equals(value))
                    matchingNode = iterator.CurrentNode;
            }

            return matchingNode;
        }

        /// <summary>
        /// Iterator to aid in iterating into the linkedlist
        /// </summary>
        /// <returns></returns>
        public IIterator<T> GetIterator()
        {
            lock (_syncRoot)
            {
                return new GenericLinkedListIterator<T>(this);
            }
        }
    }
}
