using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SDKSmartTrainnerAdaptor.Telemetry
{
    class GTA5TelemetryPluginSettings
    {
        public Int32 portFromGame = 20777; // The UDP communication port
        public Int32 portToGame = 20778; // The UDP communication port
    }

    class GTA5TelemetrySender
    {
        TelemetryWriter DataWriter;
        TelemetryPacket Data = new TelemetryPacket();

        GTA5TelemetryPluginSettings Settings = new GTA5TelemetryPluginSettings();

        public static MainWindow rootClass;
          
            public GTA5TelemetrySender(MainWindow _rootClass)                   
            {
            rootClass = _rootClass;


            try
            {
                string param = ConfigurationManager.AppSettings["portToGame"];
                Settings.portToGame = Int32.Parse(param);
            }
            catch { }

            this.DataWriter = new TelemetryWriter(Settings.portToGame);

            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Tick += TimerTick1;
            timer1.Interval = TimeSpan.FromMilliseconds(100);
            timer1.Start();

        }
        private void TimerTick1(object sender, object e)
        {

            //Data.X = player.Position.X; 


            // Share data
            byte[] bytes = PacketUtilities.ConvertPacketToByteArray(Data);
            DataWriter.SendPacket(bytes);
        }
    }

}
