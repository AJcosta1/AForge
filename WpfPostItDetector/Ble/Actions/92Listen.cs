
using SDKSmartTrainnerAdaptor;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Storage.Streams;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
    {

        public Dictionary<string, bool> isUpdating = new Dictionary<string, bool>();

        public async Task ListenAsync(GattCharacteristic characteristic, BluetoothLEDeviceDisplay device)
        {

            try
            {
                await characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
            }
            catch (Exception e)
            { }
            characteristic.ValueChanged -= (o, args) => { };

            SDKSmartTrainnerAdaptor.Ble.Start.rootClass.loggerAdd("Listen: " + device.Name);

            characteristic.ValueChanged += (o, args) =>
            {
                var buffer = args.CharacteristicValue;
                DataReader dataReader = DataReader.FromBuffer(buffer);
                byte[] bytes = new byte[buffer.Length];
                dataReader.ReadBytes(bytes);

                Variables.WorkingDataDictonaryByte[device.Id.ToString().ToUpper() + "|" + o.Uuid.ToString().ToUpper()] = bytes;

            };

            Thread.Sleep(1000);


        }
    }
}