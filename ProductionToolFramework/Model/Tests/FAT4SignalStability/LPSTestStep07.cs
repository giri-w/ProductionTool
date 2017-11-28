using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT4SignalStability
{
    public class LPSTestStep07 : TestStep
    {
        [Obsolete]
        public LPSTestStep07()
            : this(null)
        { }

        public LPSTestStep07(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Measurement Analysis";
            this.Instructions = "- Inspect left and right hand region\n" +
                                "  (Click image to Zoom)\n" +
                                "- Ensure the variatios are within specification\n" +
                                "  (If Green OK is displayed in all region, test is PASS)\n" +
                                "- Write the result in the assignment";
            
            this.SupportingImage = @"Python\figure\FAT4SignalStablity\SignalStability.png";
            this.ButtonOptions = EButtonOptions.Next | EButtonOptions.Back;
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
