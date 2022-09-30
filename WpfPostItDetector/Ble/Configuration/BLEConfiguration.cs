using SDKSmartTrainnerAdaptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKSmartTrainnerAdaptor.Ble;

namespace SDKSmartTrainnerAdaptor.Ble.Configuration
{
    public static class BLEConfiguration
    {
        /// <summary>
        /// BLE Device scan time in millisencons
        /// </summary>
        public const int scanNewDevicesTime = 30000;
        public const int scanNewDevicesTimout = 10000; 

        /// <summary>
        /// BLE Device scan time in millisencons
        /// </summary>
        public const int updateConnections = 3000;


        /// <summary>
        /// Cycle time to update data
        /// </summary>
        public const int updateTime = 500;

        /// <summary>
        /// Cycle time to update data low priority
        /// </summary>
        public const int updateTimeLowPriority = 10000;

        /// <summary>
        /// Time considerate a read as dated in seconds
        /// </summary>
        public const int datedRead = 20;

    }

    public static partial class BLEConfigurationInitialization
    {

        /// <summary>
        /// Device Configuration service characteristic etc
        /// </summary>
        public static string _service = "";
        public static string _characteristic = "";
        public static bool _read = true;



        //data to write
        public static string _WriteTratedValue_1 = "_WriteTratedValue_1";
        public static string _WriteTratedValue_2 = "_WriteTratedValue_2";
        public static string _WriteTratedValue_3 = "_WriteTratedValue_3";
        public static string _WriteTratedValue_4 = "_WriteTratedValue_4";


        public static string _dataToWrite = "_WriteTratedValue_1";



        public static void StartBLEConfigurationInitialization()
        {
            SmartTrainnerInicialization();
            HeartRateBandInicialization(); 
            SpeedCadenceInicialization();
            PowerMeterInicialization(); 

        }

        public static void addConfiguration()
            {
                Variables.ListaConfiguracaoDispositivos.Add(new DeviceDataList() { service = _service.ToUpper(), characteristic = _characteristic.ToUpper(), read = _read, dataToWrite = _dataToWrite });
                resetVariables();
            }

            public static void resetVariables()
            {

                _read = true;
                _dataToWrite = _WriteTratedValue_1;
            }
        }
    }
