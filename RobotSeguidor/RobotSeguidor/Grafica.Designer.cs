namespace RobotSeguidor
{
    partial class Grafica
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.GraphicDistance = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.GraphicDistance)).BeginInit();
            this.SuspendLayout();
            // 
            // GraphicDistance
            // 
            chartArea1.Name = "ChartArea1";
            this.GraphicDistance.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.GraphicDistance.Legends.Add(legend1);
            this.GraphicDistance.Location = new System.Drawing.Point(0, 0);
            this.GraphicDistance.Name = "GraphicDistance";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            this.GraphicDistance.Series.Add(series1);
            this.GraphicDistance.Series.Add(series2);
            this.GraphicDistance.Size = new System.Drawing.Size(1067, 734);
            this.GraphicDistance.TabIndex = 0;
            this.GraphicDistance.Text = "chart1";
            this.GraphicDistance.Visible = false;
            // 
            // Grafica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 735);
            this.Controls.Add(this.GraphicDistance);
            this.Name = "Grafica";
            this.Text = "Grafica";
            ((System.ComponentModel.ISupportInitialize)(this.GraphicDistance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart GraphicDistance;
    }
}