using System;
using System.Collections;
using System.Collections.Generic;

namespace ArrayLibrary
{
    public class ArrayList : IArrayList
    {
        private int[] _array;

        private int _currentCount;

        private double _increaseCoef = 1.33;
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
            _array = new int[5];
            _currentCount = 0;
        }

        public ArrayList(int capacity)
        {
            _array = new int[capacity];
            _currentCount = 0;
        }
        public ArrayList(int[] array)
        {
            int size = (int)(array.Length * _increaseCoef);
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
            int[] result = new int[count];
            RemoveByIndex(0, count);
            return result;
        }

        public int[] RemoveBack(int count)
        {
            int[] result = new int[count];
            RemoveByIndex(_currentCount - 1, count);
            return result;
        }

        public int[] RemoveByIndex(int index, int count)
        {
            int[] result = new int[count];
            if ((index + count) > _currentCount)
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
            for (int i = 0; i < _currentCount / 2; i++)
            {
                Swap(ref _array[i], ref _array[_currentCount - 1 - i]);
            }
        }

        public void Sort(bool ascending = true)
        {
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
            int count = 0;
            for (int i = 0; i < _currentCount; i++)
            {
                if (_array[i] == value)
                {
                    count++;
                }
            }
            int[] result = new int[count];
            for (int i = 0, j = 0; j < count; i++, j++)
            {
                if (_array[i] != value)
                {
                    result[j] = _array[i];
                }
            }
            _currentCount -= count;
            for (int i = 0; i < _currentCount - count; i++)
            {
                _array = result;
            }
            return count;
        }



        public void AddBack(IArrayList arrayList)
        {
           var array = arrayList.ToArray();
            UpdateSize(array.Length);
            for (int i = _currentCount, j = 0; j < array.Length; i++, j++)
            {
                _array[i] = array[j];
            }

            _currentCount += array.Length;
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
            int counter = array.Length;
            for (int i = 0; i < newLenght; i++)
            {
                if (i < index || i == index)
                {
                    continue;
                }
                else if (i > index && i <= _currentCount)
                {
                     _array[newLenght - counter] = _array[_currentCount - i];
                    counter--;
                }
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
            if (_array == null || _array.Length == 0)
            {
                throw new ArgumentException("Array is empty");
            }
        }

        private void UpdateSize(int countToAdd = 1)
        {
            if ((_currentCount + countToAdd) >= _array.Length)
            {
                int newSize = (int)((_array.Length + countToAdd) * _increaseCoef);
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
