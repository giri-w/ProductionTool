namespace Demcon.ProductionTool.View
{
    partial class TestStepListItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestStepListItem));
            this.ResultPicture = new System.Windows.Forms.PictureBox();
            this.NameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ResultPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // ResultPicture
            // 
            this.ResultPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ResultPicture.Image = ((System.Drawing.Image)(resources.GetObject("ResultPicture.Image")));
            this.ResultPicture.InitialImage = ((System.Drawing.Image)(resources.GetObject("ResultPicture.InitialImage")));
            this.ResultPicture.Location = new System.Drawing.Point(0, 0);
            this.ResultPicture.Name = "ResultPicture";
            this.ResultPicture.Size = new System.Drawing.Size(26, 26);
            this.ResultPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ResultPicture.TabIndex = 0;
            this.ResultPicture.TabStop = false;
            this.ResultPicture.Click += new System.EventHandler(this.ClickForward);
            // 
            // NameLabel
            // 
            this.NameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.Location = new System.Drawing.Point(30, 0);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(169, 27);
            this.NameLabel.TabIndex = 1;
            this.NameLabel.Text = "label1";
            this.NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.NameLabel.Click += new System.EventHandler(this.ClickForward);
            // 
            // TestStepListItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.ResultPicture);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(1000, 28);
            this.MinimumSize = new System.Drawing.Size(100, 28);
            this.Name = "TestStepListItem";
            this.Size = new System.Drawing.Size(198, 28);
            ((System.ComponentModel.ISupportInitialize)(this.ResultPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ResultPicture;
        private System.Windows.Forms.Label NameLabel;
    }
}
