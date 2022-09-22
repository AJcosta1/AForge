using SDKSmartTrainnerAdaptor.Ble.Configuration;
using System;
using System.BluetoothLe;
using System.Collections.Generic;
using System.Linq;
using SDKSmartTrainnerAdaptor.Ble;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
    {
      
        Adapter adapter;


        public BLEMethods()
        {

            adapter = BluetoothLE.Current.Adapter;



            adapter.DeviceConnected += (o, args) =>
            {
                WorkingDataBLE.Current.SessonData.UpdateListDeviceConnected();
            };


            adapter.DeviceDisconnected += (o, args) =>
            {
                WorkingDataBLE.Current.SessonData.UpdateListDeviceConnected();
            };

            adapter.DeviceConnectionLost += (o, args) =>
            {
                WorkingDataBLE.Current.SessonData.UpdateListDeviceConnected();
            };




        }

        public async void scanNewDevices()
            {


                if (!adapter.IsScanning)
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
                            if (WorkingDataBLE.Current.SessonData.DevicesDetected.Where(x => x.Device == a.Device).Count() == 0)
                                WorkingDataBLE.Current.SessonData.DevicesDetected.Add(
                                    new DevicesDetected() { Device = a.Device });
                            WorkingDataBLE.Current.SessonData.UpdateListDeviceConnected();

                        };



                        try
                        {
                            if (!adapter.IsScanning)
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
