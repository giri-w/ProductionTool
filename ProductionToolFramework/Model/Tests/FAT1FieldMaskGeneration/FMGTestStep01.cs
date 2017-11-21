using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT1FieldMaskGeneration
{
    public class FMGTestStep01 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep01"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public FMGTestStep01()
            : this(null)
        { }

        public FMGTestStep01(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Machine Preparation";
            this.Instructions = "- Make sure the hand positioning blocks are present\n" +
                                "- Glass plate is clean and free\n";
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
                this.Results.Add(new BooleanResult("SVN Number", "Checked", true));
            }

            this.OnTestUpdated(true);
        }
    }
}
