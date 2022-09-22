using System;
using System.BluetoothLe;
using System.Threading.Tasks;
using SDKSmartTrainnerAdaptor.Ble;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
        {
        public async Task GetData(Characteristic deviceCharacteristic)
        {

            var device = deviceCharacteristic.Service.Device;
            var _deviceCharacteristic = deviceCharacteristic.Id.ToString().ToUpper();


            WorkingDataBLE.WorkingDataDictonaryByte[device.Id.ToString().ToUpper() + "|" + _deviceCharacteristic] = await deviceCharacteristic.ReadAsync();

            WorkingDataBLE.WorkingDataDictonaryLastUpdate[device] = DateTime.Now;
        }

    }
}