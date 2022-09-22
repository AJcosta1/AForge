using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SDKSmartTrainnerAdaptor.GlobalLibs
{
    public static class Variables
    {
 
        public static SessonData sessonData { get; set; }

        public static SessonData SessonData
        {

            get  { if(sessonData==null)
                    sessonData = new SessonData();
                return sessonData; }  
        } 

    }
  

    public class SessonData : INotifyPropertyChanged
    {
 
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
            get { return ("HR= "+ HeartRateBPM); } 
        }

        public double Speed_ms
        {
            get { return GetPropertyValue<double>("Speed_ms"); }
            set { SetPropertyValue("Speed_ms", value); }
        }

        public double Speed
        {
            get { return GetPropertyValue<double>("Speed_ms")*3.6; }
           
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

 
 
}
