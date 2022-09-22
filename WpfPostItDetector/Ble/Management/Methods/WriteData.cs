
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
        public async Task WriteData(ICharacteristic characteristic)
        {
            try
            {
                for (int i = 1; i <= 3; i++)
                {
                string tag = characteristic.Id.ToString().ToUpper() + "|_WriteTratedValue_"+i.ToString();

                if (WorkingDataBLE.WorkingDataDictonaryByte.ContainsKey(tag))
                {
                    if (WorkingDataBLE.WorkingDataDictonaryByte.ContainsKey(tag + "_last"))
                    {
                        if (WorkingDataBLE.WorkingDataDictonaryByte[tag] != WorkingDataBLE.WorkingDataDictonaryByte[tag + "_last"])
                        {
                            await characteristic.WriteAsync(WorkingDataBLE.WorkingDataDictonaryByte[tag]);
                        }
                    }
                    else
                    {
                        await characteristic.WriteAsync(WorkingDataBLE.WorkingDataDictonaryByte[tag]);
                    }
                    WorkingDataBLE.WorkingDataDictonaryByte[tag + "_last"] = WorkingDataBLE.WorkingDataDictonaryByte[tag];
                    WorkingDataBLE.WorkingDataDictonaryLastUpdate[characteristic.Service.Device] = DateTime.Now;
                }
                }
            }
            catch (DeviceConnectionException e)
            {

            }
        }
    


    }
}