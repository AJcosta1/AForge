
using SDKSmartTrainnerAdaptor.Ble.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SDKSmartTrainnerAdaptor.Maths
{
    public static  partial class Maths
    {
        public static void ToGameDirection()
        {
            double max = 100;



            double target = 0;
            target = -rootClass.PosX;

            target = (target / 300) * max;
            target=target < -max ? -max : target;
            target = target > max ? max : target;
            target = target > -10 && target < 10 ? 0 : target;
            rootClass.ToGameDirection = target;
        }
       

    }
}
