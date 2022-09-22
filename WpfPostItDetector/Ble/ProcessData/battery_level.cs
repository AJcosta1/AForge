using IndoorCycling.Abstractions.Configurations.BLE;
using IndoorCycling.Abstractions.Enums.BLE;
using IndoorCycling.Abstractions.WorkingData.BLE;
using IndoorCycling.Abstractions.WorkingData.DataSavingManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IndoorCycling.Libs.BLE.ReceivedDataProcessing.Characteristics
{
    public partial class CharacteristicsRead
    {


        public static void battery_level(Dictionary<string, byte[]> WorkingDataDictonaryByte_temp)
        {
            foreach (var ListaConfiguracaoDispositivos in WorkingDataDictonaryByte_temp)
            {
                string characteristic = characteristics.BatteryLevel;
                
                string[] parts = ListaConfiguracaoDispositivos.Key.Split('|');
                characteristic = characteristic.ToUpper();
                if (characteristic == parts[1])
                {
                    string deviceID = parts[0];

                    string variableToRead1 = deviceID + "|" + characteristic + "|_ReadTratedValue_1";
                    string variableToRead2 = deviceID + "|" + characteristic + "|_ReadTratedValue_2";
                    string variableToRead3 = deviceID + "|" + characteristic + "|_ReadTratedValue_3";

                    if (connected(deviceID))
                    {

                        if (characteristic == parts[1])
                        {
                            var value = 0;
                            int byteStart = 0;


                            value = Convert.ToUInt16(ListaConfiguracaoDispositivos.Value[byteStart]);

                            
                            WorkingDataBLE.WorkingDataDictonaryTratedFloat[variableToRead1] = value;
                        }
                        else
                        {
                            WorkingDataBLE.WorkingDataDictonaryTratedFloat[variableToRead1] = 0;
                            WorkingDataBLE.WorkingDataDictonaryTratedFloat[variableToRead2] = 0;
                            WorkingDataBLE.WorkingDataDictonaryTratedFloat[variableToRead3] = 0;
                        }

                    }
                }
            }
        }
    }
}
/*
opyright 2011 Bluetooth SIG, Inc. All rights reserved.-->
<Characteristic xsi:noNamespaceSchemaLocation="http://schemas.bluetooth.org/Documents/characteristic.xsd"
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
name="Battery Level"
type="org.bluetooth.characteristic.battery_level" uuid="2A19"
last-modified="2011-12-05" approved="Yes">
  <InformativeText>
    <Abstract>The current charge level of a battery. 100%
    represents fully charged while 0% represents fully
    discharged.</Abstract>
  </InformativeText>
  <Value>
    <Field name="Level">
      <Requirement>Mandatory</Requirement>
      <Format>uint8</Format>
      <Unit>org.bluetooth.unit.percentage</Unit>
      <Minimum>0</Minimum>
      <Maximum>100</Maximum>
      <Enumerations>
        <Reserved start="101" end="255" />
      </Enumerations>
    </Field>
  </Value>
</Characteristic>


*/
