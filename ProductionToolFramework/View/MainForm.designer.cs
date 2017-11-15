﻿namespace Demcon.ProductionTool.View
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
            this.tabFatTests = new System.Windows.Forms.TabPage();
            this.tabFatTests1 = new Demcon.ProductionTool.View.TabFatTests();
            this.testPage1 = new Demcon.ProductionTool.View.FatTestPages.TestPage();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabFatTests.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabFatTests
            // 
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
            // tabFatTests1
            // 
            this.tabFatTests1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabFatTests1.Location = new System.Drawing.Point(0, 0);
            this.tabFatTests1.Margin = new System.Windows.Forms.Padding(4);
            this.tabFatTests1.Name = "tabFatTests1";
            this.tabFatTests1.Size = new System.Drawing.Size(1342, 704);
            this.tabFatTests1.TabIndex = 1;
            this.tabFatTests1.TestManager = null;
            // 
            // testPage1
            // 
            this.testPage1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.testPage1.Location = new System.Drawing.Point(0, 0);
            this.testPage1.Margin = new System.Windows.Forms.Padding(4);
            this.testPage1.Name = "testPage1";
            this.testPage1.Size = new System.Drawing.Size(1342, 704);
            this.testPage1.TabIndex = 0;
            this.testPage1.TestManager = null;
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 730);
            this.Controls.Add(this.tabControl);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "HandScan Test Tool (V1.0.0, 14-11-2017)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabFatTests.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabFatTests;
        private TabFatTests tabFatTests1;
        private FatTestPages.TestPage testPage1;
        private System.Windows.Forms.TabControl tabControl;

    }
}

