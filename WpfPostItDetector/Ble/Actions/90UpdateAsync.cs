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
            foreach (var device in adapter.ConnectedDevices.ToList())
            {
                var characteristics = WorkingDataBLE.Current.SessonData.DevicesDetected.First(d => d.Device.Id == device.Id).Characteristics;

                if (characteristics.Count == 0)
                    ScanServicesCharacteristics(device);


                if (device.State == DeviceState.Connected)
                {

                    var results = WorkingDataBLE.ListaConfiguracaoDispositivos.Select(x => x.characteristic).Distinct();

                    foreach (var characteristic in characteristics.Where(x => !x.isUpdating && results.Contains(x.Characteristic.Id.ToString().ToUpper())).ToList())
                    {
                        var result = WorkingDataBLE.ListaConfiguracaoDispositivos.Where(x => x.characteristic.ToUpper() == characteristic.Characteristic.Id.ToString().ToUpper());

                        if (result.Count() > 0)
                        {
                            if (characteristic.Characteristic.CanUpdate)
                            {
                                await ListenAsync(characteristic.Characteristic);
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
}    
 