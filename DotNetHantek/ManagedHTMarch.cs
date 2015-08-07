using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MDT.NetLibHantek
{

    // based on https://github.com/rpcope1/Hantek6022API/blob/master/PyHT6022/HTSDKScope.py

    public class ManagedHTMarch {

        //Voltage Division
        public enum THantekVoltDivision {
            _20mv = 0,
            _50mv = 1,
            _100mv = 2,
            _200mv = 3,
            _500mv = 4,
            _1V = 5,
            _2V = 6,
            _5V = 7,
        }

        //Time Division
        /* Commented enum seems to be not used */
        public enum THantekTimeDivision {
            //1016 samples
            _48MS_1NS = 0,	//960, 1, 1
            //_48MS_2NS = 1,	//960, 1, 1
            //_48MS_5NS = 2,	//960, 1, 1
            //_48MS_10NS = 3,	//960, 1, 1
            //_48MS_20NS = 4,	//960, 1, 1
            //_48MS_50NS = 5,	//960, 1, 1
            //_48MS_100NS = 6, //960, 1, 1
            //_48MS_200NS = 7, //960, 1, 1
            //_48MS_500NS = 8, //960, 1, 1
            //_48MS_1US = 9,	//960, 1, 1
            //_48MS_2US = 10,	//960, 1, 1

            //130048 samples
            _16MS_5US = 11,	//800, 1, 1
            _8MS_10US = 12,	//800, 1, 1
            _4MS_20US = 13,	//800. 1, 1
            _1MS_50US = 14,	//500, 1, 1
            //_1MS_100US = 15,	//1000, 1, 1
            //_1MS_200US = 16,	//2000, 1, 1
            //_1MS_500US = 17,	//5000, 1, 1
            //_1MS_1MS = 18,	//10000, 1, 1
            //_1MS_2MS = 19,	//20000, 1, 1

            //523264 samples
            //_1MS_5MS = 20,	//50000, 1, 1
            //_1MS_10MS = 21,	//100000, 1, 1
            //_1MS_20MS = 22,	//200000, 1, 1

            //1047552 samples
            //_1MS_50MS = 23,	//500000, 1, 1
            //_1MS_100MS = 24,	//1000000, 1, 1
            _500K_200MS = 25,//1000000, 1, 1
            _200K_500MS = 26,//1000000, 1, 1
            _100K_1S = 27,	//1000000, 1, 1
            //_100K_2S = 28,	//2000000, 1, 1
            //_100K_5S = 29,	//5000000, 1, 1
            //_100K_10S = 30,	//10000000,1,1
            //_100K_20S = 31,	//20000000,1,1
            //_100K_50S = 32,	//50000000,1,1
            //_100K_100S = 33,	//100000000,1,1
            //_100K_200S = 34,	//200000000,1,1
            //_100K_500S = 35,	//500000000,1,1
            //_100K_1000S = 36,//1000000000,1,1
            //_100K_2000S = 37,//2000000000,1,1
            //_100K_5000S = 38,//-1,1,1
        };

        public enum THantekTrigSweep {
            Auto = 0,
            Normal = 1,
            Single = 2,
        };

        public enum THantekChannelNumber {
            CH1 = 0,
            CH2 = 1,
        }

        public enum THantekTriggerSlope {
            Rise = 0,
            Fall = 1,
        }

        public enum THantekInsertMode {
            StepDValue = 0, // D-value mode
            LineDValue = 1, // Step D-value
            SinXXDValue = 2, // SinX/X D-value
        }


        #region Constants

        public const int CALIBRATION_LEVEL_ARRAY_LENGTH = 32;

        #endregion Constants
        #region IFields

        private ushort m_deviceIndex;

        private THantekVoltDivision m_voltDivCH1;
        private THantekVoltDivision m_voltDivCH2;

        private THantekVoltDivision m_voltDiv;
        private THantekTimeDivision m_timeDiv;

        #endregion IFields
        #region IConstructors

        public ManagedHTMarch(ushort deviceIndex) {
            m_deviceIndex = deviceIndex;
        }

        #endregion IConstructors
        #region IProperties

        public bool IsOpened {
            get {
                return OpenDevice(m_deviceIndex);
            }
        }

        #endregion IProperties
        #region IMethods

        [DllImport("HTMarch.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern short dsoOpenDevice(ushort deviceIndex);

        private bool OpenDevice(ushort device) {
            return dsoOpenDevice(device) == 1;
        }

        [DllImport("HTMarch.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern short dsoSetVoltDIV(ushort deviceIndex, int nCH, int nVoltDIV);

        public bool SetVoltageDivision(THantekChannelNumber channelNumber, THantekVoltDivision voltDiv) {
            m_voltDiv = voltDiv;
            switch (channelNumber) {
                case THantekChannelNumber.CH1:
                    m_voltDivCH1 = voltDiv;
                    break;
                case THantekChannelNumber.CH2:
                    m_voltDivCH2 = voltDiv;
                    break;
            }
            return dsoSetVoltDIV(m_deviceIndex, (int)channelNumber, (int)voltDiv) == 1;
        }

        [DllImport("HTMarch.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern short dsoSetTimeDIV(ushort deviceIndex, int nTimeDIV);

        public bool SetTimeDivision(THantekTimeDivision timeDiv) {
            m_timeDiv = timeDiv;
            return dsoSetTimeDIV(m_deviceIndex, (int)timeDiv) == 1;
        }

        [DllImport("HTMarch.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern short dsoReadHardData(ushort deviceIndex, short* pCH1Data, short* pCH2Data, uint nReadLen, short* pCalLevel, int nCH1VoltDIV, int nCH2VoltDIV, short nTrigSweep, short nTrigSrc, short nTrigLevel, short nSlope, int nTimeDIV, short nHTrigPos, uint nDisLen, uint* nTrigPoint, short nInsertMode);

        public bool ReadHardData(out short[] bufferCH1, out short[] bufferCH2, uint nReadLine, short[] level, THantekTrigSweep trigSweep, THantekChannelNumber trigSource, short trigLevel, THantekTriggerSlope trigSlope, short HTrigPosition, uint drawLength, out uint trigPoint, THantekInsertMode insertMode) {

            bool readSuccess = false;

            bufferCH1 = new short[nReadLine];
            bufferCH2 = new short[nReadLine];

            unsafe {
                uint trigPt = 0;
                fixed (short* pBufferCH1 = bufferCH1)
                fixed (short* pBufferCH2 = bufferCH2)
                fixed (short* pLevel = level) {
                    readSuccess = dsoReadHardData(m_deviceIndex, pBufferCH1, pBufferCH2, nReadLine, pLevel, (int)m_voltDivCH1, (int)m_voltDivCH2, (short)trigSweep, (short)trigSource, trigLevel, (short)trigSlope, (int)m_timeDiv, HTrigPosition, drawLength, &trigPt, (short)insertMode) != -1;
                }
                trigPoint = trigPt;
            }

            return readSuccess;
        }

        /// <summary>
        /// Helper function for converting the data taken from the scope into its true analog representation.
        /// Takes input from scope data, and the scaling factor, with the optional number of points in the
        /// scaling division. 
        /// </summary>
        /// <param name="buffer">Array of analog values read from the scope.</param>
        /// <returns></returns>
        public double[] ConvertReadData(short[] buffer, double scale_points=32.0) {
            double[] trueValues = new double[buffer.Length];
            for (int i = 0; i < buffer.Length; i++) {
                trueValues[i] = buffer[i] / scale_points;
            }
            return trueValues;
        }

        [DllImport("HTMarch.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern short dsoCalibrate(ushort nDeviceIndex, int nTimeDIV, int nCH1VoltDIV, int nCH2VoltDIV, short* pCalLevel);

        public bool Calibrate(THantekTimeDivision timeDiv, THantekVoltDivision voltDivCH1, THantekVoltDivision voltDivCH2, out short[] level) {
            m_timeDiv = timeDiv;
            m_voltDivCH1 = voltDivCH1;
            m_voltDivCH2 = voltDivCH2;

            bool calibrateSucess = false;
            short[] l = new short[CALIBRATION_LEVEL_ARRAY_LENGTH];
            unsafe {
                fixed (short* pLevel = l) {
                    calibrateSucess = dsoCalibrate(m_deviceIndex, (int)m_timeDiv, (int)m_voltDivCH1, (int)m_voltDivCH2, pLevel) == 1;
                }
            }
            level = l;
            return calibrateSucess;
        }

        [DllImport("HTMarch.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern short dsoGetCalLevel(ushort deviceIndex, short* level, short nLen);

        public short[] GetCalibrationLevel() {
            short[] level = new short[CALIBRATION_LEVEL_ARRAY_LENGTH];
            unsafe {
                fixed (short* pLevel = level) {
                    dsoGetCalLevel(m_deviceIndex, pLevel, CALIBRATION_LEVEL_ARRAY_LENGTH);
                }
            }
            return level;
        }

        [DllImport("HTMarch.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        private static unsafe extern short dsoSetCalLevel(ushort deviceIndex, short* level, short nLen);

        public bool SetCalibrationLevel(short[] level) {
            unsafe {
                fixed (short* pLevel = level) {
                    return dsoSetCalLevel(m_deviceIndex, pLevel, CALIBRATION_LEVEL_ARRAY_LENGTH) == 1;
                }
            }
        }

        #endregion IMethods
    }
}
