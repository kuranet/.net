using System;
using System.Collections;
using System.Collections.Generic;

namespace Lr1_ClassLibrary
{
    public class StackArgs : EventArgs
    {
        public string Message { get; set; }
    }

    public class MyStack<T> : IEnumerable<T>, ICollection
    {
        public delegate void AddingActionHandler(object source, StackArgs addedElement);
        public delegate void ClearingActionHandler(object source, StackArgs addedElement);

        private T[] _stackContent = new T[1];
        private int ElementsCount { get; set; }
        public int StackCapacity { get; private set; }

        public bool IsEmpty => ElementsCount <= 0;

        public int Count => ElementsCount;

        public bool IsReadOnly => false;

        public bool IsSynchronized => true;

        public object SyncRoot => this;

        public event AddingActionHandler SuccesfullAddition;
        public event ClearingActionHandler SuccesfullClearing;

        public MyStack() : this(1) { }

        public MyStack(int size)
        {
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException("Stack size can't be less that zero");
            }

            if (size == 0)
            {
                size = 1;
            }

            _stackContent = new T[size];
            StackCapacity = size;
        }

        public void Push(T element)
        {
            if (ElementsCount >= StackCapacity)
            {
                StackCapacity = _stackContent.Length * 2;
                Array.Resize(ref _stackContent, StackCapacity);
            }

            _stackContent[ElementsCount++] = element;

            SuccesfullAddition?.Invoke(this, new StackArgs() { Message = $"Successfully add {element}" });
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
            for (var i = ElementsCount - 1; i >= 0; i--)
            {
                yield return _stackContent[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var i = ElementsCount - 1; i >= 0; i--)
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

            SuccesfullClearing?.Invoke(this, new StackArgs() { Message = "Successfully cleared"});
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
            if (maxIndex >= array.Length)
                throw new ArgumentException();

            var insertedItemIndex = 0;
            for (var i = index; i <= maxIndex; i++)
            {
                array.SetValue(_stackContent[insertedItemIndex], i);
                insertedItemIndex++;
            }
        }
    }
}
