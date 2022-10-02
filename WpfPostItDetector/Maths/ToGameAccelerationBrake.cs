
using SDKSmartTrainnerAdaptor.Ble.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SDKSmartTrainnerAdaptor.Maths
{
    public static  partial class Maths
    {
        static PidController controllerAcceleration;
        static PidController controllerBrake;
        static int minOut = 0;
        static int maxOut = 100;
        public static void StartToGameAccelerationBrake()
        {
            var xSoc = 20;
            var tSoc = 10;
            var P = 2*xSoc;
            var I = 1.5*tSoc;
            var D = I / 5;

            controllerAcceleration = new PidController(P, I, D, minOut, maxOut); //P=1,I=1,D=1,Output between 0 and 100 
            controllerBrake = new PidController(P, I, D, minOut, maxOut); //P=1,I=1,D=1,Output between 0 and 100 
            
          }


        public static void ToGameAccelerationBrake()
        {
            rootClass.SpeedDiff = rootClass.TargetSpeed - rootClass.GameSpeed;


            if (rootClass.TargetSpeed > 1)
            {
                if (rootClass.TargetSpeed > rootClass.GameSpeed)
                {
                    rootClass.ToGameAcceleration= limit((rootClass.TargetSpeed - rootClass.GameSpeed)*20);
                    rootClass.ToGameBrake = 0;
                }

                if (rootClass.GameSpeed > rootClass.TargetSpeed)
                {
                    rootClass.ToGameBrake = limit((rootClass.GameSpeed - rootClass.TargetSpeed) * 8);
                    rootClass.ToGameAcceleration = 0;
                }           
                    
            }
            else
            {
                rootClass.ToGameAcceleration = 0;
                rootClass.ToGameBrake = 0;
            }


        }

        public static double limit(double source)
        {
            var target = source < minOut ? minOut : source;
             target = source > maxOut ? maxOut : source;

            return target;
        }

        /*
        public static void ToGameAccelerationBrake()
        {

            if (rootClass.TargetSpeed > 1)
            {
                controllerAcceleration.TargetValue = rootClass.TargetSpeed;   //Sensor read. 
                controllerAcceleration.CurrentValue = rootClass.GameSpeed;
                rootClass.ToGameAcceleration = controllerAcceleration.ControlOutput;//Use output to control actuator.
                if (rootClass.GameSpeed > rootClass.TargetSpeed)
                {
                    controllerBrake.TargetValue = rootClass.GameSpeed;//Set target . 
                    controllerBrake.CurrentValue = rootClass.TargetSpeed;//Sensor read. 
                    rootClass.ToGameBrake = controllerBrake.ControlOutput;//Use output to control actuator.
                }
                else
                    rootClass.ToGameBrake = 0;
            } else
            {
                rootClass.ToGameAcceleration = 0;
                rootClass.ToGameBrake = 0;
            }

        }
        */
        public sealed class PidController
        {
            private double processVariable = 0;
            DateTime lastcall;

            public PidController(double GainProportional, double GainIntegral, double GainDerivative, double OutputMax, double OutputMin)
            {
                this.D = GainDerivative;
                this.I = GainIntegral;
                this.P = GainProportional;
                this.OutputMax = OutputMax;
                this.OutputMin = OutputMin;
                lastcall = DateTime.Now;
            }

            /// <summary>
            /// The controller output
            /// </summary>
            public double ControlOutput
            {
                get
                {
                    TimeSpan timeSinceLastUpdate = DateTime.Now - lastcall;
                    lastcall = DateTime.Now;
                    double error = TargetValue - CurrentValue;

                    // integral term calculation
                    IntegralTerm += (I * error * timeSinceLastUpdate.TotalSeconds);
                    IntegralTerm = Clamp(IntegralTerm);

                    // derivative term calculation
                    double dInput = processVariable - ProcessVariableLast;
                    double derivativeTerm = D * (dInput / timeSinceLastUpdate.TotalSeconds);

                    // proportional term calcullation
                    double proportionalTerm = P * error;

                    double output = proportionalTerm + IntegralTerm - derivativeTerm;

                    output = Clamp(output);

                    return output;

                }
            }

            /// <summary>
            /// The derivative term is proportional to the rate of
            /// change of the error
            /// </summary>
            public double D { get; set; } = 0;

            /// <summary>
            /// The integral term is proportional to both the magnitude
            /// of the error and the duration of the error
            /// </summary>
            public double I { get; set; } = 0;

            /// <summary>
            /// The proportional term produces an output value that
            /// is proportional to the current error value
            /// </summary>
            /// <remarks>
            /// Tuning theory and industrial practice indicate that the
            /// proportional term should contribute the bulk of the output change.
            /// </remarks>
            public double P { get; set; } = 0;

            /// <summary>
            /// The max output value the control device can accept.
            /// </summary>
            public double OutputMax { get; private set; } = 0;

            /// <summary>
            /// The minimum ouput value the control device can accept.
            /// </summary>
            public double OutputMin { get; private set; } = 0;

            /// <summary>
            /// Adjustment made by considering the accumulated error over time
            /// </summary>
            /// <remarks>
            /// An alternative formulation of the integral action, is the
            /// proportional-summation-difference used in discrete-time systems
            /// </remarks>
            public double IntegralTerm { get; private set; } = 0;


            /// <summary>
            /// The current value
            /// </summary>
            public double CurrentValue
            {
                get { return processVariable; }
                set
                {
                    ProcessVariableLast = processVariable;
                    processVariable = value;
                }
            }

            /// <summary>
            /// The last reported value (used to calculate the rate of change)
            /// </summary>
            public double ProcessVariableLast { get; private set; } = 0;

            /// <summary>
            /// The desired value
            /// </summary>
            public double TargetValue { get; set; } = 0;

            /// <summary>
            /// Limit a variable to the set OutputMax and OutputMin properties
            /// </summary>
            /// <returns>
            /// A value that is between the OutputMax and OutputMin properties
            /// </returns>
            /// <remarks>
            /// Inspiration from http://stackoverflow.com/questions/3176602/how-to-force-a-number-to-be-in-a-range-in-c
            /// </remarks>
            private double Clamp(double variableToClamp)
            {
                if (variableToClamp <= OutputMin) { return OutputMin; }
                if (variableToClamp >= OutputMax) { return OutputMax; }
                return variableToClamp;
            }
        }

    }
}
