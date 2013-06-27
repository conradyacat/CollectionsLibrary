using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsLibrary
{
    public class Node<T>
    {
        public Node(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }

        public Node<T> Previous { get; internal set; }

        public Node<T> Next { get; internal set; }
    }
}
