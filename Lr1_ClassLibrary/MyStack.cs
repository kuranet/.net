using System;
using System.Collections;
using System.Collections.Generic;

namespace Lr1_ClassLibrary
{
    public class MyStack<T> : IEnumerable<T>, ICollection
        where T : IComparable
    {
        private T[] _stackContent = new T[1];
        private int ElementsCount { get; set; }
        private int StackCapacity { get; set; }

        public bool IsEmpty => ElementsCount <= 0;

        public int Count => ElementsCount;

        public bool IsReadOnly => true;

        public bool IsSynchronized => true;

        public object SyncRoot => this;

        public event Action<MyStack<T>, T> ElementAdded;

        public MyStack(int size)
        {
            _stackContent = new T[size];
        }

        public MyStack()
        {
        }

        public void Push(T element)
        {
            if (ElementsCount == StackCapacity)
            {
                var newStackCapacity = _stackContent.Length * 2;
                ResizeArray(newStackCapacity);
            }

            _stackContent[++ElementsCount] = element;
        }

        public T Pop()
        {
            if (IsEmpty)
                throw new ArgumentOutOfRangeException("Stack is empty");

            return _stackContent[--ElementsCount];
        }

        public T Peek()
        {
            if (IsEmpty)
                throw new ArgumentOutOfRangeException("Stack is empty");

            return _stackContent[ElementsCount - 1];
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var element in _stackContent)
            {
                yield return element;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var element in _stackContent)
            {
                yield return element;
            }
        }

        public bool Contains(T element)
        {
            foreach (var el in _stackContent)
            {
                if (el.Equals(element))
                    return true;
            }

            return false;
        }

        public void Clear()
        {
            _stackContent = new T[1];

            StackCapacity = 1;
            ElementsCount = 0;
        }

        public void CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException();

            if (index < 0)
                throw new ArgumentOutOfRangeException();

            if (array.Rank > 1)
                throw new RankException();

            var maxIndex = index + ElementsCount;
            if (maxIndex > array.Length)
                throw new ArgumentException();

            var insertedItemIndex = 0;
            for (var i = index; i < maxIndex; i++)
            {
                array.SetValue(_stackContent[insertedItemIndex], insertedItemIndex);
                insertedItemIndex++;
            }
        }

        private void ResizeArray(int newSize)
        {
            var stackContents = _stackContent;
            Array.Resize(ref stackContents, newSize);

            _stackContent = stackContents;
            StackCapacity = newSize;
        }
    }
}
