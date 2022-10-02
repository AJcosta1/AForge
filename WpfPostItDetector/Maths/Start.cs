
using SDKSmartTrainnerAdaptor.Ble.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SDKSmartTrainnerAdaptor.Maths
{
    public static partial class Maths
    {
        static MainWindow rootClass;

        public static void StartServices(MainWindow _rootClass)
        {
            rootClass = _rootClass;

            StartToGameAccelerationBrake();

            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Tick += TimerTick1;
            timer1.Interval = TimeSpan.FromMilliseconds(20);
            timer1.Start();

        }
        private static void TimerTick1(object sender, object e)
        {
            Speed();
            ToGameAccelerationBrake();
            ToGameDirection();
            ToTurboSlope();
            Speed();
        }

    }
}
