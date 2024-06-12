using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library_10
{
    public class HandTool : Instrument, IInit, ICloneable                    //класс internal, потому что он доступен только в этой сборке
    {                                                     //класс public, потому что нужно, чтобы работали тесты
        public string material;
        public static int count;

        static string[] materials = { "Дерево", "Сталь", "Пластик", "Железо","Алюмииний","Титан","Олово","Медь","Алмазный сплав","Родий","Латунь","Поликарбонат","Стекло","Дегированная сталь","Углеродистая сталь","Нитрид бора","Эльбор","Абразив","Конструкционная сталь","Юыстрорежущая сталь","Твердый сплав","Чугун","Минералокерамика","Сверхтвердый материал","Обсидиан","Вольфрам","Карбидный сплав","Резина" };

        protected string Material
        {
            get => material;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Строка не может быть пустой");
                }
                else
                    material = value;
            }
        }

        public Instrument GetBase
        {
            get => new Instrument(name, id.number);
        }

        public HandTool() : base()
        {
            Material = "Нет материала";
            count ++;
        }

        public HandTool(HandTool inst) 
        {
            Name = inst.name;
            id = inst.id;
            Material = inst.material;
            count++;
        }

        public override string ToString()
        {
            return base.ToString() + $", материал {Material}";
        }

        public HandTool(int id, string name, string material) : base(name, id)
        {
            Material = material;
            count++;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Материал: {Material}");
        }

        public void ShowUsual()                                        //скортыие имен
        {
            Console.WriteLine($"Название: {Name} Материал: {Material}");
        }
        public override void Init()
        {
            base.Init();
            Console.WriteLine("Введите материал ручного инструмента");
            Material = Console.ReadLine();
        }

        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

        public override void RandomInit()
        {
            base.RandomInit();
            Material = materials[rnd.Next(materials.Length)];
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is HandTool instr)
            {
                return base.Equals(obj) && instr.Material == this.Material;
            }
            else return false;
        }

        public object Clone()
        {
            return new HandTool(id.number, Name, Material);
        }

        
    }
}
