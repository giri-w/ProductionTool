﻿using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;

namespace Demcon.ProductionTool.Model.Tests.FAT4SpatialAccuracy
{
    public class SACTestStep05 : TestStep
    {
        // Variable
        private string testSetting = @"Setting\config.xml";
        private string SourceLocation = string.Empty;
        private const string InstructionText =
                                        "Locate measurement folder for position stability test\n\n" +
                                        "Measurement directory : {0}\n\n" +
                                        "Press Browse to set new measurement location";

        [Obsolete]
        public SACTestStep05()
            : this(null)
        { }

        public SACTestStep05(TestManager testManager)
            : base(testManager)
        {
            // obtain location from setting
            ChangeXml chg = new ChangeXml();
            SourceLocation = chg.ObtainElement(testSetting, "Test", "FAT4", "SAC", "SourcePS");

            this.Name = "Source Location : Position Stability";
            this.Instructions =
                                "Locate measurement folder for position stability test\n\n" +
                                "Measurement directory : " + SourceLocation + "\n\n" +
                                "Press Browse to set new measurement location";
            this.SupportingImage = @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions = EButtonOptions.Next|EButtonOptions.Back|EButtonOptions.Browse;
            this.Results = new List<Result>();

            // forward and backward handler
            this.OnTestUpdated(false);
            this.OnTestCanceled(false);
            
        }

        public override void Start()
        {
            this.Results.Clear();
            new Task(() =>
            {
                // obtain location from setting
                ChangeXml chg = new ChangeXml();
                SourceLocation = chg.ObtainElement(testSetting, "Test", "FAT4", "SAC", "SourcePS");
                this.Instructions = string.Format(SACTestStep05.InstructionText, SourceLocation);
            }).Start();
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            string remark = string.Empty;
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                bool check = string.IsNullOrEmpty(SourceLocation);
                this.Results.Add(new BooleanResult(this.Name, SourceLocation, !check));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // Back to previous step
                this.OnTestCanceled(true);
            }

            if (userAction == EButtonOptions.Browse)
            {
                // Save folder location
                this.testResult = info;

                // Update last location to setting file
                try
                {
                    ChangeXml chg = new ChangeXml();
                    chg.modifyElement(testSetting, "Test", "FAT4", "SAC", "SourcePS", this.testResult);
                    MessageBox.Show("Setting saved");
                }
                catch
                {
                    MessageBox.Show("Setting file missing");
                }

                // Write to console
                bool check = string.IsNullOrEmpty(testResult);
                this.Results.Add(new BooleanResult(this.Name, this.testResult, !check));
                this.OnTestUpdated(true);
            }

        }
    }
}
