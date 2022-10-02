using SDKSmartTrainnerAdaptor.Ble.UuidDictionary;
using SDKSmartTrainnerAdaptor;
using System;

namespace SDKSmartTrainnerAdaptor.Ble.DataPriority
{
    static partial class DataPriority
    {

        public static void SpeedValue()
        {

            //static
            string characteristic = _Characteristics.CSCMeasurement;
            string output = nameof(x.SpeedRpm);
            variableToRead _variableToRead = variableToRead.ReadTratedValue_1;


            //variable
            var temp1 =  Read(output,  characteristic, _variableToRead);

            //static
            characteristic = _Characteristics.IndoorBikeData;

            //variable
            var temp2 = Read(output, characteristic, _variableToRead);


            //variable for debug
            var temp3 = (float)Start.rootClass.DebugSpeed;
            temp2= Math.Max(temp2, temp3);


            Start.rootClass.SpeedRpm = Math.Max(temp1, temp2);
 

        }

    }
}
