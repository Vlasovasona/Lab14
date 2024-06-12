using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_10;
using System.Collections;
using System.Data;

namespace Lab_14
{

    public class MyCollection<T> : MyHashTable<T>, IEnumerable<T>, ICollection<T> where T : IInit, ICloneable, new()
    {
        public string NameOfCollection { get; set; }

        public MyCollection() : base()
        {
            NameOfCollection = "Без имени"; // Имя коллекции задается пользователем
        }

        public MyCollection(string name) : base()
        {
            NameOfCollection = name; // Имя коллекции задается пользователем
        }

        public MyCollection(string name, int size) : base(size)
        {
            NameOfCollection = name; // Имя коллекции
        }

        public MyCollection(string name, MyCollection<T> c) : base(c)
        {
            NameOfCollection = name;
        }

        public bool IsReadOnly => false;

        int ICollection<T>.Count => base.Count;

        public void Add(T item) //добавление в коллекцию
        {
            T tool = (T)item.Clone();
            AddPoint(tool);
        }

        public void Clear() //очистка памяти
        {
            base.Clear();
            count = 0;
            this.table = null;
        }

        public bool Contains(T item) //проверка имеется ли элемент в коллекции
        {
            return base.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new Exception("Массив не может быть null.");
            if (arrayIndex < 0) throw new Exception("Индекс не может быть отрицательным.");
            if (arrayIndex >= array.Length) throw new Exception("Индекс не может превышать размер массива.");
            if (arrayIndex + Count > array.Length) throw new Exception("Кол-во элементов коллеции превышает кол-во ячеек массива для заполнения.");

            int index = arrayIndex;

            for (int i = 0; i < arrayIndex; i++)
            {
                array[i] = default(T);
            }

            foreach (T element in this)
            {
                array[index] = (T)element.Clone();
                index++;
            }

            for (int i = index; i < array.Length; i++)
            {
                array[i] = default(T);
            }
        }

        public bool Remove(T item) //удаление
        {
            return base.RemoveData(item);
        }

        //НУМЕРАТОР
        public IEnumerator<T> GetEnumerator()           //обобщенный нумератор, наследует все от обобщенного
        {                                               //возвращает в Current T  
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    PointHash<T>? p = table[i];
                    while (p != null)
                    {
                        yield return p.Data;
                        p = p.Next;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() //необобщенный нумератор, не используется
        {
            throw new NotImplementedException();
        }
        //НУМЕРАТОР

        public T this[int index] // индексатор для доступа к элементам коллекции
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new Exception("Index is out of range");
                }
                int count = 0;
                for (int i = 0; i < table.Length; i++)
                {
                    if (table[i] != null)
                    {
                        PointHash<T> current = table[i];
                        while (current != null)
                        {
                            if (count == index)
                            {
                                return current.Data;
                            }
                            count++;
                            current = current.Next;
                        }
                    }
                }
                throw new Exception("Index is out of range");
            }
            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new Exception("Index is out of range");
                }
                int count = 0;
                for (int i = 0; i < table.Length; i++)
                {
                    if (table[i] != null)
                    {
                        PointHash<T> current = table[i];
                        while (current != null)
                        {
                            if (count == index)
                            {
                                T currData = current.Data;
                                if (!currData.Equals(value))
                                {
                                    Remove(currData);
                                    Add(value);
                                }
                                return;
                            }
                            count++;
                            current = current.Next;
                        }
                    }
                }
            }
        }

        public int IndexOf(T item) //метод, который возвращает порядковый номер (индекс для элемента, введенного пользователем для замены)
        { 
            int index = 0; //устанавливаем начальное значение индекса 0
            foreach (T element in this) //с помощью цикла перебыриаем элементы коллекции в попытке найти введеный элемент
            {
                if (element.Equals(item))
                {
                    return index; //как только нашли элемент возвращаем его индекс
                }
                index++; //увеличиваем на единицу значение индеса если мы не нашли элемент
            }
            throw new Exception("Элемент не найден"); 
        }
    }
}

