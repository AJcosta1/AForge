using SDKSmartTrainnerAdaptor.Ble.UuidDictionary; 

namespace SDKSmartTrainnerAdaptor.Ble.DataPriority
{
    static partial class DataPriority
    {

        public static void HeartRateValue()
        {
            //static
            string characteristic = _Characteristics.HeartRateMeasurement.ToUpper();
            string output = nameof(x.HeartRateBPM);
            variableToRead _variableToRead = variableToRead.ReadTratedValue_1;



            //variable
            var temp1 = Read(output, characteristic, _variableToRead);

            Start.rootClass.HeartRateBPM = temp1;

           



        }

    }
}
