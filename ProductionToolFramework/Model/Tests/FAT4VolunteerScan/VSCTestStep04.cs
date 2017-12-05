using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT4VolunteerScan
{
    public class VSCTestStep04 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep001"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public VSCTestStep04()
            : this(null)
        { }

        public VSCTestStep04(TestManager testManager)
            : base(testManager)
        {
            this.Name = "4. Workinstruction";
            this.Instructions = "- You can also do a visual check of the results\n" +
                                "- The images are located in the folder shown at the results\n" +
                                "- Summarize the result in the system validation document";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.Finish|EButtonOptions.Back;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
           
            if (userAction == EButtonOptions.Finish)
            {
                // Back to HomeScreen
                //MessageBox.Show("Test Finished", "FAT results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Results.Add(new BooleanResult("", "", true));
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
