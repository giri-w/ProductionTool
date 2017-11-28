using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT4SpatialAccuracy
{
    public class SACTestStep07a : TestStep
    {
        [Obsolete]
        public SACTestStep07a()
            : this(null)
        { }

        public SACTestStep07a(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Position Stability Analysis: Tangential";
            this.Instructions =
                            "- Verify position stability within 1mm (Tangential)";

            string[] theFiles = System.IO.Directory.GetFiles(@"Python\figure\FAT4SpatialAccuracy", "*_resultTangential.png");
            this.SupportingImage = theFiles[0];
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
