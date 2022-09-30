
using SDKSmartTrainnerAdaptor; 
using System;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Storage.Streams;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
    {
        public async Task GetData(GattCharacteristic characteristic, BluetoothLEDeviceDisplay device)
        {
            GattReadResult result = await characteristic.ReadValueAsync();
            if (result.Status == GattCommunicationStatus.Success)
            {
                var reader = DataReader.FromBuffer(result.Value);
                byte[] input = new byte[reader.UnconsumedBufferLength];
                reader.ReadBytes(input);


                Variables.WorkingDataDictonaryByte[device.Id.ToString().ToUpper() + "|" + characteristic.Uuid.ToString().ToUpper()] = input;

                Variables.WorkingDataDictonaryLastUpdate[device] = DateTime.Now;
                reader.ReadBytes(input);

            }

        }

    }
}