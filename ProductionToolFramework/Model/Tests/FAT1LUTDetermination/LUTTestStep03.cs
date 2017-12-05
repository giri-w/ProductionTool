using System;
using System.Collections.Generic;


namespace Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination
{
    public class LUTTestStep03 : TestStep
    {
        [Obsolete]
        public LUTTestStep03()
            : this(null)
        { }

        public LUTTestStep03(TestManager testManager)
            : base(testManager)
        {
            this.Name 			 = "3. Data Generation: GRID4";
            this.Instructions =    "1. Start the measurement\n" +
                                   "2. Break measurement when sequence has started\n" +
                                   "3. Record the measurement name in the assignment\n";
								   
            this.SupportingImage = @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions   = EButtonOptions.Next|EButtonOptions.Back;
            this.Results		 = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // conitnue to the next step
                this.Results.Add(new BooleanResult("Data Generation", "Measurement process has been done", true));
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
