using IndoorCycling.Abstractions.Configurations.BLE;
using IndoorCycling.Abstractions.WorkingData.BLE;
using IndoorCycling.Abstractions.WorkingData.Global;
using Plugin.BLE.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace IndoorCycling.Libs.BLE.ReceivedDataProcessing.Characteristics
{
    public partial class CharacteristicsRead
    {
        public static void StartServices()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(BLEConfiguration.updateTime), () =>
            {

                InterpretateReadDataFromBLE();

                return true; // True = Repeat again, False = Stop the timer
            });
        }
     
        public static void InterpretateReadDataFromBLE()
        {
            Dictionary<string, byte[]> WorkingDataDictonaryByte_temp = new Dictionary<string, byte[]>();

            lock (WorkingDataBLE.WorkingDataDictonaryByte)
                {  
                WorkingDataDictonaryByte_temp = WorkingDataBLE.WorkingDataDictonaryByte.ToDictionary(x => x.Key, x => x.Value);
            }
            

            heart_rate_measurement(WorkingDataDictonaryByte_temp);
            battery_level(WorkingDataDictonaryByte_temp);
            TACX_FEC_WRITE_CHARACTERISTIC(WorkingDataDictonaryByte_temp);
            csc_measurement(WorkingDataDictonaryByte_temp);
            indoor_bike_data(WorkingDataDictonaryByte_temp);
            cycling_power(WorkingDataDictonaryByte_temp);
            
        }

        public static bool connected(string device)
        {
            bool connected = false;

            if (device != "")
            {
                lock (WorkingDataBLE.Current.SessonData.DevicesDetected)
                {
                    if (WorkingDataBLE.Current.SessonData.DevicesDetected.Count() > 0)
                    {
                        try
                        {
                            var state = WorkingDataBLE.Current.SessonData.DevicesDetected.Where(p => p.Device.Id.ToString().ToUpper() == device).FirstOrDefault().Device.State;
                     

                        if (state == DeviceState.Connected)
                            connected = true;   
                        }
                      
                      catch
                        {

                        }
                    }
                }
            }
            return connected;
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
                RoolOverTime= 4294967295;
            }

            string oldValueString = characteristic + "|"+ deviceID + "|_" + name + "_Value_old";
            string oldTimeString = characteristic + "|" + deviceID + "|_" + name + "_Time_old";

            if (!WorkingDataBLE.WorkingDataDictonaryTratedFloat.ContainsKey(oldValueString))
                WorkingDataBLE.WorkingDataDictonaryTratedFloat[oldValueString] = NewValue;
            
            if (!WorkingDataBLE.WorkingDataDictonaryTratedFloat.ContainsKey(oldTimeString)) 
                WorkingDataBLE.WorkingDataDictonaryTratedFloat[oldTimeString] = NewTime;


            float OldValue = WorkingDataBLE.WorkingDataDictonaryTratedFloat[oldValueString];
            float OldTime = WorkingDataBLE.WorkingDataDictonaryTratedFloat[oldTimeString];


            WorkingDataBLE.WorkingDataDictonaryTratedFloat[oldValueString] = NewValue;
            WorkingDataBLE.WorkingDataDictonaryTratedFloat[oldTimeString] = NewTime;



            if (OldValue > NewValue) 
                NewValue += RollOverValue;
            if (OldTime > NewTime) 
                NewTime += RoolOverTime;


            float diffValue = NewValue - OldValue;
            float diffTime = NewTime - OldTime;

            float CalcValue = diffValue / diffTime * 1024*60; // to minute


            if (diffTime == 0) CalcValue = 0;

            
            if (CalcValue < 0)
                CalcValue=0;




            WorkingDataGlobal.Current.SessonData.Log.Add(new SessonData.Logs()
            {
                tag = oldValueString,
                Value=CalcValue

            });

            WorkingDataGlobal.Current.SessonData.Log.RemoveAll(p => p.tag == oldValueString && WorkingDataGlobal.Current.SessonData.TimeWatch_Ticks.TotalSeconds - p.Time.TotalSeconds > 3);

            CalcValue = (float)WorkingDataGlobal.Current.SessonData.Log.Where(p => p.tag == oldValueString && WorkingDataGlobal.Current.SessonData.TimeWatch_Ticks.TotalMilliseconds - p.Time.TotalMilliseconds < 2200).Max(x => x.Value);

            return CalcValue;
       }



    }
}
