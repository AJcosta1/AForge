/*using System;
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

        public static MainWindow rootClass;

        public static void StartServices(MainWindow _rootClass)
        {
            rootClass = _rootClass;

        public static ViGEmClient client;
        public static IDualShock4Controller controller;
        public static IXbox360Controller controllerXBox;

        public static MainWindow mainWindow;

        public static void Start(MainWindow _mainWindow)
        {


            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += TimerTick1;
            timer.Interval = TimeSpan.FromMilliseconds(500);

            Task.Run(() =>
            {
                mainWindow = _mainWindow;

                ViGEmClient client = new ViGEmClient();

                controller = client.CreateDualShock4Controller();


                controller.Connect();


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
        }


        public static void SetButtons()
        {
            //controller.SetAxisValue(Xbox360Axis.RightThumbX, GetSafeValue());

            controller.SetButtonState(DualShock4Button.Circle, mainWindow.PosX > 128);
            controller.SetButtonState(DualShock4Button.Cross, mainWindow.PosX > 128);
            controller.SetButtonState(DualShock4Button.Square, mainWindow.PosX > 128);
            controller.SetButtonState(DualShock4Button.Triangle, mainWindow.PosX > 128);

            controller.SetAxisValue(DualShock4Axis.RightThumbX, GetSafeValueByte());//zz
            controller.SetAxisValue(DualShock4Axis.RightThumbY, GetSafeValueByte());

            controllerXBox.SetAxisValue(Xbox360Axis.LeftThumbX, GetSafeValue());
            controllerXBox.SetAxisValue(Xbox360Axis.LeftThumbY, GetSafeValue());
            //controller.SetDPadDirection(DualShock4DPadDirection.East);


            //controller.SetSliderValue(Xbox360Axis.RightThumbY, 0); 
        }
        public static short GetSafeValue()
        {
            
            var cst= 32767;
            var pos255 = (mainWindow.PosX*cst/255);
            pos255 = pos255 < -cst ? -cst : pos255;
            pos255 = pos255 > cst ? cst : pos255;
            var shortV = ((short)cst) ;
            return shortV;
           
            var cst = 32767;

            var pos255 = (mainWindow.PosX * cst / 255);
            pos255 = pos255 < -cst ? -cst : pos255;
            pos255 = pos255 > cst ? cst : pos255;


            var shortV = ((short)pos255);

            return shortV;
        }

        public static byte GetSafeValueByte()
        {

            var pos255 = mainWindow.PosX + 128;
            pos255 = pos255 < 0 ? 0 : pos255;
            pos255 = pos255 > 255 ? 255 : pos255;


            var shortV = Convert.ToByte(pos255);

            return shortV;


        }
    }
} */