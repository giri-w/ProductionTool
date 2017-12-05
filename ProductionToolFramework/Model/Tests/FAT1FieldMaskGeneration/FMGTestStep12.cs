using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

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
            this.Name 				= "12. Field Mask Generation complete";
            this.Instructions 		= "FMG Test is complete.\n" +
                                      "Press \"Next\" to continue to the next test\n" +
                                      "Press \"Back\" to return to review the result\n" +
                                      "Press \"Finish\" to go to main menu";
            this.SupportingImage 	= @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions 		= EButtonOptions.Finish|EButtonOptions.Back|EButtonOptions.Next;
            this.Results 			= new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction==EButtonOptions.Finish)
            {
                // Back to HomeScreen
                this.Results.Add(new BooleanResult("Field Mask Generation complete", "FMG Test is finished", true));
                this.OnTestUpdated(true);

            }

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
