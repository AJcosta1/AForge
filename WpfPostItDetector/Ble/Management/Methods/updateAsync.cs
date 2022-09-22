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
using System.Linq;

namespace IndoorCycling.Libs.BLE.Management
{
    public partial class BLEManagement
    {
        
       
 
        public async Task updateAsync()
        {


            foreach (var device in WorkingDataBLE.Current.SessonData.DevicesDetected)
            {
                if(device.Device.State== DeviceState.Connected)
                foreach (var characteristic in device.Characteristics.Where(x=>!x.isUpdating).ToList())
                {
                    var result = WorkingDataBLE.ListaConfiguracaoDispositivos.Where(x => x.characteristic.ToUpper() == characteristic.Characteristic.Id.ToString().ToUpper());

                    if (result.Count()>0)
                    {

                            if (characteristic.Characteristic.CanUpdate)
                            { 
                              ble.Listen(characteristic.Characteristic);
                                characteristic.isUpdating = true;
                            }
                             
                            else
                            {

                                if (characteristic.Characteristic.CanRead)
                                    await ble.GetData(characteristic.Characteristic);

                                if (characteristic.Characteristic.CanWrite
                                    && WorkingDataBLE.ListaConfiguracaoDispositivos.Select(x => x.characteristic == characteristic.ToString() && x.read).Count() > 0)
                                        await ble.WriteData(characteristic.Characteristic);
                            }


                        }
                    }

            }
            /*
            
            foreach (var ListaConfiguracaoDispositivos in WorkingDataBLE.ListaConfiguracaoDispositivos.Where(p => p.lowPriority == lowPriority))
            {
 
                    if (_device != null)
                    {
                        switch (_device.State)
                        {
                            case DeviceState.Disconnected:
             
                            break;
                            case DeviceState.Connected:
                      
                            if (!ListaConfiguracaoDispositivos.updating)
                                {
                                    if (ListaConfiguracaoDispositivos.read)

                                        
                                    else
                                                                    }
                                else
                                {
                                    TimeSpan ts = DateTime.Now - WorkingDataBLE.WorkingDataDictonaryLastUpdate[_device];

                                    if (Convert.ToInt32(ts.TotalSeconds) > BLEConfiguration.datedRead)
                                    {
                                        
                                        ble.disconnectDevice(_device);
                                        WorkingDataBLE.WorkingDataDictonaryLastUpdate[_device] = DateTime.Now;
                                    }
                                }
                                break;
*/
                        }                   
                }     
            }    
 