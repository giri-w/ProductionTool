using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT4SpatialAccuracy
{
    public class SACTestStep13 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep001"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public SACTestStep13()
            : this(null)
        { }

        public SACTestStep13(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Spactial Accuracy Test Complete";
            this.Instructions =
                                "Spatial Accuracy Test is complete.\n" +
                                "Press FINISH to go to main menu\n" +
                                "Press NEXT to continue to the next test, or\n" +
                                "Press BACK to return to the previous step";
            this.SupportingImage = @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions = EButtonOptions.Next|EButtonOptions.Back|EButtonOptions.Finish;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                this.Results.Add(new BooleanResult("Spatial Accuracy", "Spatial Accuracy Test is finished", true));
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
