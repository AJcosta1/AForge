using IndoorCycling.Abstractions.Configurations.BLE;
using IndoorCycling.Abstractions.Enums.BLE;
using IndoorCycling.Abstractions.WorkingData.BLE;
using IndoorCycling.Abstractions.WorkingData.DataSavingManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace IndoorCycling.Libs.BLE.ReceivedDataProcessing.Characteristics
{
    public partial class CharacteristicsRead
    {

        public static void cycling_power(Dictionary<string, byte[]> WorkingDataDictonaryByte_temp)
        {
            foreach (var ListaConfiguracaoDispositivos in WorkingDataDictonaryByte_temp)
            {
                string characteristic = characteristics.CyclingPowerMeasurement;

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
                            float value = 0;
                            int byteStart = 2;

                            //Power

                            value = (float)BitConverter.ToUInt16(ListaConfiguracaoDispositivos.Value, byteStart); 
                 
                            WorkingDataBLE.WorkingDataDictonaryTratedFloat[variableToRead1] = value;


                            //Cadence


                            value = 0;
                            if (false)//bits[5]
                            {


                                string name = "Crank Revolution";
                                int startValueByte = 13;
                                bool ValueUint16 = true; //false is 32 bits
                                int startTimeByte = 15;
                                bool TimeUint16 = true;//false is 32 bits


                                value = Calc_Time_Based_Ble_Rotation(deviceID, characteristic, name, ListaConfiguracaoDispositivos.Value, startValueByte, ValueUint16, startTimeByte, TimeUint16);

                                //not tested yet
                                value = 0;
                               
                            }
                            WorkingDataBLE.WorkingDataDictonaryTratedFloat[variableToRead2] = value;
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
name="Cycling Power Measurement"
type="org.bluetooth.characteristic.cycling_power_measurement"
uuid="2A63" last-modified="2014-07-02" approved="Yes">
  <InformativeText>
    <Summary>The Cycling Power Measurement characteristic is a
    variable length structure containing a Flags field, an
    Instantaneous Power field and, based on the contents of the
    Flags field, may contain one or more additional fields as shown
    in the table below.</Summary>
  </InformativeText>
  <Value>
    <Field name="Flags">
      <Requirement>Mandatory</Requirement>
      <Format>16bit</Format>
      <BitField>
        <Bit index="0" size="1" name="Pedal Power Balance Present">
          <Enumerations>
            <Enumeration key="0" value="False" />
            <Enumeration key="1" value="True" />
          </Enumerations>
        </Bit>
        <Bit index="1" size="1"
        name="Pedal Power Balance Reference">
          <Enumerations>
            <Enumeration key="0" value="Unknown" />
            <Enumeration key="1" value="Left" />
          </Enumerations>
        </Bit>
        <Bit index="2" size="1" name="Accumulated Torque Present">
          <Enumerations>
            <Enumeration key="0" value="False" />
            <Enumeration key="1" value="True" />
          </Enumerations>
        </Bit>
        <Bit index="3" size="1" name="Accumulated Torque Source">
          <Enumerations>
            <Enumeration key="0" value="Wheel Based" />
            <Enumeration key="1" value="Crank Based" />
          </Enumerations>
        </Bit>
        <Bit index="4" size="1"
        name="Wheel Revolution Data Present">
          <Enumerations>
            <Enumeration key="0" value="False" />
            <Enumeration key="1" value="True" />
          </Enumerations>
        </Bit>
        <Bit index="5" size="1"
        name="Crank Revolution Data Present">
          <Enumerations>
            <Enumeration key="0" value="False" />
            <Enumeration key="1" value="True" />
          </Enumerations>
        </Bit>
        <Bit index="6" size="1"
        name="Extreme Force Magnitudes Present">
          <Enumerations>
            <Enumeration key="0" value="False" />
            <Enumeration key="1" value="True" />
          </Enumerations>
        </Bit>
        <Bit index="7" size="1"
        name="Extreme Torque Magnitudes Present">
          <Enumerations>
            <Enumeration key="0" value="False" />
            <Enumeration key="1" value="True" />
          </Enumerations>
        </Bit>
        <Bit index="8" size="1" name="Extreme Angles Present">
          <Enumerations>
            <Enumeration key="0" value="False" />
            <Enumeration key="1" value="True" />
          </Enumerations>
        </Bit>
        <Bit index="9" size="1" name="Top Dead Spot Angle Present">
          <Enumerations>
            <Enumeration key="0" value="False" />
            <Enumeration key="1" value="True" />
          </Enumerations>
        </Bit>
        <Bit index="10" size="1"
        name="Bottom Dead Spot Angle Present">
          <Enumerations>
            <Enumeration key="0" value="False" />
            <Enumeration key="1" value="True" />
          </Enumerations>
        </Bit>
        <Bit index="11" size="1" name="Accumulated Energy Present">
          <Enumerations>
            <Enumeration key="0" value="False" />
            <Enumeration key="1" value="True" />
          </Enumerations>
        </Bit>
        <Bit index="12" size="1"
        name="Offset Compensation Indicator">
          <Enumerations>
            <Enumeration key="0" value="False" />
            <Enumeration key="1" value="True" />
          </Enumerations>
        </Bit>
        <ReservedForFutureUse index="13" size="3" />
      </BitField>
      <b>C1:These Fields are dependent upon the Flags field</b>
      <p></p>
    </Field>
    <Field name="Instantaneous Power">
      <InformativeText>Unit is in watts with a resolution of
      1.</InformativeText>
      <Requirement>Mandatory</Requirement>
      <Format>sint16</Format>
      <Unit>org.bluetooth.unit.power.watt</Unit>
      <DecimalExponent>0</DecimalExponent>
    </Field>
    <Field name="Pedal Power Balance">
      <InformativeText>Unit is in percentage with a resolution of
      1/2.</InformativeText>
      <Requirement>Optional</Requirement>
      <Format>uint8</Format>
      <Unit>org.bluetooth.unit.percentage</Unit>
      <BinaryExponent>-1</BinaryExponent>
    </Field>
    <Field name="Accumulated Torque">
      <InformativeText>Unit is in newton metres with a resolution
      of 1/32.</InformativeText>
      <Requirement>Optional</Requirement>
      <Format>uint16</Format>
      <Unit>org.bluetooth.unit.moment_of_force.newton_metre</Unit>
      <BinaryExponent>-5</BinaryExponent>
    </Field>
    <Field name="Wheel Revolution Data - Cumulative Wheel Revolutions">

      <InformativeText>Unitless 
      <br>C1:When present, these fields are always present as a
      pair.</br></InformativeText>
      <Requirement>C1</Requirement>
      <Format>uint32</Format>
      <Unit>org.bluetooth.unit.unitless</Unit>
      <DecimalExponent>0</DecimalExponent>
    </Field>
    <Field name="Wheel Revolution Data - Last Wheel Event Time">
      <InformativeText>Unit is in seconds with a resolution of
      1/2048. 
      <br>C1:When present, these fields are always present as a
      pair.</br></InformativeText>
      <Requirement>C1</Requirement>
      <Format>uint16</Format>
      <Unit>org.bluetooth.unit.time.second</Unit>
      <BinaryExponent>-11</BinaryExponent>
    </Field>
    <Field name="Crank Revolution Data- Cumulative Crank Revolutions">

      <InformativeText>Unitless 
      <br>C2:When present, these fields are always present as a
      pair.</br></InformativeText>
      <Requirement>C2</Requirement>
      <Format>uint16</Format>
      <Unit>org.bluetooth.unit.unitless</Unit>
      <DecimalExponent>0</DecimalExponent>
    </Field>
    <Field name="Crank Revolution Data- Last Crank Event Time">
      <InformativeText>Unit is in seconds with a resolution of
      1/1024. 
      <br>C2:When present, these fields are always present as a
      pair.</br></InformativeText>
      <Requirement>C2</Requirement>
      <Format>uint16</Format>
      <Unit>org.bluetooth.unit.time.second</Unit>
      <BinaryExponent>-10</BinaryExponent>
    </Field>
    <Field name="Extreme Force Magnitudes - Maximum Force Magnitude">

      <InformativeText>Unit is in newtons with a resolution of 1. 
      <br>C3:When present, these fields are always present as a
      pair.</br></InformativeText>
      <Requirement>C3</Requirement>
      <Format>sint16</Format>
      <Unit>org.bluetooth.unit.force.newton</Unit>
      <DecimalExponent>0</DecimalExponent>
    </Field>
    <Field name="Extreme Force Magnitudes - Minimum Force Magnitude">

      <InformativeText>Unit is in newtons with a resolution of 1. 
      <br>C3:When present, these fields are always present as a
      pair.</br></InformativeText>
      <Requirement>C3</Requirement>
      <Format>sint16</Format>
      <Unit>org.bluetooth.unit.force.newton</Unit>
      <DecimalExponent>0</DecimalExponent>
    </Field>
    <Field name="Extreme Torque Magnitudes- Maximum Torque Magnitude">

      <InformativeText>Unit is in newton metres with a resolution
      of 1/32. 
      <br>C4:When present, these fields are always present as a
      pair.</br></InformativeText>
      <Requirement>C4</Requirement>
      <Format>sint16</Format>
      <Unit>org.bluetooth.unit.moment_of_force.newton_metre</Unit>
      <BinaryExponent>-5</BinaryExponent>
    </Field>
    <Field name="Extreme Torque Magnitudes- Minimum Torque Magnitude">

      <InformativeText>Unit is in newton metres with a resolution
      of 1/32. 
      <br>C4:When present, these fields are always present as a
      pair.</br></InformativeText>
      <Requirement>C4</Requirement>
      <Format>sint16</Format>
      <Unit>org.bluetooth.unit.moment_of_force.newton_metre</Unit>
      <BinaryExponent>-5</BinaryExponent>
    </Field>
    <Field name="Extreme Angles - Maximum Angle">
      <InformativeText>Unit is in degrees with a resolution of 1 
      <br>C5: When present, this field and the "Extreme Angles -
      Minimum Angle" field are always present as a pair and are
      concatenated into a UINT24 value (3 octets). As an example,
      if the Maximum Angle is 0xABC and the Minimum Angle is 0x123,
      the transmitted value is 0x123ABC.</br></InformativeText>
      <Requirement>C5</Requirement>
      <Format>uint12</Format>
      <Unit>org.bluetooth.unit.plane_angle.degree</Unit>
      <!-- 2014-07-02 - Added the Description tag to show the informational text per request from SF WG -->
      <Description>When observed with the front wheel to the right
      of the pedals, a value of 0 degrees represents the angle when
      the crank is in the 12 o'clock position and a value of 90
      degrees represents the angle, measured clockwise, when the
      crank points towards the front wheel in a 3 o'clock position.
      The left crank sensor (if fitted) detects the 0? when the
      crank it is attached to is in the 12 o'clock position and the
      right sensor (if fitted) detects the 0? when the crank it is
      attached to is in its 12 o'clock position; thus, there is a
      constant 180? difference between the right crank and the left
      crank position signals.</Description>
      <DecimalExponent>0</DecimalExponent>
    </Field>
    <Field name="Extreme Angles - Minimum Angle">
      <InformativeText>Unit is in degrees with a resolution of 1. 
      <br>C5: When present, this field and the "Extreme Angles -
      Maximum Angle" field are always present as a pair and are
      concatenated into a UINT24 value (3 octets). As an example,
      if the Maximum Angle is 0xABC and the Minimum Angle is 0x123,
      the transmitted value is 0x123ABC.</br></InformativeText>
      <Requirement>C5</Requirement>
      <Format>uint12</Format>
      <Unit>org.bluetooth.unit.plane_angle.degree</Unit>
      <!-- 2014-07-02 - Added the Description tag to show the informational text per request from SF WG -->
      <Description>When observed with the front wheel to the right
      of the pedals, a value of 0 degrees represents the angle when
      the crank is in the 12 o'clock position and a value of 90
      degrees represents the angle, measured clockwise, when the
      crank points towards the front wheel in a 3 o'clock position.
      The left crank sensor (if fitted) detects the 0? when the
      crank it is attached to is in the 12 o'clock position and the
      right sensor (if fitted) detects the 0? when the crank it is
      attached to is in its 12 o'clock position; thus, there is a
      constant 180? difference between the right crank and the left
      crank position signals.</Description>
      <DecimalExponent>0</DecimalExponent>
    </Field>
    <Field name="Top Dead Spot Angle">
      <InformativeText>Unit is in degrees with a resolution of
      1.</InformativeText>
      <Requirement>Optional</Requirement>
      <Format>uint16</Format>
      <Unit>org.bluetooth.unit.plane_angle.degree</Unit>
      <!-- 2014-07-02 - Added the Description tag to show the informational text per request from SF WG -->
      <Description>When observed with the front wheel to the right
      of the pedals, a value of 0 degrees represents the angle when
      the crank is in the 12 o'clock position and a value of 90
      degrees represents the angle, measured clockwise, when the
      crank points towards the front wheel in a 3 o'clock position.
      The left crank sensor (if fitted) detects the 0? when the
      crank it is attached to is in the 12 o'clock position and the
      right sensor (if fitted) detects the 0? when the crank it is
      attached to is in its 12 o'clock position; thus, there is a
      constant 180? difference between the right crank and the left
      crank position signals.</Description>
      <DecimalExponent>0</DecimalExponent>
    </Field>
    <Field name="Bottom Dead Spot Angle">
      <InformativeText>Unit is in degrees with a resolution of
      1.</InformativeText>
      <Requirement>Optional</Requirement>
      <Format>uint16</Format>
      <Unit>org.bluetooth.unit.plane_angle.degree</Unit>
      <!-- 2014-07-02 - Added the Description tag to show the informational text per request from SF WG -->
      <Description>When observed with the front wheel to the right
      of the pedals, a value of 0 degrees represents the angle when
      the crank is in the 12 o'clock position and a value of 90
      degrees represents the angle, measured clockwise, when the
      crank points towards the front wheel in a 3 o'clock position.
      The left crank sensor (if fitted) detects the 0? when the
      crank it is attached to is in the 12 o'clock position and the
      right sensor (if fitted) detects the 0? when the crank it is
      attached to is in its 12 o'clock position; thus, there is a
      constant 180? difference between the right crank and the left
      crank position signals.</Description>
      <DecimalExponent>0</DecimalExponent>
    </Field>
    <Field name="Accumulated Energy">
      <InformativeText>Unit is in kilojoules with a resolution of
      1.</InformativeText>
      <Requirement>Optional</Requirement>
      <Format>uint16</Format>
      <Unit>org.bluetooth.unit.energy.joule</Unit>
      <DecimalExponent>3</DecimalExponent>
    </Field>
  </Value>
  <Note>
    <p>The fields in the above table, reading from top to bottom,
    are shown in the order of LSO to MSO, where LSO = Least
    Significant Octet and MSO = Most Significant Octet. The Least
    Significant Octet represents the eight bits numbered 0 to
    7.</p>
  </Note>
</Characteristic>
*/
