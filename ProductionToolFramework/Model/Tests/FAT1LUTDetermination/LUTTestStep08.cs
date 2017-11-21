using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination
{
    public class LUTTestStep08 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTest08"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public LUTTestStep08()
            : this(null)
        { }

        public LUTTestStep08(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Data Selection Grid4";
            this.Instructions = "Write the SVN Number in the work instruction\n";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.Next;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Check or do something (with the hardware?) for the test
                this.Results.Add(new BooleanResult("Data Selection Grid4", "Checked", true));
            }

            this.OnTestUpdated(true);
        }
    }
}
