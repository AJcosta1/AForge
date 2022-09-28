using SDKSmartTrainnerAdaptor.Ble.Configuration;
using System;
using System.BluetoothLe;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDKSmartTrainnerAdaptor.Ble.Actions
{
    public partial class BLEMethods
    {  
 
 
        public void addEvents()
        {

            adapter.DeviceConnected += (o, args) =>
            {               
                 //args.Device.UpdateConnectionInterval(ConnectionInterval.Low);

                 Console.WriteLine("connected:" + args.Device.Name);
               
            };


            adapter.DeviceDisconnected += (o, args) =>
            {
                Console.WriteLine("Disconected:" + args.Device.Name);
                clearCharacteristics(args.Device);
                };

            adapter.DeviceConnectionLost += (o, args) =>
            {
                clearCharacteristics(args.Device);
            };

            adapter.DeviceDiscovered += (s, a) =>
            {
                AddOrUpdateDevice(a.Device);

                if (a != null)
                {
                    if (a.Device.Id != null)
                    {
                        if (WorkingDataBLE.Current.SessonData.DevicesDetected.ToList().Where(x => x.Device.Id == a.Device.Id).Count() == 0)
                        {
                            WorkingDataBLE.Current.SessonData.DevicesDetected.Add(
                                new DevicesDetected() { Device = a.Device });
                            Console.WriteLine("found:" + a.Device.Name);

                        }
                    }

                }
            };

        }

        private void AddOrUpdateDevice(System.BluetoothLe.Device device)
        {

            var vm = WorkingDataBLE.Current.SessonData.DevicesDetected.ToList().FirstOrDefault(x => x.Device.Id == device.Id);
                if (vm != null)
                {
                   vm.Device = device;
                }
                else
                {
                    WorkingDataBLE.Current.SessonData.DevicesDetected.Add(
                              new DevicesDetected() { Device = device });
                    Console.WriteLine("found:" + device.Name);
                }

        }

        private void clearCharacteristics(System.BluetoothLe.Device device)
        {

            WorkingDataBLE.Current.SessonData.DevicesDetected.ToList().FirstOrDefault(x => x.Device.Id == device.Id).Characteristics.Clear();
    

        }
 
    }
}
