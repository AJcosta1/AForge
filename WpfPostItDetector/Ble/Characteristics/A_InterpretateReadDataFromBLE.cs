using SDKSmartTrainnerAdaptor;
using SDKSmartTrainnerAdaptor;
using System; 
using System.Collections.Generic;
using System.Linq;
using SDKSmartTrainnerAdaptor.Ble;

namespace SDKSmartTrainnerAdaptor.Ble.Characteristics
{
    public partial class CharacteristicsRead
    { 
        public static void InterpretateReadDataFromBLE()
        {

          
                heart_rate_measurement(); 
                TACX_FEC_WRITE_CHARACTERISTIC();
                csc_measurement();
                indoor_bike_data();
                cycling_power();

            }

            public static bool connected(string device)
            {
            BluetoothLEDeviceDisplay deviceFound = Variables.DevicesDetected.FirstOrDefault(d => (d.Id.ToUpper() == device.ToUpper()));

            if (deviceFound != null)
                //return deviceFound.IsConnected;
                return true;

            return false;
          }

        public static float Calc_Time_Based_Ble_Rotation(string deviceID, string characteristic, string name, byte[] readValue, int startValueByte, bool ValueUint16, int startTimeByte, bool TimeUint16)
            {

                float NewValue = 0;
                float NewTime = 0;


                float RollOverValue = 65536;
                float RoolOverTime = 65536;

                if (ValueUint16)
                    NewValue = (float)BitConverter.ToUInt16(readValue, startValueByte);
                else
                {
                    NewValue = (float)BitConverter.ToUInt32(readValue, startValueByte);
                    RollOverValue = 4294967295;
                }

                if (TimeUint16)
                    NewTime = (float)BitConverter.ToUInt16(readValue, startTimeByte);
                else
                {
                    NewTime = (float)BitConverter.ToUInt32(readValue, startTimeByte);
                    RoolOverTime = 4294967295;
                }

                string oldValueString = characteristic + "|" + deviceID + "|_" + name + "_Value_old";
                string oldTimeString = characteristic + "|" + deviceID + "|_" + name + "_Time_old";

                if (!Variables.WorkingDataDictonaryTratedFloat.ContainsKey(oldValueString))
                Variables.WorkingDataDictonaryTratedFloat[oldValueString] = NewValue;

                if (!Variables.WorkingDataDictonaryTratedFloat.ContainsKey(oldTimeString))
                Variables.WorkingDataDictonaryTratedFloat[oldTimeString] = NewTime;




                float OldValue = Variables.WorkingDataDictonaryTratedFloat[oldValueString];
                float OldTime = Variables.WorkingDataDictonaryTratedFloat[oldTimeString];

            Variables.WorkingDataDictonaryTratedFloat[oldValueString] = NewValue;
            Variables.WorkingDataDictonaryTratedFloat[oldTimeString] = NewTime;



                if (OldValue > NewValue)
                    NewValue += RollOverValue;
                if (OldTime > NewTime)
                    NewTime += RoolOverTime;


                float diffValue = NewValue - OldValue;
                float diffTime = NewTime - OldTime;

                float CalcValue = diffValue / diffTime * 1024 * 60; // to minute


                if (diffTime == 0) CalcValue = 0;


                if (CalcValue < 0)
                    CalcValue = 0;



                return CalcValue;
            }
        }
}
