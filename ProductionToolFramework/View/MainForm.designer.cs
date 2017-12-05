namespace Demcon.ProductionTool.View
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.progressResult = new System.Windows.Forms.Label();
            this.progressBarValue = new System.Windows.Forms.ProgressBar();
            this.btnHelp = new System.Windows.Forms.Button();
            this.workInfo = new System.Windows.Forms.Label();
            this.serialInfo = new System.Windows.Forms.Label();
            this.operatorInfo = new System.Windows.Forms.Label();
            this.statusText1 = new System.Windows.Forms.Label();
            this.linkStatus1 = new System.Windows.Forms.LinkLabel();
            this.testPage1 = new Demcon.ProductionTool.View.FatTestPages.TestPage();
            this.tabFatTests1 = new Demcon.ProductionTool.View.TabFatTests();
            this.infoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::TestToolFramework.Properties.Resources.handScan_Brand2;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1352, 88);
            this.panel1.TabIndex = 9;
            // 
            // infoPanel
            // 
            this.infoPanel.BackColor = System.Drawing.Color.White;
            this.infoPanel.BackgroundImage = global::TestToolFramework.Properties.Resources.statusBar;
            this.infoPanel.Controls.Add(this.progressResult);
            this.infoPanel.Controls.Add(this.progressBarValue);
            this.infoPanel.Controls.Add(this.btnHelp);
            this.infoPanel.Controls.Add(this.workInfo);
            this.infoPanel.Controls.Add(this.serialInfo);
            this.infoPanel.Controls.Add(this.operatorInfo);
            this.infoPanel.Controls.Add(this.statusText1);
            this.infoPanel.Controls.Add(this.linkStatus1);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.infoPanel.Location = new System.Drawing.Point(0, 654);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(1350, 35);
            this.infoPanel.TabIndex = 8;
            // 
            // progressResult
            // 
            this.progressResult.AutoSize = true;
            this.progressResult.BackColor = System.Drawing.Color.Transparent;
            this.progressResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressResult.ForeColor = System.Drawing.Color.White;
            this.progressResult.Location = new System.Drawing.Point(847, 10);
            this.progressResult.Name = "progressResult";
            this.progressResult.Size = new System.Drawing.Size(109, 15);
            this.progressResult.TabIndex = 7;
            this.progressResult.Text = "Progress Result";
            this.progressResult.Visible = false;
            // 
            // progressBarValue
            // 
            this.progressBarValue.Location = new System.Drawing.Point(688, 9);
            this.progressBarValue.Name = "progressBarValue";
            this.progressBarValue.Size = new System.Drawing.Size(138, 19);
            this.progressBarValue.TabIndex = 6;
            this.progressBarValue.Visible = false;
            // 
            // btnHelp
            // 
            this.btnHelp.BackColor = System.Drawing.Color.Transparent;
            this.btnHelp.BackgroundImage = global::TestToolFramework.Properties.Resources.btnHelp;
            this.btnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.ForeColor = System.Drawing.Color.Transparent;
            this.btnHelp.Location = new System.Drawing.Point(12, 5);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(23, 23);
            this.btnHelp.TabIndex = 5;
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // workInfo
            // 
            this.workInfo.AutoSize = true;
            this.workInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.workInfo.Location = new System.Drawing.Point(249, 9);
            this.workInfo.Name = "workInfo";
            this.workInfo.Size = new System.Drawing.Size(97, 15);
            this.workInfo.TabIndex = 4;
            this.workInfo.Text = "Work Order : N/A";
            // 
            // serialInfo
            // 
            this.serialInfo.AutoSize = true;
            this.serialInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serialInfo.Location = new System.Drawing.Point(48, 9);
            this.serialInfo.Name = "serialInfo";
            this.serialInfo.Size = new System.Drawing.Size(115, 15);
            this.serialInfo.TabIndex = 3;
            this.serialInfo.Text = "Serial Number : N/A";
            // 
            // operatorInfo
            // 
            this.operatorInfo.AutoSize = true;
            this.operatorInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.operatorInfo.Location = new System.Drawing.Point(387, 9);
            this.operatorInfo.Name = "operatorInfo";
            this.operatorInfo.Size = new System.Drawing.Size(83, 15);
            this.operatorInfo.TabIndex = 2;
            this.operatorInfo.Text = "Operator : N/A";
            // 
            // statusText1
            // 
            this.statusText1.AutoSize = true;
            this.statusText1.BackColor = System.Drawing.Color.Transparent;
            this.statusText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusText1.ForeColor = System.Drawing.Color.White;
            this.statusText1.Location = new System.Drawing.Point(1235, 9);
            this.statusText1.Name = "statusText1";
            this.statusText1.Size = new System.Drawing.Size(88, 15);
            this.statusText1.TabIndex = 1;
            this.statusText1.Text = "Not Checked";
            // 
            // linkStatus1
            // 
            this.linkStatus1.AutoSize = true;
            this.linkStatus1.BackColor = System.Drawing.Color.Transparent;
            this.linkStatus1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkStatus1.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.linkStatus1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkStatus1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(161)))), ((int)(((byte)(175)))));
            this.linkStatus1.Location = new System.Drawing.Point(1098, 8);
            this.linkStatus1.Name = "linkStatus1";
            this.linkStatus1.Size = new System.Drawing.Size(131, 15);
            this.linkStatus1.TabIndex = 0;
            this.linkStatus1.TabStop = true;
            this.linkStatus1.Text = "Connection Status :";
            this.linkStatus1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkStatus_LinkClicked);
            // 
            // testPage1
            // 
            this.testPage1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.testPage1.Location = new System.Drawing.Point(0, 83);
            this.testPage1.Margin = new System.Windows.Forms.Padding(4);
            this.testPage1.Name = "testPage1";
            this.testPage1.Size = new System.Drawing.Size(1349, 569);
            this.testPage1.TabIndex = 7;
            this.testPage1.TestManager = null;
            // 
            // tabFatTests1
            // 
            this.tabFatTests1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabFatTests1.BackColor = System.Drawing.Color.White;
            this.tabFatTests1.Location = new System.Drawing.Point(0, 83);
            this.tabFatTests1.Margin = new System.Windows.Forms.Padding(4);
            this.tabFatTests1.Name = "tabFatTests1";
            this.tabFatTests1.Size = new System.Drawing.Size(1352, 569);
            this.tabFatTests1.TabIndex = 6;
            this.tabFatTests1.TestManager = null;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 689);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.infoPanel);
            this.Controls.Add(this.tabFatTests1);
            this.Controls.Add(this.testPage1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HandScan Test Tool (V1.0.0, 14-11-2017)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Label statusText1;
        private System.Windows.Forms.LinkLabel linkStatus1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel infoPanel;
        private TabFatTests tabFatTests1;
        private FatTestPages.TestPage testPage1;
        private System.Windows.Forms.Label workInfo;
        private System.Windows.Forms.Label serialInfo;
        private System.Windows.Forms.Label operatorInfo;
        private System.Windows.Forms.Button btnHelp;
        public System.Windows.Forms.Label progressResult;
        public System.Windows.Forms.ProgressBar progressBarValue;
    }
}

