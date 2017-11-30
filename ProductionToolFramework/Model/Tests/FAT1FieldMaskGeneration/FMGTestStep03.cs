using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT1FieldMaskGeneration
{
    public class FMGTestStep03 : TestStep
    {
        [Obsolete]
        public FMGTestStep03()
            : this(null)
        { }

        public FMGTestStep03(TestManager testManager)
            : base(testManager)
        {
            this.Name 				= "Data Generation";
            this.Instructions  		= "1. Start the measurement\n" +
									  "2. Break measurement when sequence has started\n" +
									  "3. Record the measurement name in the assignment\n" +
									  "4. Press Next when finished or Press Back to go to previous step\n";
            this.SupportingImage 	= @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions 		= EButtonOptions.Next | EButtonOptions.Back;
            this.Results 			= new List<Result>();
			// forward and backward handler
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                this.Results.Add(new BooleanResult("Data Generation","Measurement process has been done", true));
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
