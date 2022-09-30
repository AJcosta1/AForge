using System;
using System.Threading;
using Windows.Devices.Bluetooth;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
    {

        public void disconnectDevice(BluetoothLEDevice _device)
        {

            try
            {

                _device.Dispose();


                Thread.Sleep(1000);



            }
            catch (Exception e)
            {

                // WorkingDataEvent.ListaEventosGerados.Add(new Abstractions.WorkingData.Event.Part() { description = FormVariableNames.EventCloudNotConnectToDeviceBLE + " " + name, data = DateTime.UtcNow.ToString("HH:mm:ss", CultureInfo.InvariantCulture) });
            }

        }

    }
}