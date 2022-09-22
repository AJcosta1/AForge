using SDKSmartTrainnerAdaptor.Ble.UuidDictionary;
using SDKSmartTrainnerAdaptor.GlobalLibs;

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
            Variables.SessonData.Power = Read(output, characteristic, _variableToRead);

            

        }

    }
}
