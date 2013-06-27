using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsLibrary
{
    public class GenericLinkedListIterator<T> : IIterator<T>
    {
        private readonly GenericLinkedList<T> _linkedList;

        public GenericLinkedListIterator(GenericLinkedList<T> linkedList)
        {
            _linkedList = linkedList;
        }

        public T Current
        {
            get { return CurrentNode.Value; }
        }

        internal Node<T> CurrentNode
        {
            get;
            private set;
        }

        public bool MoveNext()
        {
            if (CurrentNode == null)
                CurrentNode = _linkedList.First;
            else
                CurrentNode = CurrentNode.Next;

            return CurrentNode != null;
        }
    }
}
