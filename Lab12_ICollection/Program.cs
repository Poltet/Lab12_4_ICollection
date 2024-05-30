using ClassLibrary1;
using System;
using ClassLibrary12;


namespace Lab12_ICollection
{
    internal class Program
    {
        static int Number(int minValue, int maxValue, string msg = "") // Ввод числа от minValue доmaxValue
        {
            Console.Write(msg + $" (целое число от {minValue} до {maxValue}): ");
            int number;
            bool isConvert;
            do
            {
                string buf = Console.ReadLine();
                isConvert = int.TryParse(buf, out number);
                if (!isConvert || number < minValue || number > maxValue)
                    Console.WriteLine("Неправильно введено число. \nПопробуйте еще раз.");
            } while (!isConvert || number < minValue || number > maxValue);
            return number;
        }
        static void Main(string[] args)
        {
            MyCollection<CelestialBody> table = new MyCollection<CelestialBody>(10);
            int answer;
            do
            {
                Console.WriteLine("\n1. Создать таблицу");
                Console.WriteLine("2. Распечатать таблицу");
                Console.WriteLine("3. Поиск"); 
                Console.WriteLine("4. Удаление");
                Console.WriteLine("5. Добавление элемента ");
                Console.WriteLine("6. Удалить таблицу");
                Console.WriteLine("7. Скопировать элементы таблицы в массив");
                Console.WriteLine("8. Индексатор - чтение");
                Console.WriteLine("9. Индексатор - установка значения");
                Console.WriteLine("10. Перебор элементов циклом foreach");
                Console.WriteLine("11. Счетчик элементов");
                Console.WriteLine("12. Свойство для чтения");
                Console.WriteLine("13. Выход");
                answer = Number(1, 13, "Выберите нoмер задания");
                switch (answer)
                {
                    case 1:     //Создать таблицу                
                        {
                            int size = Number(1, int.MaxValue, "Введите размер таблицы");
                            table = new MyCollection<CelestialBody>(size);
                            int count = Number(0, int.MaxValue, "Введите количество элементов таблицы");
                            for (int i = 0; i < count; i++)
                            {
                                CelestialBody celbody = new CelestialBody();
                                celbody.RandomInit();

                                try
                                {
                                    table.Add(celbody);
                                }
                                catch
                                {
                                    i--;
                                }
                            }
                            Console.WriteLine($"\nТаблица создана");
                            break;
                        }
                    case 2:  //Распечатать таблицу
                        {
                            if (table == null || table.Count == 0)
                                Console.WriteLine("\nТаблица пустая");
                            else
                                table.PrintTable();
                            break;
                        }
                    case 3:  //Поиск элемента
                        {
                            if (table.Count == 0)
                                Console.WriteLine("\nТаблица пустая");
                            else
                            {
                                Console.WriteLine("Введите элемент для поиска");
                                CelestialBody celbody = new CelestialBody();
                                celbody.Init();
                                bool ok = table.Contains(celbody);
                                if (ok)
                                    Console.WriteLine($"\nЭлемент найден");
                                else
                                    Console.WriteLine($"\nЭлемент не найден");
                            }
                            break;
                        }
                    case 4:  //удаление элемента
                        {
                            if (table.Count == 0)
                                Console.WriteLine("\nТаблица пустая");
                            else
                            {
                                Console.WriteLine("Введите элемент для поиска");
                                CelestialBody celbody = new CelestialBody();
                                celbody.Init();
                                bool ok = table.Remove(celbody);
                                if (ok)
                                    Console.WriteLine($"\nЭлемент удален");
                                else
                                    Console.WriteLine($"\nЭлемент не найден");
                            }
                            break;
                    }
                    case 5:  //Добавление элемента 
                        {
                            CelestialBody celbody = new CelestialBody();
                            Console.WriteLine("\n1. Добавление случайного элемента");
                            Console.WriteLine("2. Ввод элемента с клавиатуры");
                            answer = Number(1, 2, "Выберите нoмер задания");
                            switch (answer)
                            {
                                case 1:
                                    {
                                        celbody.RandomInit();
                                        break;
                                    }
                                case 2:
                                    {
                                        Console.WriteLine("Введите элемент");
                                        celbody.Init();
                                        break;
                                    }
                            }
                            try
                            {
                                table.Add(celbody);
                                Console.WriteLine($"\nЭлемент добавлен");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine("Элемент не добавлен");
                            }
                            break;
                        }
                    case 6: //удалить таблицу
                    {
                        if (table == null || table.Count == 0)
                            Console.WriteLine("\nТаблица пустая");
                        else
                        {
                            table.Clear();
                            Console.WriteLine("\nТаблица удалена");

                        }
                        break;
                    }
                    case 7: //Скопировать элементы таблицы в массив
                        {
                            if (table == null || table.Count == 0)
                                Console.WriteLine("\nТаблица пустая");
                            else
                            {
                                CelestialBody[] arr = new CelestialBody[table.Count];
                                table.CopyTo(arr,0);
                                Console.WriteLine("Элементы массива:");
                                foreach (var item in arr)
                                    Console.WriteLine(item);
                            }
                            break;
                        }
                    case 8: //Индексатор - чтение
                        {
                            if (table == null || table.Count == 0)
                                Console.WriteLine("\nТаблица пустая");
                            else
                            {
                                int index = Number(0, table.Capacity * table.Count, "Введите индекс");
                                try
                                {
                                    Console.WriteLine($" Найден элемент: {table[index]}");
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }
                            break;
                        }
                    case 9: //Индексатор - установка значения
                        {
                            if (table == null || table.Count == 0)
                                Console.WriteLine("\nТаблица пустая");
                            else
                            {
                                int index = Number(0, table.Capacity * table.Count, "Введите индекс");
                                CelestialBody celbody = new CelestialBody();
                                if (index <= table.Count)
                                {
                                    Console.WriteLine("Введите новое значение");
                                    celbody.Init();
                                }
                                try
                                {
                                    table[index] = celbody;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }
                            break;
                        }
                    case 10: //Перебор элементов циклом foreach
                        {
                            if (table == null || table.Count == 0)
                                Console.WriteLine("\nТаблица пустая");
                            else
                            {
                                foreach (CelestialBody item in table) 
                                    Console.WriteLine(item);
                            }
                            break;
                        }
                    case 11: //Счетчик элементов
                        {
                            Console.WriteLine($"\nВ коллекции {table.Count} элементов");
                            break;
                        }
                    case 12: //Свойство для чтения
                        {
                        if (table.IsReadOnly)
                            Console.WriteLine("\nКоллеция доступна только для чтения");
                        else
                            Console.WriteLine("\nКоллекция доступна не только для чтения");
                        break;
                    }
                }
            } while (answer != 13);
        }
    }
}
