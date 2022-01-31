using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ArrayLibrary
{
    public class ArrayList : IArrayList
    {
        private const double IncreaseCoef = 1.33;
        private const int DefaultSize = 5;

        private int[] _array;
        private int _currentCount;

        public int Capacity => _array.Length;
        public int Length => _currentCount;

        public int this[int index]
        {
            get
            {
                if (index >= 0 && index < _currentCount)
                {
                    return _array[index];
                }

                throw new ArgumentOutOfRangeException("Index was out of range!");
            }
            set
            {
                if (index >= 0 && index < _currentCount)
                {
                    _array[index] = value;
                }

                throw new ArgumentOutOfRangeException("Index was out of range!");
            }
        }

        public ArrayList()
        {
            _array = new int[DefaultSize];
            _currentCount = 0;
        }

        public ArrayList(int capacity)
        {
            _array = new int[capacity];
            _currentCount = 0;
        }

        public ArrayList(int[] array)
        {
            int size = (int)(array.Length * IncreaseCoef);
            size = size >= DefaultSize ? size : DefaultSize;
            _array = new int[size];
            _currentCount = array.Length;

            for (int i = 0; i < _currentCount; i++)
            {
                _array[i] = array[i];
            }
        }

        public void AddFront(int element)
        {
            AddByIndex(0, element);
        }

        public void AddBack(int element)
        {
            UpdateSize();

            _array[_currentCount++] = element;
        }

        public void AddByIndex(int index, int element)
        {
            UpdateSize();

            for (int i = _currentCount; i > index; i--)
            {
                _array[i] = _array[i - 1];
            }

            _array[index] = element;

            _currentCount++;
        }

        public int Max()
        {
            int maxI = MaxI();
            if (maxI == -1)
            {
                throw new ArgumentException("Array is empty!");
            }

            return _array[maxI];
        }

        public int MaxI()
        {
            EmptyArray();
            if (_currentCount == 0)
            {
                return -1;
            }

            int maxI = 0;
            for (int i = 1; i < _currentCount; i++)
            {
                if (_array[maxI] < _array[i])
                {
                    maxI = i;
                }
            }

            return maxI;
        }

        public int Min()
        {
            int minI = MinI();
            return _array[minI];
        }

        public int MinI()
        {
            EmptyArray();
            if (_currentCount == 0)
            {
                throw new ArgumentException("Array is empty!");
            }

            int minI = 0;
            for (int i = 1; i < _currentCount; i++)
            {
                if (_array[minI] > _array[i])
                {
                    minI = i;
                }
            }

            return minI;
        }

        public int RemoveFront()
            => RemoveByIndex(0);

        public int RemoveBack()
            => RemoveByIndex(_currentCount - 1);

        public int RemoveByIndex(int index)
        {
            int result;
            try
            {
                result = this[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentException(
                    "Array is empty or index is incorrect!");
            }
            for (int i = index; i < _currentCount - 1; i++)
            {
                _array[i] = _array[i + 1];
            }

            --_currentCount;

            return result;
        }

        public int[] RemoveFront(int count)
        {
            int[] result = RemoveByIndex(0, count);
            return result;
        }

        public int[] RemoveBack(int count)
        {
            int[] result = new int[count];

            int j = 0;
            for (int i = _currentCount - count; i <= _currentCount - 1; i++)
            {
                result[j++] = _array[i];
            }

            _currentCount -= count;

            return result;
        }

        public int[] RemoveByIndex(int index, int count)
        {
            EmptyArray();
            int[] result = new int[count];
            if (index > _currentCount || count > _currentCount)
            {
                throw new ArgumentException("Invalid index or count!");
            }

            for (int i = index, j = 0; j < count; i++, j++)
            {
                result[j] = _array[i];
            }
            for (int i = index; i < _currentCount - count; i++)
            {
                _array[i] = _array[i + count];
            }

            _currentCount -= count;

            return result;
        }

        public int IndexOf(int element)
        {
            int result = -1;
            for (int i = 0; i < _currentCount; i++)
            {
                if (_array[i] == element)
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        public void Reverse()
        {
            EmptyArray();
            for (int i = 0; i < _currentCount / 2; i++)
            {
                Swap(ref _array[i], ref _array[_currentCount - 1 - i]);
            }
        }

        public void Sort(bool ascending = true)
        {
            EmptyArray();
            int modifier = ascending ? 1 : -1;
            for (int i = 0; i < _currentCount -1; i++)
            {
                for (int j = i + 1; j < _currentCount; j++)
                {
                    if ((_array[i].CompareTo(_array[j])* modifier) == 1)
                    {
                        Swap(ref _array[i], ref _array[j]);
                    }
                }
            }
        }

        public int Remove(int value)
        {
            EmptyArray();
            int index = -1;
            for (int i = 0; i < _currentCount; i++)
            {
                if (_array[i] == value)
                {
                    index = i;
                    break;
                }
            }

            --_currentCount;
            for (int i = index; i < _currentCount; i++)
            {
                _array[i] = _array[i + 1];
            }

            return index;
        }

        public int RemoveAll(int value)
        {
            EmptyArray();
            int count = CountOf(value);

            int j = 0;
            for (int i = 0; i < Length; i++)
            {
                if (_array[i] != value)
                {
                    _array[j++] = _array[i];
                }
            }

            _currentCount -= count;

            return count;
        }

        private int CountOf(int value)
        {
            int count = 0;
            for (int i = 0; i < _currentCount; i++)
            {
                if (_array[i] == value)
                {
                    count++;
                }
            }

            return count;
        }

        public void AddBack(IEnumerable<int> arrayList)
        {
            int size = arrayList.Count();
            UpdateSize(size);
            int i = _currentCount;
            foreach (var item in arrayList)
            {
                _array[i++] = item;
            }

            _currentCount += size;
        }

        public void AddFront(IArrayList arrayList)
        {
            AddByIndex(0, arrayList);
        }

        public void AddByIndex(int index, IArrayList arrayList)
        {
            var array = arrayList.ToArray();
            UpdateSize(array.Length);
            int newLenght = _currentCount + array.Length;
            for (int i = 0; i < _currentCount - index; i++)
            {
                _array[newLenght - i - 1] = _array[_currentCount - i - 1];
            }

            for (int i = index, j = 0; j < array.Length; i++, j++)
            {
                _array[i] = array[j];
            }

            _currentCount += array.Length;
        }


        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < _currentCount; i++)
            {
                yield return _array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int[] ToArray()
        {
            int[] result = new int[Length];
            for (int i = 0; i < Length; i++)
            {
                result[i] = _array[i];
            }

            return result;
        }

        private static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        private void EmptyArray()
        {
            if (_array == null || _currentCount == 0)
            {
                throw new ArgumentException("Array is empty");
            }
        }

        private void UpdateSize(int countToAdd = 1)
        {
            if ((_currentCount + countToAdd) >= _array.Length)
            {
                int newSize = (int)((_array.Length + countToAdd) * IncreaseCoef);
                int[] newArray = new int[newSize];
                for (int i = 0; i < _currentCount; i++)
                {
                    newArray[i] = _array[i];
                }

                _array = newArray;
            }
        }
    }
}
