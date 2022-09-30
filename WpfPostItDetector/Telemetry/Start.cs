
using SDKSmartTrainnerAdaptor.Ble.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SDKSmartTrainnerAdaptor.Telemetry
{
    public static partial class Telemetry
    {

        public static MainWindow rootClass;

        public static void StartServices(MainWindow _rootClass)
        {
            rootClass = _rootClass;


            new GTA5TelemetryReceiver(rootClass);
            new GTA5TelemetrySender(rootClass);
        }
      
    }
}
