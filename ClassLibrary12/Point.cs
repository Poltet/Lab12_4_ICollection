using ClassLibrary1;
using System;

namespace ClassLibrary12
{
    public class Point<T> where T : IInit, ICloneable, new()
    {
        public T Data { get; set; }            //Информационное поле 
        public Point<T> Next { get; set; }     //Адресное поле на след. элеменет 
        public Point<T> Previous { get; set; } //Адресное поле на предыдущий элеменет 
        public Point()            //Конструктор без параметров 
        {
            Data = default(T);    //Информационное поле 0 или null
            Previous = null;      //Адресное поле на предыдущий элеменет 
            Next = null;          //Адресное поле на след. элеменет 
        }
        public Point(T data)      //Конструктор c параметром - инф. поле
        {
            Data = data;          //Информационное поле 
            Previous = null;      //Адресное поле на предыдущий элеменет 
            Next = null;          //Адресное поле на след. элеменет
        }
        public override string ToString()                //Метод для печати
        {
            return Data == null ? "" : Data.ToString();  //При пустом информационном поле возвращается
        }                                                 //путсая строка, иначе значение поля
        public override int GetHashCode()                //Получение хэш кода по инф. полю
        {
            return Data == null ? 0 : Data.GetHashCode();
        }
    }
}
