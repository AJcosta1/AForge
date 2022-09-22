using System;
using System.BluetoothLe;
using System.Threading.Tasks;
using SDKSmartTrainnerAdaptor.Ble;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
    {

        public async Task<Device> connectDevice(DevicesDetected _device)
        {

            try
            {
                if (!isScanning())
                {

                    if (WorkingDataBLE.WorkingDataDictonaryLastUpdate.ContainsKey(_device.Device))
                    {
                        TimeSpan ts = DateTime.Now - WorkingDataBLE.WorkingDataDictonaryLastUpdate[_device.Device];

                        if (Convert.ToInt32(ts.TotalSeconds) > 10)
                        {
                            WorkingDataBLE.WorkingDataDictonaryLastUpdate[_device.Device] = DateTime.Now;



                            await adapter.ConnectToDeviceAsync(_device.Device);



                        }
                    }
                    else
                    {

                        WorkingDataBLE.WorkingDataDictonaryLastUpdate[_device.Device] = DateTime.Now;

                        await adapter.ConnectToDeviceAsync(_device.Device);
                    }

                    await ScanServicesCharacteristics(_device);


                }

            }
            catch (DeviceConnectionException e)
            {
                /* // ... could not connect to device
                 foreach (var ListaConfiguracaoDispositivos in WorkingDataBLE.ListaConfiguracaoDispositivos.Where(p => p.type == deviceID))
                 {
                     name = ListaConfiguracaoDispositivos.nameFromSupplier;
                     if (name == ListaConfiguracaoDispositivos.type) ;
                 }

                 WorkingDataEvent.ListaEventosGerados.Add(new Abstractions.WorkingData.Event.Part() { description = FormVariableNames.EventCloudNotConnectToDeviceBLE + " " + name, data = DateTime.UtcNow.ToString("HH:mm:ss", CultureInfo.InvariantCulture) });
             */
            }
            return _device.Device;
        }

      

    }
}