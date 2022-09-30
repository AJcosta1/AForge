using SDKSmartTrainnerAdaptor;
using System;
using System.Threading;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
    {

        private bool isBusy = false;

        private async void Pair(BluetoothLEDeviceDisplay device)
        {
            // Do not allow a new Pair operation to start if an existing one is in progress.
            if (isBusy)
            {
                return;
            }

            isBusy = true;

            //rootPage.NotifyUser("Pairing started. Please wait...", NotifyType.StatusMessage);

            // For more information about device pairing, including examples of
            // customizing the pairing process, see the DeviceEnumerationAndPairing sample.


            // BT_Code: Pair the currently selected device.
            DevicePairingResult result = await device.DeviceInformation.Pairing.PairAsync();

            /*
            rootPage.NotifyUser($"Pairing result = {result.Status}",
                result.Status == DevicePairingResultStatus.Paired || result.Status == DevicePairingResultStatus.AlreadyPaired
                    ? NotifyType.StatusMessage
                    : NotifyType.ErrorMessage);
            */
            isBusy = false;
        }
    }
}