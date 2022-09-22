using SDKSmartTrainnerAdaptor.Ble.UuidDictionary;

namespace SDKSmartTrainnerAdaptor.Ble.Configuration
{
    public static partial class BLEConfigurationInitialization
    {

        public static void SmartTrainnerInicialization()
        {
            /// <summary>
            /// SmartTrainner
            /// </summary>
            /// r;



            /// <summary>
            /// Tacx specifics
            /// </summary>
            _service = _Services.TACX_VORTEX_PRIMARY_SERVICE;
            _characteristic = _Characteristics.TACX_FEC_READ_CHARACTERISTIC;
            addConfiguration();

            _service = _Services.FitnessMachine;
            _characteristic = _Characteristics.IndoorBikeData;
            addConfiguration();

            /// <summary>
            /// Tacx specifics
            /// </summary>
            _service = _Services.TACX_PRIMARY_SERVICE;
            _read = false;
            _characteristic = _Characteristics.TACX_FEC_WRITE_CHARACTERISTIC;
            addConfiguration();

            /// <summary>
            /// write wheel diameter
            /// </summary>
            _service = _Services.TACX_PRIMARY_SERVICE;
            _read = false;
            _characteristic = _Characteristics.TACX_FEC_WRITE_CHARACTERISTIC;
            addConfiguration();
            

            /// <summary>
            /// Wahoo specifics
            /// </summary>
            _service = _Services.CyclingPower;
            _read = false;
            _characteristic = _Characteristics.WAHOO_BRAKE_CONTROL_UUID;
            addConfiguration();


        }



    }
}
