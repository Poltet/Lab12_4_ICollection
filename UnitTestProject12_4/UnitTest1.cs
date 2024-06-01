using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ClassLibrary1;
using ClassLibrary12;

namespace UnitTestProject12_4
{
    [TestClass]
    public class UnitTest1
    {
        // Point
        [TestMethod]
        public void TestMethod_PointNull_ToString()  //ToString пустое информационное поле
        {
            Point<CelestialBody> p = new Point<CelestialBody>();
            Assert.AreEqual(p.ToString(), "");
        }
        [TestMethod]
        public void TestMethod_Point_ToString()  //ToString не пустое информационное поле
        {
            Point<CelestialBody> p = new Point<CelestialBody>();
            p.Data = new CelestialBody("1", 1, 1, 1);
            Assert.AreEqual(p.ToString(), new CelestialBody("1", 1, 1, 1).ToString());
        }

        [TestMethod]
        public void TestMethod_Point_GetHashCode_null()  //Хэш код
        {
            Point<CelestialBody> p = new Point<CelestialBody>();
            Assert.AreEqual(p.GetHashCode(), 0);
        }
        [TestMethod]
        public void TestMethod_Point_GetHashCode()  //Хэш код
        {
            Point<CelestialBody> p1 = new Point<CelestialBody>(new CelestialBody("A", 1, 1, 1));
            Point<CelestialBody> p2 = new Point<CelestialBody>(new CelestialBody("A", 1, 1, 1));
            Assert.AreEqual(p1.GetHashCode(), p2.GetHashCode());
        }

