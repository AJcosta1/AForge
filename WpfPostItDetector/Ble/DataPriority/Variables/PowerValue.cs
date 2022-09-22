using SDKSmartTrainnerAdaptor.Ble.UuidDictionary;
using SDKSmartTrainnerAdaptor;

namespace SDKSmartTrainnerAdaptor.Ble.DataPriority
{
    static partial class DataPriority
    {

        public static void PowerValue()
        {
            //static
            string characteristic = _Characteristics.CyclingPowerMeasurement;
            string output = nameof(x.Power);
            variableToRead _variableToRead = variableToRead.ReadTratedValue_1;

            //variable 
            Start.rootClass.Power = Read(output, characteristic, _variableToRead);

            

        }

    }
}
