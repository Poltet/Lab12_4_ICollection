using System;
using ClassLibrary1;

namespace ClassLibrary12
{
    public class MyHashTable<T> where T : IInit, ICloneable, new()
    {
        protected Point<T>[] table;
        int count = 0;                                  //Счетчик количества элементов в таблице
        public int Capacity => table.Length; //Свойство для чтения размера таблицы
        public int Count => count;                        //Свойство для чтения количества элементов в таблице

        public MyHashTable(int length = 10)  //Конструктор
        {
            table = new Point<T>[length];
        }
        public void PrintTable()
        {
            for (int i = 0; i < table.Length; i++)
            {
                Console.WriteLine($"{i}:");
                if (table[i] != null) //Не пустая ссылка
                {
                    Console.WriteLine(table[i].Data); //Вывод инф. поля
                    if (table[i] != null) //Не пустая цепочка
                    {
                        Point<T> current = table[i].Next;
                        while (current != null)
                        {
                            Console.WriteLine(current.Data);
                            current = current.Next;
                        }
                    }
                }
            }
        }
        public void Add(T data)
        {
            int index = GetIndex(data);
            if (table[index] == null) //Позиция путсая
            {
                table[index] = new Point<T>(data);
            }
            else //Есть цепочка
            {
                Point<T> current = table[index];
                while (current.Next != null)
                {
                    if (current.Equals(data))
                        return;
                    current = current.Next;
                }
                current.Next = new Point<T>(data); //Добавление в конец цепочки
                current.Next.Previous = current;
            }
            count++;
        }
        public bool Contains(T data)
        {
            int index = GetIndex(data);
            if (count == 0)
                throw new Exception("Таблица пустая");
            if (table[index] == null) //Цепочка пустая, элемента нет
                return false;
            if (table[index].Data.Equals(data)) //Попали на нужный элемент
                return true;
            else   //Перебираем цепочку
            {
                Point<T> current = table[index];
                while (current != null)
                {
                    if (current.Data.Equals(data))
                        return true;
                    current = current.Next;
                }
            }
            return false; //Элемент не найден
        }
        public bool Remove(T data)
        {
            if (count == 0)                                 //Таблица пустая
                throw new Exception("Таблица пустая");

            int index = GetIndex(data);                        //Находит по хэш коду индекс элемента в таблице
            if (table[index] == null)                          //Цепочка пустая
                return false;                                  //Элемент не удален
            if (table[index].Data.Equals(data))                //Первый элемент цепочки - удаляемый
            {
                if (table[index].Next == null)                 //Один элемент в цепочке
                    table[index] = null;                       //Удаление ссылки на элемент
                else                                           //Несколько элементов в цепочке
                {
                    table[index] = table[index].Next;          //Удаление ссылок на элемент
                    table[index].Previous.Next = null;         //Удаление связи на таблицу удаляемого элемента
                    table[index].Previous = null;              //Удаление ссылки на уд. эл.
                }
                count--;
                return true;                                   //Элемент удален
            }
            else                                               //Удаляемый элемент не первый
            {
                Point<T> current;
                current = table[index];
                while (current != null)                        //Перебор всех элементов
                {
                    if (current.Data.Equals(data))             //Элемент найден
                    {
                        Point<T> previous = current.Previous;  //Вспомогательная ссылка на элемент предшествующий удаляемому 
                        Point<T> next = current.Next;          //Вспомогательная ссылка на элемент после удаляемого 
                        previous.Next = next;                  //Связь предыдущего с следующим 
                        current.Previous = null;               //Удаление связи c таблицей удаляемого элемента
                        if (next != null)                      //Элемент после удаляемого не null
                        {
                            next.Previous = previous;          //Связь следующего с предыдущим
                            current.Next = null;               //Удаление связи c таблицей удаляемого элемента
                        }
                        count--;
                        return true;
                    }
                    current = current.Next;
                }
                return false;
            }
        }
        public void Clear()
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                    if (table[i].Next != null)
                        table[i].Next.Previous = null;   //Удаление связи цепочки с таблицей 
                table[i] = null;                         //Удаление первого элемента цепочки 
            }
            count = 0;
        }
        private int GetIndex(T data)
        {
            return Math.Abs(data.GetHashCode() % Capacity);
        }

    }
}
