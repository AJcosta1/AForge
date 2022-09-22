using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTurboTrainnerAdaptorBle
{
    class Utils
    {
    }

    class BleDevices
    {
    }

    public class DeviceDataList
    {


        public string service { get; set; }

        public string characteristic { get; set; }
        public bool read { get; set; }
        public string dataToWrite { get; set; }

    }
}
