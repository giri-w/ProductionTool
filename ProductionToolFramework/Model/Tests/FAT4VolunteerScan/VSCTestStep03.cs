using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT4VolunteerScan
{
    public class VSCTestStep03 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep001"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public VSCTestStep03()
            : this(null)
        { }

        public VSCTestStep03(TestManager testManager)
            : base(testManager)
        {
            this.Name = "SVN Number Information";
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
                this.Results.Add(new BooleanResult("SVN Number", "Checked", true));
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
