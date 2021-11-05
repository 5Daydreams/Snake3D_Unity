using System;

namespace _Code.Player.LinkedList
{
    public class LinkedList<T> where T : ILListNode<T>
    {
        public T Head;

        public T GetNodeAtIndex(int value)
        {
            T temp = Head;
            
            while (value > 0)
            {
                temp = temp.Next();
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
            
            while (temp.Next() != null)
            {
                temp = temp.Next();
            }

            return temp;
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

                while (temp.Next() != null)
                {
                    temp = temp.Next();
                    size++;
                }

                return size;
            }
        }
    }
}

