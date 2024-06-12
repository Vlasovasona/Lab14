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
        // ��������� ����� CaptureConsoleOutput ��������� ������� Action � �������� ���������
        private string ConsoleOutput(Action action) //��������������� ����� ��� ������ �������� ������

        {
            // ��������� ����� StringWriter, ������� ����� �������������� ��� ��������� ������ �������
            // StringWriter ��� ������� ��� StringBuilder ��� ������ �������� � ����� �����
            using (var consoleOutput = new StringWriter())
            {
                // ��������������� consoleOutput ��� ����� ������ �������, ����� ����������� ����� ���� �������
                Console.SetOut(consoleOutput);
                // ����������� ���������� �������� (action), ������� �������� �������� ������ ���������� � �������
                action.Invoke();
                return consoleOutput.ToString();
            }
        }
        //������������� ��������������� �������

        [TestMethod]
        public void EmptyCollectionException() //���� �� ������������� ������ ��� ������� �������������� � ������ ���������� 
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.EmptyCollectionException(collection);
            });
        }

        [TestMethod]
        public void EmptyMyCollectionException() //���� �� ������������� ������ ��� ������� �������������� � ������ ���������� 
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.EmptyMyCollectionException(collection);
            });
        }

        [TestMethod]
        public void IGroupingException() //���� ������������� ������ ��� ���������� ��������� � ����������� ���������� �������/������ ����������, ������������ ������ ���� IGroup
        {
            var collection = new SortedDictionary<string, Queue<object>>
        {
            { "1", new Queue<object>(new object[] { new Instrument("�������", 111), new Instrument("��������", 45) }) }
        };
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.IGroupingException(Lab_14.Program.GroupByFirstPart(collection));
            });
        }

        [TestMethod]
        public void TestIEnumerableException_EmptyCollection()//���� ������������� ������ ��� ���������� ��������� � ����������� ���������� �������/������ ����������, ������������ ������ ���� IEnumerable 
        {
            IEnumerable<object> emptyCollection = new List<object>();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.IEnumerableException(emptyCollection);
            });
        }

        [TestMethod]
        public void TestIGroupingException_EmptyGroups() ////���� ������������� ������ ��� ���������� ��������� � ����������� ���������� �������/������ ����������, ������������ ������ ���� IGroup 
        {
            IEnumerable<IGrouping<double, MeasuringTool>> emptyGroups = new List<IGrouping<double, MeasuringTool>>();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() => Lab_14.Program.IGroupingException(emptyGroups));
        }

        [TestMethod]
        public void PrintFirstCollectionTest() //���� ������ ������ ��������� 
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.PrintSortedDictionary(collection);
            });
        }

        [TestMethod]
        public void PrintFirstCollectionTest_Exception() //���� �� ������������� ������ ��� ������� ������ ������ ����������
        {
            //SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            var collection = new SortedDictionary<string, Queue<object>>
        {
            { "1 �������", new Queue<object>(new object[] { new MeasuringTool(1, "����", "������", "����������", 1) }) },
            { "2 �������", new Queue<object>(new object[] { new HandTool(12, "��������", "������"), new HandTool(15, "������", "������") }) },
        };
            var consoleOutput = ConsoleOutput(() => { Lab_14.Program.PrintSortedDictionary(collection); });
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("1: ����, �������� ������, ������� ��������� ����������, �������� 1"));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("12: ��������, �������� ������"));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("15: ������, �������� ������"));
            Lab_14.Program.EmptyCollectionException(collection);
        }

        //������ �����

        //������ ����������

        [TestMethod]
        public void Joint_FirstPart() //���� ������� join 
        {
            var collection = new SortedDictionary<string, Queue<object>>
        {
            { "1", new Queue<object>(new object[] { new MeasuringTool(1, "����", "������", "����������", 1) }) },
            { "2", new Queue<object>(new object[] { new HandTool(12, "��������", "������"), new HandTool(15, "������", "������") }) },
            { "3", new Queue<object>(new object[] { new Instrument("�������", 111), new Instrument("��������", 45) }) }
        };
            List<AccuracyToMeasuringTool> list = new List<AccuracyToMeasuringTool>();
            list = Lab_14.Program.CreateNewList();
            var result = Lab_14.Program.Join_FirstPart(collection, list);
            var consoleOutput = ConsoleOutput(() => { Lab_14.Program.PrintIEnumerableResult(result); });
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("{ Name = ����, Material = ������, Accuracy = 1, Units = ����������, ClassOfAccuracy = ����������� �������� }"));
        }

        [TestMethod]
        public void Joint_FirstPartException() //���� ���������� ��� ������ join
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
        public void FindItemsByAccuracy() //����� ��������� �� �������� �������������� ����������� 
        {
            var collection = new SortedDictionary<string, Queue<object>>
        {
            { "1", new Queue<object>(new object[] { new MeasuringTool(1, "����", "������", "����������", 1) }) },
            { "2", new Queue<object>(new object[] { new HandTool(12, "��������", "������"), new HandTool(15, "������", "������") }) },
            { "3", new Queue<object>(new object[] { new Instrument("�������", 111), new Instrument("��������", 45) }) }
        };
            var result = Lab_14.Program.FindItemsByAccuracy(collection, 1);
            var consoleOutput = ConsoleOutput(() => { Lab_14.Program.PrintIEnumerableResult(result); });
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("1: ����, �������� ������, ������� ��������� ����������, �������� 1"));
        }

        [TestMethod]
        public void WhereHandToolFirstPart() //����� ������ ������������ � ��������� 
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
        public void WhereHandtoolFirstPartLINQ_ReturnsCorrectObjects() //����� ������ ������������ � ������� LINQ ������� 
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
        public void IntersectFirstPartLINQ_ReturnsCorrectIntersection() //Union ������ ��������� ����� ���������� ��������� � ��������� 
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
        public void IntersectFirstPart_ReturnsCorrectIntersection() //Union ������ ��������� ����� ���������� ��������� � ��������� 
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
        public void IntersectFirstPart() //Intersect ������ ���� 
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            Lab_14.Program.CreateInstrumentShop_FirstPart_ForTests(ref collection);
            Instrument toolSpecial = new Library_10.Instrument("����������� ���������� ��� ��������", 12);
            collection["1 �������"].Enqueue(new Library_10.Instrument("����������� ���������� ��� ��������", 12));
            collection["2 �������"].Enqueue(new Library_10.Instrument("����������� ���������� ��� ��������", 12));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(Lab_14.Program.UnionFirstPart(collection).Count(), 9);
        }

        [TestMethod]
        public void MaxFirstPart() //����� ������������� �������� �������� ����� ������������ 
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            Lab_14.Program.CreateInstrumentShop_FirstPart_ForTests(ref collection);
            int ac = 120;
            for (int key = 1; key < 4; key++)
            {
                foreach (object item in collection[$"{key} �������"])
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
        public void MaxFirstPart_Exception() //���������� ��� ���������� ������������� ������������ � ��������� 
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
        public void GroupByFirstPart() //����������� �� �������� ������������� ������������ 
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            Lab_14.Program.CreateInstrumentShop_FirstPart_ForTests(ref collection);
            MeasuringTool toolToChange = new MeasuringTool(1, "������", "������", "�������", 5);
            for (int key = 1; key < 4; key++)
            {
                foreach (object item in collection[$"{key} �������"])
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

        ////LINQ-�������

        [TestMethod]
        public void Joint_LINQ() //�������� ����������� ������ join 
        {
            var collection = new SortedDictionary<string, Queue<object>>
        {
            { "1", new Queue<object>(new object[] { new MeasuringTool(1, "����", "������", "����������", 1) }) },
            { "2", new Queue<object>(new object[] { new HandTool(12, "��������", "������"), new HandTool(15, "������", "������") }) },
            { "3", new Queue<object>(new object[] { new Instrument("�������", 111), new Instrument("��������", 45) }) }
        };
            List<AccuracyToMeasuringTool> list = new List<AccuracyToMeasuringTool>();
            list = Lab_14.Program.CreateNewList();
            var result = Lab_14.Program.JoinFirstCollection(collection, list);
            var consoleOutput = ConsoleOutput(() => { Lab_14.Program.PrintIEnumerableResult(result); });
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("{ Name = ����, Material = ������, Accuracy = 1, Units = ����������, ClassOfAccuracy = ����������� �������� }"));
        }

        [TestMethod]
        public void FindItemsByAccuracyLINQ() //�������� ���������� ������ ������, ������� ���� ��� �������� ��������� � ��������� ��������� 
        {
            var collection = new SortedDictionary<string, Queue<object>>
        {
            { "1", new Queue<object>(new object[] { new MeasuringTool(1, "����", "������", "����������", 1) }) },
            { "2", new Queue<object>(new object[] { new HandTool(12, "��������", "������"), new HandTool(15, "������", "������") }) },
            { "3", new Queue<object>(new object[] { new Instrument("�������", 111), new Instrument("��������", 45) }) }
        };
            List<AccuracyToMeasuringTool> list = new List<AccuracyToMeasuringTool>();
            list = Lab_14.Program.CreateNewList();
            var result = Lab_14.Program.FindItemsByAccuracy_LINQ(collection, 1);
            var consoleOutput = ConsoleOutput(() => { Lab_14.Program.PrintIEnumerableResult(result); });
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("1: ����, �������� ������, ������� ��������� ����������, �������� 1"));
        }

        [TestMethod]
        public void WhereHandtoolFirstPartLINQ() //����� ������ ������������ � ��������� 
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            Lab_14.Program.CreateInstrumentShop_FirstPart_ForTests(ref collection);
            string result = "";
            foreach (var item in Lab_14.Program.WhereHandtoolFirstPartLINQ(collection))
                result += item.ToString();
            for (int key = 1; key < 4; key++)
            {
                foreach (object item in collection[$"{key} �������"])
                {
                    if (item is HandTool)
                        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(result.Contains(item.ToString()));
                }
            }
        }

        [TestMethod]
        public void IntersectFirstPartLINQ() //�������� ���������� ������ Intersect
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            Lab_14.Program.CreateInstrumentShop_FirstPart_ForTests(ref collection);
            Instrument toolSpecial = new Library_10.Instrument("����������� ���������� ��� ��������", 12);
            collection["1 �������"].Enqueue(new Library_10.Instrument("����������� ���������� ��� ��������", 12));
            collection["2 �������"].Enqueue(new Library_10.Instrument("����������� ���������� ��� ��������", 12));
            string result = "";
            foreach (var item in Lab_14.Program.UnionFirstPartLINQ(collection))
                result += item.ToString();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(result.Contains(toolSpecial.ToString()));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(result.Split(':').Count(), 10);
        }

        [TestMethod]
        public void MaxFirstPartLINQ() //����� ������������� �������� ��������
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            Lab_14.Program.CreateInstrumentShop_FirstPart_ForTests(ref collection);
            int ac = 120;
            for (int key = 1; key < 4; key++)
            {
                foreach (object item in collection[$"{key} �������"])
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
        public void GroupByFirstPart_LINQ() //����������� ��������� �� �������� 
        {
            SortedDictionary<string, Queue<object>> collection = new SortedDictionary<string, Queue<object>>();
            Lab_14.Program.CreateInstrumentShop_FirstPart_ForTests(ref collection);
            MeasuringTool toolToChange = new MeasuringTool(1, "������", "������", "�������", 5);
            for (int key = 1; key < 4; key++)
            {
                foreach (object item in collection[$"{key} �������"])
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

        ////����� �����
        ////������ ���������� 
        [TestMethod]
        public void WhereSecondPart() //����� ���� ������������, �������� ������� ������� �� ���������� ���� 
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
        public void CountSecondPart() //������� ���������� ��������� ������������ � ��������� ������ 
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 90);
            int Count = 0;
            collection[12].Name = "�������";
            foreach (MeasuringTool item in collection)
            {
                if (item.Name == "�������")
                    Count += 1;
            }
            Lab_14.Program.EmptyMyCollectionException(collection);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(Lab_14.Program.CountSecondPart(collection, "�������"), Count);
        }

        [TestMethod]
        public void CountSecondPart_Exception() //���������� ���� ������������ � ��������� ������ �� ����������
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 1);
            int Count = 0;
            collection[0].Name = "�";
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.CountSecondPart(collection, "�������");        
            });
        }

        [TestMethod]
        public void MaxSecondPart() //����� ����������� � ������������ ��������� 
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 10);
            double max = 0;
            foreach (MeasuringTool item in collection)
                if (item.accuracy > max) max = item.accuracy;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(Lab_14.Program.MaxSecondPart(collection), max);
        }

        [TestMethod]
        public void GroupSecondPart() //����������� �� �������� 
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 3);
            collection[0] = new MeasuringTool(12, "�������", "�", "�������", 1.0);
            collection[1] = new MeasuringTool(12, "�������", "�", "�������", 2.0);
            collection[2] = new MeasuringTool(12, "��������", "�", "�������", 1.0);
            var groupedData = Lab_14.Program.GroupSecondPart(collection);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(groupedData); // ���������, ��� ��������� �� ����� null

            foreach (var group in groupedData)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(group.Count() > 0); // ���������, ��� � ������ ������ ���� ��������
            }
        }

        [TestMethod]
        public void FindItem_2Part_sByAccuracy() //����� ������������ �� ��������� ��������
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 1);
            collection[0] = new MeasuringTool(1, "����", "������", "����������", 1);
            collection.Add(new MeasuringTool(13, "����", "������", "����������", 0.1));
            collection.Add(new MeasuringTool(16, "����", "������", "����������", 1));
            var result = Lab_14.Program.FindItemsByMaxValue(collection, 1);
            var consoleOutput = ConsoleOutput(() => { Lab_14.Program.PrintIEnumerableResult(result); });
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("1: ����, �������� ������, ������� ��������� ����������, �������� 1"));
        }

        //LINQ-�������
        [TestMethod]
        public void WhereSecondPartLINQ() //����� ���� ������������, �������� ������� ������� �� ���������� ����
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
        public void CountSecondPartLINQ() //����� ���������� ��������� ������������ � ��������� ������ 
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 90);
            int Count = 0;
            foreach (MeasuringTool item in collection)
            {
                if (item.Name == "�������")
                    Count += 1;
            }
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(Lab_14.Program.CountSecondPartLINQ(collection, "�������"), Count);
        }

        [TestMethod]
        public void CountSecondPartLINQ_Exception() //���������� ���� ������������ � ��������� ������ �� ���������� 
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 1);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.CountSecondPartLINQ(collection, "�");
            });
        }

        [TestMethod]
        public void MaxSecondPartLINQ() //����� ����������� � ������������ ��������� 
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 10);
            double max = 0;
            foreach (MeasuringTool item in collection)
                if (item.accuracy > max) max = item.accuracy;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(Lab_14.Program.MaxSecondPartLINQ(collection), max);
        }

        [TestMethod]
        public void MaxSecondPartLINQ_Exception() //���������� ��� ���������� ������������� ������������ � ��������� 
        {
            var collection = new SortedDictionary<string, Queue<object>>
        {
            { "2", new Queue<object>(new object[] { new HandTool(12, "��������", "������"), new HandTool(15, "������", "������") }) },
            { "3", new Queue<object>(new object[] { new Instrument("�������", 111), new Instrument("��������", 45) }) }
        }; 

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
            {
                Lab_14.Program.MaxFirstPartLINQ(collection);
            });
        }

        [TestMethod]
        public void GroupSecondPartLINQ() //����������� �� ��������
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 1);
            collection.Remove(collection[0]);
            collection.Add(new MeasuringTool(12, "�������", "�", "�������", 1.0));
            collection.Add(new MeasuringTool(12, "�������", "�", "�������", 2.0));
            collection.Add(new MeasuringTool(12, "��������", "�", "�������", 1.0));
            var groupedData = Lab_14.Program.GroupSecondPartLINQ(collection);

            var consoleOutput = ConsoleOutput(() => { Lab_14.Program.PrintGroups(Lab_14.Program.GroupSecondPartLINQ(collection)); });
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("������ � ��������� 1. ���������� ����������� � ������: 2"));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("������ � ��������� 2. ���������� ����������� � ������: 1"));
        }

        [TestMethod]
        public void FindItem_2Part_sByAccuracyLINQ()  
        {
            MyCollection<MeasuringTool> collection = new MyCollection<MeasuringTool>("q", 1);
            collection[0] = new MeasuringTool(1, "����", "������", "����������", 1);
            var result = Lab_14.Program.FindValue_2Part_ByAccuracy_LINQ(collection, 1);
            var consoleOutput = ConsoleOutput(() => { Lab_14.Program.PrintIEnumerableResult(result); });
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(consoleOutput.Contains("1: ����, �������� ������, ������� ��������� ����������, �������� 1"));
        }

        [TestClass]
        public class Test_Lab13
        {
            //������������ �������������
            [TestMethod]
            public void Test_ConstuctorWithoutParams() //���� �������� �� �������� ������� ������� MyCollection
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, collection.Count);
            }

            [TestMethod]
            public void Test_ConstuctorOnlyName() //���� �������� �� �������� ������� ������� MyCollection
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("r");
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("r", collection.NameOfCollection);
            }

            [TestMethod]
            public void Test_Constructor_Length() //�������� ������������, ������������� ��������� �� �� �����
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //������� ��������� � ������� ����� �� �����
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(collection.Count, 5); //��������� ����� ��������� ��������� 5 ���������
            }
            //������������ ������������� ���������

            //������������ ����������
            [TestMethod]
            public void GetEnumerator_WhenCollectionHasItems_ShouldEnumerateAllItems() //��������� ��� ��������� 
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 1); //������� ��������� � ��������� �� ����������
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
            public void GetEnumerator_CollectionHasRemovedElement() //��������� ��� ��������� � ��������� ���������
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 1); //������� ��������� � ��������� �� ����������
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
            //������������ ���������� ���������

            //������������ ICollection
            [TestMethod]
            public void ICollection_CopyTo() // �������� CopyTo 
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //������� ��������� � ��������� �� ����������
                Instrument[] list = new Instrument[5];
                collection.CopyTo(list, 0);
                PointHash<Instrument> value = collection.GetFirstValue();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(value.Data, list[0]);
            }

            [TestMethod]
            public void ICollection_Count() //�������� �������� ���������
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //������� ��������� � ��������� �� ����������
                Instrument[] list = new Instrument[5];
                collection.CopyTo(list, 0);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(collection.Count, list.Length);
            }
            //������������ ICollection

            //���� Exception
            [TestMethod]
            public void ICollection_CopyTo_ExceptionIndexOutsideOfListLength() //�������� ���������� ��� ������������ ����� ������� ��� ������� ������������ �������� ��������� � ������
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //������� ��������� � ��������� �� ����������
                Instrument[] list = new Instrument[5];
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
                {
                    collection.CopyTo(list, 6);
                });
            }

            [TestMethod]
            public void ICollection_CopyTo_ExceptionNotEnoughListLength() //������, ����� �� ������� ����� ��� ���� ��������� � �������
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //������� ��������� � ��������� �� ����������
                Instrument[] list = new Instrument[5];
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
                {
                    collection.CopyTo(list, 2);
                });
            }

            [TestMethod]
            public void TestClear() //�������� ������� ������
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 6);
                collection.Clear();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, collection.Count);
            }

            [TestMethod]
            public void TestAdd() //�������� ���������� ��������
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 1);
                Instrument t = new Instrument();
                collection.Add(t);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(collection.Contains(t));

            }

            //����� �� ������ ����� ��� �������� ������� ���-�������
            //���� Exception
            [TestMethod]
            public void Test_CreateTable_Exception() //������������ ������ ��� ������� ������������ ������ �������
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
                {
                    MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(-1);
                });
            }

            [TestMethod]
            public void Test_AddExistingElement_Exception() //������������ ������ ��� ������� ������������ ������ �������
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
            public void Test_PrintNullTable_Exception() //������������ ������ ��� ������� ������ ������ �������
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
                {
                    table.Print();
                });
            }//���� Exception ��������

            [TestMethod]
            public void TestCreateTable() //������������ ������������ ��� �������� ���-�������
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(5);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(table.Capacity, 5);
            }

            //����������� AddPoint
            [TestMethod]
            public void TestAddPointToHashTable() //������������ ���������� �������� � �������
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(5);
                HandTool tool = new HandTool();
                table.AddPoint(tool);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(table.Contains(tool));
            }

            [TestMethod]
            public void TestAddCount() //������������ ���������� Count ����� ���������� �������� � �������
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(5);
                HandTool tool = new HandTool();
                table.AddPoint(tool);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(6, table.Count);
            }

            //����������� �������� �������� �� �������
            [TestMethod]
            public void TestRemovePointFromHashTableTrue() //������������ ���������� �������� ������������� �������� �� �������
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
                HandTool tool = new HandTool();
                table.AddPoint(tool);
                table.RemoveData(tool);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(table.Contains(tool));
            }

            [TestMethod]
            public void TestRemovePointFromHashTable_False() //������������ �������� ��������������� �������� �� �������
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
                HandTool tool = new HandTool();
                table.AddPoint(tool);
                table.RemoveData(tool);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(table.Contains(tool));
            }

            [TestMethod]
            public void TestRemovePointFromHashTable_OutOfKey_False() //������������ �������� ��������������� �������� �� �������
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
                Instrument tool = new Instrument("��������� ������ ������ ���������", 9999);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(table.RemoveData(tool));
            }

            [TestMethod]
            public void TestRemovePoint_FromBeginingOfTableTable() //������������ �������� ������� � ������� �������� �� �������
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
                Instrument tool2 = new Instrument("����������", 98);
                Instrument tool3 = new Instrument("��������������", 85);
                Instrument tool4 = new Instrument("���������", 41);
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
            public void MyCollection_IndexOf_Exceptin() //�������� ���������� ��� ������� �������� ������ ��������������� � ��������� ��������
            {
                MyCollection<Instrument> col = new MyCollection<Instrument>("q", 2);
                Instrument tool = new Instrument();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
                {
                    col.IndexOf(tool);
                });
            }

            //������������ ������ Contains
            [TestMethod]
            public void TestContainsPointTrue() //����� Contains ����� ������� ���� � �������
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
                HandTool tool = new HandTool();
                table.AddPoint(tool);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(table.Contains(tool));
            }

            [TestMethod]
            public void TestContainsPointFalse() //����� �������� ��� � �������
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
                HandTool tool = new HandTool();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(table.Contains(tool));
            }

            //������������ ToString ��� PointHash
            [TestMethod]
            public void TestToStringPoint() //������������ ToString ��� ������ ����
            {
                HandTool tool = new HandTool();
                PointHash<Library_10.Instrument> p = new PointHash<Library_10.Instrument>(tool);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(p.ToString(), tool.ToString());
            }

            [TestMethod]
            public void TestConstructWhithoutParamNext() //����������� ���� ��� ����������, Next = null
            {
                PointHash<Instrument> p = new PointHash<Instrument>();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNull(p.Next);
            }

            [TestMethod]
            public void TestConstructWhithoutParamPred() //����������� ���� ��� ����������, Pred = null
            {
                PointHash<Instrument> p = new PointHash<Instrument>();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNull(p.Pred);
            }

            //������������ ������� ToString � GetHashCode ��� ������ PointHash
            [TestMethod]
            public void ToString_WhenDataIsNull_ReturnEmptyString() //����������� ��� ���������� ����� ToString
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
            public void GetHashCode_WhenDataIsNull_ReturnZero() //������������ GetHashCode ��� ����, ���������� � ������� ������������ ��� ����������
            {
                PointHash<Instrument> point = new PointHash<Library_10.Instrument>();
                int result = point.GetHashCode();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, result);
            }

            [TestMethod]
            public void GetHashCode_WhenDataIsNotNull_ReturnDataHashCode() //����������� GetHashCode ��� ������������ ����
            {
                Library_10.Instrument tool = new Instrument();
                tool.RandomInit();
                PointHash<Instrument> point = new PointHash<Library_10.Instrument>(tool);
                int result = point.GetHashCode();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(tool.GetHashCode(), result);
            }

            [TestMethod]
            public void CopyTo() //����������� GetHashCode ��� ������������ ����
            {
                MyCollection<Instrument> col = new MyCollection<Instrument>("w", 6);
                MyCollection<Instrument> col2 = new MyCollection<Instrument>("w", col);

                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(col.GetFirstValue().Data, col2.GetFirstValue().Data);
            }

            [TestMethod]
            public void CopyTo_Default() //����������� GetHashCode ��� ������������ ����
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
            public void CopyToCount() //����������� GetHashCode ��� ������������ ����
            {
                MyCollection<Instrument> col = new MyCollection<Instrument>("w", 6);
                MyCollection<Instrument> col2 = new MyCollection<Instrument>("w", col);

                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(col.Count, col2.Count);
            }

            [TestMethod]
            public void Remove_OneElementInChain() //����������� GetHashCode ��� ������������ ����
            {
                MyCollection<HandTool> col = new MyCollection<HandTool>("w", 1);
                PointHash<HandTool> tool = col.GetFirstValue();
                col.Remove(tool.Data);
                HandTool tool1 = new HandTool(82, "��������������", "��������");
                HandTool tool2 = new HandTool(12, "��������������", "����");
                HandTool tool3 = new HandTool(30, "��������", "������");
                HandTool tool4 = new HandTool(53, "�������", "��������������� �����");

                col.Add(tool1);
                col.Add(tool2);
                col.Add(tool3);
                col.Add(tool4);

                col.Remove(tool1);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(col.Contains(tool1));
            }

            [TestMethod]
            public void TestIndexGet() //������������ get �����������
            {
                MyCollection<HandTool> col = new MyCollection<HandTool>("w", 1);
                HandTool tool = col.GetFirstValue().Data;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(tool, col[0]);
            }

            [TestMethod]
            public void TestIndexGet_Exception() //������������ ���������� ��� get �����������
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //������� ��������� � ��������� �� ����������
                Instrument[] list = new Instrument[5];
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
                {
                    collection.Remove(collection[-1]);
                });
            }

            [TestMethod]
            public void TestIndexSet_Exception() //������������ ���������� � set 
            {
                MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //������� ��������� � ��������� �� ����������
                Instrument[] list = new Instrument[5];
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsException<Exception>(() =>
                {
                    collection[-1] = new Instrument();
                });
            }

            [TestMethod]
            public void TestIndexSet() //����������� set 
            {
                MyCollection<HandTool> col = new MyCollection<HandTool>("w", 1);
                HandTool tool = col.GetFirstValue().Data;
                col[0] = new HandTool();
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(new HandTool(), col[0]);
            }

            [TestMethod]
            public void IsReadOnly_MyCollection() //������������ �������� IsReadOnly � MyCollection
            {
                MyCollection<HandTool> col = new MyCollection<HandTool>("w", 1);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(col.IsReadOnly);
            }

            [TestMethod]
            public void Count_MyCollection() //������������ ���-�� ��������� � MyCollection
            {
                MyCollection<HandTool> col = new MyCollection<HandTool>("w", 6);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(col.Count, 6);
            }
        }
    }
}