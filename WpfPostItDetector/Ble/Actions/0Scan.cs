using SDKSmartTrainnerAdaptor; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.UI.Core;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
    {
        DeviceWatcher deviceWatcher;
        public bool isScanning = false;
        private MainWindow rootPage;


        public static MainWindow rootClass;

        public BLEMethods(MainWindow _rootClass)
        {
            rootPage = _rootClass;
        }


        public async Task ScanGatt()
        {
            if (!isScanning)
            {
                rootPage.loggerAdd("Watching for devices.");
                isUpdating.Clear();

                Variables.DevicesDetected.Clear();
                // Additional properties we would like about the device.
                // Property strings are documented here https://msdn.microsoft.com/en-us/library/windows/desktop/ff521659(v=vs.85).aspx
                string[] requestedProperties = { "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.IsConnected", "System.Devices.Aep.Bluetooth.Le.IsConnectable" };

                // BT_Code: Example showing paired and non-paired in a single query.
                string aqsAllBluetoothLEDevices = "(System.Devices.Aep.ProtocolId:=\"{bb7bb05e-5972-42b5-94fc-76eaa7084d49}\")";

                deviceWatcher =
                        DeviceInformation.CreateWatcher(
                            aqsAllBluetoothLEDevices,
                            requestedProperties,
                            DeviceInformationKind.AssociationEndpoint);

                // Register event handlers before starting the watcher.
                // Added, Updated and Removed are required to get all nearby devices
                deviceWatcher.Added += DeviceWatcher_Added;
                //deviceWatcher.Updated += DeviceWatcher_Updated;
                //deviceWatcher.Removed += DeviceWatcher_Removed;
                Variables.DevicesDetected.Clear();
                // EnumerationCompleted and Stopped are optional to implement.
                deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted;
                //deviceWatcher.Stopped += DeviceWatcher_Stopped;


                // Start the watcher.
                deviceWatcher.Start();
                isScanning = true;

            }
        }

        private async void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation deviceInfo)
        {

            // We must update the collection on the UI thread because the collection is databound to a UI element.
            await Task.Run(() =>
            {
                lock (this)
                {

                    // Protect against race condition if the task runs after the app stopped the deviceWatcher.
                    if (sender == deviceWatcher)
                    {
                        // Make sure device isn't already present in the list.
                        if (FindBluetoothLEDeviceDisplay(deviceInfo.Id) == null)
                        {
                            if (deviceInfo.Name != string.Empty)
                            {

                                // If device has a friendly name display it immediately.
                                Variables.DevicesDetected.Add(new BluetoothLEDeviceDisplay(deviceInfo));
                      

                            }
                            else
                            {
                                // Add it to a list in case the name gets updated later. 
                                //UnknownDevices.Add(deviceInfo);
                            }
                        }

                    }
                }
            });
        }

     
        private BluetoothLEDeviceDisplay FindBluetoothLEDeviceDisplay(string id)
        {
            foreach (BluetoothLEDeviceDisplay bleDeviceDisplay in Variables.DevicesDetected)
            {
                if (bleDeviceDisplay.Id == id)
                {
                    return bleDeviceDisplay;
                }
            }
            return null;
        }

        private async void DeviceWatcher_EnumerationCompleted(DeviceWatcher sender, object e)
        {
            // We must update the collection on the UI thread because the collection is databound to a UI element.
            await Task.Run(() =>
            {
                // Protect against race condition if the task runs after the app stopped the deviceWatcher.
                if (sender == deviceWatcher)
                {
                    rootPage.loggerAdd("No longer watching for devices.");
                    isScanning = false;
                }
            });
        }

        public void StopScan()
        {
            if (isScanning)
                deviceWatcher.Stop();

            isScanning = false;
        }

    }
}
