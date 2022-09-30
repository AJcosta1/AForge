
using SDKSmartTrainnerAdaptor; 
using System;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
    {

        public async Task connectDeviceAsync(BluetoothLEDeviceDisplay _device)
        {
            try
            {
                if (!Variables.WorkingDataDictonaryLastUpdate.ContainsKey(_device))
                {
                    Variables.WorkingDataDictonaryLastUpdate[_device] = DateTime.Now;
                }

                TimeSpan ts = DateTime.Now - Variables.WorkingDataDictonaryLastUpdate[_device];

                if (Convert.ToInt32(ts.TotalSeconds) > 2)
                {
                    Variables.WorkingDataDictonaryLastUpdate[_device] = DateTime.Now;

                    BluetoothLEDevice bluetoothLeDevice = await BluetoothLEDevice.FromIdAsync(_device.DeviceInformation.Id);


                    await ScanServicesCharacteristic(_device, bluetoothLeDevice);

                }

            }
            catch (Exception e)
            {

            }

        }


    }
}