        //        ////MyHashTable
        [TestMethod]
        public void TestMethod_Count()  //Счетчик элементов
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>(8);
            int count = 5;
            for (int i = 0; i < count; i++)
            {
                Planet planet = new Planet(i.ToString(), i, i, i, i);
                table.Add(planet);
            }
            Assert.AreEqual(count, table.Count);
        }
        [TestMethod] //4 часть
        public void TestMethod_Capacity()  //Размер таблицы 
        {
            int сapacity = 5;
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>(сapacity);
            Assert.AreEqual(сapacity, table.Capacity);
        }
        [TestMethod]
        public void TestMethod_IsReadOnly()  //Только для чтения 
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            Assert.IsFalse(table.IsReadOnly);
        }

        [TestMethod] //4 часть
        public void TestMethod_CopyConstructor()  //Конструктор копирования 
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>(3);
            for (int i = 1; i < 5; i++)      //Добавление элементов в таблицу
            {
                Planet planet = new Planet(i.ToString(), i, i, i, i);
                table.Add(planet);
            }
            MyCollection<CelestialBody> CopyTable = new MyCollection<CelestialBody>(table);
            Assert.IsFalse(ReferenceEquals(table, CopyTable));
            Assert.AreEqual(table.Count, CopyTable.Count);
            Assert.AreEqual(table.Capacity, CopyTable.Capacity);
        }

        [TestMethod]
        public void TestMethod_ContainsKey_EmptyTable()  //Поиск в пустой таблице
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            CelestialBody SearchItem = new CelestialBody();
            Assert.ThrowsException<Exception>(() => table.Contains(SearchItem));
        }
        [TestMethod]
        public void TestMethod_ContainsKey_EmptyChain()  //Поиск в пустой цепочке несуществующего элемента
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            CelestialBody SearchItem = new CelestialBody();
            int index = table.GetIndex(SearchItem);
            for (int i = 1; i < 1000; i++)      //Добавление элемента в другую цепочку, чтоб таблица была непустая 
            {
                Planet planet = new Planet(i.ToString(), i, i, i, i);
                if (table.GetIndex(planet) != index)
                {
                    table.Add(planet);
                    break;
                }
            }
            Assert.IsFalse(table.Contains(SearchItem));
        }
        [TestMethod]
        public void TestMethod_ContainsKey_FirstItemOfChain() //Поиск первого элемента цепочки 
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            Planet SearchItem = new Planet();
            table.Add(SearchItem);
            Assert.IsTrue(table.Contains(SearchItem));
        }
        [TestMethod]
        public void TestMethod_ContainsKey_NotFirstItem() //Поиск не первого элемента цепочки 
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            Planet SearchItem = new Planet();
            int index = table.GetIndex(SearchItem);
            int count = 0;
            for (int i = 1; i < 1000; i++) //Добавление элементов в цепочку 
            {
                Planet planet = new Planet(i.ToString(), i, i, i, i);
                if (table.GetIndex(planet) == index) //Индекс совпадает с удаляемым 
                {
                    table.Add(planet);
                    count++;
                    if (count > 2)
                        break;
                }
            }
            table.Add(SearchItem);
            Assert.IsTrue(table.Contains(SearchItem));
        }
        [TestMethod]
        public void TestMethod_ContainsKey_NotExistItem() //Поиск несуществующего элемента в цепочке
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            Planet SearchItem = new Planet();
            int index = table.GetIndex(SearchItem);
            int count = 0;
            for (int i = 1; i < 1000; i++) //Добавление элементов в цепочку 
            {
                Planet planet = new Planet(i.ToString(), i, i, i, i);
                if (table.GetIndex(planet) == index) //Индекс совпадает с удаляемым 
                {
                    table.Add(planet);
                    count++;
                    if (count > 2)
                        break;
                }
            }
            Assert.IsFalse(table.Contains(SearchItem));
        }
        [TestMethod]
        public void TestMethod_Add_ToTheBeginChain()  //Добавление в пустую цепочку 
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            Planet planet = new Planet();
            table.Add(planet);
            Assert.IsTrue(table.Contains(planet));
        }
        [TestMethod]
        public void TestMethod_Add_ToTheEndChain() //Добавление в непустую цепочку
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            Planet planet = new Planet();
            table.Add(planet);
            int count = 1; //Количество элементов в цепочке с индексом index
            int index = table.GetIndex(planet);
            for (int i = 1; i < 1000; i++)
            {
                planet = new Planet(i.ToString(), i, i, i, i);
                if (table.GetIndex(planet) == index)
                {
                    table.Add(planet);
                    count++;
                    if (count > 2)
                        break;
                }
            }
            Assert.IsTrue(table.Contains(planet));
        }
        [TestMethod]
        public void TestMethod_Add_DoubleItem()  //Добавление элемента с одинаковым ключом
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            Planet planet = new Planet();
            table.Add(planet);
            Assert.ThrowsException<Exception>(() => { table.Add(planet); });
        }
        [TestMethod]
        public void TestMethod_RemoveKey_EmptyTable()  //Удаление по ключу в пустой таблице
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            CelestialBody DeleteItem = new CelestialBody();
            Assert.ThrowsException<Exception>(() => table.Remove(DeleteItem));
        }
        [TestMethod]
        public void TestMethod_RemoveKey_EmptyChain() //Удаление элемента из пустой цепочки 
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            CelestialBody DeleteItem = new CelestialBody();
            int index = table.GetIndex(DeleteItem);
            for (int i = 1; i < 1000; i++)      //Добавление элемента в другую цепочку, чтоб таблица была непустая 
            {
                Planet planet = new Planet(i.ToString(), i, i, i, i);
                if (table.GetIndex(planet) != index)
                {
                    table.Add(planet);
                    break;
                }
            }
            Assert.IsFalse(table.Remove(DeleteItem));
        }
        [TestMethod]
        public void TestMethod_RemoveKey_FirstItemOfChain() //Удаление первого элемента цепочки 
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            CelestialBody DeleteItem = new CelestialBody();
            table.Add(DeleteItem);
            Assert.IsTrue(table.Remove(DeleteItem));
            Assert.ThrowsException<Exception>(() => table.Contains(DeleteItem)); //Пустая таблица
        }
        [TestMethod]
        public void TestMethod_RemoveKey_ItemInTheMiddleOfChain() //Удаление среднего элемента цепочки 
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            CelestialBody DeleteItem = new CelestialBody(); 
            int index = table.GetIndex(DeleteItem);
            int count = 0;
            for (int i = 1; i < 1000; i++) //Добавление элементов в цепочку 
            {
                Planet planet = new Planet(i.ToString(), i, i, i, i);
                if (table.GetIndex(planet) == index) //Индекс совпадает с удаляемым 
                {
                    table.Add(planet);
                    count++;
                    if (count == 2)
                        table.Add(DeleteItem);
                    if (count > 2)
                        break;
                }
            }
            Assert.IsTrue(table.Remove(DeleteItem));
            Assert.IsFalse(table.Contains(DeleteItem));
        }
        [TestMethod]
        public void TestMethod_RemoveKey_NotExistItem() //Удаление несуществующего элемента в непустой цепочке
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            CelestialBody DeleteItem = new CelestialBody();
            int index = table.GetIndex(DeleteItem);
            int count = 0;
            for (int i = 1; i < 1000; i++) //Добавление элементов в цепочку 
            {
                Planet planet = new Planet(i.ToString(), i, i, i, i);
                if (table.GetIndex(planet) == index) //Индекс совпадает с "удаляемым"
                {
                    table.Add(planet);
                    count++;
                    if (count > 2)
                        break;
                }
            }
            Assert.IsFalse(table.Remove(DeleteItem));
        }
        [TestMethod]
        public void TestMethod_RemoveKey_FirstItemNotEmptyChain() //Удаление первого элемента цепочки из нескольких элементов
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            CelestialBody DeleteItem = new CelestialBody();
            table.Add(DeleteItem);
            int index = table.GetIndex(DeleteItem);
            int count = 1;                 //Количество элементов цепочки 
            for (int i = 1; i < 1000; i++) //Добавление элементов в цепочку 
            {
                Planet planet = new Planet(i.ToString(), i, i, i, i);
                if (table.GetIndex(planet) == index) //Индекс совпадает с удаляемым 
                {
                    table.Add(planet);
                    count++;
                    if (count > 2)
                        break;
                }
            }
            Assert.IsTrue(table.Remove(DeleteItem));
            Assert.IsFalse(table.Contains(DeleteItem));
        }
        [TestMethod] //4 часть
        public void TestMethod_Clear() //Удаление таблицы
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            for (int i = 1; i < 10; i++) //Добавление элементов в таблицу
            {
                Planet planet = new Planet(i.ToString(), i, i, i, i);
                table.Add(planet);
            }
            table.Clear();
            Assert.AreEqual(table.Count, 0);
            foreach (var planet in table) //В таблице нет элементов для перебора
            {
                Assert.Fail(); //Завершает текущий тест с ошибкой
            }
        }
        [TestMethod] //4 часть
        public void TestMethod_CopyTo_EmptyArray()  //Копирование элементов таблицы в пустой массив
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            Assert.ThrowsException<Exception>(() => { table.CopyTo(null, 0); });
        }
        [TestMethod] //4 часть
        public void TestMethod_CopyTo_NegativeIndex()  //Копирование элементов, начиная с отрицательного индекса
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            CelestialBody[] array = new CelestialBody[3];
            Assert.ThrowsException<Exception>(() => { table.CopyTo(array, -2); });
        }
        [TestMethod] //4 часть
        public void TestMethod_CopyTo_BigIndex()  //Копирование элементов, индекс больше длины массива
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>();
            CelestialBody[] array = new CelestialBody[3];
            int index = array.Length + 5;
            Assert.ThrowsException<Exception>(() => { table.CopyTo(array, index); });
        }
        [TestMethod] //4 часть
        public void TestMethod_CopyTo_ExceptionIndex()  //Копирование элементов, count > место в массиве
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>(4);
            for (int i = 1; i < 10; i++) //Добавление элементов в таблицу
            {
                Planet planet = new Planet(i.ToString(), i, i, i, i);
                table.Add(planet);
            }
            int index = table.Count / 2;  //Половина элементов не влезет
            CelestialBody[] array = new CelestialBody[table.Count];
            Assert.ThrowsException<Exception>(() => { table.CopyTo(array, index); });
        }
        [TestMethod] //4 часть
        public void TestMethod_CopyTo()  //Копирование элементов
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>(4);
            for (int i = 1; i < 10; i++) //Добавление элементов в таблицу
            {
                Planet planet = new Planet(i.ToString(), i, i, i, i);
                table.Add(planet);
            }
            CelestialBody[] array = new CelestialBody[table.Count];
            table.CopyTo(array, 0);
            int Countplanet = 0;
            foreach (var planet in array)
            {
                Assert.IsTrue(table.Contains(planet));
                Countplanet++;
            }
            Assert.AreEqual(table.Count, Countplanet); //Все элементы проверены в цикле
        }
    }
}
