﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;



namespace SDKSmartTrainnerAdaptor.Telemetry
{
    public static class PacketUtilities
    {
        public static byte[] ConvertPacketToByteArray(TelemetryPacket packet)
        {
            int num = Marshal.SizeOf<TelemetryPacket>(packet);
            byte[] array = new byte[num];
            IntPtr intPtr = Marshal.AllocHGlobal(num);
            Marshal.StructureToPtr<TelemetryPacket>(packet, intPtr, false);
            Marshal.Copy(intPtr, array, 0, num);
            Marshal.FreeHGlobal(intPtr);
            return array;
        }

        public static TelemetryPacket ConvertToPacket(byte[] bytes)
        {
            GCHandle gCHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            TelemetryPacket arg_2A_0 = (TelemetryPacket)Marshal.PtrToStructure(gCHandle.AddrOfPinnedObject(), typeof(TelemetryPacket));
            gCHandle.Free();
            return arg_2A_0;
        }
    }

}