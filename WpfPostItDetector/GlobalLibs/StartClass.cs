using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKSmartTrainnerAdaptor.GlobalLibs
{
    public static class StartClass
    {
        public static void servicesStart()
        {
            /// <summary>
            /// Metoths to start when app starts
            /// </summary>



            /// <summary>
            /// Metoths to start when app starts
            /// </summary>

            Ble.Start.StartServices();
            MotionDetector.Start.StartServices();

        }
    }
}
