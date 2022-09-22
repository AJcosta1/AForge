using SDKSmartTrainnerAdaptor.Ble.UuidDictionary;
using SDKSmartTrainnerAdaptor.GlobalLibs;
using System.Collections;
using System.Collections.Generic;
using SDKSmartTrainnerAdaptor.Ble;

namespace SDKSmartTrainnerAdaptor.Ble.Characteristics
{
    public partial class CharacteristicsRead
    {

        public static void csc_measurement(Dictionary<string, byte[]> WorkingDataDictonaryByte_temp)
        {
            foreach (var ListaConfiguracaoDispositivos in WorkingDataDictonaryByte_temp)
            {
                string characteristic = _Characteristics.CSCMeasurement;

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
                            var bits = new BitArray(ListaConfiguracaoDispositivos.Value);


                            // <Bit index="0" size="1" name = "Wheel Revolution Data Present" >
                                                
                            if (bits[0])
                            {
                                string name= "Wheel_Revolution";
                                string target = variableToRead1;
                                int startValueByte = 1;
                                bool ValueUint16=false; //false is 32 bits
                                int startTimeByte = 5;
                                bool TimeUint16 = true;//false is 32 bits

                               WorkingDataBLE.WorkingDataDictonaryTratedFloat[target]=Calc_Time_Based_Ble_Rotation(deviceID,characteristic, name, ListaConfiguracaoDispositivos.Value, startValueByte, ValueUint16, startTimeByte, TimeUint16);
                   


                            }
                            //<Bit index="1" size="1" name = "Crank Revolution Data Present" >
                            if (bits[1])
                            {
                                string name = "Crank Revolution";
                                string target = variableToRead2;
                                int startValueByte = 7;
                                bool ValueUint16 = true; //false is 32 bits
                                int startTimeByte = 9;
                                bool TimeUint16 = true;//false is 32 bits

                               WorkingDataBLE.WorkingDataDictonaryTratedFloat[target] = Calc_Time_Based_Ble_Rotation(deviceID,characteristic, name, ListaConfiguracaoDispositivos.Value, startValueByte, ValueUint16, startTimeByte, TimeUint16);
               

                            }

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
<!--Copyright 2011 Bluetooth SIG, Inc. All rights reserved.-->
<Characteristic xsi:noNamespaceSchemaLocation="http://schemas.bluetooth.org/Documents/characteristic.xsd"
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
name="CSC Measurement"
type="org.bluetooth.characteristic.csc_measurement" uuid="2A5B"
last-modified="2012-04-12" approved="Yes">
  <InformativeText>
    <Summary>The CSC Measurement characteristic (CSC refers to
    Cycling Speed and Cadence) is a variable length structure
    containing a Flags field and, based on the contents of the
    Flags field, may contain one or more additional fields as shown
    in the tables below.</Summary>
  </InformativeText>
  <Value>
    <Field name="Flags">
      <InformativeText>These flags define which data fields are
      present in the Characteristic value.</InformativeText>
      <Requirement>Mandatory</Requirement>
      <Format>8bit</Format>
      <BitField>
        <Bit index="0" size="1"
        name="Wheel Revolution Data Present">
          <Enumerations>
            <Enumeration key="0" value="False" />
            <Enumeration key="1" value="True" requires="C1" />
          </Enumerations>
        </Bit>
        <Bit index="1" size="1"
        name="Crank Revolution Data Present">
          <Enumerations>
            <Enumeration key="0" value="False" />
            <Enumeration key="1" value="True" requires="C2" />
          </Enumerations>
        </Bit>
        <ReservedForFutureUse index="2" size="6" />
      </BitField>
    </Field>
    <Field name="Cumulative Wheel Revolutions">
      <InformativeText>C1: Field exists if the key of bit 0 of the
      Flags field is set to 1.</InformativeText>
      <Requirement>C1</Requirement>
      <Format>uint32</Format>
      <Unit>org.bluetooth.unit.unitless</Unit>
    </Field>
    <Field name="Last Wheel Event Time">
      <InformativeText>Unit has a resolution of 1/1024s. 
      <br>C1: Field exists if the key of bit 0 of the Flags field
      is set to 1.</br></InformativeText>
      <Requirement>C1</Requirement>
      <Format>uint16</Format>
      <Unit>org.bluetooth.unit.time.second</Unit>
      <BinaryExponent>-10</BinaryExponent>
    </Field>
    <Field name="Cumulative Crank Revolutions">
      <InformativeText>C2: Field exists if the key of bit 1 of the
      Flags field is set to 1.</InformativeText>
      <Requirement>C2</Requirement>
      <Format>uint16</Format>
      <Unit>org.bluetooth.unit.unitless</Unit>
    </Field>
    <Field name="Last Crank Event Time">
      <InformativeText>C2: Field exists if the key of bit 1 of the
      Flags field is set to 1. 
      <br>Unit has a resolution of 1/1024s.</br></InformativeText>
      <Requirement>C2</Requirement>
      <Format>uint16</Format>
      <Unit>org.bluetooth.unit.time.second</Unit>
      <BinaryExponent>-10</BinaryExponent>
    </Field>
  </Value>
  <Note>The fields in the above table are in the order of LSO to
  MSO. Where LSO = Least Significant Octet and MSO = Most
  Significant Octet.</Note>
</Characteristic>
*/
