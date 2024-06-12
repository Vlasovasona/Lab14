using System.Collections;
using System.Diagnostics.Metrics;
using Library_10;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualBasic;

namespace Lab_14
{
    public class Program
    {
        //методля для печати
        public static void PrintIEnumerableResult(IEnumerable<object> result) //вывод элементов типа IEnumerable 
        { 
            foreach (object item in result)
            {
                Console.WriteLine(item);
            }
        }

        public static void PrintGroups(IEnumerable<IGrouping<double, MeasuringTool>> groups) //вывод элементов типа IGrouping
        {
            foreach (IGrouping<double, MeasuringTool> result in groups)
            {
                int count = 0;
                foreach (var tool in result)
                    count++;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Группа с точностью {result.Key}. Количество иструментов в группе: {count}");
                Console.ForegroundColor = ConsoleColor.White;

                foreach (var tool in result)
                    Console.WriteLine(tool);
            }
        }

        //вспомогательные методы
        public static void IEnumerableException(IEnumerable<object> result) //ошибка при отсутствии элементов в переменной типа IEnumerable
        {
            if (result.ToList().Count() == 0) throw new Exception("Элементы не найдены");
        }

        public static void IGroupingException(IEnumerable<IGrouping<double, MeasuringTool>> groups) //ошибка при отсутствии элементов в переменной типа IGroupind
        {
            if (groups.Count() == 0) throw new Exception("Элементы не найдены");
        }

        public static void EmptyCollectionException(SortedDictionary<string, Queue<object>> collection) //ошибка при пустой коллекции из первой части
        {
            if (collection.Count() == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("Коллекция пуста");
            }
        }

        public static void EmptyMyCollectionException(MyCollection<MeasuringTool> collection) //ошибка при отсутствии элементов в коллекции из второй части
        {
            if (collection.Count() == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("Коллекция пуста");
            }
        }

        public static List<AccuracyToMeasuringTool> CreateNewList() //метод для создания списка из нового класса AccuracyToMeasuringTool, специально для join
        {
            List<AccuracyToMeasuringTool> list = new List<AccuracyToMeasuringTool>();
            AccuracyToMeasuringTool item1 = new AccuracyToMeasuringTool(0.05);
            AccuracyToMeasuringTool item2 = new AccuracyToMeasuringTool(1);
            AccuracyToMeasuringTool item3 = new AccuracyToMeasuringTool(0.01);
            AccuracyToMeasuringTool item4 = new AccuracyToMeasuringTool(0.1);
            AccuracyToMeasuringTool item5 = new AccuracyToMeasuringTool(0.5);

            list.Add(item1);
            list.Add(item2);
            list.Add(item3);
            list.Add(item4);
            list.Add(item5);

            return list;
        }

        public static sbyte InputSbyteNumber(string msg = "Введите число")  //функция для проверки введенного числа на тип sbyte
        {
            Console.WriteLine(msg); //вывод сообщения msg
            bool isConvert; //объявление переменной, отвечающей за проверку на корректность
            sbyte number; //переменная, которой будет присвоено корректно введенное число
            do
            {
                isConvert = sbyte.TryParse(Console.ReadLine(), out number); //проверка на принадлежность типу sbyte
                if (!isConvert || number <= 0)
                {
                    Console.WriteLine("Неправильно введено число. Возможно вы ввели слишком длинное число. Попробуйте заново"); //в случае провала, вывод сообщения о некорректном вводе числа
                    isConvert = false;
                }
            } while (!isConvert); //повторение цикла до тех пор, пока пользователь не введет корректное число
            return number; //ф-ция принимает значение введенного корректного числа
        }

