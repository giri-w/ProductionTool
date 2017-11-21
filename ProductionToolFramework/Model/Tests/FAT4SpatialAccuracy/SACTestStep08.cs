using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT4SpatialAccuracy
{
    public class SACTestStep08 : TestStep
    {
        [Obsolete]
        public SACTestStep08()
            : this(null)
        { }

        public SACTestStep08(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Half Measurement : 3 times";
            this.Instructions = "1. Start the measurement\n" +
                                "2. Break measurement when sequence has started\n" +
                                "3. Record the measurement name in the assignment\n" +
                                "4. Repeat Process 1-3 twice\n" +
                                "4. Press NEXT when finished or Press BACK to go to previous step\n"; 
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
