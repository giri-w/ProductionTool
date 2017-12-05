using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT4SpatialAccuracy
{
    public class SACTestStep12a : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep001"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public SACTestStep12a()
            : this(null)
        { }

        public SACTestStep12a(TestManager testManager)
            : base(testManager)
        {
            this.Name = "12a. Position Accuracy Analysis: Tangential";
            this.Instructions =
                        "- Verify accuracy and reproducibility is within 2mm (Tangential)";

            string[] theFiles = System.IO.Directory.GetFiles(@"Python\figure\FAT4SpatialAccuracy", "*_resultsX.png");
            this.SupportingImage = theFiles[0];
            this.ButtonOptions = EButtonOptions.Next|EButtonOptions.Back;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                this.Results.Add(new BooleanResult(this.Name, "Checked", true));
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
