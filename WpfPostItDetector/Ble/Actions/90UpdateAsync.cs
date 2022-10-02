using SDKSmartTrainnerAdaptor.Ble; 
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.GenericAttributeProfile;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
    {

        public async Task updateAsync()
        {
            foreach (var device in Variables.SessonData.DevicesConnected.ToList())
            {
               //if (device.DeviceInformation.IsConnected || device.DeviceInformation.IsPaired)
                    foreach (var characteristic in device.Characteristics.ToList())
                    {
                        var result = Variables.ListaConfiguracaoDispositivos.Where(x => x.characteristic.ToUpper() == characteristic.Uuid.ToString().ToUpper());

                        if (result.Count() > 0)
                        {
                         
                            GattCharacteristicProperties properties = characteristic.CharacteristicProperties;

                            if (properties.HasFlag(GattCharacteristicProperties.Notify))
                            {
                                var id = device.DeviceInformation.Id.ToString() + characteristic.Uuid.ToString();


                                if (!isUpdating.ContainsKey(id))
                                {
                                    await Start.ble.ListenAsync(characteristic, device.DeviceInformation);
                                    isUpdating[id] = true;

                                }
                            }

                            else
                            {

                                if (properties.HasFlag(GattCharacteristicProperties.Read))
                                    await Start.ble.GetData(characteristic, device.DeviceInformation);

                                if (properties.HasFlag(GattCharacteristicProperties.Write)
                                    && Variables.ListaConfiguracaoDispositivos.Select(x => x.characteristic == characteristic.ToString() && x.read).Count() > 0)
                                    await Start.ble.WriteData(characteristic, device.DeviceInformation);
                            }
                        }
                    }

            }

        }
    }
}
