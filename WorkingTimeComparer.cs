using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_10
{
    public class SortByWorkingTime : IComparer
    {
        public int Compare(object? x, object? y)
        {
            if (x == null && y == null) return -1;
            if (x is ElectricTool && y is ElectricTool)
            {
                ElectricTool tool1 = x as ElectricTool;
                ElectricTool tool2 = y as ElectricTool;
                if (tool1.workingTime < tool2.workingTime) return -1;
                else if (tool1.workingTime == tool2.workingTime) return 0;
                else return 1;
            }
            return -1;
        }
    }
}
