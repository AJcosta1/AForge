using SDKSmartTrainnerAdaptor.Ble.UuidDictionary;
using SDKSmartTrainnerAdaptor.GlobalLibs;
using System;

namespace SDKSmartTrainnerAdaptor.Ble.DataPriority
{
    static partial class DataPriority
    {

        public static void CadenceValue()
        {
            //static
            string characteristic = _Characteristics.CSCMeasurement;
            string output = nameof(x.Cadence);
            variableToRead _variableToRead = variableToRead.ReadTratedValue_2;


   
            //variable
            var temp1 = Read(output, characteristic, _variableToRead);

            //static
            characteristic = _Characteristics.CyclingPowerMeasurement;
            _variableToRead = variableToRead.ReadTratedValue_2;

            //variable
            var temp2 = Read(output, characteristic, _variableToRead);

          
                Variables.SessonData.Cadence = Math.Max(temp1, temp2);

           

        }

    }
}
