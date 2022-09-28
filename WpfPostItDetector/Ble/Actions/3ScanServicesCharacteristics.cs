using SDKSmartTrainnerAdaptor;
using SDKSmartTrainnerAdaptor.Ble.UuidDictionary;
using SDKSmartTrainnerAdaptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.BluetoothLe;
using SDKSmartTrainnerAdaptor.Ble;
using System.Linq.Expressions;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{

    public partial class BLEMethods
     {
        public async Task ScanServicesCharacteristics(Device device)
        {

           
                var _device = WorkingDataBLE.Current.SessonData.DevicesDetected.First(d => d.Device.Id == device.Id);

                try
                {
                    if (device.State==DeviceState.Connected) {
                        IReadOnlyList<Service> _servicesFound = await device?.GetServicesAsync();

                        foreach (var _service in _servicesFound)
                        {
                            IReadOnlyList<Characteristic> _characteristicsFound = await _service.GetCharacteristicsAsync();
                            foreach (var c in _characteristicsFound)
                            {
                                _device.Characteristics.Add(new DevicesCharacteristics()
                                {
                                    Characteristic = c,
                                    isUpdating = false

                                });

                            await Task.Delay(100);
                        }
                    }
                    if (_device.Characteristics.Count() > 0)
                    {
                        if (!isCompatible(device))
                        {
                            device.CancelEverything();
                            adapter.DisconnectDeviceAsync(device);
                            _device.isCompatible = false;
                        }
                    }
                    else
                        _device.isCompatible = false;


                }

            }
                catch (Exception e)
                {

                }
             
        }



    }


}