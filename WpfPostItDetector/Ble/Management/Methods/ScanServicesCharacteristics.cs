
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Exceptions;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Extensions;
using Plugin.BLE.Abstractions.EventArgs;
using System.Collections;
using IndoorCycling.Abstractions.Enums.BLE;
using IndoorCycling.Abstractions.Configurations.BLE;
using IndoorCycling.Abstractions.WorkingData.BLE;
using Xamarin.Forms;
using System.Reflection;
using IndoorCycling.Abstractions.WorkingData.Event;
using IndoorCycling.Abstractions.Enums.Speech.FomsVariableNames;
using System.Globalization;
using IndoorCycling.Abstractions.WorkingData.DataSavingManagement;

namespace IndoorCycling.Libs.BLE.Management
{
        public partial class BLEMetods
        {



        public async Task ScanServicesCharacteristics(DevicesDetected _device)
        {

            IReadOnlyList<IService> _servicesFound = await _device.Device.GetServicesAsync();

            foreach (var _service in _servicesFound)
            {
                _device.Services.Add(_service);

                IReadOnlyList<ICharacteristic> _characteristicsFound = await _service.GetCharacteristicsAsync();

                foreach(var c in _characteristicsFound)
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