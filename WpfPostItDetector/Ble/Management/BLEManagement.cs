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

namespace IndoorCycling.Libs.BLE.Management
{
    public partial class BLEManagement
    {
        
        public BLEMetods ble = new BLEMetods();
        IDevice _device = null;


        public BLEManagement()
        {
            startService();
            WorkingDataGlobal.Current.SessonData.BLEMethodsInstance = this;
        }

        public void startService()
        {
            startScan();

            Device.StartTimer(TimeSpan.FromMilliseconds(BLEConfiguration.updateConnections), () =>
            {   
                
                ble.updateConnections();

                return true; // True = Repeat again, False = Stop the timer
            });

            Device.StartTimer(TimeSpan.FromMilliseconds(BLEConfiguration.updateTime), () =>
            {

                updateAsync();

                return true; // True = Repeat again, False = Stop the timer
            });

         
        }
 
        public void startScan()
        {

            ble.scanNewDevices();
        
        }




    }
}
