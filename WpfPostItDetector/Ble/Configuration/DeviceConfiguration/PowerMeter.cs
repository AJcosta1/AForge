using SDKSmartTrainnerAdaptor.Ble.UuidDictionary;

namespace SDKSmartTrainnerAdaptor.Ble.Configuration
{
    public static partial class BLEConfigurationInitialization
    {

        public static void PowerMeterInicialization()
        {
            /// <summary>
            /// PowerMeter
            /// </summary>
            /// 


            /// <summary>
            /// PowerMeter
            /// </summary>
            _service = _Services.CyclingPower;
            _characteristic = _Characteristics.CyclingPowerMeasurement;
            addConfiguration();



        }



    }
}
