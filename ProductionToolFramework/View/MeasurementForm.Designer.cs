namespace TestToolFramework.View
{
    partial class MeasurementForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.downloadButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.sourceText = new System.Windows.Forms.TextBox();
            this.folderList = new System.Windows.Forms.ListBox();
            this.sourceList = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.explorerButton = new System.Windows.Forms.Button();
            this.includeDir = new System.Windows.Forms.RadioButton();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source List";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(419, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Available Measurement";
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(265, 207);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(101, 23);
            this.downloadButton.TabIndex = 2;
            this.downloadButton.Text = "Download >>";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(265, 247);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(101, 23);
            this.exitButton.TabIndex = 3;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // sourceText
            // 
            this.sourceText.Location = new System.Drawing.Point(35, 87);
            this.sourceText.Name = "sourceText";
            this.sourceText.Size = new System.Drawing.Size(183, 20);
            this.sourceText.TabIndex = 5;
            // 
            // folderList
            // 
            this.folderList.FormattingEnabled = true;
            this.folderList.Location = new System.Drawing.Point(413, 123);
            this.folderList.Name = "folderList";
            this.folderList.Size = new System.Drawing.Size(184, 147);
            this.folderList.TabIndex = 6;
            this.folderList.SelectedIndexChanged += new System.EventHandler(this.folderList_SelectedIndexChanged);
            this.folderList.DoubleClick += new System.EventHandler(this.folderList_DoubleClick);
            // 
            // sourceList
            // 
            this.sourceList.FormattingEnabled = true;
            this.sourceList.Location = new System.Drawing.Point(35, 126);
            this.sourceList.Name = "sourceList";
            this.sourceList.Size = new System.Drawing.Size(184, 147);
            this.sourceList.TabIndex = 7;
            this.sourceList.DoubleClick += new System.EventHandler(this.sourceList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Source Location";
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(265, 87);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(101, 23);
            this.addButton.TabIndex = 9;
            this.addButton.Text = "<< Add Source";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(265, 127);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(101, 23);
            this.DeleteButton.TabIndex = 10;
            this.DeleteButton.Text = "<< Del Source";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // explorerButton
            // 
            this.explorerButton.Location = new System.Drawing.Point(265, 167);
            this.explorerButton.Name = "explorerButton";
            this.explorerButton.Size = new System.Drawing.Size(101, 23);
            this.explorerButton.TabIndex = 11;
            this.explorerButton.Text = "<< Explorer >>";
            this.explorerButton.UseVisualStyleBackColor = true;
            this.explorerButton.Click += new System.EventHandler(this.explorerButton_Click);
            // 
            // includeDir
            // 
            this.includeDir.AutoSize = true;
            this.includeDir.Location = new System.Drawing.Point(413, 93);
            this.includeDir.Name = "includeDir";
            this.includeDir.Size = new System.Drawing.Size(170, 17);
            this.includeDir.TabIndex = 12;
            this.includeDir.TabStop = true;
            this.includeDir.Text = "Download include subdirectory";
            this.includeDir.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(32, 285);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(131, 13);
            this.linkLabel1.TabIndex = 13;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Browse download location";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // MeasurementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 311);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.includeDir);
            this.Controls.Add(this.explorerButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sourceList);
            this.Controls.Add(this.folderList);
            this.Controls.Add(this.sourceText);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MeasurementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Measurement Browser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.TextBox sourceText;
        private System.Windows.Forms.ListBox folderList;
        private System.Windows.Forms.ListBox sourceList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button explorerButton;
        private System.Windows.Forms.RadioButton includeDir;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}