using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


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
                            "- Copy the folder name and paste it in the download box\n" +
                            "- Press UPDATE to download the measurement to test folder";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.Next|EButtonOptions.Back|EButtonOptions.Update;
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
        }
    }
}
