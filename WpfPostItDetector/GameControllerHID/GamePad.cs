using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Threading;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.DualShock4;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using SDKSmartTrainnerAdaptor.Ble.Configuration;

namespace SDKSmartTrainnerAdaptor.GamePadEmulator
{
   public static class GamePadEmulator { 

        public static MainWindow rootClass;


        public static ViGEmClient client;
        public static IDualShock4Controller controller;
        public static IXbox360Controller controllerXBox;
         


        public static void Start(MainWindow _mainWindow)
        {

            rootClass = _mainWindow;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += TimerTick1;
            timer.Interval = TimeSpan.FromMilliseconds(10);

            Task.Run(() =>
            {

                ViGEmClient client = new ViGEmClient();

                controller = client.CreateDualShock4Controller();


                //controller.Connect();


                controllerXBox = client.CreateXbox360Controller();


                controllerXBox.Connect();

                Thread.Sleep(10000);

                timer.Start();
            }
          );

        }
        private static void TimerTick1(object sender, object e)
        {
            SetButtons();
            if (rootClass.IsInVehicle)
                SetButtons();
            else
                ResetButtons();

        }


        public static void SetButtons()
        {

            //Direction
            controllerXBox.SetAxisValue(Xbox360Axis.LeftThumbX, GetSafeValue(rootClass.ToGameDirection));
 
            //Brake
            controllerXBox.SetSliderValue(Xbox360Slider.LeftTrigger, GetSafeValueByte(rootClass.ToGameBrake));
    
            //Accelerate
            controllerXBox.SetSliderValue(Xbox360Slider.RightTrigger, GetSafeValueByte(rootClass.ToGameAcceleration));


        }

        public static void ResetButtons()
        {

            //Direction
            controllerXBox.SetAxisValue(Xbox360Axis.LeftThumbX, 0);

            //Accelerate
            controllerXBox.SetSliderValue(Xbox360Slider.RightTrigger,0);

            //Brake
            controllerXBox.SetSliderValue(Xbox360Slider.LeftTrigger, 0);

        }

        public static short GetSafeValue(double value)
        {            
            var cst1= 32767;
            var cst2 = rootClass._PosXAbsMin; 
            var cst = (value / cst2 )* cst1;
            var shortV = ((short)cst) ;
            return shortV;
        }
      
        public static byte GetSafeValueByte(double value)
        {
            var cst1 = 255;
            var cst2 = 100;
            var cst = (value / cst2) * cst1;
            var shortV = Convert.ToByte(cst);
            return shortV;


        }
    }
} 