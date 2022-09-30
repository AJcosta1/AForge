
using SDKSmartTrainnerAdaptor; 
using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
    {
        public async Task WriteData(GattCharacteristic characteristic, BluetoothLEDeviceDisplay device)
        {
            try
            {
                for (int i = 1; i <= 3; i++)
                {
                    string tag = characteristic.Uuid.ToString().ToUpper() + "|_WriteTratedValue_" + i.ToString();

                    if (Variables.WorkingDataDictonaryByte.ContainsKey(tag))
                    {
                        if (Variables.WorkingDataDictonaryByte.ContainsKey(tag + "_last"))
                        {
                            if (Variables.WorkingDataDictonaryByte[tag] != Variables.WorkingDataDictonaryByte[tag + "_last"])
                            {
                                await characteristic.WriteValueAsync(Variables.WorkingDataDictonaryByte[tag].AsBuffer());
                            }
                        }
                        else
                        {
                            await characteristic.WriteValueAsync(Variables.WorkingDataDictonaryByte[tag].AsBuffer());
                        }
                        Variables.WorkingDataDictonaryByte[tag + "_last"] = Variables.WorkingDataDictonaryByte[tag];
                        Variables.WorkingDataDictonaryLastUpdate[device] = DateTime.Now;
                    }
                }
            }
            catch (Exception e)
            {

            }
        }


    }
}