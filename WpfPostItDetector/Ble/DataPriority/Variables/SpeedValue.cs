using SDKSmartTrainnerAdaptor.Ble.UuidDictionary;
using SDKSmartTrainnerAdaptor.GlobalLibs;
using System;

namespace SDKSmartTrainnerAdaptor.Ble.DataPriority
{
    static partial class DataPriority
    {

        public static void SpeedValue()
        {

            //static
            string characteristic = _Characteristics.CSCMeasurement;
            string output = nameof(x.Speed_ms);
            variableToRead _variableToRead = variableToRead.ReadTratedValue_1;


            //variable
            var temp1 =  Read(output,  characteristic, _variableToRead);

            //static
            characteristic = _Characteristics.IndoorBikeData;

            //variable
            var temp2 = Read(output, characteristic, _variableToRead);


            //variable for debug
            var temp3 = 300;
            temp2= Math.Max(temp2, temp3);


            Variables.SessonData.Speed_ms = Math.Max(temp1, temp2);
 

        }

    }
}
