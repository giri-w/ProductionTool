using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TestToolFramework.View;

namespace Demcon.ProductionTool.Model.Tests.FAT4SpatialAccuracy
{
    public class SACTestStep09 : TestStep
    {
        [Obsolete]
        public SACTestStep09()
            : this(null)
        { }

        public SACTestStep09(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Data Selection : Position Accuracy";
            this.Instructions =
                            "- Select the measurement result from the list\n" +
                            "- Press DOWNLOAD to download the measurement to test folder";
            this.SupportingImage = @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions = EButtonOptions.Next|EButtonOptions.Back|EButtonOptions.Download;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            string remark = string.Empty;
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                this.Results.Add(new BooleanResult(this.Name, "Checked", true));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // Back to previous step
                this.OnTestCanceled(true);
            }

            if (userAction == EButtonOptions.Download)
            {
                MeasurementForm browser = new MeasurementForm();
                Application.Run(browser);
            }
        }
    }
}
