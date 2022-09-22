using System.BluetoothLe;
using System.Linq;
using System.Threading.Tasks;
using SDKSmartTrainnerAdaptor.Ble;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
    {

        public async Task updateAsync()
        {
            foreach (var device in WorkingDataBLE.Current.SessonData.DevicesDetected)
            {
                if (device.Device.State == DeviceState.Connected)
                    foreach (var characteristic in device.Characteristics.Where(x => !x.isUpdating).ToList())
                    {
                        var result = WorkingDataBLE.ListaConfiguracaoDispositivos.Where(x => x.characteristic.ToUpper() == characteristic.Characteristic.Id.ToString().ToUpper());

                        if (result.Count() > 0)
                        {

                            if (characteristic.Characteristic.CanUpdate)
                            {
                                Listen(characteristic.Characteristic);
                                characteristic.isUpdating = true;
                            }

                            else
                            {

                                if (characteristic.Characteristic.CanRead)
                                    await GetData(characteristic.Characteristic);

                                if (characteristic.Characteristic.CanWrite
                                    && WorkingDataBLE.ListaConfiguracaoDispositivos.Select(x => x.characteristic == characteristic.ToString() && x.read).Count() > 0)
                                    await WriteData(characteristic.Characteristic);
                            }


                        }
                    }

            }

        }
    }
}    
 