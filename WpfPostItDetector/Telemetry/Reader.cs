using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;



namespace SDKSmartTrainnerAdaptor.Telemetry
{
    sealed class TelemetryReader : IDisposable
    {
        private IPEndPoint senderIP = new IPEndPoint(IPAddress.Any, 0);
        public UdpClient udpClient;

        // Class constructor
        public TelemetryReader(int port)
        {
            try
            {
                this.InitUdp(port);
            }
            catch (Exception e)
            {

            }
        }

        private void InitUdp(int port)
        {
            try
            {
                if (this.udpClient == null)
                {
                    this.udpClient = new UdpClient();
                    this.udpClient.Connect("127.0.0.1", port);
                }
            }
            catch
            {
                this.udpClient = null;
            }
        }

       

        #region IDisposable Support
        private bool disposedValue = false; // Per rilevare chiamate ridondanti

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    udpClient.Close();
                }

                // TODO: liberare risorse non gestite (oggetti non gestiti) ed eseguire sotto l'override di un finalizzatore.
                // TODO: impostare campi di grandi dimensioni su Null.

                disposedValue = true;
            }
        }

        // TODO: eseguire l'override di un finalizzatore solo se Dispose(bool disposing) include il codice per liberare risorse non gestite.
        // ~TelemetryWriter() {
        //   // Non modificare questo codice. Inserire il codice di pulizia in Dispose(bool disposing) sopra.
        //   Dispose(false);
        // }

        // Questo codice viene aggiunto per implementare in modo corretto il criterio Disposable.
        public void Dispose()
        {
            // Non modificare questo codice. Inserire il codice di pulizia in Dispose(bool disposing) sopra.
            Dispose(true);
            // TODO: rimuovere il commento dalla riga seguente se è stato eseguito l'override del finalizzatore.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

}
