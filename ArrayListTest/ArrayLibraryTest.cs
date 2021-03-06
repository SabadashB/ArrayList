using NUnit.Framework;
using ArrayLibrary;
using System;

namespace ArrayLibraryTest
{
    public class ArrayLibraryTest
    {
        private IArrayList _arrayList;

        [SetUp]
        public void Setup()
        {
            _arrayList = new ArrayList();
        }

        [TestCase(new int[] { }, 10, new[] { 10 })]
        [TestCase(new[] { 5 }, 10, new[] { 5, 10 })]
        [TestCase(new[] { 1, 2, 3, 4 }, 10, new[] { 1, 2, 3, 4, 10 })]
        public void AddBack_WhenElementsByElementAdded_ShouldFillArray(
            int[] sourceArray, int elementToAdd, int[] expectedArray)
        {
            _arrayList = new ArrayList(sourceArray);

            _arrayList.AddBack(elementToAdd);

            CollectionAssert.AreEqual(expectedArray, _arrayList);
        }

        [TestCase(new int[] {})]
        [TestCase(new[] { 1, 2, 3, 4 })]
        public void ArrayConstructor_WhenCalled_ShouldFillArray(int[] sourceArray)
        {
            _arrayList = new ArrayList(sourceArray);

            CollectionAssert.AreEqual(sourceArray, _arrayList);
        }

