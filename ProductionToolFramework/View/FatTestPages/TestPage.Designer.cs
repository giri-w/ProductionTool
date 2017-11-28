using System;
using System.ComponentModel;

namespace Demcon.ProductionTool.View.FatTestPages
{
    partial class TestPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.DescriptionPanel = new System.Windows.Forms.Panel();
            this.ButtonsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.OKButton = new System.Windows.Forms.Button();
            this.YesButton = new System.Windows.Forms.Button();
            this.NoButton = new System.Windows.Forms.Button();
            this.FinishedButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.BackButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.stepDescriptionText = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.varLabel1 = new System.Windows.Forms.Label();
            this.varBox1 = new System.Windows.Forms.TextBox();
            this.varLabel2 = new System.Windows.Forms.Label();
            this.varBox2 = new System.Windows.Forms.TextBox();
            this.varLabel3 = new System.Windows.Forms.Label();
            this.varBox3 = new System.Windows.Forms.TextBox();
            this.varLabel4 = new System.Windows.Forms.Label();
            this.varBox4 = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.AnalyzeButton = new System.Windows.Forms.Button();
            this.RetryButton = new System.Windows.Forms.Button();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.TestNameLabel = new System.Windows.Forms.Label();
            this.TestListPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.IDLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TestStepListBox = new System.Windows.Forms.FlowLayoutPanel();
            this.TestListBox = new System.Windows.Forms.FlowLayoutPanel();
            this.SequenceNameLabel = new System.Windows.Forms.Label();
            this.ImagePanel = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressLabel = new System.Windows.Forms.Label();
            this.SupportingImageBox = new System.Windows.Forms.PictureBox();
            this.ResultsPanel = new System.Windows.Forms.Panel();
            this.ResultsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.DescriptionPanel.SuspendLayout();
            this.ButtonsFlowLayoutPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.TestListPanel.SuspendLayout();
            this.ImagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SupportingImageBox)).BeginInit();
            this.ResultsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 600F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.DescriptionPanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.TestListPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ImagePanel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ResultsPanel, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1133, 824);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // DescriptionPanel
            // 
            this.DescriptionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DescriptionPanel.BackColor = System.Drawing.SystemColors.Control;
            this.DescriptionPanel.Controls.Add(this.ButtonsFlowLayoutPanel);
            this.DescriptionPanel.Controls.Add(this.panel1);
            this.DescriptionPanel.Controls.Add(this.flowLayoutPanel3);
            this.DescriptionPanel.Controls.Add(this.flowLayoutPanel2);
            this.DescriptionPanel.Controls.Add(this.flowLayoutPanel1);
            this.DescriptionPanel.Controls.Add(this.TestNameLabel);
            this.DescriptionPanel.Location = new System.Drawing.Point(603, 3);
            this.DescriptionPanel.Name = "DescriptionPanel";
            this.DescriptionPanel.Size = new System.Drawing.Size(527, 406);
            this.DescriptionPanel.TabIndex = 0;
            // 
            // ButtonsFlowLayoutPanel
            // 
            this.ButtonsFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonsFlowLayoutPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonsFlowLayoutPanel.Controls.Add(this.OKButton);
            this.ButtonsFlowLayoutPanel.Controls.Add(this.YesButton);
            this.ButtonsFlowLayoutPanel.Controls.Add(this.NoButton);
            this.ButtonsFlowLayoutPanel.Controls.Add(this.FinishedButton);
            this.ButtonsFlowLayoutPanel.Controls.Add(this.NextButton);
            this.ButtonsFlowLayoutPanel.Controls.Add(this.BackButton);
            this.ButtonsFlowLayoutPanel.Controls.Add(this.CancelButton);
            this.ButtonsFlowLayoutPanel.Location = new System.Drawing.Point(3, 350);
            this.ButtonsFlowLayoutPanel.Name = "ButtonsFlowLayoutPanel";
            this.ButtonsFlowLayoutPanel.Size = new System.Drawing.Size(524, 53);
            this.ButtonsFlowLayoutPanel.TabIndex = 0;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(3, 3);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(120, 46);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // YesButton
            // 
            this.YesButton.Location = new System.Drawing.Point(129, 3);
            this.YesButton.Name = "YesButton";
            this.YesButton.Size = new System.Drawing.Size(120, 46);
            this.YesButton.TabIndex = 1;
            this.YesButton.Text = "Yes";
            this.YesButton.UseVisualStyleBackColor = true;
            this.YesButton.Click += new System.EventHandler(this.YesButton_Click);
            // 
            // NoButton
            // 
            this.NoButton.Location = new System.Drawing.Point(255, 3);
            this.NoButton.Name = "NoButton";
            this.NoButton.Size = new System.Drawing.Size(120, 46);
            this.NoButton.TabIndex = 2;
            this.NoButton.Text = "No";
            this.NoButton.UseVisualStyleBackColor = true;
            this.NoButton.Click += new System.EventHandler(this.NoButton_Click);
            // 
            // FinishedButton
            // 
            this.FinishedButton.Location = new System.Drawing.Point(381, 3);
            this.FinishedButton.Name = "FinishedButton";
            this.FinishedButton.Size = new System.Drawing.Size(120, 46);
            this.FinishedButton.TabIndex = 4;
            this.FinishedButton.Text = "Finished";
            this.FinishedButton.UseVisualStyleBackColor = true;
            this.FinishedButton.Click += new System.EventHandler(this.FinishedButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(3, 55);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(120, 46);
            this.NextButton.TabIndex = 6;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // BackButton
            // 
            this.BackButton.Location = new System.Drawing.Point(129, 55);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(120, 46);
            this.BackButton.TabIndex = 5;
            this.BackButton.Text = "Back";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(255, 55);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(120, 46);
            this.CancelButton.TabIndex = 9;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click_1);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.stepDescriptionText);
            this.panel1.Location = new System.Drawing.Point(3, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(527, 197);
            this.panel1.TabIndex = 7;
            // 
            // stepDescriptionText
            // 
            this.stepDescriptionText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.stepDescriptionText.Cursor = System.Windows.Forms.Cursors.Default;
            this.stepDescriptionText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepDescriptionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stepDescriptionText.Location = new System.Drawing.Point(0, 0);
            this.stepDescriptionText.Name = "stepDescriptionText";
            this.stepDescriptionText.ReadOnly = true;
            this.stepDescriptionText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.stepDescriptionText.Size = new System.Drawing.Size(527, 197);
            this.stepDescriptionText.TabIndex = 0;
            this.stepDescriptionText.Text = "Step Description";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 34);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel3.TabIndex = 6;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel2.Controls.Add(this.varLabel1);
            this.flowLayoutPanel2.Controls.Add(this.varBox1);
            this.flowLayoutPanel2.Controls.Add(this.varLabel2);
            this.flowLayoutPanel2.Controls.Add(this.varBox2);
            this.flowLayoutPanel2.Controls.Add(this.varLabel3);
            this.flowLayoutPanel2.Controls.Add(this.varBox3);
            this.flowLayoutPanel2.Controls.Add(this.varLabel4);
            this.flowLayoutPanel2.Controls.Add(this.varBox4);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 266);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(522, 28);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // varLabel1
            // 
            this.varLabel1.AutoSize = true;
            this.varLabel1.Location = new System.Drawing.Point(3, 0);
            this.varLabel1.Name = "varLabel1";
            this.varLabel1.Size = new System.Drawing.Size(28, 13);
            this.varLabel1.TabIndex = 8;
            this.varLabel1.Text = "var1";
            this.varLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // varBox1
            // 
            this.varBox1.Location = new System.Drawing.Point(37, 3);
            this.varBox1.Name = "varBox1";
            this.varBox1.Size = new System.Drawing.Size(100, 20);
            this.varBox1.TabIndex = 9;
            // 
            // varLabel2
            // 
            this.varLabel2.AutoSize = true;
            this.varLabel2.Location = new System.Drawing.Point(143, 0);
            this.varLabel2.Name = "varLabel2";
            this.varLabel2.Size = new System.Drawing.Size(28, 13);
            this.varLabel2.TabIndex = 10;
            this.varLabel2.Text = "var2";
            // 
            // varBox2
            // 
            this.varBox2.Location = new System.Drawing.Point(177, 3);
            this.varBox2.Name = "varBox2";
            this.varBox2.Size = new System.Drawing.Size(100, 20);
            this.varBox2.TabIndex = 11;
            // 
            // varLabel3
            // 
            this.varLabel3.AutoSize = true;
            this.varLabel3.Location = new System.Drawing.Point(283, 0);
            this.varLabel3.Name = "varLabel3";
            this.varLabel3.Size = new System.Drawing.Size(28, 13);
            this.varLabel3.TabIndex = 12;
            this.varLabel3.Text = "var3";
            // 
            // varBox3
            // 
            this.varBox3.Location = new System.Drawing.Point(317, 3);
            this.varBox3.Name = "varBox3";
            this.varBox3.Size = new System.Drawing.Size(100, 20);
            this.varBox3.TabIndex = 13;
            // 
            // varLabel4
            // 
            this.varLabel4.AutoSize = true;
            this.varLabel4.Location = new System.Drawing.Point(423, 0);
            this.varLabel4.Name = "varLabel4";
            this.varLabel4.Size = new System.Drawing.Size(28, 13);
            this.varLabel4.TabIndex = 14;
            this.varLabel4.Text = "var4";
            // 
            // varBox4
            // 
            this.varBox4.Location = new System.Drawing.Point(3, 29);
            this.varBox4.Name = "varBox4";
            this.varBox4.Size = new System.Drawing.Size(100, 20);
            this.varBox4.TabIndex = 15;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.Controls.Add(this.BrowseButton);
            this.flowLayoutPanel1.Controls.Add(this.AnalyzeButton);
            this.flowLayoutPanel1.Controls.Add(this.RetryButton);
            this.flowLayoutPanel1.Controls.Add(this.DownloadButton);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 295);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(522, 50);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // BrowseButton
            // 
            this.BrowseButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BrowseButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.BrowseButton.Location = new System.Drawing.Point(3, 3);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(120, 46);
            this.BrowseButton.TabIndex = 0;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = false;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // AnalyzeButton
            // 
            this.AnalyzeButton.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.AnalyzeButton.Location = new System.Drawing.Point(129, 3);
            this.AnalyzeButton.Name = "AnalyzeButton";
            this.AnalyzeButton.Size = new System.Drawing.Size(120, 46);
            this.AnalyzeButton.TabIndex = 16;
            this.AnalyzeButton.Text = "Process";
            this.AnalyzeButton.UseVisualStyleBackColor = false;
            this.AnalyzeButton.Click += new System.EventHandler(this.AnalyzeButton_Click);
            // 
            // RetryButton
            // 
            this.RetryButton.BackColor = System.Drawing.Color.Goldenrod;
            this.RetryButton.Location = new System.Drawing.Point(255, 3);
            this.RetryButton.Name = "RetryButton";
            this.RetryButton.Size = new System.Drawing.Size(120, 46);
            this.RetryButton.TabIndex = 1;
            this.RetryButton.Text = "Update";
            this.RetryButton.UseVisualStyleBackColor = false;
            this.RetryButton.Click += new System.EventHandler(this.RetryButton_Click);
            // 
            // DownloadButton
            // 
            this.DownloadButton.BackColor = System.Drawing.Color.Lime;
            this.DownloadButton.Location = new System.Drawing.Point(381, 3);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(120, 46);
            this.DownloadButton.TabIndex = 17;
            this.DownloadButton.Text = "Download";
            this.DownloadButton.UseVisualStyleBackColor = false;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // TestNameLabel
            // 
            this.TestNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TestNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestNameLabel.Location = new System.Drawing.Point(3, 3);
            this.TestNameLabel.Name = "TestNameLabel";
            this.TestNameLabel.Size = new System.Drawing.Size(521, 29);
            this.TestNameLabel.TabIndex = 1;
            this.TestNameLabel.Text = "Test Name";
            // 
            // TestListPanel
            // 
            this.TestListPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TestListPanel.BackColor = System.Drawing.SystemColors.Control;
            this.TestListPanel.Controls.Add(this.label2);
            this.TestListPanel.Controls.Add(this.IDLabel);
            this.TestListPanel.Controls.Add(this.label1);
            this.TestListPanel.Controls.Add(this.TestStepListBox);
            this.TestListPanel.Controls.Add(this.TestListBox);
            this.TestListPanel.Controls.Add(this.SequenceNameLabel);
            this.TestListPanel.Location = new System.Drawing.Point(3, 3);
            this.TestListPanel.Name = "TestListPanel";
            this.TestListPanel.Size = new System.Drawing.Size(594, 406);
            this.TestListPanel.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(215, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Steps";
            // 
            // IDLabel
            // 
            this.IDLabel.AutoSize = true;
            this.IDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDLabel.Location = new System.Drawing.Point(5, 34);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(89, 16);
            this.IDLabel.TabIndex = 7;
            this.IDLabel.Text = "Nano Core ID";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Tests";
            // 
            // TestStepListBox
            // 
            this.TestStepListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TestStepListBox.AutoScroll = true;
            this.TestStepListBox.BackColor = System.Drawing.SystemColors.Window;
            this.TestStepListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TestStepListBox.Location = new System.Drawing.Point(218, 75);
            this.TestStepListBox.Margin = new System.Windows.Forms.Padding(0);
            this.TestStepListBox.Name = "TestStepListBox";
            this.TestStepListBox.Size = new System.Drawing.Size(373, 323);
            this.TestStepListBox.TabIndex = 6;
            // 
            // TestListBox
            // 
            this.TestListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TestListBox.AutoScroll = true;
            this.TestListBox.BackColor = System.Drawing.SystemColors.Window;
            this.TestListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TestListBox.Location = new System.Drawing.Point(8, 75);
            this.TestListBox.Margin = new System.Windows.Forms.Padding(0);
            this.TestListBox.Name = "TestListBox";
            this.TestListBox.Size = new System.Drawing.Size(200, 323);
            this.TestListBox.TabIndex = 5;
            // 
            // SequenceNameLabel
            // 
            this.SequenceNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SequenceNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SequenceNameLabel.Location = new System.Drawing.Point(2, 5);
            this.SequenceNameLabel.Name = "SequenceNameLabel";
            this.SequenceNameLabel.Size = new System.Drawing.Size(583, 29);
            this.SequenceNameLabel.TabIndex = 0;
            this.SequenceNameLabel.Text = "Sequence Name";
            // 
            // ImagePanel
            // 
            this.ImagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImagePanel.BackColor = System.Drawing.SystemColors.Control;
            this.ImagePanel.Controls.Add(this.progressBar1);
            this.ImagePanel.Controls.Add(this.progressLabel);
            this.ImagePanel.Controls.Add(this.SupportingImageBox);
            this.ImagePanel.Location = new System.Drawing.Point(603, 415);
            this.ImagePanel.Name = "ImagePanel";
            this.ImagePanel.Size = new System.Drawing.Size(527, 406);
            this.ImagePanel.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(3, 380);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(518, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressLabel.Location = new System.Drawing.Point(4, 13);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(13, 20);
            this.progressLabel.TabIndex = 1;
            this.progressLabel.Text = " ";
            // 
            // SupportingImageBox
            // 
            this.SupportingImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SupportingImageBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SupportingImageBox.Location = new System.Drawing.Point(3, 36);
            this.SupportingImageBox.Name = "SupportingImageBox";
            this.SupportingImageBox.Size = new System.Drawing.Size(521, 338);
            this.SupportingImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SupportingImageBox.TabIndex = 0;
            this.SupportingImageBox.TabStop = false;
            this.SupportingImageBox.Click += new System.EventHandler(this.SupportingImageBox_Click);
            // 
            // ResultsPanel
            // 
            this.ResultsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ResultsPanel.Controls.Add(this.ResultsFlowLayoutPanel);
            this.ResultsPanel.Controls.Add(this.label3);
            this.ResultsPanel.Location = new System.Drawing.Point(3, 415);
            this.ResultsPanel.Name = "ResultsPanel";
            this.ResultsPanel.Size = new System.Drawing.Size(594, 406);
            this.ResultsPanel.TabIndex = 1;
            // 
            // ResultsFlowLayoutPanel
            // 
            this.ResultsFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultsFlowLayoutPanel.AutoScroll = true;
            this.ResultsFlowLayoutPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResultsFlowLayoutPanel.Location = new System.Drawing.Point(8, 22);
            this.ResultsFlowLayoutPanel.Name = "ResultsFlowLayoutPanel";
            this.ResultsFlowLayoutPanel.Size = new System.Drawing.Size(583, 381);
            this.ResultsFlowLayoutPanel.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Results";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // TestPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TestPage";
            this.Size = new System.Drawing.Size(1136, 830);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.DescriptionPanel.ResumeLayout(false);
            this.DescriptionPanel.PerformLayout();
            this.ButtonsFlowLayoutPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.TestListPanel.ResumeLayout(false);
            this.TestListPanel.PerformLayout();
            this.ImagePanel.ResumeLayout(false);
            this.ImagePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SupportingImageBox)).EndInit();
            this.ResultsPanel.ResumeLayout(false);
            this.ResultsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

   

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel ResultsPanel;
        private System.Windows.Forms.Panel TestListPanel;
        private System.Windows.Forms.Panel ImagePanel;
        private System.Windows.Forms.Panel DescriptionPanel;
        private System.Windows.Forms.FlowLayoutPanel ButtonsFlowLayoutPanel;
        private System.Windows.Forms.PictureBox SupportingImageBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label TestNameLabel;
        private System.Windows.Forms.Label SequenceNameLabel;
        private System.Windows.Forms.Button YesButton;
        private System.Windows.Forms.Button NoButton;
        private System.Windows.Forms.FlowLayoutPanel TestListBox;
        private System.Windows.Forms.FlowLayoutPanel TestStepListBox;
        private System.Windows.Forms.Button FinishedButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label IDLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel ResultsFlowLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Button RetryButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button AnalyzeButton;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label varLabel1;
        private System.Windows.Forms.TextBox varBox1;
        private System.Windows.Forms.Label varLabel2;
        private System.Windows.Forms.TextBox varBox2;
        private System.Windows.Forms.Label varLabel3;
        private System.Windows.Forms.TextBox varBox3;
        private System.Windows.Forms.Label varLabel4;
        private System.Windows.Forms.TextBox varBox4;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.RichTextBox stepDescriptionText;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button DownloadButton;
    }
}
