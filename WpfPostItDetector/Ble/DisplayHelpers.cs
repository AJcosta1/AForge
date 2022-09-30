using SDKSmartTrainnerAdaptor.Ble.Actions;
using SDKSmartTrainnerAdaptor.Ble.Characteristics;
using SDKSmartTrainnerAdaptor.Ble.Configuration;
using SDKSmartTrainnerAdaptor.Ble.DataPriority;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Storage.Streams;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Threading;

namespace SDKSmartTrainnerAdaptor.Ble
{
 
    /// <summary>
    ///     Display class used to represent a BluetoothLEDevice in the Device list
    /// </summary>
    public class BluetoothLEDeviceDisplay : INotifyPropertyChanged
    {
        public BluetoothLEDeviceDisplay(DeviceInformation deviceInfoIn)
        {
            DeviceInformation = deviceInfoIn; 
        }

        public DeviceInformation DeviceInformation { get; private set; }

        public string Id => DeviceInformation.Id;
        public string Name => DeviceInformation.Name;
        public bool IsPaired => DeviceInformation.Pairing.IsPaired;
        public bool IsConnected => (bool?)DeviceInformation.Properties["System.Devices.Aep.IsConnected"] == true;
        public bool IsConnectable => (bool?)DeviceInformation.Properties["System.Devices.Aep.Bluetooth.Le.IsConnectable"] == true;

        public IReadOnlyDictionary<string, object> Properties => DeviceInformation.Properties;

        

        public event PropertyChangedEventHandler PropertyChanged;

        public void Update(DeviceInformationUpdate deviceInfoUpdate)
        {
            DeviceInformation.Update(deviceInfoUpdate);

            OnPropertyChanged("Id");
            OnPropertyChanged("Name");
            OnPropertyChanged("DeviceInformation");
            OnPropertyChanged("IsPaired");
            OnPropertyChanged("IsConnected");
            OnPropertyChanged("Properties");
            OnPropertyChanged("IsConnectable");
 
        }

    

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public static class Utilities
    {
        /// <summary>
        ///     Converts from standard 128bit UUID to the assigned 32bit UUIDs. Makes it easy to compare services
        ///     that devices expose to the standard list.
        /// </summary>
        /// <param name="uuid">UUID to convert to 32 bit</param>
        /// <returns></returns>
        public static ushort ConvertUuidToShortId(Guid uuid)
        {
            // Get the short Uuid
            var bytes = uuid.ToByteArray();
            var shortUuid = (ushort)(bytes[0] | (bytes[1] << 8));
            return shortUuid;
        }

        /// <summary>
        ///     Converts from a buffer to a properly sized byte array
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] ReadBufferToBytes(IBuffer buffer)
        {
            var dataLength = buffer.Length;
            var data = new byte[dataLength];
            using (var reader = DataReader.FromBuffer(buffer))
            {
                reader.ReadBytes(data);
            }
            return data;
        }
    }
} 
