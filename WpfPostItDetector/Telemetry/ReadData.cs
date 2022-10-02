using SDKSmartTrainnerAdaptor.Properties;
using SDKSmartTrainnerAdaptor.Telemetry;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Xml.Linq;

namespace SDKSmartTrainnerAdaptor.Telemetry
{
    public static partial class Telemetry
    {
        class GTA5TelemetryReceiver
        {
            private UdpClient _udpClient;

            public event EventHandler<ReceivedEventArgs> Received;

            TelemetryPacket Data = new TelemetryPacket();

            GTA5TelemetryPluginSettings Settings = new GTA5TelemetryPluginSettings();

            public static MainWindow rootClass;

            public GTA5TelemetryReceiver(MainWindow _rootClass)
            {
                rootClass = _rootClass;
                try
                {
                    string param = ConfigurationManager.AppSettings["portFromGame"];
                    Settings.portFromGame = Int32.Parse(param);
                }
                catch { }

                IPAddress ip = IPAddress.Parse("127.0.0.1");




                start(ip, Settings.portFromGame);

            }

            public void start(IPAddress ip, int port)
            {
                if (_udpClient != null)
                    return;

                _udpClient = new UdpClient(ip.AddressFamily);
                _udpClient.EnableBroadcast = true;

                _udpClient.Client.SetSocketOption(
                    SocketOptionLevel.Socket,
                    SocketOptionName.ReuseAddress,
                    true
                );

                _udpClient.Client.Bind(new IPEndPoint(ip, port));
                /*
                StatusChanged?.Invoke(this, new UdpClientServerStatusEventArgs(
                    UdpClientServerStatusEventArgs.EServerStatus.Started,
                    _udpClient.Client.LocalEndPoint as IPEndPoint));
                */
                StartReceive();
            }

            private void StartReceive()
            {
                Task.Run(async () =>
                {
                    while (_udpClient != null)
                    {
                        try
                        {
                            UdpReceiveResult res = await _udpClient.ReceiveAsync();

                            Transmission msg = new Transmission(res.Buffer, Transmission.EType.Received);
                            msg.Origin = res.RemoteEndPoint;
                            msg.Destination = _udpClient.Client.LocalEndPoint as IPEndPoint;

                            var Data = PacketUtilities.ConvertToPacket(msg.Data);
                            Telemetry.rootClass.GameSpeedRead = Data.Speed;
                            Telemetry.rootClass.GameSlope = Data.XR;
                            Telemetry.rootClass.IsInVehicle = Data.IsInVehicle==1?true:false;

        }
                        catch (SocketException ex)
                        {

                        }
                        catch (Exception)
                        { 

                        }
                    }
                });
            }



        }
    }
    public class ReceivedEventArgs : EventArgs
    {
        public ReceivedEventArgs(Transmission message)
        {

            var Data = PacketUtilities.ConvertToPacket(message.Data); 
        }
    }


}
 