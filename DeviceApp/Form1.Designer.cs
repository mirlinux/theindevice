namespace DeviceApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbMemTotal = new System.Windows.Forms.Label();
            this.pbMem = new System.Windows.Forms.ProgressBar();
            this.pbCpu = new System.Windows.Forms.ProgressBar();
            this.lbIpAddress = new System.Windows.Forms.Label();
            this.lbMem = new System.Windows.Forms.Label();
            this.lbCpu = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tbLog = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbMemTotal);
            this.groupBox1.Controls.Add(this.pbMem);
            this.groupBox1.Controls.Add(this.pbCpu);
            this.groupBox1.Controls.Add(this.lbIpAddress);
            this.groupBox1.Controls.Add(this.lbMem);
            this.groupBox1.Controls.Add(this.lbCpu);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(31, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(439, 176);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // lbMemTotal
            // 
            this.lbMemTotal.AutoSize = true;
            this.lbMemTotal.Location = new System.Drawing.Point(301, 84);
            this.lbMemTotal.Name = "lbMemTotal";
            this.lbMemTotal.Size = new System.Drawing.Size(52, 15);
            this.lbMemTotal.TabIndex = 7;
            this.lbMemTotal.Text = "( 0 MB )";
            // 
            // pbMem
            // 
            this.pbMem.Location = new System.Drawing.Point(118, 76);
            this.pbMem.Name = "pbMem";
            this.pbMem.Size = new System.Drawing.Size(105, 23);
            this.pbMem.TabIndex = 6;
            // 
            // pbCpu
            // 
            this.pbCpu.Location = new System.Drawing.Point(118, 41);
            this.pbCpu.Name = "pbCpu";
            this.pbCpu.Size = new System.Drawing.Size(105, 23);
            this.pbCpu.TabIndex = 5;
            // 
            // lbIpAddress
            // 
            this.lbIpAddress.AutoSize = true;
            this.lbIpAddress.Location = new System.Drawing.Point(348, 19);
            this.lbIpAddress.Name = "lbIpAddress";
            this.lbIpAddress.Size = new System.Drawing.Size(44, 15);
            this.lbIpAddress.TabIndex = 4;
            this.lbIpAddress.Text = "0.0.0.0";
            // 
            // lbMem
            // 
            this.lbMem.AutoSize = true;
            this.lbMem.Location = new System.Drawing.Point(246, 84);
            this.lbMem.Name = "lbMem";
            this.lbMem.Size = new System.Drawing.Size(36, 15);
            this.lbMem.TabIndex = 3;
            this.lbMem.Text = "0 MB";
            // 
            // lbCpu
            // 
            this.lbCpu.AutoSize = true;
            this.lbCpu.Location = new System.Drawing.Point(246, 43);
            this.lbCpu.Name = "lbCpu";
            this.lbCpu.Size = new System.Drawing.Size(24, 15);
            this.lbCpu.TabIndex = 2;
            this.lbCpu.Text = "0%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mem 사용량";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "CPU 사용량";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(31, 249);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(439, 171);
            this.tbLog.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 432);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBox1;
        private Label lbIpAddress;
        private Label lbMem;
        private Label lbCpu;
        private Label label2;
        private Label label1;
        private System.Windows.Forms.Timer timer1;
        private Label lbMemTotal;
        private ProgressBar pbMem;
        private ProgressBar pbCpu;
        private TextBox tbLog;
    }
}