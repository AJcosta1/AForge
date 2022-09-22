using SDKSmartTrainnerAdaptor;
using SDKSmartTrainnerAdaptor.Ble.UuidDictionary;
using SDKSmartTrainnerAdaptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.BluetoothLe;
using SDKSmartTrainnerAdaptor.Ble;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{

    public partial class BLEMethods
     {


        public async Task ScanServicesCharacteristics(DevicesDetected _device)
        {

            IReadOnlyList<Service> _servicesFound = await _device.Device.GetServicesAsync();

            foreach (var _service in _servicesFound)
            {
                _device.Services.Add(_service);

                IReadOnlyList<Characteristic> _characteristicsFound = await _service.GetCharacteristicsAsync();

                foreach (var c in _characteristicsFound)
                {
                    _device.Characteristics.Add(new DevicesCharacteristics()
                    {
                        Characteristic = c,
                        isUpdating = false

                    });

                }


            }


        }

    }


}