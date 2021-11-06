using System;

namespace _Code.LinkedList
{
    public class CustomLinkedList<T> where T : ILListNode<T>
    {
        public T Head;

        public T GetNodeAtIndex(int value)
        {
            T temp = Head;
            
            while (value > 0)
            {
                temp = temp.Next;
                value--;

                if (temp == null)
                {
                    throw new IndexOutOfRangeException("Index longer than linked list size");
                }
            }

            return temp;
        }

        public T GetTail()
        {
            T temp = Head;

            if (temp == null)
            {
                return temp;
            }
            
            while (temp.Next != null)
            {
                temp = temp.Next;
            }

            return temp;
        }

        public void Add(T newNode)
        {
            T temp = GetTail();

            if (temp == null)
            {
                Head = newNode;
                return;
            }

            temp.Next = newNode;
        }

        public void RemoveAt(int index)
        {
            T currentNode = Head;

            if (currentNode == null)
            {
                throw new NullReferenceException("Linked List has no head");
            }
            
            if (index == 0)
            {
                Head = Head.Next;
            }

            T prevNode = Head;

            while (index > 0)
            {
                prevNode = currentNode;
                currentNode = currentNode.Next;
                index--;
                
                if (currentNode == null)
                {
                    throw new IndexOutOfRangeException("Index longer than linked list size");
                }
            }

            prevNode.Next = currentNode.Next;
        }

        public int Count
        {
            get
            {
                T temp = Head;

                if (temp == null)
                {
                    return 0;
                }
                
                int size = 1;

                while (temp.Next != null)
                {
                    temp = temp.Next;
                    size++;
                }

                return size;
            }
        }
    }
}

