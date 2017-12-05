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
            this.Name				 = "13. Look Up Table Determination complete";
            this.Instructions		 = 
										"Look Up Table Determination test is complete.\n"+
										"Press \"Finish\" to go to main menu\n"+
                                        "Press \"Back\" to return to the previous step";  
            this.SupportingImage	 = @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions		 = EButtonOptions.Finish | EButtonOptions.Back;
            this.Results 			 = new List<Result>();
			// forward and backward handler
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Finish)
            {
                // Back to HomeScreen
                //MessageBox.Show("Test Finished", "FAT results", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
