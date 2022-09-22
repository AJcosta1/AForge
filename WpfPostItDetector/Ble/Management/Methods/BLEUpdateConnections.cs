using System;
using System.Collections.Generic;
using System.Text;
using IndoorCycling.Libs.BLE;
using IndoorCycling.Abstractions.WorkingData.BLE;
using IndoorCycling.Abstractions.WorkingData.Global;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;
using IndoorCycling.Abstractions.Configurations.BLE;
using IndoorCycling.Abstractions.Enums.BLE;
using Plugin.BLE.Abstractions;
using System.Threading.Tasks;
using IndoorCycling.Abstractions.WorkingData.DataSavingManagement;
using System.Linq.Expressions;
using IndoorCycling.Abstractions.Configurations.Personal;
using System.Linq;
using System.Threading;

namespace IndoorCycling.Libs.BLE.Management
{
    public partial class BLEMetods
    {
        

        public async Task updateConnections()
        {       
 try
                {
            foreach (var device in WorkingDataBLE.Current.SessonData.DevicesDetected)
            {
               
                    if (device.Device != null)
                    {
                        switch (device.Device.State)
                        {
                            case DeviceState.Disconnected:

                       
                                await connectDevice(device);
                                break;
                            case DeviceState.Limited:
                               
                                disconnectDevice(device.Device);

                                foreach (var characteristic in device.Characteristics)
                                    characteristic.isUpdating = false;
                                Thread.Sleep(1000);
                                await connectDevice(device);

                                break;
                            case DeviceState.Connected:
                           
                                break;
                            case DeviceState.Connecting:

                                //Console.WriteLine("Connecting");
                                break;                      
                        }

                        WorkingDataBLE.Current.SessonData.UpdateListDeviceConnected();
                    }
            
            }   
            }
                catch
                { }
        }
    }
}
