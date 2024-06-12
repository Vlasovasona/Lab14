using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library_10
{
    public class ElectricTool : Instrument, IInit, ICloneable
    {
        private string powerSupply;
        public int workingTime;
        public static int count;

        static string[] supply = { "Электричество" };
        static int[] hours = { 0, 30, 60, 120, 240, 180 };

        protected string PowerSupply
        {
            get => powerSupply;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Строка не может быть пустой");
                }
                else
                    powerSupply = value;
            }
        }
        protected int WorkingTime
        {
            get => workingTime;
            set
            {
                if (value < 0)
                    throw new Exception("Время работы от аккумулятора не может быть отрицательным");
                else workingTime = value;
            }
        }

        public ElectricTool() : base()                          //конструкторы
        {
            PowerSupply = "Нет источника питания";
            WorkingTime = 0;
            count++;
        }

        public ElectricTool(ElectricTool tool)                          //конструкторы
        {
            Name = tool.name;
            id = tool.id;
            PowerSupply = tool.powerSupply;
            WorkingTime = tool.workingTime;
            count++;
        }


        public ElectricTool(int id, string name, string powerSupply, int workingTime) : base(name, id)
        {
            PowerSupply = powerSupply;
            WorkingTime = workingTime;
            count++;
        }

        public override string ToString()
        {
            return base.ToString() + $", источник питания {PowerSupply}, время работы от аккумулятора {WorkingTime}";
        }

        public override void Show()                                  //сокрытие имен
        {
            base.Show();
            Console.WriteLine($"Источник питания: {PowerSupply}, время работы от аккумулятора: {WorkingTime}");
        }

        public void ShowUsual()
        {
            Console.WriteLine($"Название: {Name}  Единицы измерения: {PowerSupply}, время работы от аккумулятора: {WorkingTime}");
        }

        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

        public override void Init()
        {
            base.Init();
            Console.WriteLine("Источник питания электрического инструмента");
            PowerSupply = Console.ReadLine();
            Console.WriteLine("Введите время работы инструмента от аккумулятора в минутах. Если аккумулятора нет, введите 0");
            try
            {
                WorkingTime = int.Parse(Console.ReadLine());
            }
            catch
            {
                WorkingTime = 0;
            }
        }

        public override void RandomInit()
        {
            base.RandomInit();
            PowerSupply = supply[rnd.Next(supply.Length)];
            WorkingTime = hours[rnd.Next(hours.Length)];
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is ElectricTool instr)
            {
                return base.Equals(obj) && instr.PowerSupply == this.PowerSupply && instr.WorkingTime == this.WorkingTime;
            }
            else return false;
        }

        public object Clone()
        {
            return new ElectricTool(id.number, Name, PowerSupply, WorkingTime);
        }

        
    }
}
