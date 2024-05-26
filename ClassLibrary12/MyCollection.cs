using ClassLibrary1;
using System;
using System.Collections;
using System.Collections.Generic;


namespace ClassLibrary12
{
    public class MyCollection<T> : MyHashTable<T>, ICollection<T> where T : IInit, ICloneable, new()
    {
        public int Count => base.Count;

        public bool IsReadOnly => false;
        public T this[int index] //индексатор
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new Exception("Индекс находится за пределами коллекции");
                int СurrentIndex = 0;
                foreach (T item in this)
                {
                    if (СurrentIndex == index)
                        return item;
                    СurrentIndex++;
                }
                throw new Exception("Индекс находится за пределами коллекции");
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new Exception("Индекс находится за пределами коллекции");

                int СurrentIndex = 0;
                Point<T> СurrentPoint;
                foreach (Point<T> point in table)
                {
                    СurrentPoint = point;
                    while (СurrentPoint != null)
                    {
                        if (СurrentIndex == index)
                        {
                            СurrentPoint.Data = value;
                            return;
                        }
                        СurrentIndex++;
                        СurrentPoint = СurrentPoint.Next;
                    }
                }
                throw new IndexOutOfRangeException("Индекс находится за пределами коллекции");
            }
        }

        public void Add(T item)
        {
            base.Add(item);
        }
        public bool Remove(T item)
        {
            return base.Remove(item);
        }

        public void Clear()
        {
            base.Clear();
        }

        public bool Contains(T item)
        {
            return base.Contains(item);
        }

        public void CopyTo(T[] array, int index)
        {
            if (array == null)
                throw new Exception("Массив пустой");

            if (index < 0 || index >= array.Length)
                throw new Exception("Индекс находится за пределами массива");

            if (array.Length - index < Count)
                throw new Exception("Недостаточно места в массиве для копирования коллекции");

            int CurrentIndex = index;                          //Индекс массива, в который будет записан элемент
            foreach (T item in this)                           //Перебор элементов коллекции
            {
                T NewData = (T)item.Clone();                   //Глубокое копирование
                array[CurrentIndex] = NewData;
                CurrentIndex++;
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            foreach (Point<T> point in table)
            {
                Point<T> current = point;
                while (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
} 
