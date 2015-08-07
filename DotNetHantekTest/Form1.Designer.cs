namespace NetLibHantekTest {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cbxVoltDiv = new System.Windows.Forms.ComboBox();
            this.cbxTimeDiv = new System.Windows.Forms.ComboBox();
            this.tbxTriggerLevel = new System.Windows.Forms.TextBox();
            this.cbxTrigSweep = new System.Windows.Forms.ComboBox();
            this.cbxRefresh = new System.Windows.Forms.CheckBox();
            this.lblRatio = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(13, 13);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1062, 421);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // cbxVoltDiv
            // 
            this.cbxVoltDiv.FormattingEnabled = true;
            this.cbxVoltDiv.Location = new System.Drawing.Point(107, 442);
            this.cbxVoltDiv.Name = "cbxVoltDiv";
            this.cbxVoltDiv.Size = new System.Drawing.Size(150, 21);
            this.cbxVoltDiv.TabIndex = 2;
            this.cbxVoltDiv.SelectedIndexChanged += new System.EventHandler(this.cbxVoltDiv_SelectedIndexChanged);
            // 
            // cbxTimeDiv
            // 
            this.cbxTimeDiv.FormattingEnabled = true;
            this.cbxTimeDiv.Location = new System.Drawing.Point(264, 442);
            this.cbxTimeDiv.Name = "cbxTimeDiv";
            this.cbxTimeDiv.Size = new System.Drawing.Size(147, 21);
            this.cbxTimeDiv.TabIndex = 3;
            this.cbxTimeDiv.SelectedIndexChanged += new System.EventHandler(this.cbxTimeDiv_SelectedIndexChanged);
            // 
            // tbxTriggerLevel
            // 
            this.tbxTriggerLevel.Location = new System.Drawing.Point(574, 440);
            this.tbxTriggerLevel.Name = "tbxTriggerLevel";
            this.tbxTriggerLevel.Size = new System.Drawing.Size(100, 20);
            this.tbxTriggerLevel.TabIndex = 4;
            this.tbxTriggerLevel.Text = "10";
            // 
            // cbxTrigSweep
            // 
            this.cbxTrigSweep.FormattingEnabled = true;
            this.cbxTrigSweep.Location = new System.Drawing.Point(418, 440);
            this.cbxTrigSweep.Name = "cbxTrigSweep";
            this.cbxTrigSweep.Size = new System.Drawing.Size(121, 21);
            this.cbxTrigSweep.TabIndex = 5;
            this.cbxTrigSweep.SelectedIndexChanged += new System.EventHandler(this.cbxTrigSweep_SelectedIndexChanged);
            // 
            // cbxRefresh
            // 
            this.cbxRefresh.AutoSize = true;
            this.cbxRefresh.Checked = true;
            this.cbxRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxRefresh.Location = new System.Drawing.Point(13, 445);
            this.cbxRefresh.Name = "cbxRefresh";
            this.cbxRefresh.Size = new System.Drawing.Size(95, 17);
            this.cbxRefresh.TabIndex = 6;
            this.cbxRefresh.Text = "Refresh Graph";
            this.cbxRefresh.UseVisualStyleBackColor = true;
            // 
            // lblRatio
            // 
            this.lblRatio.AutoSize = true;
            this.lblRatio.Location = new System.Drawing.Point(681, 440);
            this.lblRatio.Name = "lblRatio";
            this.lblRatio.Size = new System.Drawing.Size(35, 13);
            this.lblRatio.TabIndex = 7;
            this.lblRatio.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 484);
            this.Controls.Add(this.lblRatio);
            this.Controls.Add(this.cbxRefresh);
            this.Controls.Add(this.cbxTrigSweep);
            this.Controls.Add(this.tbxTriggerLevel);
            this.Controls.Add(this.cbxTimeDiv);
            this.Controls.Add(this.cbxVoltDiv);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ComboBox cbxVoltDiv;
        private System.Windows.Forms.ComboBox cbxTimeDiv;
        private System.Windows.Forms.TextBox tbxTriggerLevel;
        private System.Windows.Forms.ComboBox cbxTrigSweep;
        private System.Windows.Forms.CheckBox cbxRefresh;
        private System.Windows.Forms.Label lblRatio;
    }
}