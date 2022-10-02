using SDKSmartTrainnerAdaptor;
using SDKSmartTrainnerAdaptor.Ble.UuidDictionary; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{

    public partial class BLEMethods
    {

        public Dictionary<BluetoothLEDeviceDisplay, bool> compatibleList = new Dictionary<BluetoothLEDeviceDisplay, bool>();


        public async Task ScanServicesCharacteristic(BluetoothLEDeviceDisplay deviceInf, BluetoothLEDevice device)
        { 
            GattDeviceServicesResult resultServices = await device.GetGattServicesAsync();

            if (resultServices.Status == GattCommunicationStatus.Success)
            {
                if (isCompatible(resultServices.Services))
                {

                    Device _device = FindDevice(deviceInf.Id);

                    if (_device == null)
                    {
                        _device = new Device()
                        {
                            device = device,
                            DeviceInformation = deviceInf
                        };

                      
                    }

                    if (!_device.DeviceInformation.IsPaired)
                        Pair(_device.DeviceInformation);

                    foreach (var service in resultServices.Services)
                    { 
                        GattCharacteristicsResult resultCharacteristics = await service.GetCharacteristicsAsync();

                        if (resultCharacteristics.Status == GattCommunicationStatus.Success)
                        {
                            foreach (var Characteristics in resultCharacteristics.Characteristics.ToList())
                            {
                                _device.Characteristics.Add(Characteristics);
                            }         

                        }

                    }

                    Variables.SessonData.DevicesConnected.Add(_device);
                    SDKSmartTrainnerAdaptor.Ble.Start.rootClass.loggerAdd("Connected: " + device.Name);
                }
                else
                {   
                    compatibleList[deviceInf] = false;
                    disconnectDevice(device);
                }
            }
        }

        public bool isCompatible(IReadOnlyList<GattDeviceService> serviceList)
        {

            foreach (var service in serviceList)
            {
                if (Variables.ListaConfiguracaoDispositivos.Find(d => d.service == service.Uuid.ToString().ToUpper()) != null)
                    return true;
            }

            return false;
        }

        private Device FindDevice(string id)
        {
            foreach (Device bleDeviceDisplay in Variables.SessonData.DevicesConnected)
            {
                if (bleDeviceDisplay.DeviceInformation.Id == id)
                {
                    return bleDeviceDisplay;
                }
            }
            return null;
        }

    }


}