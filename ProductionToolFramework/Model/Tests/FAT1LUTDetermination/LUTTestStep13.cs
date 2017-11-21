using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination
{
    public class LUTTestStep13 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep12"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public LUTTestStep13()
            : this(null)
        { }

        public LUTTestStep13(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Look Up Table Determination complete";
            this.Instructions = "LUT Test is complete.\nPress Finish to return to main menu or press Back to review the result";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.Finish | EButtonOptions.Back;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Check or do something (with the hardware?) for the test
                this.Results.Add(new BooleanResult("Look Up Table Determination", "LUT Test is finished", true));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                this.OnTestCanceled(true);
            }

            
        }
    }
}
