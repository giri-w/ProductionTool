using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT4SpatialAccuracy
{
    public class SACTestStep03 : TestStep
    {
        [Obsolete]
        public SACTestStep03()
            : this(null)
        { }

        public SACTestStep03(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Full Measurement : 2 times";
            this.Instructions = "1. Start the full measurement\n" +
                                "2. Record the measurement name in the assignment\n" +
                                "3. Repeat process 1-2 again\n" +
                                "4. Press NEXT when finished or Press BACK to go to previous step\n";
            this.SupportingImage = @"Images\UI Demcon\ImNoAvailable.png";
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
