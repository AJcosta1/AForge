using System;
using System.BluetoothLe;
using System.Threading;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
    {

        public void disconnectDevice(Device _device)
        {
            try
            {
                if (!isScanning())
                {
                    adapter.DisconnectDeviceAsync(_device);

                    Thread.Sleep(1000);
                }


            }
            catch (DeviceConnectionException e)
            {

                // WorkingDataEvent.ListaEventosGerados.Add(new Abstractions.WorkingData.Event.Part() { description = FormVariableNames.EventCloudNotConnectToDeviceBLE + " " + name, data = DateTime.UtcNow.ToString("HH:mm:ss", CultureInfo.InvariantCulture) });
            }

        }


    }
}