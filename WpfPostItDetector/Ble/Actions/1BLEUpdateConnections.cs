using SDKSmartTrainnerAdaptor.GlobalLibs;
using System;
using System.BluetoothLe;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SDKSmartTrainnerAdaptor.Ble;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
    {

        public async Task updateConnections()
        {
            try
            {
                foreach (var device in WorkingDataBLE.Current.SessonData.DevicesDetected)
                {

                    if (device.Device != null)
                    {
                        switch (device.Device.State)
                        {
                            case DeviceState.Disconnected:


                                await connectDevice(device);
                                break;
                            case DeviceState.Limited:

                                disconnectDevice(device.Device);

                                foreach (var characteristic in device.Characteristics)
                                    characteristic.isUpdating = false;
                                Thread.Sleep(1000);
                                await connectDevice(device);

                                break;
                            case DeviceState.Connected:

                                break;
                            case DeviceState.Connecting:

                                //Console.WriteLine("Connecting");
                                break;
                        }

                        WorkingDataBLE.Current.SessonData.UpdateListDeviceConnected();
                    }

                }
            }
            catch
            { }
        }
    }
}
