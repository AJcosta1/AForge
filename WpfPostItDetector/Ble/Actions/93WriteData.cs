using System;
using System.BluetoothLe;
using System.Threading.Tasks;
using SDKSmartTrainnerAdaptor.Ble;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
    {
        public async Task WriteData(Characteristic characteristic)
        {
            try
            {
                for (int i = 1; i <= 3; i++)
                {
                    string tag = characteristic.Id.ToString().ToUpper() + "|_WriteTratedValue_" + i.ToString();

                    if (WorkingDataBLE.WorkingDataDictonaryByte.ContainsKey(tag))
                    {
                        if (WorkingDataBLE.WorkingDataDictonaryByte.ContainsKey(tag + "_last"))
                        {
                            if (WorkingDataBLE.WorkingDataDictonaryByte[tag] != WorkingDataBLE.WorkingDataDictonaryByte[tag + "_last"])
                            {
                                await characteristic.WriteAsync(WorkingDataBLE.WorkingDataDictonaryByte[tag]);
                            }
                        }
                        else
                        {
                            await characteristic.WriteAsync(WorkingDataBLE.WorkingDataDictonaryByte[tag]);
                        }
                        WorkingDataBLE.WorkingDataDictonaryByte[tag + "_last"] = WorkingDataBLE.WorkingDataDictonaryByte[tag];
                        WorkingDataBLE.WorkingDataDictonaryLastUpdate[characteristic.Service.Device] = DateTime.Now;
                    }
                }
            }
            catch (DeviceConnectionException e)
            {
                
            }
        }



    }
}