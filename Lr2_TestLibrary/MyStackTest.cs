using Lr1_ClassLibrary;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lr2_TestLibrary
{
    [TestFixture]
    class MyStackTest
    {
        [Test]
        [TestCase(-3)]
        [TestCase(-150)]
        public void Create_NegativeSize_Exception(int size)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new MyStack<string>(size));

            Assert.NotNull(ex);
        }

        [Test]
        public void Create_EmptyConstructor()
        {
            var stack = new MyStack<int>();

            Assert.IsTrue(stack.IsEmpty);
        }

        [Test]
        public void Create_ZeroCapacity()
        {
            var stack = new MyStack<int>(0);

            Assert.IsTrue(stack.IsEmpty);
            Assert.AreEqual(1, stack.StackCapacity);
        }

        [Test]
        [TestCase(10)]
        [TestCase(2)]
        [TestCase(3495)]
        public void Create_Size_Equal_Capacity(int size)
        {
            var stack = new MyStack<int>(size);

            Assert.AreEqual(size, stack.StackCapacity);
        }

        [Test]
        [TestCase(10)]
        [TestCase(2)]
        [TestCase(3495)]
        public void Create_PositiveSize_IsEmpty(int size)
        {
            var stack = new MyStack<int>(size);

            Assert.IsTrue(stack.IsEmpty);
        }

        [Test]
        public void Push_ToEmpty_MakesNotEmpty()
        {
            var stack = new MyStack<int>(10);
            var testValue = 3;

            stack.Push(testValue);

            Assert.IsFalse(stack.IsEmpty);
        }

        private static readonly object[] _sourceLists =
        {
        new object[] {new List<int> {1}},            //case 1
        new object[] {new List<int> {1, 2}},         //case 2
        new object[] {new List<int> {1, 2, 3, 4, 5}} //case 3
        };

        [Test]
        [TestCaseSource(nameof(_sourceLists))]
        public void Push_Count(List<int> values)
        {
            var stack = new MyStack<int>(10);

            foreach (var value in values)
            {
                stack.Push(value);
            }

            Assert.AreEqual(values.Count, stack.Count);
        }

        [Test]
        [TestCase(10)]
        [TestCase(2)]
        [TestCase(3495)]
        public void Clear_IsEmpty(int size)
        {
            var stack = new MyStack<int>(size);

            stack.Clear();

            Assert.IsTrue(stack.IsEmpty);
        }

        [Test]
        public void Peek_Empty_Exeption()
        {
            var stack = new MyStack<string>();
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => stack.Peek());

            Assert.NotNull(ex);
        }

        [Test]
        [TestCaseSource(nameof(_sourceLists))]
        public void Peek_NotEmpty_GetLastAdded(List<int> values)
        {
            // Arrange.
            var stack = new MyStack<int>();

            foreach (var value in values)
            {
                stack.Push(value);
            }

            // Act.
            var peekValue = stack.Peek();

            // Assert.
            Assert.AreEqual(values.Last(), peekValue);
        }

        [Test]
        public void Pop_Empty_Exeption()
        {
            var stack = new MyStack<string>();

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => stack.Pop());

            Assert.NotNull(ex);
        }

        [Test]
        [TestCaseSource(nameof(_sourceLists))]
        public void Pop_NotEmpty_GetLastAdded(List<int> values)
        {
            // Arrange.
            var stack = new MyStack<int>();

            foreach (var value in values)
            {
                stack.Push(value);
            }

            // Act.
            var popValue = stack.Pop();

            // Assert.
            Assert.AreEqual(values.Last(), popValue);
        }

        [Test]
        public void Pop_NotEmpty_PeekIsPrevious()
        {
            // Arrange.
            var testValues = new List<int> { 1, 2, 3, 4, 5 };
            var stack = new MyStack<int>();

            foreach (var value in testValues)
            {
                stack.Push(value);
            }

            // Act.
            var popValue = stack.Pop();

            // Assert.
            Assert.AreEqual(testValues[testValues.Count - 2], stack.Peek());
        }

        [Test]
        public void Contains_True()
        {
            var testValues = new List<int> { 1, 2, 3 };
            var requiredValue = 3;
            var stack = new MyStack<int>();

            foreach (var value in testValues)
            {
                stack.Push(value);
            }

            var contains = stack.Contains(requiredValue);

            Assert.IsTrue(contains);
        }

        [Test]
        public void Contains_False()
        {
            var testValues = new List<int> { 1, 2, 3 };
            var requiredValue = 5;
            var stack = new MyStack<int>();

            foreach (var value in testValues)
            {
                stack.Push(value);
            }

            var contains = stack.Contains(requiredValue);

            Assert.IsFalse(contains);
        }

        [Test]
        public void GetEnumerator_Object()
        {
            var values = new List<int> { 1, 2, 3 };
            var stack = new MyStack<int>();

            foreach (var value in values)
            {
                stack.Push(value);
            }

            var lastValueIndex = values.Count - 1;
            foreach (var value in (IEnumerable)stack)
            {
                Assert.AreEqual(value, values[lastValueIndex]);
                lastValueIndex--;
            }
        }

        [Test]
        public void GetEnumerator_Generic()
        {
            var values = new List<int> { 1, 2, 3 };
            var stack = new MyStack<int>();

            foreach (var value in values)
            {
                stack.Push(value);
            }

            var lastValueIndex = values.Count - 1;
            foreach (var value in stack)
            {
                Assert.AreEqual(values[lastValueIndex], value);
                lastValueIndex--;
            }
        }

        [Test]
        public void Event_SuccesfullAddition()
        {
            var testElement = 3;
            string actualMessage = null;
            var stack = new MyStack<int>();
            stack.SuccesfullAddition += delegate (object sender, StackArgs args)
            {
                actualMessage = args.Message;
            };

            stack.Push(testElement);
            Assert.AreEqual($"Successfully add {testElement}", actualMessage);
        }
        [Test]
        public void Event_SuccesfullClearing()
        {
            string actualMessage = null;
            var stack = new MyStack<int>();
            stack.SuccesfullClearing += delegate (object sender, StackArgs args)
            {
                actualMessage = args.Message;
            };

            stack.Clear();
            Assert.AreEqual("Successfully cleared", actualMessage);
        }

        [Test]
        public void CopyTo_NullTarget_ArgumentNullException()
        {
            var stack = new MyStack<int>();

            var ex = Assert.Throws<ArgumentNullException>(() => stack.CopyTo(null, 0));

            Assert.NotNull(ex);
        }

        [Test]
        public void CopyTo_NegativeIndex_ArgumentOutOfRangeException()
        {
            var stack = new MyStack<int>();
            Array myArr = Array.CreateInstance(typeof(int), 2, 3, 4);

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => stack.CopyTo(myArr, -3));

            Assert.NotNull(ex);
        }

        [Test]
        public void CopyTo_Rank2_RankException()
        {
            var stack = new MyStack<int>();
            Array myArr = Array.CreateInstance(typeof(int), 2, 3, 4);

            var ex = Assert.Throws<RankException>(() => stack.CopyTo(myArr, 3));

            Assert.NotNull(ex);
        }

        [Test]
        public void CopyTo_MaxIndex_ArgumentException()
        {
            var stack = new MyStack<int>();
            Array myArr = Array.CreateInstance(typeof(int), 2);

            var ex = Assert.Throws<ArgumentException>(() => stack.CopyTo(myArr, 3));

            Assert.NotNull(ex);
        }

        [Test]
        public void CopyTo_Successful()
        {
            var arrLenght = 10;
            var insertIndex = 0;
            var values = new List<int> { 1, 2, 3 };
            var stack = new MyStack<int>();
            foreach (var value in values)
            {
                stack.Push(value);
            }

            Array myArr = Array.CreateInstance(typeof(int), arrLenght);
            for (var i = 0; i < arrLenght; i++)
            {
                myArr.SetValue(null, i);
            }

            var checkedItemIndex = 0;
            stack.CopyTo(myArr, insertIndex);
            for (var i = 0; i < arrLenght; i++)
            {
                if (i == insertIndex + checkedItemIndex)
                {
                    var isAllChecked = checkedItemIndex > values.Count - 1;
                    if (isAllChecked == false)
                    {
                        Assert.AreEqual(values[checkedItemIndex], myArr.GetValue(i));
                        checkedItemIndex++;
                        continue;
                    }                
                }

                Assert.AreEqual(0, myArr.GetValue(i));
            }
        }
    }
}
