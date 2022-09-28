
using SDKSmartTrainnerAdaptor;
using SDKSmartTrainnerAdaptor;
using System;
using System.BluetoothLe;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SDKSmartTrainnerAdaptor.Ble;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
       {

        public Dictionary<string, bool> isUpdating = new Dictionary<string, bool>();

        public async Task ListenAsync(Characteristic characteristic)
        {

                        characteristic.ValueUpdated -= (o, args) => { };

            await Task.Delay(1000);


            characteristic.ValueUpdated += (o, args) =>
            {
                WorkingDataBLE.WorkingDataDictonaryByte[args.Characteristic.Service.Device.Id.ToString().ToUpper() + "|" + args.Characteristic.Id.ToString().ToUpper()] = args.Characteristic.Value;

                WorkingDataBLE.WorkingDataDictonaryLastUpdate[args.Characteristic.Service.Device] = DateTime.Now;
            };

            await Task.Delay(1000);

            characteristic.StartUpdatesAsync();





        }
    }
}