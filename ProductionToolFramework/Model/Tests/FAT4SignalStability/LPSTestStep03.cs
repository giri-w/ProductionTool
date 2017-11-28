using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT4SignalStability
{
    public class LPSTestStep03 : TestStep
    {
        [Obsolete]
        public LPSTestStep03()
            : this(null)
        { }

        public LPSTestStep03(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Full Measurement";
            this.Instructions =
                                "1. Start the full measurement\n" +
                                "2. Record the measurement name in the assignment\n" +
                                "3. Press NEXT when finished or Press BACK to go to previous step\n";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.Next|EButtonOptions.Back;
            this.Results = new List<Result>();
			// forward and backward handler
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                this.Results.Add(new BooleanResult(this.Name, "Measurement process has been done", true));
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
