using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_10
{
    public interface IInit                  //интерфейс - ссылочный тип, который определяет поведение методов, свойств, индексаторов и тд. Определяет что должно быть сделано, а не как
    {
        void Init();
        void RandomInit();
        void Show();
    }
}
