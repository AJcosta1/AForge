
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
using System.Threading;

namespace IndoorCycling.Libs.BLE.Management
{
    public partial class BLEMetods
    {

        public async Task<IDevice> connectDevice(DevicesDetected _device)
        {
 
            try
            {
                if (!isScanning())
                {

                    if (WorkingDataBLE.WorkingDataDictonaryLastUpdate.ContainsKey(_device.Device))
                    {
                        TimeSpan ts = DateTime.Now - WorkingDataBLE.WorkingDataDictonaryLastUpdate[_device.Device];

                        if (Convert.ToInt32(ts.TotalSeconds) > 10)
                        {
                            WorkingDataBLE.WorkingDataDictonaryLastUpdate[_device.Device] = DateTime.Now;



                            await adapter.ConnectToDeviceAsync(_device.Device);

                        

                        }
                    }
                    else
                    {

                        WorkingDataBLE.WorkingDataDictonaryLastUpdate[_device.Device] = DateTime.Now;

                        await adapter.ConnectToDeviceAsync(_device.Device);
                    }

                    await ScanServicesCharacteristics(_device);


           }

            }
            catch (DeviceConnectionException e)
            {
              
          }
            return _device.Device;
        }
 
    }
}