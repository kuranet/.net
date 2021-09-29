using System;
using System.Collections;
using System.Collections.Generic;

namespace Lr1_ClassLibrary
{
    public delegate void AddingActionHandler(object source, object addedElement);
    public delegate void ClearingActionHandler(object source);

    public class MyStack<T> : IEnumerable<T>, ICollection
        where T : IComparable
    {
        private T[] _stackContent = new T[1];
        private int ElementsCount { get; set; }
        private int StackCapacity { get; set; }

        public bool IsEmpty => ElementsCount <= 0;

        public int Count => ElementsCount;

        public bool IsReadOnly => false;

        public bool IsSynchronized => true;

        public object SyncRoot => this;

        public event AddingActionHandler SuccesfullAddition;
        public event ClearingActionHandler SuccesfullClearing;

        public MyStack(int size)
        {
            _stackContent = new T[size];
        }

        public MyStack()
        {
            StackCapacity = 1;
        }

        public void Push(T element)
        {
            if (ElementsCount >= StackCapacity)
            {
                StackCapacity = _stackContent.Length * 2;
                Array.Resize(ref _stackContent, StackCapacity);
            }

            _stackContent[ElementsCount++] = element;
            SuccesfullAddition(this, element);
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
            for (var i = 0; i < ElementsCount; i++)
            {
                yield return _stackContent[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var i = 0; i < ElementsCount; i++)
            {
                yield return _stackContent[i];
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

            SuccesfullClearing(this);
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
    }
}
