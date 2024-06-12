using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library_10
{
    public class MeasuringTool : HandTool, IInit, ICloneable
    {
        public string units;
        public double accuracy;
        public static int count;

        static string[] uni = { "Сантиметры", "Метры", "Градусы" };
        static double[] acc = { 1, 0.01, 0.1, 0.5, 0.05 };

        protected string Units
        {
            get => units;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Строка не может быть пустой");
                }
                else
                    units = value;
            }
        }
        protected double Accuracy
        {
            get => accuracy;
            set
            {
                if (value < 0)
                    throw new Exception("Точность не может быть отрицательной");
                else if (value > 5.0)
                    throw new Exception("Точность не может быть больше 5 мм");
                else accuracy = value;
            }
        }

        public MeasuringTool() : base()                          //конструкторы
        {
            Units = "Нет единиц измерения";
            Accuracy = 0;
            count++;
        }


        public MeasuringTool(int id, string name, string material, string units, double accuracy) : base(id, name, material)
        {
            Units = units;
            Accuracy = accuracy;
            count++;
        }

        public MeasuringTool(MeasuringTool meas)
        {
            Name = meas.name;
            id = meas.id;
            Units = meas.units;
            Accuracy = meas.accuracy;
            count++;
        }

        public override string ToString()
        {
            return base.ToString() + $", единицы измерения {Units}, точность {Accuracy}";
        }

        public override void Show()                                  
        {
            base.Show();
            Console.WriteLine($"Измерительный инструмент. Единицы измерения: {Units}, точность: {Accuracy}");
        }

        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

        public void ShowUsual()
        {
            Console.WriteLine($"Название: {Name} Единицы измерения: {Units}, точность: {Accuracy}");
        }

        public override void Init()
        {
            base.Init();
            Console.WriteLine("Введите единицы измерения");
            Units = Console.ReadLine();
            Console.WriteLine("Введите точность измерительного инструмента");
            try
            {
                Accuracy = double.Parse(Console.ReadLine());
            }
            catch
            {
                Accuracy = 0;
            }
        }

        public override void RandomInit()
        {
            base.RandomInit();
            Units = uni[rnd.Next(uni.Length)];
            Accuracy = acc[rnd.Next(acc.Length)];
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is MeasuringTool instr)
            {
                return base.Equals(obj) && instr.Accuracy == this.Accuracy && instr.Units == this.Units;
            }
            else return false;
        }

        public static void WoodInstrument(double number, MeasuringTool tool)            //названаие измерительного инструмента, точность которого больше чем number
        {
            if (tool.accuracy < number)
                Console.WriteLine(tool.name);
        }

        public object Clone()
        {
            return new MeasuringTool(id.number, Name, Material, Units, Accuracy);
        }

        

    }
}
