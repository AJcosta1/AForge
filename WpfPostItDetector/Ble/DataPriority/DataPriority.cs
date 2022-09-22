using SDKSmartTrainnerAdaptor.GlobalLibs;
using System;
using System.Linq;
using SDKSmartTrainnerAdaptor.Ble;

namespace SDKSmartTrainnerAdaptor.Ble.DataPriority
{
    static partial class DataPriority
    {

        public static SessonData x = Variables.SessonData;

        public enum variableToRead
            {
            ReadTratedValue_1,
            ReadTratedValue_2,
            ReadTratedValue_3


        }


        public static void CheckData()
        {

            SpeedValue();
            CadenceValue();
            PowerValue();
            HeartRateValue();
        }

        public static float Read(string output,string characteristic, variableToRead variableToReadInput )
        {

            float outputValue = 0;
            if (WorkingDataBLE.WorkingDataDictonaryTratedFloat.Count > 0)
            {
             
                lock(WorkingDataBLE.WorkingDataDictonaryTratedFloat)
                {

                    var results =WorkingDataBLE.WorkingDataDictonaryTratedFloat.Where(p => p.Key.Contains('|')).ToList();
              
                    var results2 = results.Where(x => x.Key.Split('|')[1] == characteristic.ToUpper() && x.Key.Split('|')[2] == "_" + variableToReadInput.ToString());
            

                if (results2.Count() > 0)
                        foreach (var result in results2)
                            if (result.Value > outputValue) outputValue = result.Value;
                }
            }
              return outputValue;



        }

    }
}
