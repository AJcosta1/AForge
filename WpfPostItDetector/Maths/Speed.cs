
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
        public static void Speed()
        {
 

            double wheelPerimeter =2155;
            float rotacao = (float)rootClass.SpeedRpm;

 
            double f = 1;

            double Speed = (wheelPerimeter * rotacao / 60000) * f;

           rootClass.Speed_ms = Speed; //m.s-1
           
            rootClass.TargetSpeed = rootClass.Speed_ms;


            //adapt the read
            rootClass.GameSpeed = rootClass.GameSpeedRead;

            

        }
       

    }
}
