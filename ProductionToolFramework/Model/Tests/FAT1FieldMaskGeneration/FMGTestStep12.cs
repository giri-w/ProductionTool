using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT1FieldMaskGeneration
{
    public class FMGTestStep12 : TestStep
    {
        [Obsolete]
        public FMGTestStep12()
            : this(null)
        { }

        public FMGTestStep12(TestManager testManager)
            : base(testManager)
        {
            this.Name 				= "Field Mask Generation complete";
            this.Instructions 		= "LUT Test is complete.\nPress Finish to return to main menu or press Back to review the result";
            this.SupportingImage 	= @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions 		= EButtonOptions.Finish|EButtonOptions.Back;
            this.Results 			= new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                 // Continue to the next step
                this.Results.Add(new BooleanResult("Field Mask Generation complete", "FMG Test is finished", true));
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
