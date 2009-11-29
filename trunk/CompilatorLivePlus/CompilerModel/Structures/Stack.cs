using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompilerModel.Structures
{
    public class Stack
    {
        private Node first = null;
        private int count = 0;

        public bool Empty
        {
            get
            {
                return (first == null);
            }
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        public object Pop()
        {
            if (first == null)
            {
                throw new InvalidOperationException("Cant pop from an empty stack");
            }
            else
            {
                object temp = first.Value;
                first = first.Next;
                count--;
                return temp;
            }
        }

        public void Push(object o)
        {
            first = new Node(o, first);
            count++;
        }

        class Node
        {
            public Node Next;
            public object Value;

            public Node(object value) : this(value, null) { }

            public Node(object value, Node next)
            {
                Next = next;
                Value = value;
            }
        }
    }

    class StackTest
    {
        static void Main()
        {
            Stack s = new Stack();

            if (s.Empty)
                Console.WriteLine("Stack is Empty");
            else
                Console.WriteLine("Stack is not Empty");

            for (int i = 0; i < 5; i++)
                s.Push(i);

            Console.WriteLine("Items in Stack {0}", s.Count);
            for (int i = 0; i < 5; i++)
                Console.WriteLine("Popped Item is {0} and the count is {1}", s.Pop(), s.Count);

            s = null;
        }
    }
}