        [TestCase(new[] { 5, 3, 1, 2, 7 }, 7)]
        [TestCase(new[] { 5}, 5)]
        [TestCase(new[] { 2, 8 }, 8)]
        [TestCase(new[] { -2, -1, -4, -5, -2 }, -1)]
        public void Max_WhenArrayAdded_ShouldFindMaxValue(
            int[] sourceArray, int expected)
        {
            _arrayList = new ArrayList(sourceArray);

            int actual = _arrayList.Max();

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new[] { 5 }, 5)]
        [TestCase(new[] { 5, -5, 0 }, -5)]
        [TestCase(new[] { 5, 3, 1, 2, 7 }, 1)]
        public void Min_WhenArrayAdded_ShouldFindMinValue
            (int[] sourceArray, int expected)
        {
            _arrayList = new ArrayList(sourceArray);

            int actual = _arrayList.Min();

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new[] { 5, 3, 1, 2, 7 }, 1, 2)]
        [TestCase(new[] { 5 }, 5, 0)]
        [TestCase(new[] { 5, -5, 0 }, -5, 1)]
        [TestCase(new[] { 5, 5, 5 }, 5, 0)]
        [TestCase(new[] { 5, -5, 0 }, 0, 2)]
        [TestCase(new[] { 5, -5, 0 }, 10, -1)]
        [TestCase(new int[] { }, 0, -1)]
        public void IndexOf_WhenCalled_ShouldFindIndexOfElement
            (int[] sourceArray, int searchValue, int expectedIndex)
        {
            _arrayList = new ArrayList(sourceArray);

            int actualIndex = _arrayList.IndexOf(searchValue);

            Assert.AreEqual(expectedIndex, actualIndex);
        }

        [TestCase(
            new int[] { },
            new[] { 6, 7, 8 },
            new[] { 6, 7, 8 })]
        [TestCase(
            new[] { 4 },
            new[] { 6, 7, 8 },
            new[] { 4, 6, 7, 8 })]
        [TestCase(
            new[] { 1, 2, 3, 4, 5 },
            new[] { 6, 7, 8 },
            new[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        public void AddBack_WhenArrayAdded_ShouldAddArrayElementsToEnd(
            int[] sourceArray,
            int[] arrayToAdd,
            int[] expectedArray)
        {
            _arrayList = new ArrayList(sourceArray);

            _arrayList.AddBack(arrayToAdd);

            Assert.AreEqual(expectedArray, _arrayList);
        }

        [TestCase(new int[] { }, 1, new[] { 1 })]
        [TestCase(new int[] { 1, 2, 3 }, 4, new[] { 4, 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, 5, new[] { 5, 1, 2, 3, 4 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, 6, new[] { 6, 1, 2, 3, 4, 5 })]
        public void AddFront_WhenElementAdded_ShouldAddElementToFront(
            int[] sourceArray,
            int elementToAdd,
            int[] expectedArray)
        {
            _arrayList = new ArrayList(sourceArray);

            _arrayList.AddFront(elementToAdd);

            CollectionAssert.AreEqual(expectedArray, _arrayList);
        }

        [TestCase(new int[] { },
            1,
            0,
            new[] { 1 })]
        [TestCase(new int[] { 1, 2, 3 },
            4,
            1,
            new[] { 1, 4, 2, 3 })]
        [TestCase(new int[] { 1, 2, 3, 4 },
            5,
            4,
            new[] { 1, 2, 3, 4, 5 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 },
            6,
            0,
            new[] { 6, 1, 2, 3, 4, 5 })]
        public void AddByIndex_WhenElementAdded_ShouldAddElementToIndexPosition(
            int[] sourceArray,
            int elementToAdd,
            int index,
            int[] expectedArray)
        {
            _arrayList = new ArrayList(sourceArray);

            _arrayList.AddByIndex(index, elementToAdd);

            Assert.AreEqual(expectedArray, _arrayList.ToArray());
        }

        [TestCase(new[] { 1 }, 0, 1, new int[] { })]
        [TestCase(new int[] { 5, 2, 3 }, 0, 5, new[] { 2, 3 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, 2, 3, new[] { 1, 2, 4 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, 4, 5, new[] { 1, 2, 3, 4 })]
        public void RemoveByIndex_WhenValidIndex_ShouldRemoveElementByIndex(
            int[] sourceArray,
            int indexToDelete,
            int expectedDeleted,
            int[] expectedArray)
        {
            _arrayList = new ArrayList(sourceArray);

            int actualDeleted =
                _arrayList.RemoveByIndex(indexToDelete);

            Assert.AreEqual(expectedArray, _arrayList.ToArray());
            Assert.AreEqual(expectedDeleted, actualDeleted);
        }

        [TestCase(new[] { 1 }, 1, new int[] { })]
        [TestCase(new int[] { 5, 2, 3 }, 5, new[] { 2, 3 })]
        [TestCase(new int[] { 3, 2, 3, 4 }, 3, new[] { 2, 3, 4 })]
        [TestCase(new int[] { 8, 2, 3, 4, 5 }, 8, new[] { 2, 3, 4, 5 })]
        public void RemoveFront_WhenValidIndex_ShouldRemoveFirstElement(
            int[] sourceArray,
            int expectedDeleted,
            int[] expectedArray)
        {
            _arrayList = new ArrayList(sourceArray);

            int actualDeleted =
                _arrayList.RemoveFront();

            Assert.AreEqual(expectedArray, _arrayList);
            Assert.AreEqual(expectedDeleted, actualDeleted);
        }

        [TestCase(new[] { 1 }, 1, new int[] { })]
        [TestCase(new int[] { 5, 2, 3 }, 3, new[] { 5, 2 })]
        [TestCase(new int[] { 3, 2, 3, 4 }, 4, new[] { 3, 2, 3 })]
        [TestCase(new int[] { 8, 2, 3, 4, 5 }, 5, new[] { 8, 2, 3, 4 })]
        public void RemoveBack_WhenValidIndex_ShouldRemoveFirstElement(
            int[] sourceArray,
            int expectedDeleted,
            int[] expectedArray)
        {
            _arrayList = new ArrayList(sourceArray);

            int actualDeleted =
                _arrayList.RemoveBack();

            Assert.AreEqual(expectedArray, _arrayList.ToArray());
            Assert.AreEqual(expectedDeleted, actualDeleted);
        }

        [TestCase(new[] { 1 }, 1, 0, new[] { 1 }, new int[] { })]
        [TestCase(new int[] { 5, 2, 3 }, 2, 0, new[] { 5, 2 }, new[] { 3 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 },
            3, 2,
            new[] { 3, 4, 5 }, new[] { 1, 2, 6, 7, 8 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 },
            3, 2,
            new[] { 3, 4, 5 }, new[] { 1, 2 })]
        public void RemoveCountByIndex_WhenValidInput_ShouldRemoveCountElementStartingFromIndex(
            int[] sourceArray,
            int count,
            int index,
            int[] expectedDeleted,
            int[] expectedArray)
        {
            _arrayList = new ArrayList(sourceArray);

            int[] actualDeleted =
                _arrayList.RemoveByIndex(index, count);

            Assert.AreEqual(expectedDeleted, actualDeleted);
            Assert.AreEqual(expectedArray, _arrayList.ToArray());
        }



        [TestCase(new[] { 1 }, 1, new[] { 1 }, new int[] { })]
        [TestCase(new int[] { 5, 2, 3 }, 2, new[] { 2, 3 }, new[] { 5 })]
        [TestCase(new int[] { 5, 2, 3 }, 3, new[] { 5, 2, 3 }, new int[] { })]
        [TestCase(new int[] { 5, 2, 3, 4, 6 }, 4, new[] { 2, 3, 4, 6 }, new[] { 5 })]
        public void RemoveBack_WhenValidInput_ShouldRemoveCountElementStartingFromBack(
            int[] sourceArray,
            int count,
            int[] expectedDeleted,
            int[] expectedArray)
        {
            _arrayList = new ArrayList(sourceArray);

            int[] actualDeleted =
                _arrayList.RemoveBack(count);

            Assert.AreEqual(expectedDeleted, actualDeleted);
            Assert.AreEqual(expectedArray, _arrayList.ToArray());
        }

        [TestCase(new[] { 1 }, 1, new[] { 1 }, new int[] { })]
        [TestCase(new int[] { 5, 2, 3 }, 2, new[] { 5, 2 }, new[] { 3 })]
        [TestCase(new int[] { 5, 2, 3 }, 3, new[] { 5, 2, 3 }, new int[] { })]
        [TestCase(new int[] { 5, 2, 3, 4, 6 }, 4, new[] { 5, 2, 3, 4 }, new[] { 6 })]
        public void RemoveFront_WhenValidInput_ShouldRemoveCountElementStartingFromFront(
            int[] sourceArray,
            int count,
            int[] expectedDeleted,
            int[] expectedArray)
        {
            _arrayList = new ArrayList(sourceArray);

            int[] actualDeleted =
                _arrayList.RemoveFront(count);

            Assert.AreEqual(expectedDeleted, actualDeleted);
            Assert.AreEqual(expectedArray, _arrayList);
        }

        [Test]
        public void RemoveBack_WhenInvalidIndex_ShouldThrowArgumentException()
        {
            try
            {
                _arrayList.RemoveBack();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Array is empty or index is incorrect!",
                    ex.Message);
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void RemoveCountByIndex_WhenInvalidIndex_ShouldThrowArgumentException()
        {
            try
            {
                _arrayList.RemoveByIndex(0,4);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Array is empty",
                    ex.Message);
                Assert.Pass();
            }

            Assert.Fail();
        }

        [TestCase(new int[] { 1 }, new int[] { 1 })]
        [TestCase(new int[] { 5, 2, 3 }, new int[] { 3, 2, 5 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, new int[] { 4, 3, 2, 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, new int[] { 5, 4, 3, 2, 1 })]
        public void Reverse_WhenValidIndex_ShouldReturnReverseArray(
           int[] sourceArray,
           int[] expectedArray)
        {
            _arrayList = new ArrayList(sourceArray);

            _arrayList.Reverse();

            Assert.AreEqual(expectedArray, _arrayList);
        }

        [TestCase(new int[] { 1 }, true, new int[] { 1 })]
        [TestCase(new int[] { 5, 2, 3 }, true, new int[] { 2, 3, 5 })]
        [TestCase(new int[] { 2, 3, 1, 4 }, true, new int[] { 1, 2, 3, 4 })]
        [TestCase(new int[] { 5, 3, 2, 1, 4 }, true, new int[] { 1, 2, 3, 4, 5 })]
        public void SortUp_WhenValidIndex_ShouldReturnSortUpArray(
           int[] sourceArray,
           bool ascending,
           int[] expectedArray)
        {
            _arrayList = new ArrayList(sourceArray);

            _arrayList.Sort(ascending);

            Assert.AreEqual(expectedArray, _arrayList);
        }

        [TestCase(new int[] { 1 }, false, new int[] { 1 })]
        [TestCase(new int[] { 5, 2, 3 }, false, new int[] { 5, 3, 2 })]
        [TestCase(new int[] { 2, 3, 1, 4 }, false, new int[] { 4, 3, 2, 1 })]
        [TestCase(new int[] { 5, 3, 2, 1, 4 }, false, new int[] { 5, 4, 3, 2, 1 })]
        public void SortDown_WhenValidIndex_ShouldReturnSortDownArray(
            int[] sourceArray,
            bool ascending,
            int[] expectedArray)
        {
            _arrayList = new ArrayList(sourceArray);

            _arrayList.Sort(ascending);

            Assert.AreEqual(expectedArray, _arrayList);
        }

        [TestCase(new[] { 4 }, 4, 0, new int[] { })]
        [TestCase(new int[] { 2, 5, 3 }, 5, 1, new[] { 2, 3 })]
        [TestCase(new int[] { 2, 3, 3, 4 }, 3, 1, new[] { 2, 3, 4 })]
        [TestCase(new int[] { 3, 2, 8, 4, 8 }, 8, 2, new[] { 3, 2, 4, 8 })]
        public void Remove_WhenValidIndex_ShouldRemoveElement(
            int[] sourceArray,
            int expectedDeleted,
            int expectedIndex,
            int[] expectedArray)
        {
            _arrayList = new ArrayList(sourceArray);

            int actualDeleted =
                _arrayList.Remove(expectedDeleted);

            Assert.AreEqual(expectedArray, _arrayList);
            Assert.AreEqual(expectedIndex, actualDeleted);
        }

        [TestCase(new[] { 4 }, 4, 1, new int[] { })]
        [TestCase(new int[] { 2, 5, 3 }, 5, 1, new[] { 2, 3 })]
        [TestCase(new int[] { 2, 3, 3, 4 }, 3, 2, new[] { 2, 4 })]
        [TestCase(new int[] { 3, 2, 8, 4, 8 }, 8, 2, new[] { 3, 2, 4 })]
        public void RemoveAll_WhenValidIndex_ShouldRemoveAllElements(
            int[] sourceArray,
            int expectedDeleted,
            int expectedCount,
            int[] expectedArray)
        {
            _arrayList = new ArrayList(sourceArray);

            int actualCount = _arrayList.RemoveAll(expectedDeleted);

            Assert.AreEqual(expectedArray, _arrayList);
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestCase(
            new[] { 1, 2, 3, 4, 5 },
            2,
            new[] { 6, 7, 8 },
            new[] { 1, 2, 6, 7, 8, 3, 4, 5 })]
        [TestCase(
            new[] { 1, 2, 3, 4, 5 },
            1,
            new[] { 6, 7, 8 },
            new[] { 1, 6, 7, 8, 2, 3, 4, 5 })]
        public void AddByIndex_WhenArrayAdded_ShouldAddArrayElementsToIndex(
            int[] sourceArray,
            int index,
            int[] arrayToAdd,
            int[] expectedArray)
        {
            _arrayList = new ArrayList(sourceArray);

            _arrayList.AddByIndex(index, new ArrayList(arrayToAdd));

            CollectionAssert.AreEqual(expectedArray, _arrayList);
        }

        [TestCase(
            new[] { 1, 2, 3, 4, 5 },
            new[] { 6, 7, 8 },
            new[] { 6, 7, 8, 1, 2, 3, 4, 5 })]
        [TestCase(
            new int[] { },
            new[] { 6, 7, 8 },
            new[] { 6, 7, 8 })]
        [TestCase(
            new[] { 1},
            new[] { 6, 7, 8 },
            new[] { 6, 7, 8, 1})]
        public void AddInFront_WhenArrayAdded_ShouldAddArrayElementsToFront(
            int[] sourceArray,
            int[] arrayToAdd,
            int[] expectedArray)
        {
            _arrayList = new ArrayList(sourceArray);

            _arrayList.AddFront(new ArrayList(arrayToAdd));

            Assert.AreEqual(expectedArray, _arrayList);
        }
    }
}