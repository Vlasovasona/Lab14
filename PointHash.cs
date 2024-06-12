using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_10;

namespace Lab_14
{
    public class PointHash<T> where T: IInit, new()
    {
        public T? Data { get; set; }
        public PointHash<T>? Next { get; set; }
        public PointHash<T>? Pred { get; set; }

        public static T MakeRandomItem() //создание информационного поля
        {
            T data = new T();
            data.RandomInit();
            return data;
        }

        public PointHash()
        {
            this.Data = default(T); //если мы подставим сюда ссылку, то будет null, иначе (если значимый тип) будет присвоено 0
            this.Next = null;
            this.Pred = null;
        }

        public PointHash(T data)
        {
            this.Data = data;
            this.Next = null;
            this.Pred = null;
        }

        public override string ToString() //преобразование элемента типа Point в строку 
        {
            return Data == null ? "" : Data.ToString(); //проверка на null (если Data пустая, будет возвращена пустая строка)
        }

        public override int GetHashCode() //ананлогично работает метод GetHashCode (нужен, т.к. будем работать с хеш-таблицами)
        {
            return Data == null ? 0 : Data.GetHashCode();
        }
    }
}

