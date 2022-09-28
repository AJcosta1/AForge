using SDKSmartTrainnerAdaptor.Ble.Configuration;
using System;
using System.BluetoothLe;
using System.Collections.Generic;
using System.Linq; 

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
    {  
        Adapter adapter;
  
        public BLEMethods()
        {

            adapter = System.BluetoothLe.BluetoothLE.Current.Adapter;

            addEvents();

        }

        public async void scanNewDevices()
            {

                
                    //Get all necessary Found services

                    List<Guid> _GuidServiceList = new List<Guid>();

                    var results = WorkingDataBLE.ListaConfiguracaoDispositivos.Select(x => x.service).Distinct();


                    if (results.Count() > 0)
                    {

                        foreach (var result in results)
                            _GuidServiceList.Add(Guid.Parse(result));


                        Guid[] GuidServiceList = _GuidServiceList.ToArray();

                        adapter.ScanTimeout = BLEConfiguration.scanNewDevicesTime;
                                     
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

    
        }
