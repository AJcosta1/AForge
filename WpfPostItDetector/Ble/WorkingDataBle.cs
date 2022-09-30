using SDKSmartTrainnerAdaptor.Ble.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;

namespace SDKSmartTrainnerAdaptor.Ble
{

    public static class Variables
    {
        /// <summary>
        /// Dictionary where are stored device read data
        /// </summary>
        public static Dictionary<string, byte[]> WorkingDataDictonaryByte = new Dictionary<string, byte[]>();

        /// <summary>
        /// Dictionary where are stored device trated data
        /// </summary>
        public static Dictionary<string, float> WorkingDataDictonaryTratedFloat = new Dictionary<string, float>();

        /// <summary>
        /// List with configuration of Devices, ex: service ,characteristic etc
        /// Initialize configuration
        /// </summary>
        public static List<DeviceDataList> ListaConfiguracaoDispositivos = new List<DeviceDataList>();
        public static BluetoothLEDevice ConnectedBluetoothLeDevice = null;
        public static Dictionary<BluetoothLEDeviceDisplay, DateTime> WorkingDataDictonaryLastUpdate = new Dictionary<BluetoothLEDeviceDisplay, DateTime>();

        public static ObservableCollection<BluetoothLEDeviceDisplay> DevicesDetected = new ObservableCollection<BluetoothLEDeviceDisplay>();

        public static SessonData sessonData { get; set; }

        public static SessonData SessonData
        {

            get
            {
                if (sessonData == null)
                    sessonData = new SessonData();
                return sessonData;
            }
        }

    }

    public partial class SessonData : INotifyPropertyChanged
    { 

        public ObservableCollection<Device> devicesConnected = new ObservableCollection<Device>();
        public ObservableCollection<Device> DevicesConnected
        {

            get { return devicesConnected; }
            set
            {
                devicesConnected = value;
                OnPropertyChanged("DevicesConnected");
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;




            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public T GetPropertyValue<T>(string property, object def = null)
        {
            if (Data.ContainsKey(property))
                return (T)Data[property];
            else
               if (def != null) return (T)def;

            return default(T);
        }

        public void SetPropertyValue(string property, object value)
        {
            if (Data.ContainsKey(property))
            {

                OnPropertyChanged(property);
                NotifyElementsUI(property);
            }



            Data[property] = value;
        }
            public void NotifyElementsUI(string property)
            {
                var x = this.GetType().GetProperty("UI" + property);
                if (x != null)
                {
                    OnPropertyChanged("UI" + property);

                    property = "UI" + property;
                }
            }

        public Dictionary<string, Object> Data = new Dictionary<string, Object>();
        public Dictionary<string, System.Reflection.PropertyInfo> DataProperty = new Dictionary<string, System.Reflection.PropertyInfo>();
        public Dictionary<string, string> DontExist = new Dictionary<string, string>();


    }
 

    public class DeviceDataList
    {


        public string service { get; set; }

        public string characteristic { get; set; }
        public bool read { get; set; }
        public string dataToWrite { get; set; }

    }


    public class Device : INotifyPropertyChanged
    {

        public BluetoothLEDevice device;
        public List<GattCharacteristic> Characteristics { get; set; } = new List<GattCharacteristic>();
        public BluetoothLEDeviceDisplay DeviceInformation { get; set; }


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

}
