
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
          
        public  async void scanNewDevices()
        {


            if(!adapter.IsScanning)
            {


                foreach (var item in WorkingDataBLE.Current.SessonData.DevicesDetected)
                {
                    disconnectDevice(item.Device);

                }

            WorkingDataBLE.Current.SessonData.DevicesDetected.Clear();

              



                //Get all necessary Found services

                List<Guid> _GuidServiceList = new List<Guid>();

            var results = WorkingDataBLE.ListaConfiguracaoDispositivos.Select(x => x.service).Distinct();


                if (results.Count() > 0)
                {

                    foreach (var result in results)
                        _GuidServiceList.Add(Guid.Parse(result));


                    Guid[] GuidServiceList = _GuidServiceList.ToArray();

                    adapter.ScanTimeout = BLEConfiguration.scanNewDevicesTime;
                    adapter.DeviceDiscovered += (s, a) =>
                    {
                        if(WorkingDataBLE.Current.SessonData.DevicesDetected.Where(x=>x.Device==a.Device).Count()==0)
                                WorkingDataBLE.Current.SessonData.DevicesDetected.Add(
                                    new DevicesDetected() { Device = a.Device });
                        WorkingDataBLE.Current.SessonData.UpdateListDeviceConnected();

                    };



                    try
                    {
                        if (!ble.Adapter.IsScanning)
                            await adapter.StartScanningForDevicesAsync(GuidServiceList);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);

                    }



                }


            }
        }

        public bool isScanning()
        {
            return adapter.IsScanning;
        }

    }
}