using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination
{
    public class LUTTestStep04 : TestStep
    {
        [Obsolete]
        public LUTTestStep04()
            : this(null)
        { }

        public LUTTestStep04(TestManager testManager)
            : base(testManager)
        {
            this.Name			 	= "Data Selection: GRID4";
            this.Instructions 		= "Write the SVN Number in the work instruction\n";
            this.SupportingImage	= string.Empty;
            this.ButtonOptions 		= EButtonOptions.Next|EButtonOptions.Back;
            this.Results 			= new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                 // Continue to the next step
                this.Results.Add(new BooleanResult("Data Selection Grid4", "Checked", true));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // back to previous step
                this.OnTestCanceled(true);
            }

        }
    }
}
