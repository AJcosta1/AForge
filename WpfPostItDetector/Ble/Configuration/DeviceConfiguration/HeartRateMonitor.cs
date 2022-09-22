using SDKSmartTrainnerAdaptor.Ble.UuidDictionary;

namespace SDKSmartTrainnerAdaptor.Ble.Configuration
{
    public static partial class BLEConfigurationInitialization
    {

        public static void HeartRateBandInicialization()
        {
        /// <summary>
        /// HeartRateBand
        /// </summary>
        /// 


        /// <summary>
        /// HeartRateBand
        /// </summary>
        _service = _Services.HeartRate; 
        _characteristic = _Characteristics.HeartRateMeasurement;
        addConfiguration();



        }



    }
}
