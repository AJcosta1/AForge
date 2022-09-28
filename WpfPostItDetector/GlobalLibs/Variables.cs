using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace SDKSmartTrainnerAdaptor
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Aplication
        public double Slope
        {
            get { return GetPropertyValue<double>("Slope"); }
            set { SetPropertyValue("Slope", value); }
        }

        public String UISlope
        {
            get { return ("%= " + Slope); }
        }

        public double Power
        {
            get { return GetPropertyValue<double>("Power"); }
            set
            {
                SetPropertyValue("Power", value);
            }
        }
        public String UIPower
        {
            get { return ("Wtts= " + Power); }
        }

        public double HeartRateBPM
        {
            get { return GetPropertyValue<double>("HeartRateBPM"); }
            set { SetPropertyValue("HeartRateBPM", value); }
        }

        public String UIHeartRateBPM
        {
            get { return ("HR= " + HeartRateBPM); }
        }

        public double Speed_ms
        {
            get { return GetPropertyValue<double>("Speed_ms"); }
            set { SetPropertyValue("Speed_ms", value); }
        }

        public double Speed
        {
            get { return GetPropertyValue<double>("Speed_ms") * 3.6; }

        }

        public String UISpeed
        {
            get { return ("V= " + Speed); }
        }

        public double Acceleration
        {
            get { return GetPropertyValue<double>("Acceleration"); }
            set
            {
                SetPropertyValue("Acceleration", value);
            }
        }
        public String UIAcceleration
        {
            get { return ("A%= " + Acceleration); }
        }

        public double Brake
        {
            get { return GetPropertyValue<double>("Brake"); }
            set
            {
                SetPropertyValue("Brake", value);
            }
        }
        public String UIBrake
        {
            get { return ("B%= " + Brake); }
        }

        public double Direction
        {
            get { return GetPropertyValue<double>("Direction"); }
            set
            {
                SetPropertyValue("Direction", value);
            }
        }
        public String UIDirection
        {
            get { return ("G= " + Direction); }
        }

        public double Cadence
        {
            get { return GetPropertyValue<double>("Cadence"); }
            set { SetPropertyValue("Cadence", value); }
        }

        public double WheelPerimeter { get; set; } = 2175;


        #endregion



        public T GetPropertyValue<T>(string property, object def = null)
        {
            if (Data.ContainsKey(property))
                return (T)Data[property];
            else
               if (def != null) return (T)def;

            return default(T);
        }

        #region Motion Detector Related



        public ObservableCollection<FilterInfo> VideoDevices { get; set; }

        public FilterInfo CurrentDevice
        {
            get { return _currentDevice; }
            set { _currentDevice = value; this.OnPropertyChanged("CurrentDevice"); }
        }
        private FilterInfo _currentDevice;

        /// <summary>
        /// Displays the original image taken from the camera
        /// </summary>
        public bool Original
        {
            get { return _original; }
            set { _original = value; this.OnPropertyChanged("Original"); }
        }
        private bool _original;

        /// <summary>
        /// Displays the image taken from the camera with a grayscale filter applied
        /// </summary>
        public bool Grayscaled
        {
            get { return _grayscale; }
            set { _grayscale = value; this.OnPropertyChanged("Grayscaled"); }
        }
        private bool _grayscale;

        /// <summary>
        /// Displays the image taken from the camera with a threshold filter applied
        /// </summary>
        public bool Thresholded
        {
            get { return _thresholded; }
            set { _thresholded = value; this.OnPropertyChanged("Thresholded"); }
        }
        private bool _thresholded;

        /// <summary>
        /// Threshold of the thresholding filter
        /// </summary>
        public int Threshold
        {
            get { return _threshold; }
            set { _threshold = value; this.OnPropertyChanged("Threshold"); this.OnPropertyChanged("RGB"); }
        }
        private int _threshold;

        /// <summary>
        /// Color picker: red channel
        /// </summary>
        public int Red
        {
            get { return _red; }
            set { _red = value; this.OnPropertyChanged("Red"); this.OnPropertyChanged("RGB"); }
        }
        private int _red;

        /// <summary>
        /// Color picker: blue channel
        /// </summary>
        public int Blue
        {
            get { return _blue; }
            set { _blue = value; this.OnPropertyChanged("Blue"); this.OnPropertyChanged("RGB"); }
        }
        private int _blue;

        /// <summary>
        /// Color picker: green channel
        /// </summary>
        public int Green
        {
            get { return _green; }
            set { _green = value; this.OnPropertyChanged("Green"); }
        }
        private int _green;

        /// <summary>
        /// True if the user hit the color picking button and is choosing a color
        /// </summary>
        public bool PickingColor
        {
            get { return _pickingColor; }
            set { _pickingColor = value; this.OnPropertyChanged("PickingColor"); }
        }
        private bool _pickingColor;

        /// <summary>
        /// Displays the image with a Euclidean color filter applied
        /// </summary>
        public bool ColorFiltered
        {
            get { return _colorFiltered; }
            set { _colorFiltered = value; this.OnPropertyChanged("ColorFiltered"); }
        }
        private bool _colorFiltered;

        /// <summary>
        /// Displays the image inverted color filter applied
        /// </summary>
        public bool Inverted
        {
            get { return _inverted; }
            set { _inverted = value; this.OnPropertyChanged("Inverted"); }
        }
        private bool _inverted;

        /// <summary>
        /// Radius of the euclidean color filter
        /// </summary>
        public short Radius
        {
            get { return _radius; }
            set { _radius = value; this.OnPropertyChanged("Radius"); }
        }
        private short _radius;

        /// <summary>
        /// RGB
        /// </summary>
        public String RGB
        {
            set { }
            get { return Red + "," + Green + "," + Blue; }

        }


        /// <summary>
        /// Position
        /// </summary>
        public double PosX
        {
            get { return _PosX - _PosX0; }
            set { }

        }
        public double _PosX
        {

            get { return GetPropertyValue<double>("_PosX"); }
            set { SetPropertyValue("_PosX", value);
                SetPropertyValue("PosX", value);
                _PosXMin = _PosXMin > _PosX ? _PosX : _PosXMin;
                _PosXMax =  _PosX > _PosXMax?  _PosX : _PosXMax;
                //SetPropertyValue("_PosXAVG", GetAvg("_PosXAVG", value));
                //if (Math.Abs(_PosXAVG - value) < 15)
                //   Calibrate();
                _PosX0= (_PosXMax- _PosXMin)/ 2;
            }

        }

        public double _PosXAVG
        {
            get { return GetPropertyValue<double>("_PosXAVG"); }
            set { }
        }

        public double _PosX0;

        private double _PosXMin = 100000;
        private double _PosXMax = -100000;
        #endregion

        #region Notifiers
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

            var TimeWatch = Convert.ToDouble((DateTime.Now).Ticks);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }

        }

        public Dictionary<string, Object> Data = new Dictionary<string, Object>();
        public Dictionary<string, System.Reflection.PropertyInfo> DataProperty = new Dictionary<string, System.Reflection.PropertyInfo>();
        public Dictionary<string, string> DontExist = new Dictionary<string, string>();

        #endregion

        #region Maths Avg

        public List<Logs> Log = new List<Logs>();

        public class Logs
        {

            public string tag;
            public double Value;
            public TimeSpan Time = TimeSpan.FromTicks((long)Convert.ToDouble((DateTime.Now).Ticks));
        }

        protected double GetAvg(string propertyName, double value, double time = 10)
        {
            double target = value;          

            TimeSpan Time = TimeSpan.FromTicks((long)Convert.ToDouble((DateTime.Now).Ticks));

            Log.Add(new Logs() { tag = propertyName, Value = value, Time = Time });

            var results = Log.Where(z => z.tag == nameof(propertyName));
          
            if (results.Count() > 0)
                    target= results.Where(y => (Time - y.Time).TotalSeconds <= time).Average(z => z.Value);


            //Log.RemoveAll(y => (Time - y.Time).TotalSeconds > 30);


            return target;

        }

 
        #endregion
    }



}
