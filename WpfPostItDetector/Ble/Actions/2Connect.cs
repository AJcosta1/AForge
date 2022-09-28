using System;
using System.BluetoothLe;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SDKSmartTrainnerAdaptor.Ble;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
    {

        public async Task connectDevices()
        {
            foreach (var device in adapter.DiscoveredDevices.Distinct().ToList())
            {
                var _device = WorkingDataBLE.Current.SessonData.DevicesDetected.First(d => d.Device.Id == device.Id);

                if (isCompatible(device) && _device.isCompatible)
                {
                    if (device.State != DeviceState.Connected) {
                        if (adapter.ConnectedDevices.FirstOrDefault(x => x.Id == device.Id) == null)
                        {

                            if (!WorkingDataBLE.WorkingDataDictonaryLastUpdate.ContainsKey(device))
                                WorkingDataBLE.WorkingDataDictonaryLastUpdate[device] = DateTime.Now;


                            TimeSpan ts = DateTime.Now - WorkingDataBLE.WorkingDataDictonaryLastUpdate[device];

                            if (Convert.ToInt32(ts.TotalSeconds) > 5)
                            {
                                try {

                                    adapter.ConnectToDeviceAsync(device, new ConnectParameters(autoConnect: true, forceBleTransport: true));

                                    WorkingDataBLE.WorkingDataDictonaryLastUpdate[device] = DateTime.Now;

                                }
                                catch (DeviceConnectionException e)
                                {
                                }

                            }
                        }
                    }
                    else if (device.State == DeviceState.Limited)
                    {
                        clearCharacteristics(device);
                    }
                }
            }
        }

        public bool isCompatible(Device device)
        {
            var characteristics = WorkingDataBLE.Current.SessonData.DevicesDetected.First(d => d.Device.Id == device.Id).Characteristics;

            if (characteristics.Count == 0)
            {
                return true;
            }

            var results = WorkingDataBLE.ListaConfiguracaoDispositivos.Select(x => x.characteristic.ToUpper()).Distinct();

            foreach (var _characteristic in characteristics.ToList())
            {
                if (results.Contains(_characteristic.Characteristic.Id.ToString().ToUpper()))
                    return true;

            }

            return false;
        }
    }
}