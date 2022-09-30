﻿using SDKSmartTrainnerAdaptor.Ble.Actions;
using SDKSmartTrainnerAdaptor.Ble.Characteristics;
using SDKSmartTrainnerAdaptor.Ble.Configuration;
using SDKSmartTrainnerAdaptor.Ble.DataPriority;
using System;
using System.Windows.Threading;

namespace SDKSmartTrainnerAdaptor.Ble
{

    public static class Start
    {
        public static BLEMethods ble;
        public static MainWindow rootClass;

        public static void StartServices(MainWindow _rootClass)
        {
            rootClass = _rootClass;
            ble = new BLEMethods(rootClass);


            Configuration.BLEConfigurationInitialization.StartBLEConfigurationInitialization();

            ble.ScanGatt();


            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += TimerTick1;
            timer.Interval = TimeSpan.FromMilliseconds(BLEConfiguration.updateConnections);
            timer.Start();

            DispatcherTimer timer2 = new DispatcherTimer();
            timer2.Tick += TimerTick2;
            timer2.Interval = TimeSpan.FromMilliseconds(BLEConfiguration.updateTime);
            timer2.Start();

            DispatcherTimer timer3 = new DispatcherTimer();
            timer3.Tick += TimerTick3;
            timer3.Interval = TimeSpan.FromMilliseconds(BLEConfiguration.updateTime);
            timer3.Start();


        }
        private static async void TimerTick1(object sender, object e)
        {
            await ble.updateConnections();
        }

        private static async void TimerTick2(object sender, object e)
        {
           // ble.connectDevices();

        }
        private static async void TimerTick3(object sender, object e)
        {
            ble.updateAsync();
            CharacteristicsRead.InterpretateReadDataFromBLE();
            DataPriority.DataPriority.CheckData();

        }

    }
} 
