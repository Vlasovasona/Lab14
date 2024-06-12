using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_10
{
    public class AccuracyToMeasuringTool
    {
        public string classOfAccuracy;
        public double accuracy;

        public double Accuracy
        {
            get => accuracy;
            set
            {
                accuracy = value;
            }
        }

        public string ClassOfAccuracy
        {
            get => classOfAccuracy;
            set
            {
                classOfAccuracy = value;
            }
        }


        public AccuracyToMeasuringTool(double accuracy)
        {
            if (accuracy <= 0.1)
                ClassOfAccuracy = "Относительная точность";
            else
                classOfAccuracy = "Приведенная точность";
            //ClassOfAccuracy = classOfAccuracy;
            Accuracy = accuracy;
        }
    }
}
