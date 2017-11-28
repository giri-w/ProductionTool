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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabFatTests = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.statusText1 = new System.Windows.Forms.Label();
            this.linkStatus1 = new System.Windows.Forms.LinkLabel();
            this.tabFatTests1 = new Demcon.ProductionTool.View.TabFatTests();
            this.testPage1 = new Demcon.ProductionTool.View.FatTestPages.TestPage();
            this.tabControl.SuspendLayout();
            this.tabFatTests.SuspendLayout();
            this.infoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabFatTests);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1350, 730);
            this.tabControl.TabIndex = 0;
            // 
            // tabFatTests
            // 
            this.tabFatTests.Controls.Add(this.panel1);
            this.tabFatTests.Controls.Add(this.infoPanel);
            this.tabFatTests.Controls.Add(this.tabFatTests1);
            this.tabFatTests.Controls.Add(this.testPage1);
            this.tabFatTests.Location = new System.Drawing.Point(4, 22);
            this.tabFatTests.Name = "tabFatTests";
            this.tabFatTests.Padding = new System.Windows.Forms.Padding(3);
            this.tabFatTests.Size = new System.Drawing.Size(1342, 704);
            this.tabFatTests.TabIndex = 0;
            this.tabFatTests.Text = "FAT tests";
            this.tabFatTests.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::TestToolFramework.Properties.Resources.handScan_Brand;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1342, 84);
            this.panel1.TabIndex = 5;
            // 
            // infoPanel
            // 
            this.infoPanel.BackColor = System.Drawing.Color.White;
            this.infoPanel.Controls.Add(this.statusText1);
            this.infoPanel.Controls.Add(this.linkStatus1);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.infoPanel.Location = new System.Drawing.Point(3, 666);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(1336, 35);
            this.infoPanel.TabIndex = 4;
            // 
            // statusText1
            // 
            this.statusText1.AutoSize = true;
            this.statusText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusText1.Location = new System.Drawing.Point(1235, 9);
            this.statusText1.Name = "statusText1";
            this.statusText1.Size = new System.Drawing.Size(77, 15);
            this.statusText1.TabIndex = 1;
            this.statusText1.Text = "Not Checked";
            // 
            // linkStatus1
            // 
            this.linkStatus1.AutoSize = true;
            this.linkStatus1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkStatus1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkStatus1.Location = new System.Drawing.Point(1120, 9);
            this.linkStatus1.Name = "linkStatus1";
            this.linkStatus1.Size = new System.Drawing.Size(112, 15);
            this.linkStatus1.TabIndex = 0;
            this.linkStatus1.TabStop = true;
            this.linkStatus1.Text = "Connection Status :";
            this.linkStatus1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkStatus_LinkClicked);
            // 
            // tabFatTests1
            // 
            this.tabFatTests1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabFatTests1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(191)))), ((int)(((byte)(255)))));
            this.tabFatTests1.Location = new System.Drawing.Point(0, 84);
            this.tabFatTests1.Margin = new System.Windows.Forms.Padding(4);
            this.tabFatTests1.Name = "tabFatTests1";
            this.tabFatTests1.Size = new System.Drawing.Size(1342, 587);
            this.tabFatTests1.TabIndex = 0;
            this.tabFatTests1.TestManager = null;
            this.tabFatTests1.Load += new System.EventHandler(this.tabFatTests1_Load);
            // 
            // testPage1
            // 
            this.testPage1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.testPage1.Location = new System.Drawing.Point(0, 84);
            this.testPage1.Margin = new System.Windows.Forms.Padding(4);
            this.testPage1.Name = "testPage1";
            this.testPage1.Size = new System.Drawing.Size(1342, 587);
            this.testPage1.TabIndex = 0;
            this.testPage1.TestManager = null;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 730);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "HandScan Test Tool (V1.0.0, 14-11-2017)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabFatTests.ResumeLayout(false);
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabFatTests;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel infoPanel;
        private TabFatTests tabFatTests1;
        private FatTestPages.TestPage testPage1;
        private System.Windows.Forms.LinkLabel linkStatus1;
        private System.Windows.Forms.Label statusText1;
    }
}

