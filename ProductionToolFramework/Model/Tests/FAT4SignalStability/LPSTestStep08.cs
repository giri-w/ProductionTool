using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT4SignalStability
{
    public class LPSTestStep08 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep001"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public LPSTestStep08()
            : this(null)
        { }

        public LPSTestStep08(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Laser Power Stability Test Complete";
            this.Instructions = 
                                "Laser Power Stability Test is complete.\n"+
                                "Press FINISH to go to main menu\n"+
                                "Press NEXT to continue to the next test, or\n" +
                                "Press BACK to return to the previous step";                                
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.Finish|EButtonOptions.Back|EButtonOptions.Next;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Check or do something (with the hardware?) for the test
                this.Results.Add(new BooleanResult("Laser Power Stability", "Laser Power Stability Test is finished", true));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // Check or do something (with the hardware?) for the test
                this.OnTestCanceled(true);
            }
        }
    }
}