        public static int InputIntNumber(string msg = "Введите число")  //функция для проверки введенного числа на тип sbyte
        {
            Console.WriteLine(msg); //вывод сообщения msg
            bool isConvert; //объявление переменной, отвечающей за проверку на корректность
            int number; //переменная, которой будет присвоено корректно введенное число
            do
            {
                isConvert = int.TryParse(Console.ReadLine(), out number); //проверка на принадлежность типу sbyte
                if (!isConvert || number <= 0)
                {
                    Console.WriteLine("Неправильно введено число. Возможно вы ввели слишком длинное число. Попробуйте заново"); //в случае провала, вывод сообщения о некорректном вводе числа
                    isConvert = false;
                }
            } while (!isConvert); //повторение цикла до тех пор, пока пользователь не введет корректное число
            return number; //ф-ция принимает значение введенного корректного числа
        }

        public static void CreateInstrumentShop_FirstPart(ref SortedDictionary<string, Queue<object>> collection) //метод для создания коллекции для первой части, использутеся дла program 
        {
            collection = new SortedDictionary<string, Queue<object>>();
            int countOfInstrumentShops = InputIntNumber("Введите количество магазинов");
            for (int i = 0;  i < countOfInstrumentShops; i++)
            {
                int number = 0;
                int countOfInstrumentsInShop = InputIntNumber($"Введите количество инструментов в {i+1} магазине");
                Queue<object> queue = new Queue<object>(countOfInstrumentShops);
                for (int j = 0; j < countOfInstrumentsInShop; j++)
                {
                    if (number == 0)
                    {
                        Library_10.Instrument tool = new Library_10.Instrument();
                        tool.RandomInit();
                        queue.Enqueue(tool);
                        number = 1;
                    }
                    else if (number == 1)
                    {
                        HandTool tool2 = new HandTool();
                        tool2.RandomInit();
                        queue.Enqueue(tool2);
                        number = 2;
                    }
                    else if (number == 2)
                    {
                        MeasuringTool tool3 = new MeasuringTool();
                        tool3.RandomInit();
                        queue.Enqueue(tool3);
                        number = 0;
                    }
                }
                collection.Add($"{i+1} магазин", queue);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Коллекция сформирована");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void CreateInstrumentShop_FirstPart_ForTests(ref SortedDictionary<string, Queue<object>> collection) //метод для создания коллекции для первой части, использутеся для тестирования 
        {
            collection = new SortedDictionary<string, Queue<object>>();

            for (int i = 0; i < 3; i++)
            {
                int number = 0;
                Queue<object> queue = new Queue<object>(4);
                for (int j = 0; j < 4; j++)
                {
                    if (number == 0)
                    {
                        Library_10.Instrument tool = new Library_10.Instrument();
                        tool.RandomInit();
                        queue.Enqueue(tool);
                        number = 1;
                    }
                    else if (number == 1)
                    {
                        HandTool tool2 = new HandTool();
                        tool2.RandomInit();
                        queue.Enqueue(tool2);
                        number = 2;
                    }
                    else if (number == 2)
                    {
                        MeasuringTool tool3 = new MeasuringTool();
                        tool3.RandomInit();
                        queue.Enqueue(tool3);
                        number = 0;
                    }
                }
                collection.Add($"{i + 1} магазин", queue);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Коллекция сформирована");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintSortedDictionary(SortedDictionary<string, Queue<object>> collection) //метод для печати коллекции из первой части
        {
            if (collection.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("Коллекция пуста");
            }
            for (int key = 1; key < collection.Count + 1; key++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{key} магазин\n");
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var instrument in collection[$"{key} магазин"])
                    Console.WriteLine(instrument);
            }
            Console.WriteLine("\nЭлементы выведены");
        }

        //методы расширения для первой части

        public static IEnumerable<object> Join_FirstPart(SortedDictionary<string, Queue<object>> collection, List<AccuracyToMeasuringTool> list) //метод для объединения измерительных инструментов с объектами класса AccuracyToMeasuringTool
        {
            var result = collection.Values
                      .SelectMany(item => item)
                      .Where(tool => tool is MeasuringTool)
                      .Join(list,
                            tool => ((MeasuringTool)tool).accuracy,
                            t => t.Accuracy,
                            (tool, t) => new
                            {
                                Name = ((MeasuringTool)tool).Name,
                                Material = ((MeasuringTool)tool).material,
                                Accuracy = ((MeasuringTool)tool).accuracy,
                                Units = ((MeasuringTool)tool).units,
                                ClassOfAccuracy = t.ClassOfAccuracy
                            });
            IEnumerableException(result);
            return result;
        }

        public static List<object> FindItemsByAccuracy(SortedDictionary<string, Queue<object>> dictionary, double accuracy) //метод для выборки элементов по значению accuracy
        {
            List<object> items = new List<object>();

            foreach (var item in dictionary.Values.SelectMany(x => x).Where(x => x is MeasuringTool && ((MeasuringTool)x).accuracy == accuracy))
            {
                items.Add(item);
            }
            return items;
        }

        public static List<HandTool> WhereHandToolFirstPart(SortedDictionary<string, Queue<object>> collection) //метод для нахождения всех ручных инструментов в коллекции
        {
            var handTools = collection
                .SelectMany(pair => pair.Value.OfType<HandTool>())
                .ToList();
            IEnumerableException(handTools);
            return handTools;
        }

        public static IEnumerable<object> UnionFirstPart(SortedDictionary<string, Queue<object>> collection) //метод для нахождения одинаковых элементов среди магазинов
        {
            if (collection.Count() < 2) throw new Exception("Не хватает магазинов для слияния");
            var inter = collection["1 магазин"].Union(collection["2 магазин"]);
            IEnumerableException(inter);
            return inter;
        }

        public static double MaxFirstPart(SortedDictionary<string, Queue<object>> collection) //метод для поиска максимальной точности измерительного инструмента
        {
            if (collection.Values.SelectMany(q => q).All(obj => !(obj is MeasuringTool)))
            {
                throw new Exception("В коллекции нет измерительных инструментов");
            }
            double maxAccuracy = collection
                .SelectMany(pair => pair.Value.OfType<MeasuringTool>())
                .ToList()
                .Max(x => x.accuracy);
            return maxAccuracy;
        }

        public static IEnumerable<IGrouping<double, MeasuringTool>> GroupByFirstPart(SortedDictionary<string, Queue<object>> collection) //метод для группировки по возрастанию точности
        {
            var sortId = collection
                .SelectMany(pair => pair.Value.OfType<MeasuringTool>())
                .GroupBy(x => x.accuracy)
                .OrderBy(g => g.Key);
            IGroupingException(sortId);
            return sortId;
        }

        //методы расширения для второй части

        public static IEnumerable<object> FindItemsByMaxValue(MyCollection<MeasuringTool> collection, double accuracy) //метод для нахождения элементов в коллекции с максимальной точностью
        {
            var items = collection.Where(x => x is MeasuringTool && ((MeasuringTool)x).accuracy == accuracy);
            IEnumerableException(items);
            return items;
        }

        public static IEnumerable<object> WhereSecondPart(MyCollection<MeasuringTool> collection2) //метод для нахождения тех элементов, названия которых состоят из нескольких слов
        {
            var subset2 = collection2.Where(x => x.Name.Contains(" ")).OrderBy(x => x).Select(x => x);
            IEnumerableException(subset2);
            return subset2;
        }

        public static int CountSecondPart(MyCollection<MeasuringTool> collection2, string nameOfTool) //метод для нахождения количества элементов коллекции с именем nameOfTool
        {
            int result = collection2.Count(x => x.Name == nameOfTool);
            if (result == 0) throw new Exception($"Элементов с названием {nameOfTool} ы коллекции не найдено");
            return result;
        }

        public static double MaxSecondPart(MyCollection<MeasuringTool> collection2) //метод для нахождния максимальной точности
        {
            return collection2.Max(x => x.accuracy);
        }

        public static IEnumerable<IGrouping<double, MeasuringTool>> GroupSecondPart(MyCollection<MeasuringTool> collection2) //метод для группировки по возрастаниюточности измерительного инструмента
        {
            var groupedData = collection2.OrderBy(x => x.accuracy)
                              .GroupBy(x => x.accuracy);
            IGroupingException(groupedData);
            return groupedData;
        }

        //LINQ-запросы для первой части

        public static IEnumerable<object> JoinFirstCollection(SortedDictionary<string, Queue<object>> dictionary, List<AccuracyToMeasuringTool> acc) //метод для объединения измерительных инструментов со списком объектов класса AccuracyToMeasuringTool
        {
            var result = from item in dictionary.Values
                         from tool in item
                         where tool is MeasuringTool
                         join t in acc on ((MeasuringTool)tool).accuracy equals (t.Accuracy)
                         select new
                         {
                             Name = ((MeasuringTool)tool).Name,
                             Material = ((MeasuringTool)tool).material,
                             Accuracy = ((MeasuringTool)tool).accuracy,
                             Units = ((MeasuringTool)tool).units,
                             ClassOfAccuracy = t.ClassOfAccuracy
                         };
            IEnumerableException(result);
            return result;
        }

        public static IEnumerable<object> WhereHandtoolFirstPartLINQ(SortedDictionary<string, Queue<object>> collection) //метод для поиска ручных инструментов в коллекции
        {
            var handTools_LINQ = from item in collection.Values
                                 from tools in item
                                 where tools is HandTool
                                 select tools;
            IEnumerableException(handTools_LINQ);
            return handTools_LINQ;
        }

        public static List<object> FindItemsByAccuracy_LINQ(SortedDictionary<string, Queue<object>> dictionary, double accuracy) //метод для нахождения элемента с указанной точностью
        {
            List<object> items = (
                from queue in dictionary.Values
                from tools in queue
                where tools is MeasuringTool && ((MeasuringTool)tools).accuracy == accuracy
                select tools).ToList();
            return items;
        }

        public static IEnumerable<object> UnionFirstPartLINQ(SortedDictionary<string, Queue<object>> collection) //метод для поиска одинаковых элементов в коллекциях
        {
            if (collection.Count() < 2) throw new Exception("Не хватает магазинов для слияния");
            var inter = from tool in (collection["1 магазин"]).Union(collection["2 магазин"])
                        select tool;
            IEnumerableException(inter);
            return inter;
        }

        public static double MaxFirstPartLINQ(SortedDictionary<string, Queue<object>> collection) //метод для нахождения максимальной точности измерительных инструментов
        {
            var measuringTools = from item in collection.Values
                                 from tool in item
                                 where tool is MeasuringTool
                                 select (MeasuringTool)tool;
            if (measuringTools.Count() == 0) throw new Exception("Измерительные инструменты не найдены");

            double MaxAccuracy_LINQ = (from item in collection.Values
                                    from tools in item
                                    where tools is MeasuringTool
                                    select ((MeasuringTool)tools).accuracy).Max();
            return MaxAccuracy_LINQ;
        }

        public static IEnumerable<IGrouping<double, MeasuringTool>> GroupByFirstPartLINQ(SortedDictionary<string, Queue<object>> collection) //метод для группировки по точности
        {
            var groups_LINQ = from item in collection.Values
                              from tools in item.OfType<MeasuringTool>()
                              group tools by tools.accuracy into toolGroup
                              orderby toolGroup.Key
                              select toolGroup;
            IGroupingException(groups_LINQ);
            return groups_LINQ;
        }

        //LINQ-запросы для второй части

        public static List<MeasuringTool> FindValue_2Part_ByAccuracy_LINQ(MyCollection<MeasuringTool> collection, double accuracy) //получение списка элементов по устновленой тончости
        {
            List<MeasuringTool> items = (
                from tools in collection
                where ((MeasuringTool)tools).accuracy == accuracy
                select (MeasuringTool)tools).ToList();
            
            return items;
        }

        public static IEnumerable<object> WhereSecondPartLINQ(MyCollection<MeasuringTool> collection2) //получение тех объектов, чьи имено состоят из нескольких слов
        {
            var result = from tools in collection2
                         where tools.Name.Contains(" ")
                         orderby tools
                         select tools;
            IEnumerableException(result);
            return result;
        }

        public static int CountSecondPartLINQ(MyCollection<MeasuringTool> collection2, string name) //поиск сколько раз входит в коллекцию объект с указанным именем
        {
            int result = (from tools in collection2 where tools.Name == name select tools).Count<MeasuringTool>();
            if (result == 0) throw new Exception($"Элемент с имененм {name} не найден");
            return result;
        }

        public static double MaxSecondPartLINQ(MyCollection<MeasuringTool> collection2) //поиск макимальной точности измерительного инструмкнта
        {
            return (from tool in collection2 select tool.accuracy).Max();
        }

        public static IEnumerable<IGrouping<double, MeasuringTool>> GroupSecondPartLINQ(MyCollection<MeasuringTool> collection2) //группировка по точности
        {
            var groupItems = (from tool in collection2
                              orderby tool.accuracy
                              group tool by tool.accuracy into toolGroup
                              select toolGroup).ToList();
            IEnumerableException(groupItems);
            return groupItems;
        }

        public static void Main(string[] args)
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            MyCollection<MeasuringTool> collection2 = new MyCollection<MeasuringTool>("2");
            sbyte answer1, answer2; //объявление переменных, которые отвечают за выбранный пункт меню
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1. Первая часть лабораторной работы");
                Console.WriteLine("2. Вторая часть лабораторной работы");
                Console.WriteLine("\n3. Завершить работу программы");
                Console.WriteLine("___________________________________________________________________________________________");

                answer1 = InputSbyteNumber();

                switch (answer1)
                {
                    case 1: //формирование коллекции ввод длины
                        {
                            do
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("\n1. Сформировать сеть магазинов по продаже инструментов");
                                Console.WriteLine("2. Вывод коллекции");
                                Console.WriteLine("3. Выполнить запрос на выборку все ручных инструментов (Where)");
                                Console.WriteLine("4. Добавить одинаковые элементы в два магазина и вывести их с помощью метода Intersect");
                                Console.WriteLine("5. Найти измерительный инструмент с максимальной точностью (Max, SelectMany, Where, дополнительный запрос)");
                                Console.WriteLine("6. Группировка по возрастанию точности в измерительных инструментах (Group by, orderby, select)");
                                Console.WriteLine("7. Объединение двух коллекций по одинаковому имени (join)");

                                Console.WriteLine("\n8. Выйти из кейса");
                                Console.WriteLine("___________________________________________________________________________________________");

                                answer2 = InputSbyteNumber();

                                switch (answer2)
                                {
                                    case 1: //формирование коллекции
                                        {
                                            try
                                            {
                                                CreateInstrumentShop_FirstPart(ref collection);
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                                            }
                                            break;
                                        }
                                    case 2:
                                        {
                                            try
                                            {
                                                PrintSortedDictionary(collection);
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                                            }
                                            break;
                                        }
                                    case 3: //запрос на выборку все ручных инструментов (Where)
                                        {
                                            try
                                            {
                                                EmptyCollectionException(collection);
                                                Console.ForegroundColor = ConsoleColor.White;
                                                try
                                                {
                                                    Console.WriteLine("\nДемонстрация метода расширения (пояснение: измерительные инструменты наследуются от ручных)\n");
                                                    PrintIEnumerableResult(WhereHandToolFirstPart(collection));
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                }
                                                try
                                                {
                                                    Console.WriteLine("\nДемонстрация LINQ запроса\n");
                                                    PrintIEnumerableResult(WhereHandtoolFirstPartLINQ(collection));
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                                            }
                                            break;
                                        }
                                    case 4: //Intersect (подумать еще)
                                        {
                                            try
                                            {
                                                EmptyCollectionException(collection);
                                                Console.ForegroundColor = ConsoleColor.White;
                                                try
                                                {
                                                    Console.WriteLine("\nДемонстрация метода расширения\n");
                                                    PrintIEnumerableResult(UnionFirstPart(collection));
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                }
                                                try
                                                {
                                                    Console.WriteLine("\nДемонстрация LINQ запроса\n");
                                                    PrintIEnumerableResult(UnionFirstPartLINQ(collection));
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                                            }
                                            break;
                                        }
                                    case 5: //агрегирование данных
                                        {
                                            try
                                            {
                                                try
                                                {
                                                    EmptyCollectionException(collection);
                                                    Console.ForegroundColor = ConsoleColor.White;
                                                    try
                                                    {
                                                        Console.WriteLine("\nДемонстрация метода расширения\n");
                                                        Console.WriteLine("Самая большая точность измерительного инструмента " + MaxFirstPart(collection));
                                                        Console.WriteLine("Инструменты с " + MaxFirstPart(collection) + " точностью");
                                                        PrintIEnumerableResult(FindItemsByAccuracy(collection, MaxFirstPart(collection)));
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                    }
                                                    try
                                                    {
                                                        Console.WriteLine("\nДемонстрация LINQ запроса\n");
                                                        Console.WriteLine("Самая большая точность измерительного инструмента " + MaxFirstPartLINQ(collection));
                                                        Console.WriteLine("Инструменты с " + MaxFirstPartLINQ(collection) + " точностью");
                                                        PrintIEnumerableResult(FindItemsByAccuracy_LINQ(collection, MaxFirstPartLINQ(collection)));
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                                            }
                                            break;
                                        }
                                    case 6: //группировка по вточности
                                        {
                                            try
                                            {
                                                EmptyCollectionException(collection);
                                                Console.ForegroundColor = ConsoleColor.White;
                                                try
                                                {
                                                    Console.WriteLine("\nДемонстрация метода расширения\n");
                                                    PrintGroups(GroupByFirstPart(collection));
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                }
                                                try
                                                {
                                                    Console.WriteLine("\nДемонстрация LINQ запроса\n");
                                                    PrintGroups(GroupByFirstPartLINQ(collection));
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                                            }
                                            break;
                                        }
                                    case 7: //jOIN
                                        {
                                            try
                                            {
                                                EmptyCollectionException(collection);

                                                try
                                                {
                                                    try
                                                    {
                                                        Console.WriteLine("Демонстрация метода расширения");
                                                        PrintIEnumerableResult(Join_FirstPart(collection, CreateNewList()));
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                    }
                                                    try
                                                    {
                                                        Console.WriteLine("Демонстрация LINQ запроса");
                                                        PrintIEnumerableResult(JoinFirstCollection(collection, CreateNewList()));
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                                            }
                                            break;
                                        }
                                    case 8: //завершение работы программы
                                        {
                                            Console.WriteLine("Демонстрация завершена");
                                            break;
                                        }
                                    default: //неправильный ввод пункта меню
                                        {
                                            Console.WriteLine("Неправильно задан пункт меню");
                                            break;
                                        }
                                }
                            } while (answer2 != 8);
                            break;
                        }
                    case 2:
                        {
                            do
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("\n1. Создать хеш-таблицу");
                                Console.WriteLine("2. Вывод коллекции");
                                Console.WriteLine("3. Выполнить запрос на выборку всех инструментов, название которых состоит из нескольких слов (Where)");
                                Console.WriteLine("4. Получение счетчика. Сколько раз в коллекции содержится элемент с указанным названием (Count)");
                                Console.WriteLine("5. Найти измерительные инструменты с максимальной точностью (Max, дополнительный метод)");
                                Console.WriteLine("6. Группировка по возрастанию точности в измерительных инструментах (Group by)");

                                Console.WriteLine("\n7. Выйти из кейса");
                                Console.WriteLine("___________________________________________________________________________________________");

                                answer2 = InputSbyteNumber();

                                switch (answer2)
                                {
                                    case 1: //формирование коллекции
                                        {
                                            try
                                            {
                                                int size = InputIntNumber("Введите количество элементов хеш-таблицы");
                                                if (size <= 0) throw new Exception("хеш-таблица не может быть нулевой или отрицательной длины");
                                                collection2 = new MyCollection<MeasuringTool>("Коллекция", size);
                                                Console.WriteLine("Хеш-таблица сформирована");
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                                            }
                                            break;
                                        }
                                    case 2:
                                        {
                                            try
                                            {
                                                EmptyMyCollectionException(collection2);
                                                Console.WriteLine("Вывод хеш-таблицы");
                                                collection2.Print();
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                                            }
                                            break;
                                        }
                                    case 3: //запрос на выборку (Where)
                                        {
                                            try
                                            {
                                                EmptyMyCollectionException(collection2);
                                                Console.ForegroundColor = ConsoleColor.White;
                                                try
                                                {
                                                    Console.WriteLine("\nДемонстрация метода расширения\n");
                                                    PrintIEnumerableResult(WhereSecondPart(collection2));
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                }
                                                try
                                                {
                                                    Console.WriteLine("\nДемонстрация LINQ запроса\n");
                                                    PrintIEnumerableResult(WhereSecondPart(collection2));
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                                            }
                                            break;
                                        }
                                    case 4: //Count
                                        {
                                            try
                                            {
                                                EmptyMyCollectionException(collection2);
                                                Console.WriteLine("Введите название инструмента, который нужно посчитать во всех магазинах");

                                                string inputName = Console.ReadLine();
                                                try
                                                {
                                                    Console.WriteLine("\nДемонстрация метода расширения\n");

                                                    int count = CountSecondPart(collection2, inputName);
                                                    Console.WriteLine("В коллекции " + count + " инструмента(-ов) с названием " + inputName);
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                }
                                                try
                                                {
                                                    Console.WriteLine("\nДемонстрация LINQ запроса\n");
                                                    Console.WriteLine("В коллекции " + CountSecondPartLINQ(collection2, inputName) + " инструмента(-ов) с названием " + inputName);

                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                                            }
                                            break;
                                        }
                                    case 5: //агрегирование данных
                                        {
                                            try
                                            {
                                                EmptyMyCollectionException(collection2);
                                                try
                                                {
                                                    Console.ForegroundColor = ConsoleColor.White;
                                                    try
                                                    {
                                                        Console.WriteLine("\nДемонстрация метода расширения\n");
                                                        Console.WriteLine($"Максимальная точность {MaxSecondPart(collection2)}");
                                                        Console.WriteLine("Инструменты с максимальной точностью:");
                                                        PrintIEnumerableResult(FindItemsByMaxValue(collection2, MaxSecondPart(collection2)));
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                    }
                                                    try
                                                    {
                                                        Console.WriteLine("\nДемонстрация LINQ запроса\n");
                                                        Console.WriteLine($"Максимальная точность {MaxSecondPartLINQ(collection2)}");
                                                        Console.WriteLine("Инструменты с максимальной точностью:");
                                                        PrintIEnumerableResult(FindValue_2Part_ByAccuracy_LINQ(collection2, MaxSecondPartLINQ(collection2)));
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                                            }
                                            break;
                                        }
                                    case 6: //группировка
                                        {
                                            try
                                            {
                                                EmptyMyCollectionException(collection2);
                                                Console.ForegroundColor = ConsoleColor.White;
                                                try
                                                {
                                                    Console.WriteLine("\nДемонстрация метода расширения\n");
                                                    PrintGroups(GroupSecondPart(collection2));
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                }
                                                try
                                                {
                                                    Console.WriteLine("\nДемонстрация LINQ запроса\n");
                                                    PrintGroups(GroupSecondPartLINQ(collection2));
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                                            }
                                            break;
                                        }
                                    case 7:
                                        {
                                            Console.WriteLine("Демонстрация завершена");
                                            break;
                                        }
                                    default: //неправильный ввод пункта меню
                                        {
                                            Console.WriteLine("Неправильно задан пункт меню");
                                            break;
                                        }
                                }
                            } while (answer2 != 7);
                            break;
                        }
                    case 3: //завершение работы программы
                        {
                            Console.WriteLine("Демонстрация завершена");
                            break;
                        }
                    default: //неправильный ввод пункта меню
                        {
                            Console.WriteLine("Неправильно задан пункт меню");
                            break;
                        }
                }
            } while (answer1 != 12);
        }
    }
}

