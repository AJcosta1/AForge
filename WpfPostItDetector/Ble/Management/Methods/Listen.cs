
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

        public void Listen(ICharacteristic characteristic)
        {
 

        characteristic.ValueUpdated -= (o, args) => { };

        Thread.Sleep(1000);

     
        characteristic.ValueUpdated += (o, args) =>
        {
            WorkingDataBLE.WorkingDataDictonaryByte[args.Characteristic.Service.Device.Id.ToString().ToUpper() + "|" + args.Characteristic.Id.ToString().ToUpper()] = args.Characteristic.Value;

            WorkingDataBLE.WorkingDataDictonaryLastUpdate[args.Characteristic.Service.Device] = DateTime.Now;
        };
 
        Thread.Sleep(1000);
        characteristic.StartUpdatesAsync();
   
            



        }

      
    }
}