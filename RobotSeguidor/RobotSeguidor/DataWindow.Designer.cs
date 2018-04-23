namespace RobotSeguidor
{
    partial class DataWindow
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
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.lbDistanceHCSR04 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.Eplb = new System.Windows.Forms.Label();
            this.PWMlb = new System.Windows.Forms.Label();
            this.Edlb = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.Kptxb = new System.Windows.Forms.TextBox();
            this.Kitxb = new System.Windows.Forms.TextBox();
            this.Kdtxb = new System.Windows.Forms.TextBox();
            this.Eilb = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(101, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sensor HC-SR04";
            // 
            // lbDistanceHCSR04
            // 
            this.lbDistanceHCSR04.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbDistanceHCSR04.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDistanceHCSR04.Font = new System.Drawing.Font("Modern No. 20", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDistanceHCSR04.Location = new System.Drawing.Point(287, 27);
            this.lbDistanceHCSR04.Name = "lbDistanceHCSR04";
            this.lbDistanceHCSR04.Size = new System.Drawing.Size(189, 86);
            this.lbDistanceHCSR04.TabIndex = 3;
            this.lbDistanceHCSR04.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Modern No. 20", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(507, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 38);
            this.label4.TabIndex = 5;
            this.label4.Text = "[cm]";
            // 
            // Eplb
            // 
            this.Eplb.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Eplb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Eplb.Font = new System.Drawing.Font("Modern No. 20", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Eplb.Location = new System.Drawing.Point(122, 181);
            this.Eplb.Name = "Eplb";
            this.Eplb.Size = new System.Drawing.Size(111, 47);
            this.Eplb.TabIndex = 6;
            this.Eplb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PWMlb
            // 
            this.PWMlb.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.PWMlb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PWMlb.Font = new System.Drawing.Font("Modern No. 20", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PWMlb.Location = new System.Drawing.Point(301, 361);
            this.PWMlb.Name = "PWMlb";
            this.PWMlb.Size = new System.Drawing.Size(138, 62);
            this.PWMlb.TabIndex = 8;
            this.PWMlb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Edlb
            // 
            this.Edlb.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Edlb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Edlb.Font = new System.Drawing.Font("Modern No. 20", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Edlb.Location = new System.Drawing.Point(541, 132);
            this.Edlb.Name = "Edlb";
            this.Edlb.Size = new System.Drawing.Size(111, 47);
            this.Edlb.TabIndex = 10;
            this.Edlb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(68, 195);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 21);
            this.label9.TabIndex = 12;
            this.label9.Text = "Ep";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(68, 271);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 21);
            this.label10.TabIndex = 13;
            this.label10.Text = "Kp";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(227, 390);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 21);
            this.label11.TabIndex = 14;
            this.label11.Text = "PWM";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(272, 271);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 21);
            this.label12.TabIndex = 15;
            this.label12.Text = "Ki";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(445, 145);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 21);
            this.label13.TabIndex = 16;
            this.label13.Text = "Edbefore";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(478, 271);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 21);
            this.label14.TabIndex = 17;
            this.label14.Text = "Kd";
            // 
            // Kptxb
            // 
            this.Kptxb.Font = new System.Drawing.Font("Modern No. 20", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Kptxb.Location = new System.Drawing.Point(122, 257);
            this.Kptxb.Multiline = true;
            this.Kptxb.Name = "Kptxb";
            this.Kptxb.Size = new System.Drawing.Size(111, 54);
            this.Kptxb.TabIndex = 18;
            this.Kptxb.Text = "0";
            this.Kptxb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Kptxb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Kptxb_KeyDown);
            // 
            // Kitxb
            // 
            this.Kitxb.Font = new System.Drawing.Font("Modern No. 20", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Kitxb.Location = new System.Drawing.Point(315, 257);
            this.Kitxb.Multiline = true;
            this.Kitxb.Name = "Kitxb";
            this.Kitxb.Size = new System.Drawing.Size(113, 54);
            this.Kitxb.TabIndex = 19;
            this.Kitxb.Text = "0";
            this.Kitxb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Kitxb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Kitxb_KeyDown);
            // 
            // Kdtxb
            // 
            this.Kdtxb.Font = new System.Drawing.Font("Modern No. 20", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Kdtxb.Location = new System.Drawing.Point(541, 257);
            this.Kdtxb.Multiline = true;
            this.Kdtxb.Name = "Kdtxb";
            this.Kdtxb.Size = new System.Drawing.Size(111, 54);
            this.Kdtxb.TabIndex = 20;
            this.Kdtxb.Text = "0";
            this.Kdtxb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Kdtxb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Kdtxb_KeyDown);
            // 
            // Eilb
            // 
            this.Eilb.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Eilb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Eilb.Font = new System.Drawing.Font("Modern No. 20", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Eilb.Location = new System.Drawing.Point(315, 181);
            this.Eilb.Name = "Eilb";
            this.Eilb.Size = new System.Drawing.Size(113, 47);
            this.Eilb.TabIndex = 21;
            this.Eilb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(260, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 21);
            this.label3.TabIndex = 22;
            this.label3.Text = "Eint";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Modern No. 20", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(541, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 47);
            this.label5.TabIndex = 23;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(444, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 21);
            this.label6.TabIndex = 24;
            this.label6.Text = "Edcurrent";
            // 
            // DataWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(743, 537);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Eilb);
            this.Controls.Add(this.Kdtxb);
            this.Controls.Add(this.Kitxb);
            this.Controls.Add(this.Kptxb);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.Edlb);
            this.Controls.Add(this.PWMlb);
            this.Controls.Add(this.Eplb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbDistanceHCSR04);
            this.Controls.Add(this.label2);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Location = new System.Drawing.Point(900, 250);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SensoresFrontales";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.IO.Ports.SerialPort serialPort1;
        public System.Windows.Forms.Label lbDistanceHCSR04;
        public System.Windows.Forms.Label Eplb;
        public System.Windows.Forms.Label PWMlb;
        public System.Windows.Forms.Label Edlb;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.TextBox Kptxb;
        public System.Windows.Forms.TextBox Kitxb;
        public System.Windows.Forms.TextBox Kdtxb;
        public System.Windows.Forms.Label Eilb;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}