using SDKSmartTrainnerAdaptor.Ble.Configuration;
using System;
using System.BluetoothLe;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKSmartTrainnerAdaptor.Ble
{
    public partial class SessonDataBLE : INotifyPropertyChanged
    {
        public ObservableCollection<DevicesDetected> devicesConected = new ObservableCollection<DevicesDetected>();

        public ObservableCollection<DevicesDetected> devicesDetected = new ObservableCollection<DevicesDetected>();


        public ObservableCollection<DevicesDetected> DevicesDetected
        {
            get
            {
                if (devicesDetected.Count() == 0)
                {
                    DevicesDetectedState = "No Devices Found, turn on Bluetooth in settings and turn on peripherals devices then search again... Note: in early Android Devices to be able to find devices turn on GPS";
                }
                else
                {
                    DevicesDetectedState = "";
                }

                return devicesDetected;
            }
            set
            {
                devicesDetected = value;
                OnPropertyChanged("DevicesDetected");
            }
        }

        public string devicesDetectedState { get; set; }


        public string DevicesDetectedState
        {
            get
            {

                return devicesDetectedState;
            }
            set
            {
                devicesDetectedState = value;
                OnPropertyChanged("DevicesDetectedState");
            }
        }

        public void UpdateListDeviceDetected()
        {
            

            foreach (var d in DevicesDetected.Distinct().ToList())
            {
                d.Name = d.Device.Name;
                d.State = d.Device.State.ToString();

            }
        }
    }

    public class DevicesDetected : INotifyPropertyChanged
    {
        public Device Device;
        public List<DevicesCharacteristics> Characteristics { get; set; } = new List<DevicesCharacteristics>();
        public List<Service> Services { get; set; } = new List<Service>();

        public bool isCompatible = true;

        public string name { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
                OnPropertyChanged("Text");
            }
        }

        public string state { get; set; }
        public string State
        {
            get { return state; }
            set
            {
                state = value;
                OnPropertyChanged("State");
                OnPropertyChanged("Text");
            }
        }

        public string Id { get; set; }
        public string Bat { get; set; }
        public string Text
        {

            get
            {

                return Name + " - " + State + Bat;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;


        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
    public class DevicesCharacteristics
    {
        public Characteristic Characteristic { get; set; }
        public bool isUpdating { get; set; }
    }

      public class WorkingDataBLE
    {
        // Singleton
        public static WorkingDataBLE Current = new WorkingDataBLE();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public SessonDataBLE sessonData { get; set; } = new SessonDataBLE();

        public SessonDataBLE SessonData
        {
            get { return sessonData; }
            set
            {
                sessonData = value;

                OnPropertyChanged("SessonData");

            }
        }
        /// <summary>
        /// Data to write
        /// </summary>
        public static byte[] TurboTrainnerLoad = null;
        public static byte[] defaultWriting = null;

        /// <summary>
        /// Dictionary where are stored device read data
        /// </summary>
        public static Dictionary<string, byte[]> WorkingDataDictonaryByte = new Dictionary<string, byte[]>();
        public static Dictionary<Device, DateTime> WorkingDataDictonaryLastUpdate = new Dictionary<Device, DateTime>();
        public static Dictionary<string, string> WorkingDataDictonaryString = new Dictionary<string, string>();
        public static Dictionary<string, bool> WorkingDataDictonaryBool = new Dictionary<string, bool>();
             
        public static Dictionary<string, Service> WorkingDataDictonaryService = new Dictionary<string, Service>();
        public static Dictionary<string, Service> WorkingDataDictonaryServiceAvaliable = new Dictionary<string, Service>();
        public static Dictionary<string, Characteristic> WorkingDataDictonaryCharacteristic = new Dictionary<string, Characteristic>();

        /// <summary>
        /// Dictionary where are stored device trated data
        /// </summary>
        public static Dictionary<string, float> WorkingDataDictonaryTratedFloat = new Dictionary<string, float>(); 

        /// <summary>
        /// List with configuration of Devices, ex: service ,characteristic etc
        /// Initialize configuration
        /// </summary>
        public static List<DeviceDataList> ListaConfiguracaoDispositivos = new List<DeviceDataList>();

 

    }

    public class DeviceDataList
    {

      
        public string service { get; set; }

        public string characteristic { get; set; }
        public bool read { get; set; }
        public string dataToWrite { get; set; }
     
    }
 
    public partial class SessonDataBLE : INotifyPropertyChanged
    {

        public SessonDataBLE()
        {


        }


        public int[] _selectedPosition { get; set; } = { 0, 0 };

        public int[] selectedPosition
        {
            get { return _selectedPosition; }
            set { _selectedPosition = value; }
        }







        public event PropertyChangedEventHandler PropertyChanged;


        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Dictionary<string, Object> Data = new Dictionary<string, Object>();
        public Dictionary<string, System.Reflection.PropertyInfo> DataProperty = new Dictionary<string, System.Reflection.PropertyInfo>();
        public Dictionary<string, string> DontExist = new Dictionary<string, string>();

   

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

                OnPropertyChanged(property);
            OnPropertyChanged(property+"_Color");


            Data[property] = value;

        }

     

        public void NotifySubElementsUI(string pos, string property, string prefix)
        {

            pos += prefix;
            property += prefix;

            if (!DataProperty.ContainsKey(pos)) DataProperty[pos] = this.GetType().GetProperty(pos);
            if (!DataProperty.ContainsKey(property)) DataProperty[property] = this.GetType().GetProperty(property);


            var target = DataProperty[pos];
            var source = DataProperty[property];

            if (target != null && source != null)
            {

                target.SetValue(this, source.GetValue(this, null), null);
            }
        }
 
    }

}
