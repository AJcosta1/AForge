using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
    {
        public async Task updateConnections()
        {

            foreach (var device in Variables.DevicesDetected.ToList())
            {
                lock (compatibleList)
                {
                    if (!compatibleList.ContainsKey(device))
                        compatibleList[device] = true;
                }
                if (compatibleList[device] && !device.IsConnected && FindDevice(device.Id) == null)
                {
                    await connectDeviceAsync(device);
                }

            }
        }
    }
}
