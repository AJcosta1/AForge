using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SDKSmartTrainnerAdaptor.Telemetry
{
    public static partial class Telemetry
    {
        class GTA5TelemetryReceiver
        {
            TelemetryReader DataReader;
            TelemetryPacket Data = new TelemetryPacket();

            GTA5TelemetryPluginSettings Settings = new GTA5TelemetryPluginSettings();

            public static MainWindow rootClass;

            public GTA5TelemetryReceiver(MainWindow _rootClass)
            {
                rootClass = _rootClass;


                try
                {
                    string param = ConfigurationManager.AppSettings["portFromGame"];
                    Settings.portToGame = Int32.Parse(param);
                }
                catch { }

                this.DataReader = new TelemetryReader(Settings.portFromGame);

                try
                {
                    this.DataReader.udpClient.BeginReceive(new AsyncCallback(recv), null);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }


                DispatcherTimer timer1 = new DispatcherTimer();
                timer1.Tick += TimerTick1;
                timer1.Interval = TimeSpan.FromMilliseconds(100);
                timer1.Start();

            }
            private void TimerTick1(object sender, object e)
            {
                rootClass.GameSpeedRead = Data.Speed;
                rootClass.GameSlope = Data.Roll;
            }

            private void recv(IAsyncResult res)
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, Settings.portFromGame);
                byte[] received = this.DataReader.udpClient.EndReceive(res, ref RemoteIpEndPoint);

                //Process codes

                Data=PacketUtilities.ConvertToPacket(received);
                this.DataReader.udpClient.BeginReceive(new AsyncCallback(recv), null);
            }

        }
    }
}
