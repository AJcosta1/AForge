using SDKSmartTrainnerAdaptorBle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTurboTrainnerAdaptorBle
{
    public partial class BLEConfigurationInitialization
    {
        public List<DeviceDataList> DeviceConfigurationList = new List<DeviceDataList>();

        string _service = "";
        string _characteristic = "";
        bool _read = false;
        string  _dataToWrite="";

        public BLEConfigurationInitialization()
        {
            SmartTrainnerInicialization();
        }


        public void SmartTrainnerInicialization()
        {
            /// <summary>
            /// SmartTrainner
            /// </summary>
            /// r;



            /// <summary>
            /// Tacx specifics
            /// </summary>
            _service = Services.TACX_VORTEX_PRIMARY_SERVICE;
            _characteristic = Characteristics.TACX_FEC_READ_CHARACTERISTIC;
            addConfiguration();

            _service = Services.FitnessMachine;
            _characteristic = Characteristics.IndoorBikeData;
            addConfiguration();

            /// <summary>
            /// Tacx specifics
            /// </summary>
            _service = Services.TACX_PRIMARY_SERVICE;
            _read = false;
            _characteristic = Characteristics.TACX_FEC_WRITE_CHARACTERISTIC;
            addConfiguration();

            /// <summary>
            /// write wheel diameter
            /// </summary>
            _service = Services.TACX_PRIMARY_SERVICE;
            _read = false;
            _characteristic = Characteristics.TACX_FEC_WRITE_CHARACTERISTIC;
            addConfiguration();


            /// <summary>
            /// Wahoo specifics
            /// </summary>
            _service = Services.CyclingPower;
            _read = false;
            _characteristic = Characteristics.WAHOO_BRAKE_CONTROL_UUID;
            addConfiguration();

            /// <summary>
            /// Wahoo specifics
            /// </summary>
            _service = Services.HeartRate;
            _read = false;
            _characteristic = Characteristics.WAHOO_BRAKE_CONTROL_UUID;
            addConfiguration();
        }


        public void addConfiguration()
        {
            DeviceConfigurationList.Add(new DeviceDataList() { service = _service.ToUpper(), characteristic = _characteristic.ToUpper(), read = _read, dataToWrite = _dataToWrite });
            resetVariables();
        }

        public void resetVariables()
        {

            var _read = true;
            _dataToWrite = "";
        }

    }
}
