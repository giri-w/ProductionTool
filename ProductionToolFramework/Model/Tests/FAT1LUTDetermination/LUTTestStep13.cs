using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination
{
    public class LUTTestStep13 : TestStep
    {
        [Obsolete]
        public LUTTestStep13()
            : this(null)
        { }

        public LUTTestStep13(TestManager testManager)
            : base(testManager)
        {
            this.Name				 = "Look Up Table Determination complete";
            this.Instructions		 = 
										"Look Up Table Determination  Test is complete.\n"+
										"Press FINISH to go to main menu\n"+
										"Press NEXT to continue to the next test, or\n" +
										"Press BACK to return to the previous step";  
            this.SupportingImage	 = @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions		 = EButtonOptions.Finish | EButtonOptions.Back | EButtonOptions.Next;
            this.Results 			 = new List<Result>();
			// forward and backward handler
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                this.Results.Add(new BooleanResult("Look Up Table Determination", "LUT Test is finished", true));
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
