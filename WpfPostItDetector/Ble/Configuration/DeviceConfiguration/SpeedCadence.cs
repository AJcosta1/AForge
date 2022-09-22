using SDKSmartTrainnerAdaptor.Ble.UuidDictionary;

namespace SDKSmartTrainnerAdaptor.Ble.Configuration
{
    public static partial class BLEConfigurationInitialization
    {

        public static void SpeedCadenceInicialization()
        {
            /// <summary>
            /// SpeedCadence
            /// </summary>
            /// 

            /// <summary>
            /// SpeedCadence
            /// </summary>
            _service = _Services.CyclingSpeedandCadence; 
            _characteristic = _Characteristics.CSCMeasurement;
            addConfiguration();


        }



    }
}
