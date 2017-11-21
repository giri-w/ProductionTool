using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT4SpatialAccuracy
{
    public class SACTestStep07 : TestStep
    {
        [Obsolete]
        public SACTestStep07()
            : this(null)
        { }

        public SACTestStep07(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Position Stability Analysis";
            this.Instructions =
                            "- Verify position stability within 1mm (Tangential)" +
                            "- Verify position stability within 2mm (Longitudinal)";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.Next|EButtonOptions.Back;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
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
