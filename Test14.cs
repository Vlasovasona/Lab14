using Library_10;
using Lab_14;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using NUnit.Framework;

namespace Lab13_Test
{
    [TestClass]
    public class Test14
    {
        // Приватний метод CaptureConsoleOutput принимает делегат Action в качестве параметра
        private string ConsoleOutput(Action action) //вспомогательным метод для тестов проверки печати

        {
            // Создается новый StringWriter, который будет использоваться для перехвата вывода консоли
            // StringWriter это обертка над StringBuilder для записи символов в поток строк
            using (var consoleOutput = new StringWriter())
            {
                // Устанавливается consoleOutput как поток вывода консоли, чтобы перехватить вывод этой консоли
                Console.SetOut(consoleOutput);
                // Выполняется переданное действие (action), которое содержит операции вывода информации в консоль
                action.Invoke();
                return consoleOutput.ToString();
            }
        }
        //тиестирование вспомогательных методов

        [TestMethod]
        public void EmptyCollectionException() //тест на возникновение ошибки при попытке взаимодействия с пустой коллекцией 
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.EmptyCollectionException(collection);
            });
        }

        [TestMethod]
        public void EmptyMyCollectionException() //тест на возникновение ошибки при попытке взаимодействия с пустой коллекцией 
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.EmptyMyCollectionException(collection);
            });
        }

        [TestMethod]
        public void IGroupingException() //тест возникновения ошибки при отсутствии элементов в резульатате выполнения запроса/метода расширения, возвращающих объект типа IGroup
        {
            var collection = new SortedDictionary<string, Queue<object>>
        {
            { "1", new Queue<object>(new object[] { new Instrument("Циркуль", 111), new Instrument("Отвертка", 45) }) }
        };
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.IGroupingException(Lab_14.Program.GroupByFirstPart(collection));
            });
        }

        [TestMethod]
        public void TestIEnumerableException_EmptyCollection()//тест возникновения ошибки при отсутствии элементов в резульатате выполнения запроса/метода расширения, возвращающих объект типа IEnumerable 
        {
            IEnumerable<object> emptyCollection = new List<object>();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.IEnumerableException(emptyCollection);
            });
        }

        [TestMethod]
        public void TestIGroupingException_EmptyGroups() ////тест возникновения ошибки при отсутствии элементов в резульатате выполнения запроса/метода расширения, возвращающих объект типа IGroup 
        {
            IEnumerable<IGrouping<double, MeasuringTool>> emptyGroups = new List<IGrouping<double, MeasuringTool>>();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() => Lab_14.Program.IGroupingException(emptyGroups));
        }

        [TestMethod]
        public void PrintFirstCollectionTest() //тест печати первой коллекции 
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.PrintSortedDictionary(collection);
            });
        }

        [TestMethod]
        public void PrintFirstCollectionTest_Exception() //тест на возникновение ошибки при попытке печати пустой коллекцией
        {
            //SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            var collection = new SortedDictionary<string, Queue<object>>
        {
            { "1 магазин", new Queue<object>(new object[] { new MeasuringTool(1, "Пила", "Дерево", "Сантиметры", 1) }) },
            { "2 магазин", new Queue<object>(new object[] { new HandTool(12, "Болгарка", "Металл"), new HandTool(15, "Лобзик", "Железо") }) },
        };
            var consoleOutput = ConsoleOutput(() => { Lab_14.Program.PrintSortedDictionary(collection); });
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("1: Пила, материал Дерево, единицы измерения Сантиметры, точность 1"));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("12: Болгарка, материал Металл"));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("15: Лобзик, материал Железо"));
            Lab_14.Program.EmptyCollectionException(collection);
        }

        //первая часть

        //методы расширения

        [TestMethod]
        public void Joint_FirstPart() //тест функции join 
        {
            var collection = new SortedDictionary<string, Queue<object>>
        {
            { "1", new Queue<object>(new object[] { new MeasuringTool(1, "Пила", "Дерево", "Сантиметры", 1) }) },
            { "2", new Queue<object>(new object[] { new HandTool(12, "Болгарка", "Металл"), new HandTool(15, "Лобзик", "Железо") }) },
            { "3", new Queue<object>(new object[] { new Instrument("Циркуль", 111), new Instrument("Отвертка", 45) }) }
        };
            List<AccuracyToMeasuringTool> list = new List<AccuracyToMeasuringTool>();
            list = Lab_14.Program.CreateNewList();
            var result = Lab_14.Program.Join_FirstPart(collection, list);
            var consoleOutput = ConsoleOutput(() => { Lab_14.Program.PrintIEnumerableResult(result); });
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("{ Name = Пила, Material = Дерево, Accuracy = 1, Units = Сантиметры, ClassOfAccuracy = Приведенная точность }"));
        }

        [TestMethod]
        public void Joint_FirstPartException() //тест исключения при вызове join
        {
            var collection = new SortedDictionary<string, Queue<object>>();
            List<AccuracyToMeasuringTool> list = new List<AccuracyToMeasuringTool>();
            list = Lab_14.Program.CreateNewList();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.Join_FirstPart(collection, list);
            });

        }

        [TestMethod]
        public void FindItemsByAccuracy() //поиск элементов по точности измерительного инструмента 
        {
            var collection = new SortedDictionary<string, Queue<object>>
        {
            { "1", new Queue<object>(new object[] { new MeasuringTool(1, "Пила", "Дерево", "Сантиметры", 1) }) },
            { "2", new Queue<object>(new object[] { new HandTool(12, "Болгарка", "Металл"), new HandTool(15, "Лобзик", "Железо") }) },
            { "3", new Queue<object>(new object[] { new Instrument("Циркуль", 111), new Instrument("Отвертка", 45) }) }
        };
            var result = Lab_14.Program.FindItemsByAccuracy(collection, 1);
            var consoleOutput = ConsoleOutput(() => { Lab_14.Program.PrintIEnumerableResult(result); });
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("1: Пила, материал Дерево, единицы измерения Сантиметры, точность 1"));
        }

        [TestMethod]
        public void WhereHandToolFirstPart() //поиск ручных инструментов в коллекции 
        {
            var collection = new SortedDictionary<string, Queue<object>>
        {
            { "1", new Queue<object>(new object[] { new MeasuringTool(), new MeasuringTool() }) },
            { "2", new Queue<object>(new object[] { new HandTool(), new HandTool() }) },
            { "3", new Queue<object>(new object[] { new ElectricTool(), new ElectricTool() }) }
        };

            // Act
            var result = Lab_14.Program.WhereHandToolFirstPart(collection);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(4, result.Count);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(result.All(tool => tool is HandTool));
        }

        [TestMethod]
        public void WhereHandtoolFirstPartLINQ_ReturnsCorrectObjects() //поиск ручных инструментов с помощью LINQ запроса 
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            Lab_14.Program.CreateInstrumentShop_FirstPart_ForTests(ref collection);
            var result = Lab_14.Program.WhereHandtoolFirstPartLINQ(collection);
            int count = 0;
            foreach (var item in collection.Values.SelectMany(q => q))
                if (item is HandTool instrument)
                    count++;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(result.Count(), count);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(result.All(obj => obj is HandTool));
        }

        [TestMethod]
        public void IntersectFirstPartLINQ_ReturnsCorrectIntersection() //Union первая коллекция поиск одинаковых элементов в коллекции 
        {
            var collection = new SortedDictionary<string, Queue<object>>
        {
            { "1", new Queue<object>(new object[] { new MeasuringTool(), new MeasuringTool() }) },
        };
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.UnionFirstPartLINQ(collection);
            });

        }

        [TestMethod]
        public void IntersectFirstPart_ReturnsCorrectIntersection() //Union первая коллекция поиск одинаковых элементов в коллекции 
        {
            var collection = new SortedDictionary<string, Queue<object>>
        {
            { "1", new Queue<object>(new object[] { new MeasuringTool(), new MeasuringTool() }) },
        };
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.UnionFirstPart(collection);
            });

        }

        [TestMethod]
        public void IntersectFirstPart() //Intersect второй тест 
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            Lab_14.Program.CreateInstrumentShop_FirstPart_ForTests(ref collection);
            Instrument toolSpecial = new Library_10.Instrument("Добавленный инструмент для проверки", 12);
            collection["1 магазин"].Enqueue(new Library_10.Instrument("Добавленный инструмент для проверки", 12));
            collection["2 магазин"].Enqueue(new Library_10.Instrument("Добавленный инструмент для проверки", 12));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(Lab_14.Program.UnionFirstPart(collection).Count(), 9);
        }

        [TestMethod]
        public void MaxFirstPart() //поиск максимального хначения точности среди инструментов 
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            Lab_14.Program.CreateInstrumentShop_FirstPart_ForTests(ref collection);
            int ac = 120;
            for (int key = 1; key < 4; key++)
            {
                foreach (object item in collection[$"{key} магазин"])
                {
                    if (item is MeasuringTool)
                    {
                        MeasuringTool measuringTool = (MeasuringTool)item;
                        measuringTool.accuracy = ac;
                        ac -= 10;
                    }
                }
            }

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(Lab_14.Program.MaxFirstPart(collection), 120);
        }

        [TestMethod]
        public void MaxFirstPart_Exception() //исключение при отсутствии измерительных инструментов в коллекции 
        {
            var collection = new SortedDictionary<string, Queue<object>>
        {
            { "2", new Queue<object>(new object[] { new HandTool(), new HandTool() }) },
            { "3", new Queue<object>(new object[] { new ElectricTool(), new ElectricTool() }) }
        };
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.MaxFirstPart(collection);
            });
        }

        [TestMethod]
        public void GroupByFirstPart() //группировка по точности измерительных инструментов 
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            Lab_14.Program.CreateInstrumentShop_FirstPart_ForTests(ref collection);
            MeasuringTool toolToChange = new MeasuringTool(1, "Лобзик", "Дерево", "Градусы", 5);
            for (int key = 1; key < 4; key++)
            {
                foreach (object item in collection[$"{key} магазин"])
                {
                    if (item is MeasuringTool)
                    {
                        MeasuringTool measuringTool = (MeasuringTool)item;
                        measuringTool.accuracy = 5;
                    }
                }
            }

            var result = Lab_14.Program.GroupByFirstPart(collection);
            foreach (var group in result)
            {
                foreach (var tool in group)
                {
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(group.Key, tool.accuracy);
                }
            }
        }

        ////LINQ-запросы

        [TestMethod]
        public void Joint_LINQ() //проверка коррекцтной работы join 
        {
            var collection = new SortedDictionary<string, Queue<object>>
        {
            { "1", new Queue<object>(new object[] { new MeasuringTool(1, "Пила", "Дерево", "Сантиметры", 1) }) },
            { "2", new Queue<object>(new object[] { new HandTool(12, "Болгарка", "Металл"), new HandTool(15, "Лобзик", "Железо") }) },
            { "3", new Queue<object>(new object[] { new Instrument("Циркуль", 111), new Instrument("Отвертка", 45) }) }
        };
            List<AccuracyToMeasuringTool> list = new List<AccuracyToMeasuringTool>();
            list = Lab_14.Program.CreateNewList();
            var result = Lab_14.Program.JoinFirstCollection(collection, list);
            var consoleOutput = ConsoleOutput(() => { Lab_14.Program.PrintIEnumerableResult(result); });
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("{ Name = Пила, Material = Дерево, Accuracy = 1, Units = Сантиметры, ClassOfAccuracy = Приведенная точность }"));
        }

        [TestMethod]
        public void FindItemsByAccuracyLINQ() //проверка корректной работы метода, который ищет все элементы коллекции с указанной точностью 
        {
            var collection = new SortedDictionary<string, Queue<object>>
        {
            { "1", new Queue<object>(new object[] { new MeasuringTool(1, "Пила", "Дерево", "Сантиметры", 1) }) },
            { "2", new Queue<object>(new object[] { new HandTool(12, "Болгарка", "Металл"), new HandTool(15, "Лобзик", "Железо") }) },
            { "3", new Queue<object>(new object[] { new Instrument("Циркуль", 111), new Instrument("Отвертка", 45) }) }
        };
            List<AccuracyToMeasuringTool> list = new List<AccuracyToMeasuringTool>();
            list = Lab_14.Program.CreateNewList();
            var result = Lab_14.Program.FindItemsByAccuracy_LINQ(collection, 1);
            var consoleOutput = ConsoleOutput(() => { Lab_14.Program.PrintIEnumerableResult(result); });
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("1: Пила, материал Дерево, единицы измерения Сантиметры, точность 1"));
        }

        [TestMethod]
        public void WhereHandtoolFirstPartLINQ() //поиск ручных инструментов в коллекции 
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            Lab_14.Program.CreateInstrumentShop_FirstPart_ForTests(ref collection);
            string result = "";
            foreach (var item in Lab_14.Program.WhereHandtoolFirstPartLINQ(collection))
                result += item.ToString();
            for (int key = 1; key < 4; key++)
            {
                foreach (object item in collection[$"{key} магазин"])
                {
                    if (item is HandTool)
                        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(result.Contains(item.ToString()));
                }
            }
        }

        [TestMethod]
        public void IntersectFirstPartLINQ() //проверка корректной работы Intersect
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            Lab_14.Program.CreateInstrumentShop_FirstPart_ForTests(ref collection);
            Instrument toolSpecial = new Library_10.Instrument("Добавленный инструмент для проверки", 12);
            collection["1 магазин"].Enqueue(new Library_10.Instrument("Добавленный инструмент для проверки", 12));
            collection["2 магазин"].Enqueue(new Library_10.Instrument("Добавленный инструмент для проверки", 12));
            string result = "";
            foreach (var item in Lab_14.Program.UnionFirstPartLINQ(collection))
                result += item.ToString();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(result.Contains(toolSpecial.ToString()));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(result.Split(':').Count(), 10);
        }

        [TestMethod]
        public void MaxFirstPartLINQ() //поиск паксимального значения точности
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            Lab_14.Program.CreateInstrumentShop_FirstPart_ForTests(ref collection);
            int ac = 120;
            for (int key = 1; key < 4; key++)
            {
                foreach (object item in collection[$"{key} магазин"])
                {
                    if (item is MeasuringTool)
                    {
                        MeasuringTool measuringTool = (MeasuringTool)item;
                        measuringTool.accuracy = ac;
                        ac -= 10;
                    }
                }
            }
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(Lab_14.Program.MaxFirstPartLINQ(collection), 120);
        }

        [TestMethod]
        public void GroupByFirstPart_LINQ() //группировка элементов по точности 
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            Lab_14.Program.CreateInstrumentShop_FirstPart_ForTests(ref collection);
            MeasuringTool toolToChange = new MeasuringTool(1, "Лобзик", "Дерево", "Градусы", 5);
            for (int key = 1; key < 4; key++)
            {
                foreach (object item in collection[$"{key} магазин"])
                {
                    if (item is MeasuringTool)
                    {
                        MeasuringTool measuringTool = (MeasuringTool)item;
                        measuringTool.accuracy = 5;
                    }
                }
            }

            var result = Lab_14.Program.GroupByFirstPartLINQ(collection);
            foreach (var group in result)
            {
                foreach (var tool in group)
                {
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(group.Key, tool.accuracy);
                }
            }
        }

        ////втаря часть
        ////методы расширения 
        [TestMethod]
        public void WhereSecondPart() //поиск всех инструментов, название которых состоят из нескольких слов 
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 25);
            var consoleOutput = Lab_14.Program.WhereSecondPart(collection);
            string result = "";
            foreach (MeasuringTool s in Lab_14.Program.WhereSecondPart(collection))
                result += s.ToString();
            foreach (MeasuringTool item in collection)
            {
                if (item.Name.Contains(" "))
                {
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(result.Contains(item.ToString()));
                }
            }
        }

        [TestMethod]
        public void CountSecondPart() //подсчет количества вхождений инструментов в указанным именем 
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 90);
            int Count = 0;
            collection[12].Name = "Линейка";
            foreach (MeasuringTool item in collection)
            {
                if (item.Name == "Линейка")
                    Count += 1;
            }
            Lab_14.Program.EmptyMyCollectionException(collection);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(Lab_14.Program.CountSecondPart(collection, "Линейка"), Count);
        }

        [TestMethod]
        public void CountSecondPart_Exception() //исключение если инструментов с указанным именем не обнаружено
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 1);
            int Count = 0;
            collection[0].Name = "Л";
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.CountSecondPart(collection, "Линейка");        
            });
        }

        [TestMethod]
        public void MaxSecondPart() //поиск инструмента с максимальной точностью 
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 10);
            double max = 0;
            foreach (MeasuringTool item in collection)
                if (item.accuracy > max) max = item.accuracy;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(Lab_14.Program.MaxSecondPart(collection), max);
        }

        [TestMethod]
        public void GroupSecondPart() //группировка по точности 
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 3);
            collection[0] = new MeasuringTool(12, "Молоток", "Ь", "Градусы", 1.0);
            collection[1] = new MeasuringTool(12, "Молоток", "Ь", "Градусы", 2.0);
            collection[2] = new MeasuringTool(12, "Болгарка", "Ь", "Градусы", 1.0);
            var groupedData = Lab_14.Program.GroupSecondPart(collection);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(groupedData); // Проверяем, что результат не равен null

            foreach (var group in groupedData)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(group.Count() > 0); // Проверяем, что в каждой группе есть элементы
            }
        }

        [TestMethod]
        public void FindItem_2Part_sByAccuracy() //поиск инструментов по указанной точности
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 1);
            collection[0] = new MeasuringTool(1, "Пила", "Дерево", "Сантиметры", 1);
            collection.Add(new MeasuringTool(13, "Пила", "Металл", "Сантиметры", 0.1));
            collection.Add(new MeasuringTool(16, "Пила", "Металл", "Сантиметры", 1));
            var result = Lab_14.Program.FindItemsByMaxValue(collection, 1);
            var consoleOutput = ConsoleOutput(() => { Lab_14.Program.PrintIEnumerableResult(result); });
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("1: Пила, материал Дерево, единицы измерения Сантиметры, точность 1"));
        }

        //LINQ-запросы
        [TestMethod]
        public void WhereSecondPartLINQ() //поиск всех инструментов, название которых состоят из нескольких слов
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 25);
            var consoleOutput = Lab_14.Program.WhereSecondPart(collection);
            string result = "";
            foreach (MeasuringTool s in Lab_14.Program.WhereSecondPartLINQ(collection))
                result += s.ToString();
            foreach (MeasuringTool item in collection)
            {
                if (item.Name.Contains(" "))
                {
                    Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(result.Contains(item.ToString()));
                }
            }
        }

        [TestMethod]
        public void CountSecondPartLINQ() //поиск количества вхождений инструментов в указанным именем 
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 90);
            int Count = 0;
            foreach (MeasuringTool item in collection)
            {
                if (item.Name == "Линейка")
                    Count += 1;
            }
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(Lab_14.Program.CountSecondPartLINQ(collection, "Линейка"), Count);
        }

        [TestMethod]
        public void CountSecondPartLINQ_Exception() //исключение если инструментов с указанным именем не обнаружено 
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 1);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.CountSecondPartLINQ(collection, "Л");
            });
        }

        [TestMethod]
        public void MaxSecondPartLINQ() //поиск инструмента с максимальной точностью 
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 10);
            double max = 0;
            foreach (MeasuringTool item in collection)
                if (item.accuracy > max) max = item.accuracy;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(Lab_14.Program.MaxSecondPartLINQ(collection), max);
        }

        [TestMethod]
        public void MaxSecondPartLINQ_Exception() //исключение при отсутствии измерительных инструментов в коллекции 
        {
            var collection = new SortedDictionary<string, Queue<object>>
        {
            { "2", new Queue<object>(new object[] { new HandTool(12, "Болгарка", "Металл"), new HandTool(15, "Лобзик", "Железо") }) },
            { "3", new Queue<object>(new object[] { new Instrument("Циркуль", 111), new Instrument("Отвертка", 45) }) }
        }; 

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.MaxFirstPartLINQ(collection);
            });
        }

        [TestMethod]
        public void GroupSecondPartLINQ() //группировка по точности
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 1);
            collection.Remove(collection[0]);
            collection.Add(new MeasuringTool(12, "Молоток", "Ь", "Градусы", 1.0));
            collection.Add(new MeasuringTool(12, "Молоток", "Ь", "Градусы", 2.0));
            collection.Add(new MeasuringTool(12, "Болгарка", "Ь", "Градусы", 1.0));
            var groupedData = Lab_14.Program.GroupSecondPartLINQ(collection);

            var consoleOutput = ConsoleOutput(() => { Lab_14.Program.PrintGroups(Lab_14.Program.GroupSecondPartLINQ(collection)); });
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("Группа с точностью 1. Количество иструментов в группе: 2"));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("Группа с точностью 2. Количество иструментов в группе: 1"));
        }

        [TestMethod]
        public void FindItem_2Part_sByAccuracyLINQ()  
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 1);
            collection[0] = new MeasuringTool(1, "Пила", "Дерево", "Сантиметры", 1);
            var result = Lab_14.Program.FindValue_2Part_ByAccuracy_LINQ(collection, 1);
            var consoleOutput = ConsoleOutput(() => { Lab_14.Program.PrintIEnumerableResult(result); });
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("1: Пила, материал Дерево, единицы измерения Сантиметры, точность 1"));
        }

        [TestClass]
        public class Test_Lab13
        {
            //тестирование конструкторов
            [TestMethod]
            public void Test_ConstuctorWithoutParams() //тест проверка на создание пустого объекта MyCollection
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, collection.Count);
            }

            [TestMethod]
            public void Test_ConstuctorOnlyName() //тест проверка на создание пустого объекта MyCollection
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("r");
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("r", collection.NameOfCollection);
            }

            [TestMethod]
            public void Test_Constructor_Length() //проверка конструктора, формаирующего коллекцию по ее длине
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //создаем коллекцию с помощью ввода ее длины
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(collection.Count, 5); //проверяем чтобы коллекция содержала 5 элементов
            }
            //тестирование конструкторов завершено

            //тестирование нумератора
            [TestMethod]
            public void GetEnumerator_WhenCollectionHasItems_ShouldEnumerateAllItems() //нумератор для коллекции 
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 1); //создаем коллекцию и заполняем ее элементами
                Instrument tool1 = new Instrument("Q", 1);
                Instrument tool2 = new Instrument("W", 12);
                Instrument tool3 = new Instrument("E", 123);
                PointHash<Instrument> firstElement = collection.GetFirstValue();

                collection.Add(tool1);
                collection.Add(tool2);
                collection.Add(tool3);
                collection.Remove(firstElement.Data);

                Instrument[] result = new Instrument[3];
                int index = 0;
                foreach (Instrument item in collection)
                {
                    result[index] = item;
                    index++;
                }
                CollectionAssert.AreEqual(new Instrument[] { tool1, tool2, tool3 }, result);
            }

            [TestMethod]
            public void GetEnumerator_CollectionHasRemovedElement() //нумератор для коллекции с удаленным элементом
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 1); //создаем коллекцию и заполняем ее элементами
                Instrument tool1 = new Instrument("Q", 1);
                Instrument tool2 = new Instrument("W", 12);
                Instrument tool3 = new Instrument("E", 123);
                PointHash<Instrument> firstElement = collection.GetFirstValue();

                collection.Add(tool1);
                collection.Add(tool2);
                collection.Add(tool3);

                collection.Remove(tool2);
                collection.Remove(firstElement.Data);

                Instrument[] result = new Instrument[2];
                int index = 0;
                foreach (Instrument item in collection)
                {
                    result[index] = item;
                    index++;
                }

                CollectionAssert.AreEqual(new Instrument[] { tool1, tool3 }, result);
            }
            //тестирование нумератора завершено

            //тестирование ICollection
            [TestMethod]
            public void ICollection_CopyTo() // проверка CopyTo 
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //создаем коллекцию и заполняем ее элементами
                Instrument[] list = new Instrument[5];
                collection.CopyTo(list, 0);
                PointHash<Instrument> value = collection.GetFirstValue();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(value.Data, list[0]);
            }

            [TestMethod]
            public void ICollection_Count() //проверка счетчика элементов
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //создаем коллекцию и заполняем ее элементами
                Instrument[] list = new Instrument[5];
                collection.CopyTo(list, 0);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(collection.Count, list.Length);
            }
            //тестирование ICollection

            //блок Exception
            [TestMethod]
            public void ICollection_CopyTo_ExceptionIndexOutsideOfListLength() //проверка исключения при некорректном вводе индекса при попытке скопирровать значения коллекции в массив
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //создаем коллекцию и заполняем ее элементами
                Instrument[] list = new Instrument[5];
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
                {
                    collection.CopyTo(list, 6);
                });
            }

            [TestMethod]
            public void ICollection_CopyTo_ExceptionNotEnoughListLength() //ошибка, когда не хватает места для всех элементов в массиве
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //создаем коллекцию и заполняем ее элементами
                Instrument[] list = new Instrument[5];
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
                {
                    collection.CopyTo(list, 2);
                });
            }

            [TestMethod]
            public void TestClear() //проверка очистки памяти
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 6);
                collection.Clear();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, collection.Count);
            }

            [TestMethod]
            public void TestAdd() //проврека добавления элемента
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 1);
                Instrument t = new Instrument();
                collection.Add(t);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(collection.Contains(t));

            }

            //ТЕСТЫ ИЗ ВТОРОЙ ЧАСТИ ДЛЯ ПОКРЫТИЯ КЛАССОВ ХЕШ-ТАБЛИЦЫ
            //блок Exception
            [TestMethod]
            public void Test_CreateTable_Exception() //тестирование ошибки при попытке формирования пустой таблицы
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
                {
                    MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(-1);
                });
            }

            [TestMethod]
            public void Test_AddExistingElement_Exception() //тестирование ошибки при попытке формирования пустой таблицы
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
                Instrument tool = new Instrument("q", 1);
                table.AddPoint(tool);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
                {
                    table.AddPoint(tool);
                });
            }

            [TestMethod]
            public void Test_PrintNullTable_Exception() //тестирование ошибки при попытке печати пустой таблицы
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
                {
                    table.Print();
                });
            }//блок Exception закончен

            [TestMethod]
            public void TestCreateTable() //тестирование конструктора для создания хеш-таблицы
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(5);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(table.Capacity, 5);
            }

            //тестривание AddPoint
            [TestMethod]
            public void TestAddPointToHashTable() //тестирование добавления элемента в таблицу
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(5);
                HandTool tool = new HandTool();
                table.AddPoint(tool);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(table.Contains(tool));
            }

            [TestMethod]
            public void TestAddCount() //тестирование увеличения Count после добавления элемента в таблицу
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(5);
                HandTool tool = new HandTool();
                table.AddPoint(tool);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(6, table.Count);
            }

            //тестиование удаления элемента из таблицы
            [TestMethod]
            public void TestRemovePointFromHashTableTrue() //тестирование добавления удаления существующего элемента из таблицы
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
                HandTool tool = new HandTool();
                table.AddPoint(tool);
                table.RemoveData(tool);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(table.Contains(tool));
            }

            [TestMethod]
            public void TestRemovePointFromHashTable_False() //тестирование удаления несуществующего элемента из таблицы
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
                HandTool tool = new HandTool();
                table.AddPoint(tool);
                table.RemoveData(tool);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(table.Contains(tool));
            }

            [TestMethod]
            public void TestRemovePointFromHashTable_OutOfKey_False() //тестирование удаления несуществующего элемента из таблицы
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
                Instrument tool = new Instrument("Бензопила дружба нового поколения", 9999);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(table.RemoveData(tool));
            }

            [TestMethod]
            public void TestRemovePoint_FromBeginingOfTableTable() //тестирование удаления первого в цепочке элемента из таблицы
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
                Instrument tool2 = new Instrument("Перфоратор", 98);
                Instrument tool3 = new Instrument("Штангенциркуль", 85);
                Instrument tool4 = new Instrument("Микрометр", 41);
                Instrument tool5 = new Instrument("RRR", 1234);
                Instrument tool6 = new Instrument("RRR", 1235);

                table.AddPoint(tool2);
                table.AddPoint(tool3);
                table.AddPoint(tool4);
                table.AddPoint(tool5);
                table.AddPoint(tool6);

                PointHash<Instrument> tool = new PointHash<Instrument>();
                PointHash<Instrument> pointHash = table.GetFirstValue();
                tool = pointHash;
                table.RemoveData(tool.Data);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(table.Contains(tool.Data));
            }

            [TestMethod]
            public void MyCollection_IndexOf_Exceptin() //проверка исключения при попытке получить индекс несуществующего в коллекции элемента
            {
                MyCollection<Instrument> col = new MyCollection<Instrument>("q", 2);
                Instrument tool = new Instrument();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
                {
                    col.IndexOf(tool);
                });
            }

            //тестирование метода Contains
            [TestMethod]
            public void TestContainsPointTrue() //метод Contains когда элемент есть в таблице
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
                HandTool tool = new HandTool();
                table.AddPoint(tool);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(table.Contains(tool));
            }

            [TestMethod]
            public void TestContainsPointFalse() //когда элемента нет в таблице
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
                HandTool tool = new HandTool();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(table.Contains(tool));
            }

            //тестирование ToString для PointHash
            [TestMethod]
            public void TestToStringPoint() //тестирование ToString для класса узла
            {
                HandTool tool = new HandTool();
                PointHash<Library_10.Instrument> p = new PointHash<Library_10.Instrument>(tool);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(p.ToString(), tool.ToString());
            }

            [TestMethod]
            public void TestConstructWhithoutParamNext() //конструктор узла без параметров, Next = null
            {
                PointHash<Instrument> p = new PointHash<Instrument>();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNull(p.Next);
            }

            [TestMethod]
            public void TestConstructWhithoutParamPred() //конструктор узла без параметров, Pred = null
            {
                PointHash<Instrument> p = new PointHash<Instrument>();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNull(p.Pred);
            }

            //тестирование методов ToString и GetHashCode для класса PointHash
            [TestMethod]
            public void ToString_WhenDataIsNull_ReturnEmptyString() //конструктор без параметров метод ToString
            {
                PointHash<Instrument> point = new PointHash<Library_10.Instrument>();
                string result = point.ToString();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("", result);
            }

            [TestMethod]
            public void ToString_WhenDataIsNotNull_ReturnDataToString()
            {
                Library_10.Instrument tool = new Instrument();
                tool.RandomInit();
                PointHash<Instrument> point = new PointHash<Instrument>(tool);
                string result = point.ToString();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(tool.ToString(), result);
            }

            [TestMethod]
            public void GetHashCode_WhenDataIsNull_ReturnZero() //тестирование GetHashCode для узла, созданного с помощью конструктора без параметров
            {
                PointHash<Instrument> point = new PointHash<Library_10.Instrument>();
                int result = point.GetHashCode();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, result);
            }

            [TestMethod]
            public void GetHashCode_WhenDataIsNotNull_ReturnDataHashCode() //тестиование GetHashCode для заполненного узла
            {
                Library_10.Instrument tool = new Instrument();
                tool.RandomInit();
                PointHash<Instrument> point = new PointHash<Library_10.Instrument>(tool);
                int result = point.GetHashCode();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(tool.GetHashCode(), result);
            }

            [TestMethod]
            public void CopyTo() //тестиование GetHashCode для заполненного узла
            {
                MyCollection<Instrument> col = new MyCollection<Instrument>("w", 6);
                MyCollection<Instrument> col2 = new MyCollection<Instrument>("w", col);

                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(col.GetFirstValue().Data, col2.GetFirstValue().Data);
            }

            [TestMethod]
            public void CopyTo_Default() //тестиование GetHashCode для заполненного узла
            {
                MyCollection<Instrument> col = new MyCollection<Instrument>("w", 1);
                Instrument[] arr = new Instrument[3];
                arr[0] = new Instrument("ww", 12);
                arr[1] = new Instrument("ww", 11);
                arr[2] = new Instrument("ww", 10);
                col.CopyTo(arr, 1);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(col[0], arr[1]);
            }

            [TestMethod]
            public void CopyToCount() //тестиование GetHashCode для заполненного узла
            {
                MyCollection<Instrument> col = new MyCollection<Instrument>("w", 6);
                MyCollection<Instrument> col2 = new MyCollection<Instrument>("w", col);

                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(col.Count, col2.Count);
            }

            [TestMethod]
            public void Remove_OneElementInChain() //тестиование GetHashCode для заполненного узла
            {
                MyCollection<HandTool> col = new MyCollection<HandTool>("w", 1);
                PointHash<HandTool> tool = col.GetFirstValue();
                col.Remove(tool.Data);
                HandTool tool1 = new HandTool(82, "Штангенциркуль", "Алюминий");
                HandTool tool2 = new HandTool(12, "Штангенциркуль", "Медь");
                HandTool tool3 = new HandTool(30, "Углометр", "Резина");
                HandTool tool4 = new HandTool(53, "Кусачки", "Конструкционная сталь");

                col.Add(tool1);
                col.Add(tool2);
                col.Add(tool3);
                col.Add(tool4);

                col.Remove(tool1);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(col.Contains(tool1));
            }

            [TestMethod]
            public void TestIndexGet() //тестирование get индексатора
            {
                MyCollection<HandTool> col = new MyCollection<HandTool>("w", 1);
                HandTool tool = col.GetFirstValue().Data;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(tool, col[0]);
            }

            [TestMethod]
            public void TestIndexGet_Exception() //тестирование исключение при get индексатора
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //создаем коллекцию и заполняем ее элементами
                Instrument[] list = new Instrument[5];
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
                {
                    collection.Remove(collection[-1]);
                });
            }

            [TestMethod]
            public void TestIndexSet_Exception() //тестирование исключения в set 
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //создаем коллекцию и заполняем ее элементами
                Instrument[] list = new Instrument[5];
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
                {
                    collection[-1] = new Instrument();
                });
            }

            [TestMethod]
            public void TestIndexSet() //тестиование set 
            {
                MyCollection<HandTool> col = new MyCollection<HandTool>("w", 1);
                HandTool tool = col.GetFirstValue().Data;
                col[0] = new HandTool();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(new HandTool(), col[0]);
            }

            [TestMethod]
            public void IsReadOnly_MyCollection() //тестирование свойства IsReadOnly в MyCollection
            {
                MyCollection<HandTool> col = new MyCollection<HandTool>("w", 1);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(col.IsReadOnly);
            }

            [TestMethod]
            public void Count_MyCollection() //тестирование кол-ва элементов в MyCollection
            {
                MyCollection<HandTool> col = new MyCollection<HandTool>("w", 6);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(col.Count, 6);
            }
        }
    }
}