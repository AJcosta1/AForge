
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

        public void disconnectDevice(IDevice _device)
        {
            try
            {
                if (!isScanning())
                {                    
                    adapter.DisconnectDeviceAsync(_device);
                
                    Thread.Sleep(1000);
                }     
               

            }
            catch (DeviceConnectionException e)
            {

               // WorkingDataEvent.ListaEventosGerados.Add(new Abstractions.WorkingData.Event.Part() { description = FormVariableNames.EventCloudNotConnectToDeviceBLE + " " + name, data = DateTime.UtcNow.ToString("HH:mm:ss", CultureInfo.InvariantCulture) });
            }

        }

    }
}