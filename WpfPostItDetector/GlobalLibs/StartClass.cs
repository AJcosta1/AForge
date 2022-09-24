using SDKSmartTrainnerAdaptor.GamePadEmulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            GamePad.Start(this);


        }
    }
}
