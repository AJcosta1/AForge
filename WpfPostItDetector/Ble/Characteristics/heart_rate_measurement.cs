﻿using System;
using System.Collections;
using System.Collections.Generic;
using SDKSmartTrainnerAdaptor.Ble.UuidDictionary;
using SDKSmartTrainnerAdaptor;
using SDKSmartTrainnerAdaptor.Ble;
using System.Linq;

namespace SDKSmartTrainnerAdaptor.Ble.Characteristics
{
    public partial class CharacteristicsRead
    {

        public static void heart_rate_measurement()
        {
            foreach (var ListaConfiguracaoDispositivos in WorkingDataBLE.WorkingDataDictonaryByte.ToList())
            {
                string characteristic = _Characteristics.HeartRateMeasurement;

                string[] parts = ListaConfiguracaoDispositivos.Key.Split('|');
                characteristic = characteristic.ToUpper();
                if (characteristic == parts[1])
                {
                    string deviceID = parts[0];

                    string variableToRead1 = deviceID+"|"+ characteristic + "|_ReadTratedValue_1";
                    string variableToRead2 = deviceID + "|" + characteristic + "|_ReadTratedValue_2";
                    string variableToRead3 = deviceID + "|" + characteristic + "|_ReadTratedValue_3";

                    if (connected(deviceID))
                    {

                        if (characteristic == parts[1])
                        {
                            var bits = new BitArray(ListaConfiguracaoDispositivos.Value);
                            var value = 0;
                            int byteStart = 1;

                            if (bits[0])
                            {
                                //bit 0 = 1 UINT16

                                value = BitConverter.ToUInt16(ListaConfiguracaoDispositivos.Value, byteStart);
                            }
                            else
                            {
                                //bit 0 = 0 UINT8
                                value = Convert.ToUInt16(ListaConfiguracaoDispositivos.Value[byteStart]);

                            }
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
<?xml version="1.0" encoding="utf-8"?>
<!-- Copyright 2011 Bluetooth SIG, Inc. All rights reserved. -->
<Characteristic xsi:noNamespaceSchemaLocation="http://schemas.bluetooth.org/Documents/characteristic.xsd"
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
type="org.bluetooth.characteristic.heart_rate_measurement"
uuid="2A37" name="Heart Rate Measurement">
  <InformativeText></InformativeText>
  <Value>
    <Field name="Flags">
      <Requirement>Mandatory</Requirement>
      <Format>8bit</Format>
      <BitField>
        <Bit index="0" size="1" name="Heart Rate Value Format bit">
          <Enumerations>
            <Enumeration key="0"
            value="Heart Rate Value Format is set to UINT8. Units: beats per minute (bpm)"
            requires="C1" />
            <Enumeration key="1"
            value="Heart Rate Value Format is set to UINT16. Units: beats per minute (bpm)"
            requires="C2" />
          </Enumerations>
        </Bit>
        <Bit index="1" size="2" name="Sensor Contact Status bits">
          <Enumerations>
            <Enumeration key="0"
            value="Sensor Contact feature is not supported in the current connection" />
            <Enumeration key="1"
            value="Sensor Contact feature is not supported in the current connection" />
            <Enumeration key="2"
            value="Sensor Contact feature is supported, but contact is not detected" />
            <Enumeration key="3"
            value="Sensor Contact feature is supported and contact is detected" />
          </Enumerations>
        </Bit>
        <Bit index="3" size="1" name="Energy Expended Status bit">
          <Enumerations>
            <Enumeration key="0"
            value="Energy Expended field is not present" />
            <Enumeration key="1"
            value="Energy Expended field is present. Units: kilo Joules"
            requires="C3" />
          </Enumerations>
        </Bit>
        <Bit index="4" size="1" name="RR-Interval bit">
          <Enumerations>
            <Enumeration key="0"
            value="RR-Interval values are not present." />
            <Enumeration key="1"
            value="One or more RR-Interval values are present."
            requires="C4" />
          </Enumerations>
        </Bit>
        <ReservedForFutureUse index="5" size="3">
        </ReservedForFutureUse>
      </BitField>
    </Field>
    <Field name="Heart Rate Measurement Value (uint8)">
      <InformativeText>Note: The format of the Heart Rate
      Measurement Value field is dependent upon bit 0 of the Flags
      field.</InformativeText>
      <Requirement>C1</Requirement>
      <Format>uint8</Format>
      <Unit>org.bluetooth.unit.period.beats_per_minute</Unit>
    </Field>
    <Field name="Heart Rate Measurement Value (uint16)">
      <InformativeText>Note: The format of the Heart Rate
      Measurement Value field is dependent upon bit 0 of the Flags
      field.</InformativeText>
      <Requirement>C2</Requirement>
      <Format>uint16</Format>
      <Unit>org.bluetooth.unit.period.beats_per_minute</Unit>
    </Field>
    <Field name="Energy Expended">
      <InformativeText>The presence of the Energy Expended field is
      dependent upon bit 3 of the Flags field.</InformativeText>
      <Requirement>C3</Requirement>
      <Format>uint16</Format>
      <Unit>org.bluetooth.unit.energy.joule</Unit>
    </Field>
    <Field name="RR-Interval">
      <InformativeText>
        <!-- The presence of the RR-Interval field is dependent upon bit 4 of the Flags field. 
                <p>The RR-Interval value represents the time between two R-Wave detections.</p> 
                
                <p>Because several RR-Intervals may be measured between transmissions of the HEART RATE MEASUREMENT characteristic, 
                multiple RR-Interval sub-fields may be present in the characteristic. The number of RR-Interval sub-fields present 
                is determined by a combination of the overall length of the characteristic and whether or not the characteristic contains 
                the Energy Expended field.</p>
                
                <p>Where there are multiple RR-Interval values transmitted in the HEART RATE MEASUREMENT characteristic, the field uses the following format:</p>
                <p>RR-Interval Value 0 (LSO...MSO), RR-Interval Value 1 (LSO...MSO), RR-Interval Value 2 (LSO...MSO), RR-Interval Value n (LSO...MSO).</p>
                <p>Where the RR-Interval Value 0 is older than the RR-Interval Value 1.</p>
                <p>RR-Interval Value 0 is transmitted first followed by the newer measurements.</p>-->
      </InformativeText>
      <Requirement>C4</Requirement>
      <Format>uint16</Format>
      <Unit>org.bluetooth.unit.time.second</Unit>
      <Description>Resolution of 1/1024 second</Description>
    </Field>
  </Value>
  <Note>
    <p>The fields in the above table are in the order of LSO to
    MSO. Where LSO = Least Significant Octet and MSO = Most
    Significant Octet.</p>
  </Note>
</Characteristic>

*/
