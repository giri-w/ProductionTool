﻿using System.Windows.Forms;

namespace Demcon.ProductionTool.View
{
    public partial class InputDialog : Form
    {
        private InputDialog(string title, string text)
        {
            InitializeComponent();
            this.Text = title;
            this.MessageLabel.Text = text;
            this.InputTextBox.Select();
        }

        public static bool GetInput(string title, string text, string info, out string input)
        {
            bool acceptInput = false;
            
            
            InputDialog dialog = new InputDialog(title, text);
            dialog.InputTextBox.Text = info;
            input = string.Empty;
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                input = dialog.InputTextBox.Text.Trim();
                acceptInput = true;
            }

            dialog.Dispose();

            return acceptInput;
        }
    }
}
