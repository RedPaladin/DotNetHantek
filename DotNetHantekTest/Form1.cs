using MDT.NetLibHantek;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace NetLibHantekTest {
    public partial class Form1 : Form {
        

        private ManagedHTMarch m_hantek = new ManagedHTMarch(0);

        private ManagedHTMarch.THantekTrigSweep m_trigSweep;

        private short[] m_calLevel = new short[32];

        private Timer m_updateGraph;

        private int m_risingEdge = 0;
        private int m_fallingEdge = 0;

        private double m_lastVoltage = 0;
        
        public Form1() {
            InitializeComponent();

            cbxVoltDiv.Items.AddRange(Enum.GetNames(typeof(ManagedHTMarch.THantekVoltDivision)));
            cbxVoltDiv.SelectedItem = ManagedHTMarch.THantekVoltDivision._2V.ToString();

            cbxTimeDiv.Items.AddRange(Enum.GetNames(typeof(ManagedHTMarch.THantekTimeDivision)));
            cbxTimeDiv.SelectedItem = ManagedHTMarch.THantekTimeDivision._16MS_5US.ToString();

            cbxTrigSweep.Items.AddRange(Enum.GetNames(typeof(ManagedHTMarch.THantekTrigSweep)));
            cbxTrigSweep.SelectedItem = ManagedHTMarch.THantekTrigSweep.Auto.ToString();

            m_updateGraph = new Timer();

            m_updateGraph.Tick += m_updateGraph_Tick;

            m_updateGraph.Interval = 1000;

            m_updateGraph.Start();
        }

        void m_updateGraph_Tick(object sender, EventArgs e) {
            if (!cbxRefresh.Checked) return;

            uint nReadLine = 130048;

            short triggerLevel = short.Parse(tbxTriggerLevel.Text);

            short[] dataCH1 = new short[nReadLine];
            short[] dataCH2 = new short[nReadLine];

            uint trigPoint = 0;

            DateTime start = DateTime.Now;
            Debug.WriteLine("Go !");
            m_hantek.ReadHardData(out dataCH1, out dataCH2, nReadLine, m_calLevel, m_trigSweep, ManagedHTMarch.THantekChannelNumber.CH1, triggerLevel, ManagedHTMarch.THantekTriggerSlope.Rise, 10, nReadLine, out trigPoint, ManagedHTMarch.THantekInsertMode.StepDValue);

            m_risingEdge = 0;
            m_fallingEdge = 0;

            double[] analogValues = m_hantek.ConvertReadData(dataCH1);

            foreach (short v in analogValues) {

                if ((m_lastVoltage < 0.5) && (v > 0.5)) {
                    m_risingEdge++;
                }
                if ((m_lastVoltage > 0.5) && (v < 0.5)) {
                    m_fallingEdge++;
                }

                m_lastVoltage = v;
            }

            Debug.WriteLine(string.Format("Rising Edges = {0} / Falling Edges = {1}", m_risingEdge, m_fallingEdge));

            lblRatio.Text = (DateTime.Now - start).ToString();
            Debug.WriteLine(lblRatio.Text);

            chart1.Series.Clear();

            chart1.ChartAreas[0].AxisY.Maximum = 2;
            chart1.ChartAreas[0].AxisY.Minimum = -2;
            chart1.ChartAreas[0].AxisX.Maximum = nReadLine;

            Series s = chart1.Series.Add("Channel 1");

            s.ChartType = SeriesChartType.Line;

            for (int i = 1; i < analogValues.Length; i++) {
                if (i > trigPoint) {
                    s.Points.AddY(analogValues[i]);
                }
            }
        }

        private void cbxVoltDiv_SelectedIndexChanged(object sender, EventArgs e) {

            string value = cbxVoltDiv.Text;
            if (!string.IsNullOrEmpty(value)) {
                ManagedHTMarch.THantekVoltDivision voltDiv = (ManagedHTMarch.THantekVoltDivision)Enum.Parse(typeof(ManagedHTMarch.THantekVoltDivision), value);

                m_hantek.SetVoltageDivision(ManagedHTMarch.THantekChannelNumber.CH1, voltDiv);

                m_calLevel = m_hantek.GetCalibrationLevel();
                m_hantek.SetCalibrationLevel(m_calLevel);
            }
        }

        private void cbxTimeDiv_SelectedIndexChanged(object sender, EventArgs e) {

            string value = cbxTimeDiv.Text;
            if (!string.IsNullOrEmpty(value)) {
                ManagedHTMarch.THantekTimeDivision timeDiv = (ManagedHTMarch.THantekTimeDivision)Enum.Parse(typeof(ManagedHTMarch.THantekTimeDivision), value);

                m_hantek.SetTimeDivision(timeDiv);

                m_calLevel = m_hantek.GetCalibrationLevel();
                m_hantek.SetCalibrationLevel(m_calLevel);
            }
        }

        private void cbxTrigSweep_SelectedIndexChanged(object sender, EventArgs e) {
            string value = cbxTrigSweep.Text;
            if (!string.IsNullOrEmpty(value)) {
                m_trigSweep = (ManagedHTMarch.THantekTrigSweep)Enum.Parse(typeof(ManagedHTMarch.THantekTrigSweep), value);
            }
        }
    }
}
