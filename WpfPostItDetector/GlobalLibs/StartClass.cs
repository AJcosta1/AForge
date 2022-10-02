using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKSmartTrainnerAdaptor.GamePadEmulator;

namespace SDKSmartTrainnerAdaptor
{
    public partial class MainWindow
    {
        public void servicesStart()
        {
            /// <summary>
            /// Metoths to start when app starts
            /// </summary>



            /// <summary>
            /// Metoths to start when app starts
            /// </summary>

            Ble.Start.StartServices(this);

            Maths.Maths.StartServices(this);

            Telemetry.Telemetry.StartServices(this);

            //GamePadEmulator.GamePadEmulator.Start(this);



        }
    }
}
